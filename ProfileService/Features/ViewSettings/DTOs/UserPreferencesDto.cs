namespace ProfileService.Features.ViewSettings.DTOs
{
    public class UserPreferencesDto
    {
        public string Language { get; set; } = string.Empty;
        public string Theme { get; set; } = string.Empty;
        public string WeightUnit { get; set; } = string.Empty;
        public string HeightUnit { get; set; } = string.Empty;
        public string DistanceUnit { get; set; } = string.Empty;
    }
}
