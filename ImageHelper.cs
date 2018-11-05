using iDiTect.Pdf.Editing;
using iDiTect.Pdf.Editing.Flow;
using iDiTect.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace iDiTect.Pdf.Demo
{
    public static class ImageHelper
    {        
        public static void AddImage()
        {
            PdfFile pdfFile = new PdfFile();
            PdfDocument document;

            using (FileStream fs = File.OpenRead("sample.pdf"))
            {
                //Read pdf document from stream
                document = pdfFile.Import(fs);
            }
            //Get first page of pdf
            PdfPage page = document.Pages[0];
            //Create page level builder
            PageContentBuilder builder = new PageContentBuilder(page);

            //Add image using builder's DrawImage directly
            using (Stream imgStream = File.OpenRead("sample.jpg"))
            {
                //Set image position
                builder.Position.Translate(100, 100);

                //insert image as original size
                builder.DrawImage(imgStream);
                //insert image with customized size
                //builder.DrawImage(imgStream, new Size(80, 80));
            }

            //Add image using Block object
            using (Stream imgStream = File.OpenRead("sample2.jpg"))
            {
                //Set image position
                builder.Position.Translate(100, 400);

                Block block = new Block();
                //insert image as original size
                //block.InsertImage(imgStream);
                //insert image with customized size
                block.InsertImage(imgStream, new Size(100, 100));

                builder.DrawBlock(block);
            }

            using (FileStream fs = File.OpenWrite("InsertImage.pdf"))
            {
                pdfFile.Export(document, fs);
            }
        }

        public static void AddImageInLine()
        {           
            PdfDocument document = new PdfDocument();

            //Create document level builder
            using (PdfDocumentBuilder builder = new PdfDocumentBuilder(document))
            {
                builder.InsertParagraph();
                //You can insert same image to different position in page, or insert different images to respective position
                using (Stream sampleImage = File.OpenRead("sample.jpg"))
                {
                    builder.InsertImageInline(sampleImage, new Size(40, 40));
                    builder.InsertText(", second one:");
                    builder.InsertImageInline(sampleImage, new Size(100, 100));
                    builder.InsertText(" and third one:");
                    builder.InsertImageInline(sampleImage, new Size(100, 60));
                    builder.InsertText(" the end.");
                }
            }
                
            using (FileStream fs = File.OpenWrite("InsertImageInLine.pdf"))
            {
                PdfFile pdfFile = new PdfFile();
                pdfFile.Export(document, fs);
            }
        }

        public static void AddImageAsPage()
        {            
            PdfDocument document = new PdfDocument();
                       
            using (Stream imgStream = File.OpenRead("sample.jpg"))
            {
                iDiTect.Pdf.Resources.ImageSource image = new iDiTect.Pdf.Resources.ImageSource(imgStream);

                //Create a new page with image's size
                PdfPage page = new PdfPage();
                page.Size = new Size(image.Width, image.Height);
                PageContentBuilder builder = new PageContentBuilder(page);

                //draw image to page at position (0,0)
                builder.DrawImage(image);

                document.Pages.Add(page);
            }           

            using (FileStream fs = File.OpenWrite("ConvertImageToPdf.pdf"))
            {
                PdfFile pdfFile = new PdfFile();
                pdfFile.Export(document, fs);
            }
        }

        public static void AddImageWatermark()
        {
            PdfFile pdfFile = new PdfFile();
            PdfDocument document;

            using (FileStream fs = File.OpenRead("sample.pdf"))
            {
                //Read pdf document from stream
                document = pdfFile.Import(fs);
            }
            //Get first page of pdf
            PdfPage page = document.Pages[0];
            PageContentBuilder builder = new PageContentBuilder(page);

            //Set watermark image position
            builder.Position.Translate(100, 100);
            using (Stream stream = File.OpenRead("watermark.png"))
            {
                //Insert watermark image as original size
                builder.DrawImage(stream);
                //Insert watermark image in customized size
                //builder.DrawImage(stream, new Size(80, 80));
            }

            using (FileStream fs = File.OpenWrite("ImageWatermark.pdf"))
            {
                pdfFile.Export(document, fs);
            }
        }
    }
}
