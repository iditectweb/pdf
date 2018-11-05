using iDiTect.Pdf.ColorSpaces;
using iDiTect.Pdf.Editing;
using iDiTect.Pdf.Editing.Flow;
using iDiTect.Pdf.Editing.Tables;
using iDiTect.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace iDiTect.Pdf.Demo
{
    public static class TableHelper
    {
        public static void AddTable()
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.AddPage();
            PageContentBuilder builder = new PageContentBuilder(page);

            builder.Position.Translate(50, 50);
            Table table = CreateSimpleTable();
            table.LayoutType = TableLayoutType.FixedWidth;
            builder.DrawTable(table, 500);

            PdfFile pdfFile = new PdfFile();
            File.WriteAllBytes("AddTable.pdf", pdfFile.Export(document));
        }

        public static void AddTable2()
        {
            PdfDocument document = new PdfDocument();
            using (PdfDocumentBuilder builder = new PdfDocumentBuilder(document))
            {
                Table table = CreateSimpleTable();
                table.LayoutType = Editing.Flow.TableLayoutType.FixedWidth;
                builder.InsertTable(table);

                table = CreateTableFrame();
                table.LayoutType = Editing.Flow.TableLayoutType.FixedWidth;
                builder.InsertTable(table);
            }

            PdfFile pdfFile = new PdfFile();
            File.WriteAllBytes("AddTable2.pdf", pdfFile.Export(document));
        }

        public static Table CreateSimpleTable()
        {
            Table table = new Table();

            RgbColor bordersColor = new RgbColor(73, 90, 128);
            RgbColor headerColor = new RgbColor(34, 143, 189);
            RgbColor defaultRowColor = new RgbColor(176, 224, 230);

            Border border = new Border(1, BorderStyle.Single, bordersColor);
            table.Borders = new TableBorders(border);
            table.DefaultCellState.Borders = new TableCellBorders(border, border, border, border);
            table.DefaultCellState.Padding = new System.Windows.Thickness(2);

            //Add table title
            TableRow titleRow = table.Rows.AddTableRow();
            TableCell titleCell = titleRow.Cells.AddTableCell();
            titleCell.ColumnSpan = 3;
            Block titleBlock = titleCell.Blocks.AddBlock();
            titleBlock.HorizontalAlignment = HorizontalAlignment.Center;
            titleBlock.InsertText("Simple Table Title");

            //Add table header
            TableRow headerRow = table.Rows.AddTableRow();
            //Add first column
            TableCell column1 = headerRow.Cells.AddTableCell();
            column1.Background = headerColor;
            column1.Borders = new TableCellBorders(border, border, border, border, null, border);
            //Add second column
            TableCell column2 = headerRow.Cells.AddTableCell();
            column2.Background = headerColor;
            Block column2Block = column2.Blocks.AddBlock();
            column2Block.GraphicState.FillColor = RgbColors.White;
            column2Block.InsertText("Product");
            //Add third column
            TableCell column3 = headerRow.Cells.AddTableCell();
            column3.Background = headerColor;
            Block column3Block = column3.Blocks.AddBlock();
            column3Block.GraphicState.FillColor = RgbColors.White;
            column3Block.InsertText("Price");
        
            //Add table rows
            Random r = new Random();
            for (int i = 0; i < 50; i++)
            {
                RgbColor rowColor = i % 2 == 0 ? defaultRowColor : RgbColors.White;

                TableRow row = table.Rows.AddTableRow();

                TableCell idCell = row.Cells.AddTableCell();
                idCell.Background = rowColor;
                idCell.Blocks.AddBlock().InsertText(i.ToString());

                TableCell productCell = row.Cells.AddTableCell();
                productCell.Background = rowColor;
                productCell.Blocks.AddBlock().InsertText(String.Format("Product{0}", i));

                TableCell priceCell = row.Cells.AddTableCell();
                priceCell.Background = rowColor;
                priceCell.Blocks.AddBlock().InsertText(r.Next(10, 1000).ToString()); 
            }

            return table;
        }

        public static Table CreateTableFrame()
        {
            Table table = new Table();

            //Set table border
            table.Margin = new System.Windows.Thickness(3);
            table.Borders = new TableBorders(new Border(3, RgbColors.Black));
            table.BorderSpacing = 5;
            //Set cell border
            Border border = new Border(1, RgbColors.Black);
            table.DefaultCellState.Borders = new TableCellBorders(border, border, border, border);
            table.DefaultCellState.Padding = new System.Windows.Thickness(5);
            
            TableRow row = table.Rows.AddTableRow();

            //Add a merged cell in 2x2
            TableCell cell = row.Cells.AddTableCell();
            cell.RowSpan = 2;
            cell.ColumnSpan = 2;
            cell.Blocks.AddBlock().InsertText("Text 1");            

            //Add a single cell
            cell = row.Cells.AddTableCell();
            cell.Blocks.AddBlock().InsertText("Text 2");

            row = table.Rows.AddTableRow();
            //Add a single cell
            cell = row.Cells.AddTableCell();
            cell.Blocks.AddBlock().InsertText("Text 3");

            row = table.Rows.AddTableRow();
            //Add a single cell
            cell = row.Cells.AddTableCell();
            cell.Blocks.AddBlock().InsertText("Text 4");            

            //Add a merged cell in 1x2
            cell = row.Cells.AddTableCell();
            cell.ColumnSpan = 2;
            cell.Blocks.AddBlock().InsertText("Text 5");
            
            return table;
        }
    }
}
