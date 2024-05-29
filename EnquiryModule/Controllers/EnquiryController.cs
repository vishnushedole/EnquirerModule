using EnquiryModule.Infrastructure;
using EnquiryModule.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnquiryModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiryController : ControllerBase
    {
        private readonly EnquiryModuleRepo _repo;

        public EnquiryController(EnquiryModuleRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("CreateEnquiry")]
        public IActionResult CreateEnquiry([FromBody] Enquirer enquirer)
        {
            try
            {

                if (enquirer is null)
                {
                    return BadRequest("Enquiry data cannot be empty");
                }
                else if (_repo.GetEnquirer(enquirer.EnquiryId) is null)
                {
                    return Ok(_repo.CreateEnquiry(enquirer));
                }
                else
                {
                    return BadRequest("Enquirer alread exist");
                }
            }catch (Exception ex) 
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
                if(_repo.GetEnquirer(enquirer.EnquiryId) is not null)
                {
                    if (_repo.GetManager(enquirer.EmployeeId) is not null)
                        return Ok(_repo.UpdateEnquirer(enquirer));
                    else
                        return NotFound("Manager with Id  " + enquirer.EmployeeId + " doesnot exist");
                }
                else
                    return NotFound("Enquiry with Id  "+enquirer.EnquiryId+" doesnot exist");

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

            using (var memoryStream = new MemoryStream())
            {
                await docModel.Doc.CopyToAsync(memoryStream);
                var docData = memoryStream.ToArray();


                var docId = _repo.CreateDocument(docModel.EnqId, docModel.CustId, docModel.DocTypeId, docData);

                return Ok(new { DocId = docId });
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
        public async Task<IActionResult> UpdateDocument(DocumentRequest docModel)
        {
            if (docModel == null || docModel.Doc == null || docModel.Doc.Length == 0)
                return BadRequest("Invalid document");

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
        public IActionResult AssignManager(int EnqId) 
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
