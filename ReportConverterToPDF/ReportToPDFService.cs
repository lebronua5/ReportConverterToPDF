using Newtonsoft.Json;
using ReportConverterToPDF.Model.ReportConverterToPDF.Model;
using System;
using System.IO;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Timers;

namespace ReportConverterToPDF
{
    public partial class ReportToPDFService : ServiceBase
    {
        private FozzyCoreQuery _fozzyCoreQuery;
        private PDFBuilder _pdfBuilder;
        public ReportToPDFService()
        {
            InitializeComponent();
            _fozzyCoreQuery = new FozzyCoreQuery();
            _pdfBuilder = new PDFBuilder();
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            var settings = JsonConvert.DeserializeObject<SettingsInfo>(File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}/Content/Settings.json"));
           var timer = new Timer(settings.Data.FirstStart * 1000 * 60);

            timer.Elapsed += new ElapsedEventHandler((obj, eventArg) =>
            {
                _fozzyCoreQuery.GetToken();
                var reportIds = _fozzyCoreQuery.GetNewReportIds();
               // reportIds.Add(28);

                foreach (var reportId in reportIds)
                {
                    ProcessReportToPdf(reportId);
                }

                timer.Interval = settings.Data.RegularStart * 1000 * 60;
            });

            timer.Enabled = true;
            timer.Start();
        }      

        protected override void OnStop()
        {
        }      

        private void ProcessReportToPdf(int reportId)
        {
            try
            {
                var report = _fozzyCoreQuery.GetReportDetails(reportId);
                GetImageContentList(report);

                var pdfReportBase64 = _pdfBuilder.CreateReportInPDF(report);

                var dateTime = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
                var folderName = $"FZMobileVehicleMonitoring//{report.IdTrailer}//{dateTime}";
                var fileName = $"{dateTime}.PDF";

                //Create folder
                var createFolderResponce = _fozzyCoreQuery.OwnCloudFolderCreate(folderName, report.IdVCMH);

                if (createFolderResponce.errorCode == 0)
                {
                    // Upload pdf to folder
                    var createFileResponce = _fozzyCoreQuery.OwnCloudFileUpload(pdfReportBase64, folderName, fileName, report.IdVCMH);

                    if (!ReferenceEquals(createFileResponce, null) && createFileResponce.errorCode == 0)
                    {
                        _fozzyCoreQuery.SetReportPdf(new Model.ReportPdfInfo { id = report.IdVCMH, error = string.Empty, url_pdf = $"{folderName}//{fileName}" });
                        //var photoContent = _fozzyCoreQuery.OwnCloudFileDownload(folderName, fileName, report.IdVCMH);
                        //OpenPdfFileFromBase64(photoContent.file);
                    }
                }
            }
            catch (Exception ex)
            {
                _fozzyCoreQuery.SetReportPdf(new Model.ReportPdfInfo { id = reportId.ToString(), error = ex.ToString(), url_pdf = string.Empty });
            }

           
        }

        private void OpenPdfFileFromBase64(string pdf)
        {
            byte[] bytes = Convert.FromBase64String(pdf);
            var stream = new FileStream(@"C:\2.PDF", FileMode.OpenOrCreate);
            var writer = new BinaryWriter(stream);
            writer.Write(bytes, 0, bytes.Length);
            writer.Close();

            System.Diagnostics.Process.Start(@"C:\2.PDF");
        }

        private void GetImageContentList(Model.ReportDetails report)
        {
            if (!ReferenceEquals(report.Photos, null))
            {
                foreach (var photo in report.Photos.Range)
                {
                    var url = photo.URL_photo;

                    if (!ReferenceEquals(url, null))
                    {
                        var match = Regex.Match(url, @"[A-Za-z0-9\-_]*//[A-Za-z0-9\-_]*//[A-Za-z0-9\-_]*");
                        var pathToImage = match.Value;
                        var imageName = $"{url.Replace($"{pathToImage}//", "")}.JPEG";

                        if (pathToImage != string.Empty && imageName != string.Empty)
                        {
                            var photoContent = _fozzyCoreQuery.OwnCloudFileDownload(pathToImage, imageName);

                            if (!ReferenceEquals(photoContent, null))
                                photo.ImageContent = Convert.FromBase64String(photoContent.file);
                        }
                    }
                }                
            }
        }
    }
}
