using iDiTect.Pdf.Editing;
using iDiTect.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace iDiTect.Pdf.Demo
{
    public static class SecurityHelper
    {
        public static void Encrypt()
        {
            PdfFile pdfFile = new PdfFile();
            PdfDocument document;

            using (FileStream fs = File.OpenRead("sample.pdf"))
            {
                //Read pdf document from stream
                document = pdfFile.Import(fs);
            }

            //Specifies if the PDF document should be encrypted
            pdfFile.ExportSettings.IsEncrypted = true;
            pdfFile.ExportSettings.UserPassword = "password";

            using (FileStream fs = File.Create("Encrypted.pdf"))
            {                
                pdfFile.Export(document, fs);
            }
        }

        public static void Decrypt()
        {
            PdfFile pdfFile = new PdfFile();
            PdfDocument document;

            pdfFile.ImportSettings.UserPassword = "password";

            using (FileStream fs = File.OpenRead("Encrypted.pdf"))
            {
                //Read pdf document from stream
                document = pdfFile.Import(fs);                
            }

            int pageCount = document.Pages.Count;
            Console.WriteLine(pageCount);

            //Or you can make all processing and modifying to pdf file
            // Such as adding a text in the beginning of the 1st page
            PageContentBuilder builder = new PageContentBuilder(document.Pages[0]);
            builder.Position.Translate(50, 150);
            builder.DrawText("The PDF document is decrypted!");

            using (FileStream fs = File.Create("Decrypted.pdf"))
            {
                pdfFile.Export(document, fs);
            }
        }
    }
}
