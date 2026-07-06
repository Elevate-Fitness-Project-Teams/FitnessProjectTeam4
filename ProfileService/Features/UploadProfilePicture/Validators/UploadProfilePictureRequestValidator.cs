using FluentValidation;
using ProfileService.Features.UploadProfilePicture.ViewModels;

namespace ProfileService.Features.UploadProfilePicture.Validators
{
    public class UploadProfilePictureRequestValidator : AbstractValidator<UploadProfilePictureRequestViewModel>
    {
        private const int MaxFileSizeInBytes = 5 * 1024 * 1024;

        public UploadProfilePictureRequestValidator()
        {
            RuleFor(x => x.File)
                .NotNull().WithMessage("VAL_FILE_REQUIRED")
                .Must(BeAValidImage).WithMessage("VAL_INVALID_FILE_TYPE")
                .Must(BeWithinAllowedSize).WithMessage("VAL_FILE_SIZE_EXCEEDED");
        }

        private bool BeAValidImage(IFormFile file)
        {
            if (file == null) return false;

            var allowedContentTypes = new[] { "image/jpeg", "image/jpg", "image/png" };
            if (!allowedContentTypes.Contains(file.ContentType.ToLower()))
            {
                return false;
            }

            var allowedExtensions = new[] { ".jpeg", ".jpg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            return allowedExtensions.Contains(extension);
        }

        private bool BeWithinAllowedSize(IFormFile file)
        {
            if (file == null) return false;

            return file.Length <= MaxFileSizeInBytes;
        }
    }
}
