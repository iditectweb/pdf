using iDiTect.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace iDiTect.Pdf.Demo
{
    public static class CreateLoadHelper
    {
        public static void CreateNewPdfDocument()
        {
            //Create new pdf document
            PdfDocument document = new PdfDocument();

            document.DocumentInfo.Author = "test author";
            document.DocumentInfo.Description = "test description";
            document.DocumentInfo.Title = "test title";

            //Insert a new page
            PdfPage page = document.Pages.AddPage();

            //The default page size is A4
            //Customize the page sie directly
            page.Size = new Size(800, 1000);
            //Or change the value by standard paper size
            //page.Size = PaperTypeConverter.ToSize(PaperTypes.A4);


            PdfFile pdfFile = new PdfFile();

            //Save pdf to file using stream
            using (FileStream fs = File.Create("CreateNew.pdf"))
            {
                pdfFile.Export(document, fs);
            }
            //Save pdf to file using byte array
            //File.WriteAllBytes("CreateNew.pdf", pdfFile.Export(document));
        }

        public static void LoadExistedPdfDocument()
        {
            PdfFile pdfFile = new PdfFile();
            PdfDocument document;

            //Read pdf document from stream
            using (FileStream fs = File.OpenRead("sample.pdf"))
            {
                document = pdfFile.Import(fs);
            }
            //Read pdf document from byte array
            //document = pdfFile.Import(File.ReadAllBytes("sample.pdf"));

            //Get page count from existing pdf file
            int pageCount = document.Pages.Count;
            Console.WriteLine(pageCount);
        }
    }
}
