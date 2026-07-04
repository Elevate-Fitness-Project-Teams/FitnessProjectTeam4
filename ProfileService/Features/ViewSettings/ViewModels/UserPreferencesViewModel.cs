namespace ProfileService.Features.ViewSettings.ViewModels
{
    public class UserPreferencesViewModel
    {
        public string Language { get; set; } = string.Empty;
        public string Theme { get; set; } = string.Empty;
        public string WeightUnit { get; set; } = string.Empty;
        public string HeightUnit { get; set; } = string.Empty;
        public string DistanceUnit { get; set; } = string.Empty;
    }
}
