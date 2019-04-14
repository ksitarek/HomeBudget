using HomeBudget.Application.Commands.Common;
using System;

namespace HomeBudget.Application.Commands.Categories
{
    public class ArchiveCategoryCommand : Command
    {
        public Guid CategoryId { get; }

        public ArchiveCategoryCommand(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}