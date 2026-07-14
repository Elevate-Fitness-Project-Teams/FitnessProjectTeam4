using Microsoft.EntityFrameworkCore;
using NutritionService.Domain.Entities;
using NutritionService.Domain.Enums;

namespace NutritionService.Persistence
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(NutritionDbContext context)
        {
            if (await context.Meals.AnyAsync())
                return;

            // Meal IDs
            var oatmealId = Guid.NewGuid();
            var eggWhitesId = Guid.NewGuid();
            var pancakesId = Guid.NewGuid();
            var chickenSaladId = Guid.NewGuid();
            var grilledSalmonId = Guid.NewGuid();
            var turkeyWrapId = Guid.NewGuid();
            var steakVeggiesId = Guid.NewGuid();
            var pastaId = Guid.NewGuid();
            var smoothieId = Guid.NewGuid();
            var proteinBarsId = Guid.NewGuid();

            var meals = new List<Meal>
            {
                // === BREAKFAST ===
                new Meal
                {
                    MealId = oatmealId,
                    Name = "Oatmeal with Fruits",
                    Type = MealType.Breakfast,
                    PrepTimeInMinutes = 10,
                    Difficulty = DifficultyLevel.Easy,
                    ImageUrl = "https://example.com/images/oatmeal.jpg",
                    IngredientsJson = "[\"Oats\", \"Banana\", \"Berries\", \"Honey\", \"Milk\"]",
                    InstructionsJson = "[\"Cook oats with milk\", \"Slice banana\", \"Top with fruits and honey\"]",
                    AllergensJson = "[\"Milk\"]",
                    TagsJson = "[\"High Fiber\", \"Vegetarian\"]",
                    VariationsJson = "[\"With Greek Yogurt\", \"With Almond Milk\", \"With Chia Seeds\"]",
                    NutritionFacts = new NutritionFacts
                    {
                        Id = Guid.NewGuid(),
                        Calories = 280,
                        Protein = 8.0f,
                        Carbs = 52.0f,
                        Fats = 5.5f,
                        Fiber = 7.0f
                    }
                },
                new Meal
                {
                    MealId = eggWhitesId,
                    Name = "Egg Whites with Toast",
                    Type = MealType.Breakfast,
                    PrepTimeInMinutes = 15,
                    Difficulty = DifficultyLevel.Easy,
                    ImageUrl = "https://example.com/images/egg-whites.jpg",
                    IngredientsJson = "[\"Egg Whites\", \"Whole Wheat Toast\", \"Spinach\", \"Tomato\"]",
                    InstructionsJson = "[\"Scramble egg whites with spinach\", \"Toast bread\", \"Serve together\"]",
                    AllergensJson = "[\"Eggs\", \"Gluten\"]",
                    TagsJson = "[\"High Protein\", \"Low Fat\"]",
                    VariationsJson = "[\"With Avocado\", \"With Cheese\"]",
                    NutritionFacts = new NutritionFacts
                    {
                        Id = Guid.NewGuid(),
                        Calories = 220,
                        Protein = 24.0f,
                        Carbs = 18.0f,
                        Fats = 3.0f,
                        Fiber = 3.5f
                    }
                },
                new Meal
                {
                    MealId = pancakesId,
                    Name = "Protein Pancakes",
                    Type = MealType.Breakfast,
                    PrepTimeInMinutes = 20,
                    Difficulty = DifficultyLevel.Medium,
                    ImageUrl = "https://example.com/images/pancakes.jpg",
                    IngredientsJson = "[\"Protein Powder\", \"Oat Flour\", \"Egg\", \"Banana\", \"Baking Powder\"]",
                    InstructionsJson = "[\"Mix dry ingredients\", \"Add wet ingredients\", \"Cook on skillet\", \"Top with berries\"]",
                    AllergensJson = "[\"Eggs\", \"Gluten\", \"Milk\"]",
                    TagsJson = "[\"High Protein\", \"Muscle Building\"]",
                    VariationsJson = "[\"Chocolate Flavor\", \"With Maple Syrup\", \"Blueberry\"]",
                    NutritionFacts = new NutritionFacts
                    {
                        Id = Guid.NewGuid(),
                        Calories = 380,
                        Protein = 30.0f,
                        Carbs = 42.0f,
                        Fats = 8.0f,
                        Fiber = 4.0f
                    }
                },

                // === LUNCH ===
                new Meal
                {
                    MealId = chickenSaladId,
                    Name = "Grilled Chicken Salad",
                    Type = MealType.Lunch,
                    PrepTimeInMinutes = 25,
                    Difficulty = DifficultyLevel.Easy,
                    ImageUrl = "https://example.com/images/chicken-salad.jpg",
                    IngredientsJson = "[\"Chicken Breast\", \"Lettuce\", \"Tomato\", \"Olive Oil\", \"Lemon\"]",
                    InstructionsJson = "[\"Grill the chicken\", \"Chop vegetables\", \"Mix together\", \"Add dressing\"]",
                    AllergensJson = null,
                    TagsJson = "[\"High Protein\", \"Low Carb\", \"Healthy\"]",
                    VariationsJson = "[\"With Quinoa\", \"With Avocado\", \"Caesar Style\"]",
                    NutritionFacts = new NutritionFacts
                    {
                        Id = Guid.NewGuid(),
                        Calories = 350,
                        Protein = 35.5f,
                        Carbs = 12.0f,
                        Fats = 15.0f,
                        Fiber = 4.5f
                    }
                },
                new Meal
                {
                    MealId = grilledSalmonId,
                    Name = "Grilled Salmon with Rice",
                    Type = MealType.Lunch,
                    PrepTimeInMinutes = 30,
                    Difficulty = DifficultyLevel.Medium,
                    ImageUrl = "https://example.com/images/salmon.jpg",
                    IngredientsJson = "[\"Salmon Fillet\", \"Brown Rice\", \"Broccoli\", \"Lemon\", \"Garlic\"]",
                    InstructionsJson = "[\"Season salmon\", \"Grill for 8 minutes\", \"Cook rice\", \"Steam broccoli\"]",
                    AllergensJson = "[\"Fish\"]",
                    TagsJson = "[\"High Protein\", \"Omega-3\", \"Healthy Fats\"]",
                    VariationsJson = "[\"With Sweet Potato\", \"Teriyaki Style\"]",
                    NutritionFacts = new NutritionFacts
                    {
                        Id = Guid.NewGuid(),
                        Calories = 480,
                        Protein = 40.0f,
                        Carbs = 38.0f,
                        Fats = 18.0f,
                        Fiber = 5.0f
                    }
                },
                new Meal
                {
                    MealId = turkeyWrapId,
                    Name = "Turkey Wrap",
                    Type = MealType.Lunch,
                    PrepTimeInMinutes = 10,
                    Difficulty = DifficultyLevel.Easy,
                    ImageUrl = "https://example.com/images/turkey-wrap.jpg",
                    IngredientsJson = "[\"Turkey Slices\", \"Whole Wheat Tortilla\", \"Hummus\", \"Cucumber\", \"Lettuce\"]",
                    InstructionsJson = "[\"Spread hummus on tortilla\", \"Layer turkey and vegetables\", \"Roll and cut\"]",
                    AllergensJson = "[\"Gluten\", \"Sesame\"]",
                    TagsJson = "[\"High Protein\", \"Quick\", \"Portable\"]",
                    VariationsJson = "[\"With Cheese\", \"Spicy Version\"]",
                    NutritionFacts = new NutritionFacts
                    {
                        Id = Guid.NewGuid(),
                        Calories = 310,
                        Protein = 28.0f,
                        Carbs = 30.0f,
                        Fats = 9.0f,
                        Fiber = 4.0f
                    }
                },

                // === DINNER ===
                new Meal
                {
                    MealId = steakVeggiesId,
                    Name = "Steak with Roasted Vegetables",
                    Type = MealType.Dinner,
                    PrepTimeInMinutes = 35,
                    Difficulty = DifficultyLevel.Medium,
                    ImageUrl = "https://example.com/images/steak.jpg",
                    IngredientsJson = "[\"Beef Steak\", \"Bell Peppers\", \"Zucchini\", \"Olive Oil\", \"Garlic\"]",
                    InstructionsJson = "[\"Season steak\", \"Sear on high heat\", \"Roast vegetables in oven\", \"Rest steak 5 minutes\"]",
                    AllergensJson = null,
                    TagsJson = "[\"High Protein\", \"Iron Rich\", \"Keto\"]",
                    VariationsJson = "[\"With Mashed Potatoes\", \"With Mushroom Sauce\"]",
                    NutritionFacts = new NutritionFacts
                    {
                        Id = Guid.NewGuid(),
                        Calories = 520,
                        Protein = 45.0f,
                        Carbs = 15.0f,
                        Fats = 30.0f,
                        Fiber = 4.0f
                    }
                },
                new Meal
                {
                    MealId = pastaId,
                    Name = "Chicken Pasta",
                    Type = MealType.Dinner,
                    PrepTimeInMinutes = 25,
                    Difficulty = DifficultyLevel.Easy,
                    ImageUrl = "https://example.com/images/pasta.jpg",
                    IngredientsJson = "[\"Whole Wheat Pasta\", \"Chicken Breast\", \"Tomato Sauce\", \"Garlic\", \"Basil\"]",
                    InstructionsJson = "[\"Cook pasta al dente\", \"Grill diced chicken\", \"Simmer sauce\", \"Combine and serve\"]",
                    AllergensJson = "[\"Gluten\"]",
                    TagsJson = "[\"Balanced\", \"Muscle Building\"]",
                    VariationsJson = "[\"With Pesto\", \"Creamy Alfredo\", \"With Shrimp\"]",
                    NutritionFacts = new NutritionFacts
                    {
                        Id = Guid.NewGuid(),
                        Calories = 550,
                        Protein = 38.0f,
                        Carbs = 60.0f,
                        Fats = 14.0f,
                        Fiber = 6.0f
                    }
                },

                // === SNACK ===
                new Meal
                {
                    MealId = smoothieId,
                    Name = "Protein Smoothie",
                    Type = MealType.Snack,
                    PrepTimeInMinutes = 5,
                    Difficulty = DifficultyLevel.Easy,
                    ImageUrl = "https://example.com/images/smoothie.jpg",
                    IngredientsJson = "[\"Protein Powder\", \"Banana\", \"Peanut Butter\", \"Almond Milk\"]",
                    InstructionsJson = "[\"Add all ingredients to blender\", \"Blend until smooth\"]",
                    AllergensJson = "[\"Nuts\", \"Milk\"]",
                    TagsJson = "[\"High Protein\", \"Quick\"]",
                    VariationsJson = "[\"With Oats\", \"With Cocoa Powder\"]",
                    NutritionFacts = new NutritionFacts
                    {
                        Id = Guid.NewGuid(),
                        Calories = 320,
                        Protein = 28.0f,
                        Carbs = 30.0f,
                        Fats = 10.0f,
                        Fiber = 3.0f
                    }
                },
                new Meal
                {
                    MealId = proteinBarsId,
                    Name = "Homemade Protein Bars",
                    Type = MealType.Snack,
                    PrepTimeInMinutes = 15,
                    Difficulty = DifficultyLevel.Easy,
                    ImageUrl = "https://example.com/images/protein-bars.jpg",
                    IngredientsJson = "[\"Protein Powder\", \"Oats\", \"Honey\", \"Peanut Butter\", \"Dark Chocolate Chips\"]",
                    InstructionsJson = "[\"Mix all ingredients\", \"Press into baking dish\", \"Refrigerate for 2 hours\", \"Cut into bars\"]",
                    AllergensJson = "[\"Nuts\", \"Gluten\", \"Milk\"]",
                    TagsJson = "[\"High Protein\", \"Meal Prep\", \"Portable\"]",
                    VariationsJson = "[\"No-Bake Version\", \"With Coconut\"]",
                    NutritionFacts = new NutritionFacts
                    {
                        Id = Guid.NewGuid(),
                        Calories = 250,
                        Protein = 20.0f,
                        Carbs = 28.0f,
                        Fats = 8.0f,
                        Fiber = 3.0f
                    }
                }
            };

            context.Meals.AddRange(meals);
            await context.SaveChangesAsync();

            if (!await context.MealPlans.AnyAsync())
            {
                var mealPlans = new List<MealPlan>
                {
                    new MealPlan
                    {
                        MealPlanId = Guid.NewGuid(),
                        Name = "Fat Loss Plan",
                        Description = "A low-calorie plan designed to help you burn fat while maintaining muscle mass.",
                        TargetCalorieRangeMin = 1500,
                        TargetCalorieRangeMax = 1800,
                        MealPlanItems = new List<MealPlanItem>
                        {
                            new MealPlanItem { Id = Guid.NewGuid(), MealId = eggWhitesId, DayOfWeek = DayOfWeek.Saturday, MealTime = MealType.Breakfast },
                            new MealPlanItem { Id = Guid.NewGuid(), MealId = chickenSaladId, DayOfWeek = DayOfWeek.Saturday, MealTime = MealType.Lunch },
                            new MealPlanItem { Id = Guid.NewGuid(), MealId = steakVeggiesId, DayOfWeek = DayOfWeek.Saturday, MealTime = MealType.Dinner },
                            new MealPlanItem { Id = Guid.NewGuid(), MealId = oatmealId, DayOfWeek = DayOfWeek.Sunday, MealTime = MealType.Breakfast },
                            new MealPlanItem { Id = Guid.NewGuid(), MealId = turkeyWrapId, DayOfWeek = DayOfWeek.Sunday, MealTime = MealType.Lunch },
                            new MealPlanItem { Id = Guid.NewGuid(), MealId = chickenSaladId, DayOfWeek = DayOfWeek.Sunday, MealTime = MealType.Dinner }
                        }
                    },
                    new MealPlan
                    {
                        MealPlanId = Guid.NewGuid(),
                        Name = "Balanced Maintenance Plan",
                        Description = "A balanced plan for maintaining your current weight with proper nutrition.",
                        TargetCalorieRangeMin = 1800,
                        TargetCalorieRangeMax = 2200,
                        MealPlanItems = new List<MealPlanItem>
                        {
                            new MealPlanItem { Id = Guid.NewGuid(), MealId = oatmealId, DayOfWeek = DayOfWeek.Saturday, MealTime = MealType.Breakfast },
                            new MealPlanItem { Id = Guid.NewGuid(), MealId = grilledSalmonId, DayOfWeek = DayOfWeek.Saturday, MealTime = MealType.Lunch },
                            new MealPlanItem { Id = Guid.NewGuid(), MealId = smoothieId, DayOfWeek = DayOfWeek.Saturday, MealTime = MealType.Snack },
                            new MealPlanItem { Id = Guid.NewGuid(), MealId = pastaId, DayOfWeek = DayOfWeek.Saturday, MealTime = MealType.Dinner },
                            new MealPlanItem { Id = Guid.NewGuid(), MealId = pancakesId, DayOfWeek = DayOfWeek.Sunday, MealTime = MealType.Breakfast },
                            new MealPlanItem { Id = Guid.NewGuid(), MealId = turkeyWrapId, DayOfWeek = DayOfWeek.Sunday, MealTime = MealType.Lunch },
                            new MealPlanItem { Id = Guid.NewGuid(), MealId = proteinBarsId, DayOfWeek = DayOfWeek.Sunday, MealTime = MealType.Snack }
                        }
                    },
                    new MealPlan
                    {
                        MealPlanId = Guid.NewGuid(),
                        Name = "Mass Gainer Plan",
                        Description = "High calorie and high protein plan for gaining muscle mass and strength.",
                        TargetCalorieRangeMin = 2500,
                        TargetCalorieRangeMax = 3000,
                        MealPlanItems = new List<MealPlanItem>
                        {
                            new MealPlanItem { Id = Guid.NewGuid(), MealId = pancakesId, DayOfWeek = DayOfWeek.Saturday, MealTime = MealType.Breakfast },
                            new MealPlanItem { Id = Guid.NewGuid(), MealId = grilledSalmonId, DayOfWeek = DayOfWeek.Saturday, MealTime = MealType.Lunch },
                            new MealPlanItem { Id = Guid.NewGuid(), MealId = smoothieId, DayOfWeek = DayOfWeek.Saturday, MealTime = MealType.Snack },
                            new MealPlanItem { Id = Guid.NewGuid(), MealId = steakVeggiesId, DayOfWeek = DayOfWeek.Saturday, MealTime = MealType.Dinner },
                            new MealPlanItem { Id = Guid.NewGuid(), MealId = oatmealId, DayOfWeek = DayOfWeek.Sunday, MealTime = MealType.Breakfast },
                            new MealPlanItem { Id = Guid.NewGuid(), MealId = pastaId, DayOfWeek = DayOfWeek.Sunday, MealTime = MealType.Lunch },
                            new MealPlanItem { Id = Guid.NewGuid(), MealId = proteinBarsId, DayOfWeek = DayOfWeek.Sunday, MealTime = MealType.Snack },
                            new MealPlanItem { Id = Guid.NewGuid(), MealId = steakVeggiesId, DayOfWeek = DayOfWeek.Sunday, MealTime = MealType.Dinner }
                        }
                    }
                };

                context.MealPlans.AddRange(mealPlans);
                await context.SaveChangesAsync();
            }
        }
    }
}
