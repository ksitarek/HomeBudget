using HomeBudget.Application.Commands.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudget.Api.Requests.Categories
{
    public class CreateCategoryRequest
    {
        public string Label { get; set; }
    }

    public class RenameCategoryRequest
    {
        public string NewLabel { get; set; }
    }
}