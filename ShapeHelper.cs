using iDiTect.Pdf.ColorSpaces;
using iDiTect.Pdf.Editing;
using iDiTect.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace iDiTect.Pdf.Demo
{
    public static class ShapeHelper
    {
        public static void AddShape()
        {
            //This example is using page level builder
            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.AddPage();

            PageContentBuilder builder = new PageContentBuilder(page);

            //Add line shape
            builder.GraphicState.StrokeColor = new RgbColor(255, 0, 0);
            builder.DrawLine(new Point(10, 10), new Point(200, 20));

            //Add rectangle shape by Block object
            Block block = new Block();
            block.GraphicState.StrokeColor = new RgbColor(255, 0, 0);
            block.GraphicState.FillColor = new RgbColor(0, 255, 0);
            block.GraphicState.IsFilled = true;
            block.InsertRectangle(new Rect(0, 0, 50, 50));
            builder.Position.Translate(10, 20);
            builder.DrawBlock(block);

            using (FileStream fs = File.Create("AddShape.pdf"))
            {
                PdfFile pdfFile = new PdfFile();
                pdfFile.Export(document, fs);
            }
        }

        public static void AddShape2()
        {
            //This example is using document level builder
            PdfDocument document = new PdfDocument();
            using (PdfDocumentBuilder builder = new PdfDocumentBuilder(document))
            {
                //Add line shape 
                Block lineBlock = new Block();
                lineBlock.GraphicState.StrokeColor = new RgbColor(255, 0, 0);
                lineBlock.InsertLine(new Point(10, 10), new Point(200, 20));
                builder.InsertBlock(lineBlock);

                //Add rectangle shape
                Block rectBlock = new Block();
                rectBlock.GraphicState.StrokeColor = new RgbColor(255, 0, 0);
                rectBlock.GraphicState.FillColor = new RgbColor(0, 255, 0);
                rectBlock.GraphicState.IsFilled = true;
                rectBlock.InsertRectangle(new Rect(0, 0, 50, 50));
                builder.InsertBlock(rectBlock);
            }

            using (FileStream fs = File.Create("AddShape2.pdf"))
            {
                PdfFile pdfFile = new PdfFile();
                pdfFile.Export(document, fs);
            }
        }
    }
}
