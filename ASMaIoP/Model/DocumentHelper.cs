using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;

namespace ASMaIoP.Model
{
    internal class DocumentHelper
    {
        WordprocessingDocument wordDoc;
        string docPath;
        string docText;
        string docTemplate = "";
        MemoryStream templateStream = new MemoryStream();
        public DocumentHelper(string docPath)
        {
            this.docPath = docPath;
            wordDoc = WordprocessingDocument.Open(docPath, true);
            string docText = null;
            using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
            {
                docText = sr.ReadToEnd();
            }
        }
        public DocumentHelper(byte[] doc)
        {
            templateStream.Write(doc, 0, (int)doc.Length);

            wordDoc = WordprocessingDocument.Open(templateStream, true);
            using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
            {
                docText = sr.ReadToEnd();
            }
        }

        public void Replace(string oldString, string newString)
        {
            Regex regexText = new Regex($"\\({oldString}\\)");
            docText = regexText.Replace(docText, newString);
        }

        public void Save(string path)
        {
            using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
            {
                sw.Write(docText);
            }
            wordDoc.MainDocumentPart.Document.Save();
            byte[] result = null;
            templateStream.Position = 0;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                templateStream.CopyTo(memoryStream);
                result = memoryStream.ToArray();
            }

            File.WriteAllBytes(path, result);
        }
    }

    internal class DocumentHelperRemove : DocumentHelper
    {
       public DocumentHelperRemove() : base("remove.docx")
       {
            
       }

    }
}
