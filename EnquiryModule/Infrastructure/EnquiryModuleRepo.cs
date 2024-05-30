using EnquiryModule.Models;
using Microsoft.EntityFrameworkCore;


namespace EnquiryModule.Infrastructure
{
    public class EnquiryModuleRepo : IEnquiryModule
    {
        private readonly EnquiryModuleContext _db;
        public EnquiryModuleRepo(EnquiryModuleContext db)
        {
            _db = db;
        }
        

        public Document CreateDocument(int enqId, int docType, byte[] docData)
        {
            Document document = new Document();
            document.EnqId = enqId; 
            document.CustId = null;
            document.DocTypeId = docType;
            document.Status = 0;
            document.Doc = docData;

            _db.Documents.Add(document);
            _db.SaveChanges();

            return document;
        }

        public Enquirer CreateEnquiry(Enquirer enquirer)
        {


           _db.Enquirers.Add(enquirer);
            _db.SaveChanges();
            return enquirer;

        }

        public Enquirer GetEnquirer(int EnquiryId)
        {
           return _db.Enquirers.FirstOrDefault(e=>e.EnquiryId == EnquiryId && e.Status !=1);
        }
      


        public Enquirer UpdateEnquirer(Enquirer enquirer)
        {
            _db.Enquirers
            .Where(c => c.EnquiryId == enquirer.EnquiryId)
            .ExecuteUpdate(setters =>
                setters.SetProperty(p => p.Email, enquirer.Email)
                       .SetProperty(p => p.FirstName, enquirer.FirstName)
                       .SetProperty(p => p.LastName, enquirer.LastName)
                       .SetProperty(p => p.Addr, enquirer.Addr)
                       .SetProperty(p => p.PhoneNo, enquirer.PhoneNo)
                       .SetProperty(p => p.City, enquirer.City)
                       .SetProperty(p => p.Stat, enquirer.Stat)
                       .SetProperty(p => p.Country, enquirer.Country)
                       .SetProperty(p => p.PinCode, enquirer.PinCode)
                       .SetProperty(p => p.MaritalStatus, enquirer.MaritalStatus)
                       .SetProperty(p => p.Gender, enquirer.Gender)
                       .SetProperty(p => p.Age, enquirer.Age)
                       .SetProperty(p => p.Status, enquirer.Status)
                       .SetProperty(p => p.Dob, enquirer.Dob)
                       .SetProperty(p => p.EmployeeId, enquirer.EmployeeId)
            );
            _db.SaveChanges();
            return enquirer;
        }

        public DocWithImageData GetDocument(int DocId)
        {
             var document = _db.Documents.FirstOrDefault(e=>e.DocId == DocId);
            if(document == null)
            {
                return null;
            }
            var docModel = new GetDocumentRequest
            {
                DocId = document.DocId,
                EnqId = document.EnqId,
                DocTypeId = document.DocTypeId,
                Status = document.Status
               
            };

            var imageData = (byte[])document.Doc;
            var base64Image = Convert.ToBase64String(imageData);
            return new DocWithImageData { DocModel = docModel, ImageData = "data:image/jpeg;base64," + base64Image };
        }

        public Document UpdateDocument(Document document)
        {
            _db.Documents
         .Where(d => d.DocId == document.DocId)
            .ExecuteUpdate(setters =>
             setters.SetProperty(p => p.DocTypeId, document.DocTypeId)
                    .SetProperty(p => p.Status, document.Status)
                    .SetProperty(p => p.Doc, document.Doc)
         );

            _db.SaveChanges();
            
            return document;
        }
        public int? AssignManager(int EnquiryId)
        {
            var enquiry = GetEnquirer(EnquiryId);
            if (enquiry == null)
                return -1;

            var MgrAssignEnq = _db.MgrAssignedEnquires.FirstOrDefault(e => e.EnquiryId == EnquiryId);

            if (MgrAssignEnq is not null)
                return MgrAssignEnq.EmpId;

            var leastLoadedManagerId = _db.MgrAssignedEnquires
             .Where(m => m.Isprocessed != true)
             .GroupBy(m => m.EmpId)
             .Select(group => new
             {
                 EmpId = group.Key,
                 EnquiryCount = group.Count()
             })
             .OrderBy(x => x.EnquiryCount)
             .Select(x => x.EmpId)
             .FirstOrDefault();

            if (leastLoadedManagerId.HasValue)
            {
                // Insert the new enquiry assignment for the least loaded manager
                var newAssignment = new MgrAssignedEnquire
                {
                    EmpId = leastLoadedManagerId.Value,
                    EnquiryId = EnquiryId,
                    Isprocessed = false
                };

                _db.MgrAssignedEnquires.Add(newAssignment);
                _db.SaveChanges();
                return leastLoadedManagerId.Value;
            }
            return -1;
        }
        public int GetAllEnquires()
        {
            return _db.Enquirers.ToList().Count();
        }
        public bool checkManager(int? EnqId,int? EmpId)
        {
            var data = _db.MgrAssignedEnquires.FirstOrDefault(e => e.EnquiryId == EnqId && e.EmpId==EmpId);

            if (data == null)
                return false;

            return true;
        }
        public bool checkDocType(int? DocTypeId)
        {
            var data = _db.DocTypes.FirstOrDefault(e => e.DocType1==DocTypeId);

            if (data == null)
                return false;

            return true;
        }
        public bool CheckValidDocUpdate(int? DocId, int? EnqId)
        {
            var data = _db.Documents.FirstOrDefault(e => e.DocId == DocId && e.EnqId == EnqId);

            if (data == null)
                return false;

            return true;
        }
    }
}
