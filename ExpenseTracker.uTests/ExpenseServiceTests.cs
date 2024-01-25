using AutoMapper;
using ExpenseTracker.Common.DTOs;
using ExpenseTracker.Common.Models;
using ExpenseTracker.Core.Exceptions;
using ExpenseTracker.Core.Implementations.Services;
using ExpenseTracker.Core.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace ExpenseTracker.uTests
{
    [TestFixture]
    public class ExpenseServiceTests
    {
        private Mock<IExpenseRepository> _mockExpenseRepository;
        private Mock<IMapper> _mockMapper;
        private ExpenseService _expenseService;

        [SetUp]
        public void Setup()
        {
            _mockExpenseRepository = new Mock<IExpenseRepository>();
            _mockMapper = new Mock<IMapper>();  
            _expenseService = new ExpenseService(_mockExpenseRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task AddExpenseForUser_ValidInput_ReturnsTrue()
        {
            // Arrange
            var expenseRequestDto = new ExpenseRequestDto
            {
                Amount = 100.0m,
                Category = "Food",
                Date = DateTime.Now,
                ShortDescription = "Lunch"
            };

            _mockExpenseRepository.Setup(x => x.InsertAsync(It.IsAny<Expense>()))
                                  .ReturnsAsync(new Expense());

            // Act
            var result = await _expenseService.AddExpenseForUser(expenseRequestDto, 1);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task AddExpenseForUser_InvalidInput_ThrowsException()
        {
            // Arrange
            var expenseRequestDto = new ExpenseRequestDto
            {
                Amount = -1.0m,
                Category = null,
                Date = DateTime.Now,
                ShortDescription = null
            };

            // Act
            // Assert
            var exception = Assert.ThrowsAsync<CustomAppException>(async () => await _expenseService.AddExpenseForUser(expenseRequestDto, 1));
            StringAssert.Contains("Please provide valid expense data!", exception.Message);
        }
    }
}
