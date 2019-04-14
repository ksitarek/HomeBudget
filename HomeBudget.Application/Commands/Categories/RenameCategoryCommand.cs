using HomeBudget.Application.Commands.Common;
using System;

namespace HomeBudget.Application.Commands.Categories
{
    public class RenameCategoryCommand : Command
    {
        public Guid CategoryId { get; }
        public string NewLabel { get; }

        public RenameCategoryCommand(Guid categoryId, string newLabel)
        {
            CategoryId = categoryId;
            NewLabel = newLabel;
        }
    }
}