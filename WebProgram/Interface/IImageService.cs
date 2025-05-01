namespace WebProgram.Interface
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile file);
    }
}
