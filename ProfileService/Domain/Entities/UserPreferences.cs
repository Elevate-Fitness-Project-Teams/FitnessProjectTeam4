using ProfileService.Domain.Common;

namespace ProfileService.Domain.Entities
{
    public class UserPreferences : BaseEntity
    {
        public string Language { get; set; } = "en";
        public string Theme { get; set; } = "light";
        public string WeightUnit { get; set; } = "kg";
        public string HeightUnit { get; set; } = "cm";
        public string DistanceUnit { get; set; } = "km";
    }
}
