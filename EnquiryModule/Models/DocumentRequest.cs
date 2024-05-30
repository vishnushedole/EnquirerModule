using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EnquiryModule.Models
{
    [ExcludeFromCodeCoverage]
    public class DocumentRequest
    {

        [Required]
        public int EnqId { get; set; }

        [Required]
        public int DocTypeId { get; set; }

        [Required]
        public IFormFile? Doc { get; set; }

    }
    
    public class UpdateDocumentRequest
    {

        public int DocId { get; set; }

        [Required]
        public int EnqId { get; set; }

        public int CustId { get; set; }

        [Required]
        public int DocTypeId { get; set; }

        [Required]
        public int Status { get; set; } 

        [Required]
        public IFormFile? Doc { get; set; }

    }
    public class GetDocumentRequest
    {

        public int DocId { get; set; }

        [Required]
        public int EnqId { get; set; }

        public int CustId { get; set; }

        [Required]
        public int DocTypeId { get; set; }

        [Required]
        public int Status { get; set; }

     

    }
    public class DocWithImageData
    {
        public GetDocumentRequest? DocModel { get; set; }
        public string? ImageData { get; set; }
    }
    public class DocumentResponse
    {
        public int docId { get; set; }
        public string? updatedImage { get; set; }
    }
}
