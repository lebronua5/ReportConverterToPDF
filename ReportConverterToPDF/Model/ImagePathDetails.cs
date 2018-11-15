using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportConverterToPDF.Model
{
    public class ImagePathDetails
    {
        public Response response { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
        public int errorCode { get; set; }
        public object errorString { get; set; }
        public bool keepAfterProcessing { get; set; }
    }

    public class FileList
    {
        public int contentlength { get; set; }
        public string contenttype { get; set; }
        public string file_name { get; set; }
        public string folder_path { get; set; }
        public string lastmodified { get; set; }
        public string meta_data { get; set; }
        public string type { get; set; }
        public string update_date { get; set; }
    }

    public class Response2
    {
        public List<FileList> file_list { get; set; }
    }

    public class Response
    {
        public Response2 response { get; set; }
    }

    public class ResponseStatus
    {
        public object error { get; set; }
        public bool status { get; set; }
    }
}
