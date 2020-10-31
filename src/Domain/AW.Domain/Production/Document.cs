using AW.Domain.HumanResources;
using System;

namespace AW.Domain.Production
{
    public class Document
    {
        public string DocumentNode { get; set; }
        public int DocumentLevel { get; set; }
        public string Title { get; set; }
        public Employee Owner { get; set; }
        public bool FolderFlag { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string Revision { get; set; }
        public int ChangeNumber { get; set; }
        public int Status { get; set; }
        public string DocumentSummary { get; set; }
        public byte[] DocumentContents { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}