using Microsoft.AspNetCore.Http;

namespace ToshokanApp.Core.Services;

public interface ICloudinaryService
{
    Task<string> UploadImageAsync(IFormFile file, string folder);
    Task<string> UploadPdfAsync(IFormFile file, string folder);
    Task<bool> DeleteImageAsync(string publicId);
} 