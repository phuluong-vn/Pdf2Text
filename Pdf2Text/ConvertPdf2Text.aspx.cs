using Aspose.Pdf.Text;
using cwegineeringedgehtmlstage.App_Code;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using SautinSoft.Document;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Pdf2Text
{
    public partial class ConvertPdf2Text : System.Web.UI.Page
    {
        private string fName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            HttpPostedFile file = HttpContext.Current.Request.Files[0];
            string filename = HttpContext.Current.Server.MapPath("~/SourceFiles/" + flUploadPdf.FileName);
            Session["fName"] = flUploadPdf.FileName;
            file.SaveAs(filename);
        }

        protected void btnCovert_Click(object sender, EventArgs e)
        {
            string SourcePdfPath = HttpContext.Current.Server.MapPath("~/SourceFiles/" + Session["fName"]);
            string outputPath = HttpContext.Current.Server.MapPath("~/DestinationFiles/");
            StringBuilder text = new StringBuilder();
            using (PdfReader reader = new PdfReader(SourcePdfPath))
            {
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.AppendLine(PdfTextExtractor.GetTextFromPage(reader, i));
                }
            }
            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(outputPath, "Pdf2Text.txt")))
            {
                outputFile.WriteLine(text);
            }
        }

        protected void btnAddtext1_Click(object sender, EventArgs e)
        {
            try
            {
                string oldFile = HttpContext.Current.Server.MapPath("oldFile.pdf");
                //string oldFile = "oldFile.pdf";
                string newFile = HttpContext.Current.Server.MapPath("newFile.pdf");
                //string newFile = "newFile.pdf";

                // open the reader
                PdfReader reader = new PdfReader(oldFile);
                Rectangle size = reader.GetPageSizeWithRotation(1);
                Document document = new Document(size);

                // open the writer
                // FileStream fs = new FileStream(newFile, FileMode.Create, FileAccess.Write);
                FileStream fs = new FileStream(newFile, FileMode.Create, FileAccess.Write);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();

                // the pdf content
                PdfContentByte cb = writer.DirectContent;

                // select the font properties
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb.SetColorFill(BaseColor.DARK_GRAY);
                cb.SetFontAndSize(bf, 8);

                // write the text in the pdf content
                cb.BeginText();
                string text = "Some random blablablabla...";
                // put the alignment and coordinates here
                cb.ShowTextAligned(1, text, 520, 640, 0);
                cb.EndText();
                cb.BeginText();
                text = "Other random blabla...";
                // put the alignment and coordinates here
                cb.ShowTextAligned(2, text, 100, 200, 0);
                cb.EndText();

                // create the new page and add it to the pdf
                PdfImportedPage page = writer.GetImportedPage(reader, 1);
                cb.AddTemplate(page, 0, 0);

                // close the streams and voilá the file should be changed :)
                document.Close();
                fs.Close();
                writer.Close();
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnAddtext2_Click(object sender, EventArgs e)
        {
            string oldFile = HttpContext.Current.Server.MapPath("oldFile.pdf");
            //string oldFile = "oldFile.pdf";
            
            string fileResult = HttpContext.Current.Server.MapPath("newFile.pdf"); ;
            DocumentCore dc = DocumentCore.Load(oldFile);
            ContentRange cr = dc.Content.Find("Customer Approval:").FirstOrDefault();
            if (cr != null)
                cr.End.Insert("Y");
            ContentRange name = dc.Content.Find("Name:").FirstOrDefault();
            if (name != null)
                name.End.Insert("Tuan Phu");
            ContentRange date  = dc.Content.Find("Date:").FirstOrDefault();
            if (date != null)
                date.End.Insert(DateTime.Now.ToShortDateString());
            dc.Save(fileResult);

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(oldFile) { UseShellExecute = true });
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(fileResult) { UseShellExecute = true });

            MemoryStream contentStream = new MemoryStream(File.ReadAllBytes(fileResult));

            string extension = System.IO.Path.GetExtension(fileResult);
            string fileName = System.IO.Path.GetFileName(fileResult);
            string fileNamePDF = GeneratedCode.hashwithtime(fileName) + extension;
            if (contentStream.Length > 0)
            {
                Storage storage = new Storage();
                string documentURL = storage.UploadToAzureCDN(contentStream, fileNamePDF);
            }

        }

        protected void btnAddtext3_Click(object sender, EventArgs e)
        {
            string oldFile = HttpContext.Current.Server.MapPath("oldFile.pdf");
            //string oldFile = "oldFile.pdf";

            string fileResult = HttpContext.Current.Server.MapPath("newFile.pdf"); ;
            MemoryStream contentStream = new MemoryStream(File.ReadAllBytes(fileResult));
             
            if (contentStream.Length > 0)
            {
                Storage storage = new Storage();
                string documentURL = storage.UploadToAzureCDN(contentStream, "sample.pdf");
            }
        }
    }
}