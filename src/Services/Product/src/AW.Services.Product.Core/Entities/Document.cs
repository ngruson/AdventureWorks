using System;

namespace AW.Services.Product.Core.Entities
{
    public class Document
    {
        public string DocumentNode { get; private set; }
        public int DocumentLevel { get; private set; }
        public string Title { get; private set; }
        public string Owner { get; private set; }
        public bool FolderFlag { get; private set; }
        public string FileName { get; private set; }
        public string FileExtension { get; private set; }
        public string Revision { get; private set; }
        public int ChangeNumber { get; private set; }
        public int Status { get; private set; }
        public string DocumentSummary { get; private set; }
        public byte[] DocumentContents { get; private set; }
        public DateTime ModifiedDate { get; private set; }
    }
}