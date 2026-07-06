namespace NutritionService.Features.Meals.DTOs
{
    public class MealDetailDto
    {
        public Guid MealId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int PrepTimeInMinutes { get; set; }
        public string Difficulty { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

        
        public int Calories { get; set; }
        public float Protein { get; set; }
        public float Carbs { get; set; }
        public float Fats { get; set; }
        public float Fiber { get; set; }

       
        public string Ingredients { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
        public string? Allergens { get; set; }
        public string? Tags { get; set; }
        public string? Variations { get; set; }
    }
}
