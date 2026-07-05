using FluentValidation;
using ProfileService.Features.UpdateSettings.ViewModels;

namespace ProfileService.Features.UpdateSettings.Validator
{
    public class UpdateSettingsRequestValidator : AbstractValidator<UpdateSettingsRequestViewModel>
    {
        public UpdateSettingsRequestValidator()
        {
            RuleFor(x => x)
                .Must(HaveAtLeastOneSettingProvided)
                .WithMessage("VAL_REQUIRED_FIELD");

            When(x => x.Preferences != null && x.Preferences.Theme != null, () =>
            {
                RuleFor(x => x.Preferences.Theme).Must(t => t == "light" || t == "dark");
            });
        }

        private bool HaveAtLeastOneSettingProvided(UpdateSettingsRequestViewModel request)
        {
            if (request == null) return false;

            bool hasPreferences = request.Preferences != null &&
                (request.Preferences.Language != null || request.Preferences.Theme != null ||
                 request.Preferences.WeightUnit != null || request.Preferences.HeightUnit != null || request.Preferences.DistanceUnit != null);

            bool hasNotifications = request.Notifications != null &&
                (request.Notifications.WorkoutReminders.HasValue || request.Notifications.MealReminders.HasValue ||
                 request.Notifications.AchievementAlerts.HasValue || request.Notifications.WeeklyReports.HasValue ||
                 request.Notifications.EmailNotifications.HasValue || request.Notifications.PushNotifications.HasValue);

            bool hasPrivacy = request.Privacy != null &&
                (request.Privacy.ProfileVisibility != null || request.Privacy.ShowProgressToFriends.HasValue || request.Privacy.AllowDataSharing.HasValue);

            return hasPreferences || hasNotifications || hasPrivacy;
        }
    }
}
