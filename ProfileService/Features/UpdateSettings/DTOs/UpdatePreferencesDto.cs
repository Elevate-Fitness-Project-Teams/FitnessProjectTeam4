namespace ProfileService.Features.UpdateSettings.DTOs
{
    public class UpdatePreferencesDto
    {
        public string? Language { get; set; }
        public string? Theme { get; set; }
        public string? WeightUnit { get; set; }
        public string? HeightUnit { get; set; }
        public string? DistanceUnit { get; set; }
    }
}
