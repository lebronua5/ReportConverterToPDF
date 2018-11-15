using System.Collections.Generic;
using TextSharpImage = iTextSharp.text.Image;


namespace ReportConverterToPDF.Model
{
    public class ImageOrientation
    {
        public ImageOrientation()
        {
            VerticalImages = new List<TextSharpImageContent>();
            HorizontalImages = new List<TextSharpImageContent>();
        }

        public List<TextSharpImageContent> VerticalImages { get; set; }
        public List<TextSharpImageContent> HorizontalImages { get; set; }
    }

    public class TextSharpImageContent
    {
        public TextSharpImage Image { get; set; }
        public Range ImageContent { get; set; }
    }
}
