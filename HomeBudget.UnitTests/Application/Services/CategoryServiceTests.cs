using AutoMapper;
using HomeBudget.Application.Commands.Categories;
using HomeBudget.Application.Domain;
using HomeBudget.Application.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeBudget.Tests.UnitTests.Application.Services
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private BudgetContext _context;
        private IMapper _mapper;

        private CategoryService _categoryService;

        private Category _category;

        private CreateCategoryCommand _createCategoryCommand;
        private RenameCategoryCommand _renameCategoryCommand;
        private ArchiveCategoryCommand _deleteCategoryCommand;

        [SetUp]
        public void SetupTests()
        {
            _category = new Category()
            {
                Label = "TestCommand"
            };

            _createCategoryCommand = new CreateCategoryCommand("TestCommand");
            _renameCategoryCommand = new RenameCategoryCommand(Guid.NewGuid(), "TestCommand2");
            _deleteCategoryCommand = new ArchiveCategoryCommand(Guid.NewGuid());

            SetupContextMock();
            SetupMapperMock();

            _categoryService = new CategoryService(_context, _mapper);
        }

        private void SetupContextMock()
        {
            var dbOptions = new DbContextOptionsBuilder<BudgetContext>()
                   .UseInMemoryDatabase(databaseName: "TestCatalog")
                   .Options;
            var contextMock = new Mock<BudgetContext>(dbOptions);
            contextMock
                .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1)
                .Verifiable();

            _context = contextMock.Object;
        }

        private void SetupMapperMock()
        {
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<Category>(_createCategoryCommand))
                .Returns(_category);

            _mapper = mapperMock.Object;
        }

        [Test]
        public async Task Handle_CreateCategoryCommand_Successfuly()
        {
            var result = await _categoryService.Handle(_createCategoryCommand, CancellationToken.None);

            Mock.Get(_context).Verify(s => s.SaveChangesAsync(CancellationToken.None));
            Assert.IsTrue(result.IsSuccess);

            Assert.IsNotNull(result.ObjectId);
            Assert.AreNotEqual(Guid.Empty, result.ObjectId);
        }

        [Test]
        public async Task Handle_CreateCategoryCommand_SaveChangesAsync_Throws()
        {
            var exception = new Exception();
            Mock.Get(_context).Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Throws(exception).Verifiable();

            var result = await _categoryService.Handle(_createCategoryCommand, CancellationToken.None);

            Mock.Get(_context).Verify(s => s.SaveChangesAsync(CancellationToken.None));
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("There was an error creating the category.", result.Exception.Message);
            Assert.AreEqual(exception, result.Exception.InnerException);
        }

        [Test]
        public async Task Handle_RenameCategoryCommand_Successfuly()
        {
            var result = await _categoryService.Handle(_renameCategoryCommand, CancellationToken.None);

            Mock.Get(_context).Verify(s => s.SaveChangesAsync(CancellationToken.None));
            Assert.IsTrue(result.IsSuccess);

            Assert.IsNotNull(result.ObjectId);
            Assert.AreNotEqual(Guid.Empty, result.ObjectId);
        }

        [Test]
        public async Task Handle_RenameCategoryCommand_SaveChangesAsync_Throws()
        {
            var exception = new Exception();
            Mock.Get(_context).Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Throws(exception).Verifiable();

            var result = await _categoryService.Handle(_renameCategoryCommand, CancellationToken.None);

            Mock.Get(_context).Verify(s => s.SaveChangesAsync(CancellationToken.None));
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("There was an error when trying to rename the category.", result.Exception.Message);
            Assert.AreEqual(exception, result.Exception.InnerException);
        }

        [Test]
        public async Task Handle_DeleteCategoryCommand_Successfuly()
        {
            var result = await _categoryService.Handle(_deleteCategoryCommand, CancellationToken.None);

            Mock.Get(_context).Verify(s => s.SaveChangesAsync(CancellationToken.None));
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNull(result.ObjectId);
        }

        [Test]
        public async Task Handle_DeleteCategoryCommand_SaveChangesAsync_Throws()
        {
            var exception = new Exception();
            Mock.Get(_context).Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Throws(exception).Verifiable();

            var result = await _categoryService.Handle(_deleteCategoryCommand, CancellationToken.None);

            Mock.Get(_context).Verify(s => s.SaveChangesAsync(CancellationToken.None));
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("There was an error when trying to delete the category.", result.Exception.Message);
            Assert.AreEqual(exception, result.Exception.InnerException);
        }
    }
}