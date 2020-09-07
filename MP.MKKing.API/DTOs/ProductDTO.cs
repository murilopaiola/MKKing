namespace MP.MKKing.API.DTOs
{
    /// <summary>
    /// The product DTO to be returned to the client
    /// </summary>
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public string ProductType { get; set; }

        public string ProductBrand { get; set; }
    }
}