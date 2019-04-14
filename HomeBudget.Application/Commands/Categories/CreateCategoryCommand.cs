using HomeBudget.Application.Commands.Common;
using System.Collections.Generic;
using System.Text;

namespace HomeBudget.Application.Commands.Categories
{
    public class CreateCategoryCommand : Command
    {
        public string Label { get; }

        public CreateCategoryCommand(string label)
        {
            Label = label;
        }
    }
}