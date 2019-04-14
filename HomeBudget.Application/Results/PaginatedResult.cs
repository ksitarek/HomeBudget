using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeBudget.Application.Results
{
    public class PaginatedResult<T>
    {
        public int CurrentPage { get; }
        public int PerPage { get; }

        public int TotalPages { get; }
        public long TotalResults { get; }

        public IEnumerable<T> Results { get; }
        public bool IsEmpty => Results == null || Results.Any() == false;

        protected PaginatedResult()
        {
            Results = Enumerable.Empty<T>();
        }

        public PaginatedResult(IEnumerable<T> results, int currentPage, int perPage, long totalResults)
        {
            Results = results;
            CurrentPage = currentPage;
            PerPage = perPage;
            TotalResults = totalResults;

            TotalPages = (int)Math.Ceiling(totalResults / (decimal)perPage);
        }

        public static PaginatedResult<T> Empty => new PaginatedResult<T>();
    }
}