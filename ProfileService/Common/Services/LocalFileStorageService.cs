using ProfileService.Common.Services.Interfaces;

namespace ProfileService.Common.Services
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<LocalFileStorageService> _logger;

        public LocalFileStorageService(IWebHostEnvironment environment, ILogger<LocalFileStorageService> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken)
        {
            try
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath ?? _environment.ContentRootPath, "Uploads", "ProfilePictures");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileExtension = Path.GetExtension(fileName);
                var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStreamOnDisk = new FileStream(filePath, FileMode.Create))
                {
                    await fileStream.CopyToAsync(fileStreamOnDisk, cancellationToken);
                }

                _logger.LogInformation("File saved successfully to local storage at: {FilePath}", filePath);

                
                var relativeUrl = $"/Uploads/ProfilePictures/{uniqueFileName}";
                return relativeUrl;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save the file {FileName} to local storage.", fileName);
                throw;
            }
        }



        public Task DeleteFileAsync(string relativeUrl)
        {
            if (string.IsNullOrWhiteSpace(relativeUrl))
            {
                return Task.CompletedTask;
            }

            try
            {
                var pathWithoutLeadingSlash = relativeUrl.TrimStart('/');

                var normalizedPath = pathWithoutLeadingSlash.Replace('/', Path.DirectorySeparatorChar);

                var physicalPath = Path.Combine(_environment.WebRootPath ?? _environment.ContentRootPath, normalizedPath);

                if (File.Exists(physicalPath))
                {
                    File.Delete(physicalPath);
                    _logger.LogInformation("Successfully deleted old file at: {PhysicalPath}", physicalPath);
                }
                else
                {
                    _logger.LogWarning("File not found for deletion at: {PhysicalPath}", physicalPath);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to delete the file: {RelativeUrl}", relativeUrl);
            }

            return Task.CompletedTask;
        }
    }
}
