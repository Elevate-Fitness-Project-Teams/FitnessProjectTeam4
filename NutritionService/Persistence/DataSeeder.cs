using Microsoft.EntityFrameworkCore;
using NutritionService.Domain.Entities;
using NutritionService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutritionService.Persistence
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(NutritionDbContext context)
        {
            if (await context.Meals.AnyAsync())
                return;

            var breakfastId = Guid.NewGuid();
            var lunchId = Guid.NewGuid();
            var snackId = Guid.NewGuid();

            var meals = new List<Meal>
            {
                new Meal
                {
                    MealId = breakfastId,
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
                    MealId = lunchId,
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
                    MealId = snackId,
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
                        Description = "A plan designed to help you burn fat and maintain muscle.",
                        TargetCalorieRangeMin = 1600,
                        TargetCalorieRangeMax = 2000,
                        MealPlanItems = new List<MealPlanItem>
                        {
                            new MealPlanItem
                            {
                                Id = Guid.NewGuid(),
                                MealId = breakfastId,
                                DayOfWeek = DayOfWeek.Saturday,
                                MealTime = MealType.Breakfast
                            },
                            new MealPlanItem
                            {
                                Id = Guid.NewGuid(),
                                MealId = lunchId,
                                DayOfWeek = DayOfWeek.Saturday,
                                MealTime = MealType.Lunch
                            }
                        }
                    },
                    new MealPlan
                    {
                        MealPlanId = Guid.NewGuid(),
                        Name = "Mass Gainer Plan",
                        Description = "High calorie and high protein plan for gaining muscle mass.",
                        TargetCalorieRangeMin = 2500,
                        TargetCalorieRangeMax = 3000,
                        MealPlanItems = new List<MealPlanItem>
                        {
                            new MealPlanItem
                            {
                                Id = Guid.NewGuid(),
                                MealId = snackId,
                                DayOfWeek = DayOfWeek.Sunday,
                                MealTime = MealType.Snack
                            }
                        }
                    }
                };

                context.MealPlans.AddRange(mealPlans);
                await context.SaveChangesAsync();
            }
        }
    }
}
