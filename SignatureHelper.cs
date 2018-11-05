using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace iDiTect.Pdf.Demo
{
    public static class SignatureHelper
    {
        public static void AddTextSignature2PDF()
        {
            //Input your certificate and password
            PdfCertificate cert = new PdfCertificate("test.pfx", "iditect");
            PdfSigner signer = new PdfSigner("sample.pdf", cert);

            //Set signature information
            signer.SignatureInfo.Contact = "123456789";
            signer.SignatureInfo.Reason = "Sign by iDiTect";
            signer.SignatureInfo.Location = "World Wide Web";
            //Field name need to be unique in the same pdf document
            signer.SignatureInfo.FieldName = "iDiTect Sign Field";
            //Sign in target page
            signer.SignatureInfo.PageId = 0;
            //Sign in target area
            signer.SignatureInfo.Rect = new Rectangle(50, 100, 150, 50);
            signer.SignatureAlgorithm = SignatureAlgorithm.SHA256;
            signer.SignatureType = SignatureType.Text;

            signer.Sign("signed.pdf");

        }

        public static void AddImageSignature2PDF()
        {
            //Input your certificate and password
            PdfCertificate cert = new PdfCertificate("test.pfx", "iditect");
            PdfSigner signer = new PdfSigner("sample.pdf", cert);

            //Support commonly used image format, like jpg, png, gif
            signer.SignatureImageFile = "sample.jpg";
            //Field name need to be unique in the same pdf document
            signer.SignatureInfo.FieldName = "iDiTect Sign";
            //Sign in target page
            signer.SignatureInfo.PageId = 0;
            //Sign in target area
            signer.SignatureInfo.Rect = new Rectangle(50, 100, 100, 50);
            signer.SignatureAlgorithm = SignatureAlgorithm.SHA256;
            signer.SignatureType = SignatureType.Image;

            signer.Sign("signed.pdf");

        }

        public static void CheckPDF()
        {

            List<SignatureInfo> infos = PdfSigner.DetectSignature("signed.pdf");
            foreach (SignatureInfo info in infos)
            {
                Console.WriteLine(info.Contact);
                Console.WriteLine(info.DigitalSigner);
                Console.WriteLine(info.FieldName);
                Console.WriteLine(info.Location);
                Console.WriteLine(info.PageId);
                Console.WriteLine(info.Reason);
                Console.WriteLine(info.Rect.X + "-" + info.Rect.Y + "-" + info.Rect.Width + "-" + info.Rect.Height);
                Console.WriteLine(info.SignDate);
            }
            Console.ReadLine();
        }
    }
}
