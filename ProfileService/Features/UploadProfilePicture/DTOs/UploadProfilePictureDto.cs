namespace ProfileService.Features.UploadProfilePicture.DTOs
{
    public class UploadProfilePictureDto
    {
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public Stream Content { get; set; } = Stream.Null;
    }
}
