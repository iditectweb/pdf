using iDiTect.Pdf.Actions;
using iDiTect.Pdf.Annotations;
using iDiTect.Pdf.ColorSpaces;
using iDiTect.Pdf.Editing;
using iDiTect.Pdf.IO;
using iDiTect.Pdf.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace iDiTect.Pdf.Demo
{
    public static class LinkHelper
    {
        public static void AddLinkInsideDocument()
        {
            PdfFile pdfFile = new PdfFile();
            PdfDocument document = pdfFile.Import(File.ReadAllBytes("sample.pdf"));
            
            PdfPage page = document.Pages[0];
            PageContentBuilder builder = new PageContentBuilder(page);
            builder.GraphicState.FillColor = new RgbColor(100, 255, 0, 0);

            //Add internal link with Destination
            PageFit pageFit = document.Destinations.AddPageFit();
            //Navigate to the second page
            pageFit.Page = document.Pages[1];
            Rect rectArea = new Rect(50, 100, 100, 100);
            Link linkWithDestination = new Link(pageFit);
            linkWithDestination.Rect = rectArea;
            page.Annotations.Add(linkWithDestination);
            builder.DrawRectangle(rectArea);

            //Add internal link with Location
            Location location = new Location();
            //Navigate to second page
            location.Page = document.Pages[1];
            //Navigate to the target point in page
            location.Left = 275;
            location.Top = 400;
            //Set Zoom value in viewer, the value "3" means showing in 300% of original size
            location.Zoom = 3;            
            rectArea = new Rect(50, 250, 100, 100);
            Link linkWithLocation = page.Annotations.AddLink(location);
            linkWithLocation.Rect = rectArea;
            builder.DrawRectangle(rectArea);

            //Add internal link with Action
            GoToAction goToAction = new GoToAction();
            //Set navigation to either Destination or Location
            goToAction.Destination = location;
            //goToAction.Destination = pageFit;
            rectArea = new Rect(50, 400, 100, 100);
            Link goToLink = page.Annotations.AddLink(goToAction);
            goToLink.Rect = rectArea;
            builder.DrawRectangle(rectArea);

            //Add internal link with Block
            Block block = new Block();
            block.InsertText("This is a ");
            block.GraphicState.FillColor = new RgbColor(0, 0, 255);
            //Set navigation to either Destination or Location
            block.InsertHyperlinkStart(pageFit);
            //block.InsertHyperlinkStart(location);
            block.InsertText("navigation link");
            block.InsertHyperlinkEnd();
            builder.Position.Translate(50, 550);
            builder.DrawBlock(block);

            File.WriteAllBytes("AddLink.pdf", pdfFile.Export(document));            
        }

        public static void AddLinkToWebLink()
        {
            PdfFile pdfFile = new PdfFile();
            PdfDocument document = pdfFile.Import(File.ReadAllBytes("sample.pdf"));
            
            PdfPage page = document.Pages[0];
            PageContentBuilder builder = new PageContentBuilder(page);
            builder.GraphicState.FillColor = new RgbColor(100, 255, 0, 0);                       
                      
            //Add external hyperlink with Action
            UriAction uriAction = new UriAction();
            uriAction.Uri = new Uri("http://www.iditect.com");
            Rect rectArea = new Rect(50, 150, 100, 100);
            var uriLink = page.Annotations.AddLink(uriAction);
            uriLink.Rect = rectArea;
            builder.DrawRectangle(rectArea);  
          
            //Add external hyperlink with Block
            Block block = new Block();
            block.InsertText("This is a ");
            block.GraphicState.FillColor = new RgbColor(0, 0, 255);
            block.InsertHyperlinkStart(new Uri("http://www.iditect.com"));
            block.InsertText("hyperlink");
            block.InsertHyperlinkEnd();
            builder.Position.Translate(50, 300);
            builder.DrawBlock(block);
           
            File.WriteAllBytes("AddLink2.pdf", pdfFile.Export(document));      
        }

    }
}
