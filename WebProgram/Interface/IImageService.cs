namespace WebProgram.Interface
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile file);
        Task DeleteImageAsync(string name);
        Task<IFormFile> GetImageAsync(string name);
    }
}
