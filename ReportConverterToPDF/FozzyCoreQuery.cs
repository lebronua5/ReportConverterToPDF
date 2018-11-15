using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using ReportConverterToPDF.Model;
using ReportConverterToPDF.Model.Folder;
using ReportConverterToPDF.Model.ImageContent;

namespace ReportConverterToPDF
{
    public class FozzyCoreQuery
    {
        string _user = "j-MVMPdfService";
        string _password = "FP7e3*-Cxb2";
        Guid _appGuid = Guid.Parse("4aac7b69-b678-49cc-9bad-53cf7d34ef52");
        dynamic _session;

        public void GetToken()
        {
            string json = JsonConvert.SerializeObject(new
            {
                appGuid = _appGuid.ToString(),
                appVersion = "0.1",
                login = _user,
                password = _password
            });

            var result = QueryBuilder.MakeRequest<dynamic>("POST", "Logon", json);
            _session = result.session;
        }

        public ReportDetails GetReportDetails(int reportId)
        {
            string json = JsonConvert.SerializeObject(new
            {
                sid = _session.sid,
                operationName = FozzyCoreOperationNames.GetReportData,
                request = $"<data id=\"{reportId}\"/>"
            });

            var result = QueryBuilder.MakeRequest<dynamic>("POST", "Execute", json, reportId.ToString());           
            return Deserialize<ReportDetails>(result.dataSet.Table[0].Column1.ToString());           
        }

        public void SetReportPdf(ReportPdfInfo reportPdfInfo)
        {
            string json = JsonConvert.SerializeObject(new
            {
                sid = _session.sid,
                operationName = FozzyCoreOperationNames.SetReportPdf,
                request = $"<data id=\"{reportPdfInfo.id}\" url_pdf=\"{reportPdfInfo.url_pdf}\" error=\"{reportPdfInfo.error}\"/>"
            });

            QueryBuilder.MakeRequest<dynamic>("POST", "Execute", json, reportPdfInfo.id);          
        }

        public List<int> GetNewReportIds()
        {
            string json = JsonConvert.SerializeObject(new
            {
                sid = _session.sid,
                operationName = FozzyCoreOperationNames.GetNoPdfReports,
                request = ""
            });

            var dataSet = QueryBuilder.MakeRequest<GetNoPdfReports>("POST", "Execute", json);
            return dataSet?.dataSet?.Table?.Select(n =>  n.ddt ).ToList();
        }

        public CreateEntityResponce OwnCloudFileUpload(string  pdfBase64file, string folderPath, string fileName, string idVCMH = null)
        {
            string json = JsonConvert.SerializeObject(new
            {
                sid = _session.sid,
                appGuid = _appGuid.ToString(),
                appVersion = "0.1",
                request = string.Empty,
                operationName = FozzyCoreOperationNames.Owncloud,
                folderPath = folderPath,
                file_name = fileName,
                file = pdfBase64file                
            });

           return QueryBuilder.MakeRequest<CreateEntityResponce>("POST", "OwnCloudFileUpload", json, idVCMH);
        }

        public CreateEntityResponce OwnCloudFolderCreate(string folderPath, string idVCMH = null)
        {
            string json = JsonConvert.SerializeObject(new
            {
                sid = _session.sid,
                appGuid = _appGuid.ToString(),
                appVersion = "0.1",               
                operationName = FozzyCoreOperationNames.Owncloud,
                folderPath = folderPath,             
            });

            return QueryBuilder.MakeRequest<CreateEntityResponce>("POST", "OwnCloudFolderCreate", json, idVCMH);
        }

        public List<FileList> OwnCloudFileList(Range image)
        {           
            string json = JsonConvert.SerializeObject(new
            {
                appGuid = _appGuid.ToString(),
                appVersion = "0.1",
                sid = _session.sid,        
                operationName = FozzyCoreOperationNames.Owncloud,
                folderPath = image.URL_photo
            });

            var result = QueryBuilder.MakeRequest<ImagePathDetails>("POST", "OwnCloudFileList", json);

            return result.response.response.file_list;
        }

        public Model.ImageContent.Response2 OwnCloudFileDownload(string folderName, string fileName, string idVCMH = null)
        {
            string json = JsonConvert.SerializeObject(new
            {
                appGuid = _appGuid.ToString(),
                appVersion = "0.1",
                sid = _session.sid,
                operationName = FozzyCoreOperationNames.Owncloud,
                folderPath = folderName,
                file_name = fileName
            });

            var result = QueryBuilder.MakeRequest<ImageFileContent>("POST", "OwnCloudFileDownload", json, idVCMH);

            return result.response.response;
        }


        private T Deserialize<T>(string input) where T : class
        {
            var ser = new XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }
    }
}
