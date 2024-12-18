﻿namespace BerriedDelight.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<Berry>? Berries { get; set; }
    }
}
