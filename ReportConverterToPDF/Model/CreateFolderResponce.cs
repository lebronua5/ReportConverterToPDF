namespace ReportConverterToPDF.Model.Folder
{
    public class ResponseStatus
    {
        public object error { get; set; }
        public bool status { get; set; }
    }

    public class CreateEntityResponce
    {
        public ResponseStatus ResponseStatus { get; set; }
        public int errorCode { get; set; }
        public object errorString { get; set; }
        public bool keepAfterProcessing { get; set; }
    }
}
