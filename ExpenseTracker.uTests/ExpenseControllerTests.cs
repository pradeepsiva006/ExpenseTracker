using ExpenseTracker.API.Controllers;
using ExpenseTracker.Common.DTOs;
using ExpenseTracker.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Claims;

namespace ExpenseTracker.uTests
{
    [TestFixture]
    public class ExpenseControllerTests
    {
        private Mock<IExpenseService> _mockExpenseService;
        private Mock<ILogger<ExpenseController>> _mockLogger;
        private ExpenseController _expenseController;

        [SetUp]
        public void Setup()
        {
            _mockExpenseService = new Mock<IExpenseService>();
            _mockLogger = new Mock<ILogger<ExpenseController>>();
            _expenseController = new ExpenseController(_mockLogger.Object, _mockExpenseService.Object);

            var mockClaimsPrincipal = new Mock<ClaimsPrincipal>();
            mockClaimsPrincipal.Setup(c => c.FindFirst(ClaimTypes.NameIdentifier))
                              .Returns(new Claim(ClaimTypes.NameIdentifier, "123"));
            var controllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = mockClaimsPrincipal.Object }
            };

            _expenseController.ControllerContext = controllerContext;
        }

        [Test]
        public async Task AddExpenseForUser_ValidInput_ReturnsOk()
        {
            // Arrange
            var expenseRequestDto = new ExpenseRequestDto
            {
                Amount = 100,
                Category = "Food",
                Date = DateTime.UtcNow,
                ShortDescription = "Lunch"
            };

            _mockExpenseService.Setup(x => x.AddExpenseForUser(It.IsAny<ExpenseRequestDto>(), It.IsAny<int>()))
                              .ReturnsAsync(true);

            // Act
            var result = await _expenseController.AddExpenseForUser(expenseRequestDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual("Expense Added!", ((BaseResponseDto)okResult.Value).Message);
        }

        [Test]
        public async Task AddExpenseForUser_InvalidUserId_ReturnsBadRequest()
        {
            // Arrange
            var expenseRequestDto = new ExpenseRequestDto
            {
                Amount = 100,
                Category = "Food",
                Date = DateTime.UtcNow,
                ShortDescription = "Lunch"
            };
            var mockClaimsPrincipal = new Mock<ClaimsPrincipal>();
            mockClaimsPrincipal.Setup(c => c.FindFirst(ClaimTypes.NameIdentifier))
                              .Returns(new Claim(ClaimTypes.NameIdentifier, "ABC"));
            var controllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = mockClaimsPrincipal.Object }
            };

            _expenseController.ControllerContext = controllerContext;

            // Act
            var result = await _expenseController.AddExpenseForUser(expenseRequestDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual("Invalid UserId in the token!", badRequestResult.Value);
        }
    }

}