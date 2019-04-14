namespace HomeBudget.Application.Domain
{
    public class Category : BaseEntity
    {
        public string Label { get; set; }
        public bool IsArchived { get; set; }

        public Category()
        {
            IsArchived = false;
        }
    }
}