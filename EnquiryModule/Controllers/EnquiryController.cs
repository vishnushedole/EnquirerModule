using EnquiryModule.Infrastructure;
using EnquiryModule.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnquiryModule.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EnquiryController : ControllerBase
    {
        private readonly EnquiryModuleRepo _repo;

        public EnquiryController(EnquiryModuleRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("CreateEnquiry")]
        public IActionResult CreateEnquiry([FromBody] Enquirer? enquirer)
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if(enquirer?.EmployeeId!=null)
                {
                    return BadRequest("Employee Id should be null");
                }
                return Ok(_repo.CreateEnquiry(enquirer));
            }
            catch (Exception ex) 
            {
             return BadRequest(ex.Message);
            }

         
        }
        [HttpGet("GetEnquiry")]
        public IActionResult GetEnquiryById(int EnquiryId)
        {
            var enquirer = _repo.GetEnquirer(EnquiryId);
            if (enquirer == null)
            {
                return NotFound("Enquiry Not found");
            }
            return Ok(enquirer);
        }

        [HttpPut("UpdateEnquiry")]
        public IActionResult UpdateEnquiry([FromBody] Enquirer enquirer)
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (_repo.GetEnquirer(enquirer.EnquiryId) is not null)
                {
                    if (_repo.checkManager(enquirer.EnquiryId,enquirer.EmployeeId))                  
                        return Ok(_repo.UpdateEnquirer(enquirer));
                    else
                        return NotFound("Invalid Manager");
                }
                else
                    return NotFound("Enquiry with Id  "+enquirer.EnquiryId+" does not exist");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("UploadDocument")]
        public async Task<IActionResult> UploadDocument(DocumentRequest docModel)
        {
            if (docModel == null || docModel.Doc == null || docModel.Doc.Length == 0)
                return BadRequest("Invalid document");

            if (_repo.GetEnquirer(docModel.EnqId) is null)
                return NotFound("Enquirer Not found");

            if (!_repo.checkDocType(docModel.DocTypeId))
                return NotFound("DocType is not valid");

            using (var memoryStream = new MemoryStream())
            {
                await docModel.Doc.CopyToAsync(memoryStream);
                var docData = memoryStream.ToArray();


                var docId = _repo.CreateDocument(docModel.EnqId, docModel.DocTypeId, docData);

                return Ok(new {Id=docId.DocId,Document=docId.Doc});
            }
        }

        [HttpGet("GetDocument")]
        public IActionResult GetDocumentById(int DocId)
        {
            var document = _repo.GetDocument(DocId);
            if (document is not null)
                return Ok(document);

            return NotFound("Document not found");
        }

        [HttpPut("UpdateDocument")]
        public async Task<IActionResult> UpdateDocument(UpdateDocumentRequest docModel)
        {
            if (docModel == null || docModel.Doc == null || docModel.Doc.Length == 0)
                return BadRequest("Invalid document");

            if (!_repo.checkDocType(docModel.DocTypeId))
                return NotFound("DocType Id not found");

            if (docModel.Status > 2 || docModel.Status < 0)
                return BadRequest("Invalid status type");

            if (!_repo.CheckValidDocUpdate(docModel.DocId, docModel.EnqId))
                return BadRequest("Invalid Update request");

            using (var memoryStream = new MemoryStream())
            {
                await docModel.Doc.CopyToAsync(memoryStream);
                var docData = memoryStream.ToArray();

                Document document = new Document();
                document.DocId = docModel.DocId;
                document.EnqId = docModel.EnqId;
                document.CustId = docModel.CustId;
                document.Status = docModel.Status;
                document.Doc = docData;
                document.DocTypeId = docModel.DocTypeId;
                var doc = _repo.UpdateDocument(document);

                var base64Image = Convert.ToBase64String(document.Doc);
                DocumentResponse docRes = new DocumentResponse();
                docRes.docId = doc.DocId;
                docRes.updatedImage = "data:image/jpeg;base64," + base64Image;
                return Ok(docRes);
            }
        }
        [HttpPost("AssignManager")]
        public IActionResult AssignManager([FromBody]int EnqId) 
        { 
            var response = _repo.AssignManager(EnqId);
            if (response == -1)
                return NotFound("Enquirer Not found");

            return Ok(response);
        }

        [HttpGet("GetAllEnquires")]
        public int GetAllEnquires()
        {
            return _repo.GetAllEnquires();
        }
       
    }
}
