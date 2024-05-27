using EnquiryModule.Models;

namespace EnquiryModule.Infrastructure
{
    public interface IEnquiryModule
    {
        public Enquirer CreateEnquiry(Enquirer enquirer);

        public Enquirer UpdateEnquirer(Enquirer enquirer);

        public Enquirer GetEnquirer(int EnquiryId);

        public Document CreateDocument(int enqId, int custId, int docType, byte[] docData);

        public DocWithImageData GetDocument(int DocId);

        public Document UpdateDocument(Document document);

        public int AssignManager(int EnquiryId);

    }

}
