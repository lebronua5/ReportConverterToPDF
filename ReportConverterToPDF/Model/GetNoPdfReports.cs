using System.Collections.Generic;

namespace ReportConverterToPDF.Model
{
    public class GetNoPdfReports
    {
        public DataSet dataSet { get; set; }
        public int RetValue { get; set; }
        public int errorCode { get; set; }
        public object errorString { get; set; }
    }

    public class Table
    {
        public int ddt { get; set; }
    }

    public class DataSet
    {
        public IList<Table> Table { get; set; }
    }    
}
