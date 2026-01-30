namespace Test.Helper.Project.DTOs
{
    public class CatalogItemDto
    {
        public string? Id { get; set; }
        public required string Name { get; set; }
        public int Price { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}