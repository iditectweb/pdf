using iDiTect.Pdf.Editing;
using iDiTect.Pdf.InteractiveForms;
using iDiTect.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace iDiTect.Pdf.Demo
{
    public static class FormFieldsHelper
    {
        public static void CreateFormField()
        {
            PdfDocument document = new PdfDocument();            
            PdfPage page = document.Pages.AddPage();
            PageContentBuilder builder = new PageContentBuilder(page);

            double padding = 10;
            double topOffset = 100;

            //Create CheckBox FormField
            CheckBoxField check = new CheckBoxField("checkBox1");            
            check.IsChecked = true;
            //Add CheckBox to document fields
            document.AcroForm.FormFields.Add(check);
            //Draw CheckBox to page
            builder.Position.Translate(50, topOffset);
            builder.DrawWidget(check, new Size(10, 10));
            //Draw CheckBox description  
            builder.Position.Translate(50 + 10 + padding, topOffset);
            builder.DrawText("CheckBox");

            //Create ComboBox FormField
            ComboBoxField combo = new ComboBoxField("comboBox1");
            combo.TextState.FontSize = 16;
            combo.Options.Add(new ChoiceOption("Combo 1"));
            combo.Options.Add(new ChoiceOption("Combo 2"));
            combo.Options.Add(new ChoiceOption("Combo 3"));
            combo.Value = combo.Options[2];
            //Add ComboBox to document fields
            document.AcroForm.FormFields.Add(combo);
            //Draw ComboBox to page
            topOffset += 30;
            builder.Position.Translate(50, topOffset);
            builder.DrawWidget(combo, new Size(100, 30));
            //Draw ComboBox description  
            builder.Position.Translate(50 + 100 + padding, topOffset);
            builder.DrawText("ComboBox");

            //Create ListBox FormField
            ListBoxField list = new ListBoxField("listBox1");
            list.AllowMultiSelection = true;
            list.TextState.FontSize = 16;
            list.Options.Add(new ChoiceOption("List 1"));
            list.Options.Add(new ChoiceOption("List 2"));
            list.Options.Add(new ChoiceOption("List 3"));
            list.Value = new ChoiceOption[] { list.Options[0], list.Options[2] };
            //Add ListBox to document fields
            document.AcroForm.FormFields.Add(list);
            //Draw ListBox to page
            topOffset += 30;
            builder.Position.Translate(50, topOffset);
            builder.DrawWidget(list, new Size(100, 100));
            //Draw ListBox description  
            builder.Position.Translate(50 + 100 + padding, topOffset);
            builder.DrawText("ListBox");

            //Create RadioButton FormField
            RadioButtonField radio = new RadioButtonField("radioButton1");
            radio.Options.Add(new RadioOption("Radio 1"));
            radio.Options.Add(new RadioOption("Radio 2"));
            radio.Value = radio.Options[1];
            //Add RadioButton to document fields
            document.AcroForm.FormFields.Add(radio);
            //Draw RadioButton to page
            topOffset += 100;
            foreach (RadioOption option in radio.Options)
            {
                topOffset += 30;
                builder.Position.Translate(50, topOffset);
                builder.DrawWidget(radio, option, new Size(10, 10));
            }
            //Draw ListBox description  
            builder.Position.Translate(50 + 10 + padding, topOffset);
            builder.DrawText("RadioButton");

            //Create TextBox FormField
            TextBoxField textBox = new TextBoxField("textBox1");
            textBox.TextState.FontSize = 16;
            textBox.Value = "Input...";
            //Add TextBox to document fields
            document.AcroForm.FormFields.Add(textBox);
            //Draw TextBox to page
            topOffset += 30;
            builder.Position.Translate(50, topOffset);
            builder.DrawWidget(textBox, new Size(100, 30));
            //Draw TextBox description  
            builder.Position.Translate(50 + 100 + padding, topOffset);
            builder.DrawText("TextBox");

            using (FileStream fs = File.OpenWrite("CreateFormField.pdf"))
            {
                PdfFile pdfFile = new PdfFile();
                pdfFile.Export(document, fs);
            }
        }


        public static void ReadAndModifyFormField()
        {
            PdfFile pdfFile = new PdfFile();
            PdfDocument document = pdfFile.Import(File.ReadAllBytes("CreateFormField.pdf"));
                       
            foreach (FormField field in document.AcroForm.FormFields)
            {
                switch (field.FieldType)
                {                    
                    case FormFieldType.CheckBox:
                        if (field.Name == "checkBox1")
                            ((CheckBoxField)field).IsChecked = false;
                        break;
                    case FormFieldType.ComboBox:
                        if (field.Name == "comboBox1")
                            ((ComboBoxField)field).Value = ((ComboBoxField)field).Options[0];
                        break;
                    case FormFieldType.ListBox:
                        if (field.Name == "listBox1")
                            ((ListBoxField)field).Value = new ChoiceOption[] { ((ListBoxField)field).Options[0] };
                        break;
                    case FormFieldType.RadioButton:
                        if (field.Name == "radioButton1")
                            ((RadioButtonField)field).Value = ((RadioButtonField)field).Options[0];
                        break;                    
                    case FormFieldType.TextBox:
                        if (field.Name == "textBox1")
                            ((TextBoxField)field).Value = "new text";
                        break;
                }
            }

            File.WriteAllBytes("ModifyFormField.pdf", pdfFile.Export(document));
           
        }
    }
}
