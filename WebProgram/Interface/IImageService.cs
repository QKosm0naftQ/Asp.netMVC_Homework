namespace WebProgram.Interface
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile file);
        Task DeleteImageAsync(string name);
        Task<string> SaveImageFromUrlAsync(string imageUrl);
        Task<string> SaveImageFromBase64Async(string input);
        Task<IFormFile> GetImageAsync(string name);
    }
}
