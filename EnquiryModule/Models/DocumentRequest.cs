using System.Diagnostics.CodeAnalysis;

namespace EnquiryModule.Models
{
    [ExcludeFromCodeCoverage]
    public class DocumentRequest
    {

        public int DocId { get; set; }
        public int EnqId { get; set; }
        public int CustId { get; set; }
        public int DocTypeId { get; set; }
        public int Status { get; set; }
        public IFormFile? Doc { get; set; }

    }
    public class DocWithImageData
    {
        public DocumentRequest? DocModel { get; set; }
        public string? ImageData { get; set; }
    }
    public class DocumentResponse
    {
        public int docId { get; set; }
        public string? updatedImage { get; set; }
    }
}
