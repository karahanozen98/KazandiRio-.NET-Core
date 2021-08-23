namespace KazandiRio.Application.DTO
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public int? CategoryId { get; set; }
        public string ImageUrl { get; set; }
    }
}
