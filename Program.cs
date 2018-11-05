using iDiTect.Pdf.Actions;
using iDiTect.Pdf.Editing;
using iDiTect.Pdf.IO;
using iDiTect.Pdf.Licensing;
using iDiTect.Pdf.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace iDiTect.Pdf.Demo
{
    class Program
    {
        static void Main(string[] args)
        {           
            //This license registration line need to be at very beginning of our other code
            LicenseManager.SetKey("CLBUL-WPNT5-W5VYK-TK88W-KJZ3X-X6C8Z");


            //CreateLoadHelper.CreateNewPdfDocument();
            TextHelper.AddText();
            //ImageHelper.AddImageAsPage();
            //SecurityHelper.Encrypt();
            //FormFieldsHelper.CreateFormField();
            //ShapeHelper.AddShape();
            //LinkHelper.AddLinkInsideDocument();
            //TableHelper.AddTable2();
            //PageHelper.SplitPage();
            //SignatureHelper.AddTextSignature2PDF();
        }

       
    }
}
