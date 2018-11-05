using iDiTect.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace iDiTect.Pdf.Demo
{
    public static class PageHelper
    {
        public static void InsertPage()
        {
            PdfFile pdfFile = new PdfFile();
            PdfDocument document = pdfFile.Import(File.ReadAllBytes("sample.pdf"));

            //Create a new Pdf page
            PdfPage page = new PdfPage();
            //Insert new page after the first page of document
            document.Pages.Insert(1, page);

            File.WriteAllBytes("InsertPage.pdf", pdfFile.Export(document));
        }

        public static void RemovePage()
        {
            PdfFile pdfFile = new PdfFile();
            PdfDocument document = pdfFile.Import(File.ReadAllBytes("sample.pdf"));

            //Delete the second page of document
            document.Pages.RemoveAt(1);

            File.WriteAllBytes("RemovePage.pdf", pdfFile.Export(document));
        }

        public static void SplitPage()
        {
            PdfFile pdfFile = new PdfFile();
            PdfDocument document = pdfFile.Import(File.ReadAllBytes("sample.pdf"));

            //Split each page to a new Pdf document
            List<PdfDocument> splitDocuments = document.Split();

            for (int i = 0; i < splitDocuments.Count; i++)
            {
                PdfDocument split = splitDocuments[i];
                File.WriteAllBytes(String.Format("Split-page{0}.pdf", i + 1), pdfFile.Export(split));
            }  
        }

        public static void MergePage()
        {
            PdfFile pdfFile = new PdfFile();
            PdfDocument document = pdfFile.Import(File.ReadAllBytes("sample.pdf"));

            PdfDocument document2 = pdfFile.Import(File.ReadAllBytes("sample2.pdf"));

            document.Merge(document2);

            File.WriteAllBytes("MergePage.pdf", pdfFile.Export(document));
        }
    }
}
