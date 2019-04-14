using HomeBudget.Application.Domain;
using HomeBudget.Application.Queries.Common;
using HomeBudget.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBudget.Application.Queries.Categories
{
    public class CategoryListQuery : PaginatedQuery<Category>
    {
        public string Search { get; set; }
    }

    public class CategoryQuery : IRequest<Category>
    {
        public Guid Id { get; set; }
    }
}