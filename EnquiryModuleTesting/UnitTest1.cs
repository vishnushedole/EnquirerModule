using EnquiryModule.Controllers;
using EnquiryModule.Infrastructure;
using EnquiryModule.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace EnquiryModuleTesting
{
    public class DocumentResponse
    {
        public int DocId { get; set; }
        public string UpdatedImage { get; set; }
    }
    [TestClass]
    public class UnitTest1
    {
        private EnquiryController _controller;
        private EnquiryModuleRepo _repo;

        [TestInitialize]
        public void Setup()
        {
            // Initialize DbContext with in-memory database or real database for integration tests
            //var config = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json")
            //    .Build();
            EnquiryModuleContext db = new EnquiryModuleContext();
            _repo = new EnquiryModuleRepo(db);
            _controller = new EnquiryController(_repo);

            
        }

        [TestMethod]
        public void CreateEnquiry_ReturnsOkResult()
        {
            // Arrange
            var enquirer = new Enquirer
            {
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                Addr = "123 Main St",
                PhoneNo = "1234567890",
                City = "Test City",
                Stat = "Test State",
                Country = "Test Country",
                PinCode = "123456",
                MaritalStatus = "Single",
                Gender = "Male",
                Age = 30,
                Status = 1,
                Dob = DateTime.Now.AddYears(-30),
                EmployeeId = 2
            };

            var prevCount = _controller.GetAllEnquires();
            // Act
            var result = _controller.CreateEnquiry(enquirer);

            
            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(enquirer, okResult.Value);
            Assert.AreEqual(prevCount + 1, _controller.GetAllEnquires());
        }
        [TestMethod]
        public void CreateEnquiry_ReturnsBadRequestOnException()
        {
            // Arrange
            var enquirer = new Enquirer
            {
               
                Email = null, // Assuming Email is required, this should trigger an exception
                FirstName = "John",
                LastName = "Doe",
                Addr = "123 Main St",
                PhoneNo = "1234567890",
                City = "Test City",
                Stat = "Test State",
                Country = "Test Country",
                PinCode = "123456",
                MaritalStatus = "Single",
                Gender = "Male",
                Age = 30,
                Status = 1,
                Dob = DateTime.Now.AddYears(-30),
                EmployeeId = 1
            };

            // Act & Assert
            var prevCount = _controller.GetAllEnquires();
            // Act
           

            var result = _controller.CreateEnquiry(enquirer);
           

            Assert.AreEqual(prevCount , _controller.GetAllEnquires());

        }
        [TestMethod]
        public void GetEnquiryById_ReturnsOkResult_WithValidId()
        {
            // Arrange
            var enquiryId = 3;
           
     
            // Act
            var result = _controller.GetEnquiryById(enquiryId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            var returnedEnquirer = okResult.Value as Enquirer;
            Assert.IsNotNull(returnedEnquirer);
            Assert.AreEqual("string", returnedEnquirer.Email);
        }
        [TestMethod]
        public void GetEnquiryById_ReturnsNotFound_WithInvalidId()
        {
            // Arrange
            var enquiryId = 1;
           

            // Act
            var result = _controller.GetEnquiryById(enquiryId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public void UpdateEnquiry_ReturnsUpdatedEnquirer()
        {
            // Arrange
            var updatedEnquirer = new Enquirer
            {
                EnquiryId = 3,
                Email = "new@example.com",
                FirstName = "Jane",
                LastName = "Doe",
                Addr = "456 Main St",
                PhoneNo = "0987654321",
                City = "New City",
                Stat = "New State",
                Country = "New Country",
                PinCode = "654321",
                MaritalStatus = "Married",
                Gender = "Female",
                Age = 28,
                Status = 2,
                Dob = DateTime.Now.AddYears(-28),
                EmployeeId = 2
            };

            // Act
            var result = _controller.UpdateEnquiry(updatedEnquirer);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var returnedEnquirer = okResult.Value as Enquirer;
            Assert.IsNotNull(returnedEnquirer);
            Assert.AreEqual(updatedEnquirer.Email, returnedEnquirer.Email);
            Assert.AreEqual(updatedEnquirer.FirstName, returnedEnquirer.FirstName);
            Assert.AreEqual(updatedEnquirer.LastName, returnedEnquirer.LastName);
            Assert.AreEqual(updatedEnquirer.Addr, returnedEnquirer.Addr);
            Assert.AreEqual(updatedEnquirer.PhoneNo, returnedEnquirer.PhoneNo);
            Assert.AreEqual(updatedEnquirer.City, returnedEnquirer.City);
            Assert.AreEqual(updatedEnquirer.Stat, returnedEnquirer.Stat);
            Assert.AreEqual(updatedEnquirer.Country, returnedEnquirer.Country);
            Assert.AreEqual(updatedEnquirer.PinCode, returnedEnquirer.PinCode);
            Assert.AreEqual(updatedEnquirer.MaritalStatus, returnedEnquirer.MaritalStatus);
            Assert.AreEqual(updatedEnquirer.Gender, returnedEnquirer.Gender);
            Assert.AreEqual(updatedEnquirer.Age, returnedEnquirer.Age);
            Assert.AreEqual(updatedEnquirer.Status, returnedEnquirer.Status);
            Assert.AreEqual(updatedEnquirer.Dob, returnedEnquirer.Dob);
            Assert.AreEqual(updatedEnquirer.EmployeeId, returnedEnquirer.EmployeeId);
        }
        [TestMethod]
        public void UpdateEnquiry_ReturnsUpdatedEnquirerBadRequest()
        {
            // Arrange
            var updatedEnquirer = new Enquirer
            {
                EnquiryId = 3,
                Email = null,
                FirstName = "Jane",
                LastName = "Doe",
                Addr = "456 Main St",
                PhoneNo = "0987654321",
                City = "New City",
                Stat = "New State",
                Country = "New Country",
                PinCode = "654321",
                MaritalStatus = "Married",
                Gender = "Female",
                Age = 28,
                Status = 2,
                Dob = DateTime.Now.AddYears(-28),
                EmployeeId = 2
            };

          
            var result = _controller.UpdateEnquiry(updatedEnquirer);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
       

        }
        [TestMethod]
        public async Task UploadDocument_ReturnsBadRequest_ForInvalidDocument()
        {
            // Arrange
            var docRequest = new DocumentRequest
            {
                Doc = null // Invalid document
            };

            // Act
            var result = await _controller.UploadDocument(docRequest);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("Invalid document", badRequestResult.Value);
        }

        [TestMethod]
        public async Task UploadDocument_ReturnsOk_ForValidDocument()
        {
            // Arrange
            var docContent = new byte[] { 1, 2, 3, 4, 5 }; // Sample document content
            var docStream = new MemoryStream(docContent);
            var docRequest = new DocumentRequest
            {
                DocId = 0,
                EnqId = 3,
                CustId = 1,
                DocTypeId = 1,
                Status = 0,
                Doc = new FormFile(docStream, 0, docStream.Length, "Doc", "test.pdf")
            };

           

            // Act
            var result = await _controller.UploadDocument(docRequest);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var resultValue = okResult.Value as dynamic;
            Assert.IsNotNull(resultValue);
            //Assert.IsTrue(resultValue.DocId > 0); // Assuming DocId is generated and greater than 0
        }
        [TestMethod]
        public void GetDocumentById_ReturnsOk_ForValidDocumentId()
        {
            // Act
            var result = _controller.GetDocumentById(2);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var document = okResult.Value as DocWithImageData;
            Assert.IsNotNull(document);
            Assert.AreEqual(7, document.DocModel.EnqId);
        }

        [TestMethod]
        public void GetDocumentById_ReturnsNotFound_ForInvalidDocumentId()
        {
            // Act
            var result = _controller.GetDocumentById(99);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }
        [TestMethod]
        public async Task UpdateDocument_ReturnsOk_ForValidDocumentRequest()
        {
            // Arrange
            var docContent = new byte[] { 1, 2, 3, 4, 5 }; // Sample document content
            var docStream = new MemoryStream(docContent);
            var docRequest = new DocumentRequest
            {
                DocId = 2,
                EnqId = 7,
                CustId = 1,
                Status = 1,
                DocTypeId = 1,
                Doc = new FormFile(docStream, 0, docStream.Length, "Doc", "test.pdf")
            };

            // Act
            var result = await _controller.UpdateDocument(docRequest);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            // Assuming your controller returns an object of a known type, let's call it DocumentResponse
            var resultValue = okResult.Value as DocumentResponse;
            Assert.IsNotNull(resultValue);
            Assert.AreEqual(2, resultValue.DocId);

            // Check if updated image URL is returned
            var updatedImage = resultValue.UpdatedImage;
            Assert.IsNotNull(updatedImage);
            Assert.IsTrue(updatedImage.StartsWith("data:image/jpeg;base64,"));
            // You can further validate the content of the updated image if needed
        }
        [TestMethod]
        public async Task UpdateDocument_ReturnsBadRequest_ForInvalidDocumentRequest()
        {
            // Arrange
            var invalidDocRequest = new DocumentRequest { }; // Invalid request with null properties

            // Act
            var result = await _controller.UpdateDocument(invalidDocRequest);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
        [TestMethod]
        public void AssignManager_ReturnsOk_ForValidEnquiryId()
        {
            // Arrange
            int enqId = 21; // Valid enquiry ID

            // Act
            var result = _controller.AssignManager(enqId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
        }
        [TestMethod]
        public void AssignManager_ReturnsNotFound_ForInvalidEnquiryId()
        {
            // Arrange
            int enqId = 99; // Invalid enquiry ID

            // Act
            var result = _controller.AssignManager(enqId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }
    }
}