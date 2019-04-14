using HomeBudget.Application.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

namespace HomeBudget.Application.Queries.Common
{
    public class PaginatedQuery<T> : PaginatedQuery, IRequest<PaginatedResult<T>>
    {
        public IQueryable<T> Modify(IQueryable<T> queryable)
        {
            if (!string.IsNullOrEmpty(OrderBy))
            {
                var sortCrit = OrderBy;
                if (Descending)
                {
                    sortCrit += " DESC";
                }
                queryable = queryable.OrderBy(sortCrit);
            }

            queryable = queryable
                .Skip(Page * PerPage)
                .Take(PerPage);

            return queryable;
        }
    }

    public class PaginatedQuery
    {
        public int Page { get; set; } = 0;
        public int PerPage { get; set; } = 5;
        public string OrderBy { get; set; }
        public bool Descending { get; set; }
    }
}