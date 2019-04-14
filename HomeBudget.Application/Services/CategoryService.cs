using AutoMapper;
using HomeBudget.Application.Commands.Categories;
using HomeBudget.Application.Commands.Common;
using HomeBudget.Application.Domain;
using HomeBudget.Application.Queries.Categories;
using HomeBudget.Application.Queries.Common;
using HomeBudget.Application.Results;
using HomeBudget.Application.Services.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeBudget.Application.Services
{
    public class CategoryService :
        IRequestHandler<CategoryListQuery, PaginatedResult<Category>>,

        IRequestHandler<CreateCategoryCommand, CommandResult>,
        IRequestHandler<RenameCategoryCommand, CommandResult>,
        IRequestHandler<ArchiveCategoryCommand, CommandResult>
    {
        private readonly BudgetContext _context;
        private readonly IMapper _mapper;

        public CategoryService(BudgetContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CommandResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var newCategory = _mapper.Map<Category>(request);

            try
            {
                await _context.AddAsync(newCategory, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return CommandResult.Success(newCategory.Id);
            }
            catch (Exception e)
            {
                return CommandResult.Error(new CreateCategoryException("There was an error creating the category.", e));
            }
        }

        public async Task<CommandResult> Handle(RenameCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var category = new Category { Id = request.CategoryId };
                _context.Attach(category);

                _mapper.Map(request, category);

                await _context.SaveChangesAsync(cancellationToken);

                return CommandResult.Success(request.CategoryId);
            }
            catch (Exception e)
            {
                return CommandResult.Error(new RenameCategoryException("There was an error when trying to rename the category.", e));
            }
        }

        public async Task<CommandResult> Handle(ArchiveCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var category = new Category { Id = request.CategoryId };
                _context.Attach(category);

                // We don't really want to delete the category from the database,
                // because that would break all already existing references.
                // Mark it as deleted, instead.
                category.IsArchived = true;

                await _context.SaveChangesAsync(cancellationToken);

                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Error(new DeleteCategoryException("There was an error when trying to delete the category.", e));
            }
        }

        public async Task<PaginatedResult<Category>> Handle(CategoryListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Categories.Where(c => c.IsArchived == false);

            // if provided, apply filter
            if (string.IsNullOrEmpty(request.Search) == false)
            {
                var labelFilter = request.Search.ToLowerInvariant();
                query = query.Where(c => c.Label.ToLowerInvariant() == labelFilter);
            }

            var totalCnt = await query.CountAsync(cancellationToken);

            var pagedQuery = request.Modify(query);
            var result = await pagedQuery.ToListAsync(cancellationToken);

            return new PaginatedResult<Category>(result, request.Page, request.PerPage, totalCnt);
        }
    }
}