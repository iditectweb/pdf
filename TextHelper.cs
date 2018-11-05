
using iDiTect.Pdf.Basic.Model;
using iDiTect.Pdf.Basic.Primitives;
using iDiTect.Pdf.ColorSpaces;
using iDiTect.Pdf.Editing;
using iDiTect.Pdf.Editing.Flow;
using iDiTect.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace iDiTect.Pdf.Demo
{
    public static class TextHelper
    {
        public static void AddText()
        {
            //This example is using page level builder, all the text will be add in current pdf page
            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.AddPage();           

            //Create page level builder
            PageContentBuilder builder = new PageContentBuilder(page);

            //Add text using builder's DrawText directly

            builder.TextState.FontSize = 18;
            builder.GraphicState.FillColor = RgbColors.Black;
            //Set the position for text
            builder.Position.Translate(50, 50);
            builder.DrawText("Insert text directly in builder");

            //Add text using Block object

            //Set the position for first block
            builder.Position.Translate(50, 100);
            //Create the first block with different font style
            Block block = new Block();
            block.HorizontalAlignment = Editing.Flow.HorizontalAlignment.Left;
            block.TextState.FontSize = 16;
            block.TextState.SetFont(new FontFamily("Times New Roman"));
            block.InsertText("This is a Normal style sentence with target font.");
            block.TextState.SetFont(new FontFamily("Times New Roman"), FontStyles.Italic, FontWeights.Bold);
            block.InsertText(" This is another sentence in Italic and Bold style with target font.");
            //Add first sentence to page, if you don't know how much height need to show the text, just using 
            //PositiveInfinity property, our builder will measure the height depend on the given width automatically
            builder.DrawBlock(block, new Size(page.Size.Width - 50 * 2, double.PositiveInfinity));

            //Get the actual size used by the first block, it works after block drawn 
            Size usedSize = block.DesiredSize; 

            //Set the position for the second block
            builder.Position.Translate(50, 100 + usedSize.Height);
            //Crete the second block with different text color
            Block block2 = new Block();
            block2.GraphicState.FillColor = RgbColors.Black;
            block2.InsertText("Black Text,");
            block2.GraphicState.FillColor = new RgbColor(255, 0, 0);
            block2.InsertText(" Colorful Text.");

            //Set wanted line width to measure the size used by the second block
            Size needSize2 = block2.Measure(page.Size.Width - 50 * 2);
            //Add second sentence to page
            builder.DrawBlock(block2, needSize2);

            using (FileStream fs = File.OpenWrite("InsertText.pdf"))
            {
                PdfFile pdfFile = new PdfFile();
                pdfFile.Export(document, fs);
            }
        }

        public static void AddText2()
        {
            //This example is using document level builder(flow-like), if the text inserted is out of one page,
            //the builder will insert the left text on a second page automatically
            PdfDocument document = new PdfDocument();            

            //Create document level builder
            using(PdfDocumentBuilder builder = new PdfDocumentBuilder(document))
            {
                //Set page size and margins
                builder.SectionState.PageSize = PaperTypeConverter.ToSize(PaperTypes.A4);
                builder.SectionState.PageMargins = new Padding(20);

                //Set text alignment
                builder.ParagraphState.HorizontalAlignment = Editing.Flow.HorizontalAlignment.Center;
                //Set font style 
                builder.CharacterState.SetFont(new FontFamily("Times New Roman"));
                builder.CharacterState.FontSize = 40;
                builder.InsertParagraph();
                builder.InsertText("Document Title");
                builder.InsertLineBreak();

                //Add several paragraphs to page
                builder.ParagraphState.HorizontalAlignment = Editing.Flow.HorizontalAlignment.Left;
                builder.CharacterState.FontSize = 20;
                for (int i = 0; i < 20; i++)
                {
                    builder.InsertParagraph();
                    string text = "";
                    for (int j = 1; j < 11; j++)
                    {
                        text += "This is sentence " + j.ToString() + ". ";
                    }
                    builder.InsertText(text);
                    builder.InsertLineBreak();
                }
            }
            
            using (FileStream fs = File.Create("InsertText2.pdf"))
            {
                PdfFile pdfFile = new PdfFile();
                pdfFile.Export(document, fs);
            }
        }
        
        public static void AddTextWatermark()
        {
            PdfFile pdfFile = new PdfFile();
            PdfDocument document = pdfFile.Import(File.ReadAllBytes("sample.pdf"));
                       
            //Get first page of pdf
            PdfPage page = document.Pages[0];
            PageContentBuilder builder = new PageContentBuilder(page);
            
            //Create a block with watermark text
            Block block = new Block();
            //Set text color and font size
            block.GraphicState.FillColor = new RgbColor(100, 255, 0, 0);
            block.TextState.FontSize = 50;
            //Set a text format.
            block.HorizontalAlignment = Editing.Flow.HorizontalAlignment.Center;
            block.VerticalAlignment = Editing.Flow.VerticalAlignment.Center;
            block.InsertText("watermark");
            Size needSize = block.Measure();

            //Set watermark text position and rotation
            builder.Position.Translate((page.Size.Width - needSize.Width) / 2, (page.Size.Height - needSize.Height) / 2);
            builder.Position.Rotate(-45);
            builder.DrawBlock(block);

            File.WriteAllBytes("TextWatermark.pdf", pdfFile.Export(document));            
        }
      

    }
}
