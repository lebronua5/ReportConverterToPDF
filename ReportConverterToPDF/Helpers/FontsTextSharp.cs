using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportConverterToPDF.Helpers
{
    public static class FontsTextSharp
    {
        
        public static string TimesNewRomanPath = AppDomain.CurrentDomain.BaseDirectory + "/Content/times.ttf";

        public static Font NormalTimesFont12 = FontFactory.GetFont(TimesNewRomanPath, BaseFont.IDENTITY_H, 12, Font.NORMAL);
        public static Font BoldTimesFont12 = FontFactory.GetFont(TimesNewRomanPath, BaseFont.IDENTITY_H, 12, Font.BOLD);
        public static Font NormalTimesFont11 = FontFactory.GetFont(TimesNewRomanPath, BaseFont.IDENTITY_H, 11, Font.NORMAL);
        public static Font BoldTimesFont11 = FontFactory.GetFont(TimesNewRomanPath, BaseFont.IDENTITY_H, 11, Font.BOLD);
        public static Font BoldTimesFont16 = FontFactory.GetFont(TimesNewRomanPath, BaseFont.IDENTITY_H, 16, Font.BOLD);
        public static Font BoldTimesFont18 = FontFactory.GetFont(TimesNewRomanPath, BaseFont.IDENTITY_H, 18, Font.BOLD);
        public static Font BoldTimesFont14 = FontFactory.GetFont(TimesNewRomanPath, BaseFont.IDENTITY_H, 14, Font.BOLD);
        public static Font NormalTimesFont14 = FontFactory.GetFont(TimesNewRomanPath, BaseFont.IDENTITY_H, 14, Font.NORMAL);
        public static Font NormalTimesFont8 = FontFactory.GetFont(TimesNewRomanPath, BaseFont.IDENTITY_H, 8, Font.NORMAL);
        public static Font ItalicTimesFont12 = FontFactory.GetFont(TimesNewRomanPath, BaseFont.IDENTITY_H, 12, Font.BOLDITALIC);
        public static Font BoldTimesFont9 = FontFactory.GetFont(TimesNewRomanPath, BaseFont.IDENTITY_H, 9, Font.BOLD);
        public static Font NormalTimesFont9 = FontFactory.GetFont(TimesNewRomanPath, BaseFont.IDENTITY_H, 9, Font.NORMAL);
        public static Font BoldTimesFont10 = FontFactory.GetFont(TimesNewRomanPath, BaseFont.IDENTITY_H, 10, Font.BOLD);
    }
}
