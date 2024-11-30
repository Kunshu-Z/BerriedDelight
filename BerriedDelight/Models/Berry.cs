namespace BerriedDelight.Models
{
    public class Berry
    {
        //Fields for Berry
        public int BerryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageThumbnailUrl { get; set; }
        public string? RecommendedRecipie { get; set; }
        public bool InStock { get; set; }
        public bool IsPopularBerry { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
    }
}
