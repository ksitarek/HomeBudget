using AutoMapper;
using HomeBudget.Application.Domain;
using HomeBudget.Application.Queries.Categories;
using HomeBudget.Application.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeBudget.IntegrationTests.Application.Services
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private DbContextOptions<BudgetContext> _options;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<BudgetContext>()
                .UseInMemoryDatabase("BudgetContextTests")
                .Options;

            _mapper = Mock.Of<IMapper>();

            // make sure we have empty context
            using (var ctx = new BudgetContext(_options))
            {
                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();
            }
        }

        [Test]
        public async Task Will_List_Categories_FirstPage()
        {
            // arrange
            using (var context = new BudgetContext(_options))
            {
                context.Add(new Category() { Id = Guid.NewGuid(), Label = "1" });
                context.Add(new Category() { Id = Guid.NewGuid(), Label = "2" });
                context.Add(new Category() { Id = Guid.NewGuid(), Label = "3", IsArchived = true });
                context.Add(new Category() { Id = Guid.NewGuid(), Label = "4" });
                await context.SaveChangesAsync();
            }

            var query = new CategoryListQuery()
            {
                PerPage = 2,
                OrderBy = nameof(Category.Label),
                Descending = true
            };

            using (var context = new BudgetContext(_options))
            {
                // act
                var service = new CategoryService(context, _mapper);
                var result = await service.Handle(query, CancellationToken.None);

                Assert.IsFalse(result.IsEmpty);
                Assert.AreEqual(3, result.TotalResults);
                Assert.AreEqual(2, result.Results.Count());
                Assert.AreEqual(2, result.TotalPages);

                Assert.AreEqual("4", result.Results.ElementAt(0).Label);
                Assert.AreEqual("2", result.Results.ElementAt(1).Label);
            }
        }

        [Test]
        public async Task Will_List_Categories_SecondPage()
        {
            // arrange
            using (var context = new BudgetContext(_options))
            {
                context.Add(new Category() { Id = Guid.NewGuid(), Label = "1" });
                context.Add(new Category() { Id = Guid.NewGuid(), Label = "2" });
                context.Add(new Category() { Id = Guid.NewGuid(), Label = "3", IsArchived = true });
                context.Add(new Category() { Id = Guid.NewGuid(), Label = "4" });
                await context.SaveChangesAsync();
            }

            var query = new CategoryListQuery()
            {
                PerPage = 2,
                Page = 1,
                OrderBy = nameof(Category.Label),
                Descending = true
            };

            using (var context = new BudgetContext(_options))
            {
                // act
                var service = new CategoryService(context, _mapper);
                var result = await service.Handle(query, CancellationToken.None);

                Assert.IsFalse(result.IsEmpty);
                Assert.AreEqual(3, result.TotalResults);
                Assert.AreEqual(1, result.Results.Count());
                Assert.AreEqual(2, result.TotalPages);

                Assert.AreEqual("1", result.Results.ElementAt(0).Label);
            }
        }
    }
}