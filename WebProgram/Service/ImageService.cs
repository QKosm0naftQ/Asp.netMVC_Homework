﻿using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using WebProgram.Interface;

namespace WebProgram.Service
{
    public class ImageService(IConfiguration configuration) : IImageService
    {
        public async Task DeleteImageAsync(string name)
        {
            var sizes = configuration.GetRequiredSection("ImageSizes").Get<List<int>>();
            var dir = Path.Combine(Directory.GetCurrentDirectory(), configuration["ImagesDir"]!);

            Task[] tasks = sizes
                .AsParallel()
                .Select(size =>
                {
                    return Task.Run(() =>
                    {
                        var path = Path.Combine(dir, $"{size}_{name}");
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                    });
                })
                .ToArray();

            await Task.WhenAll(tasks);
        }

        public async Task<string> SaveImageFromUrlAsync(string imageUrl)
        {
            using var httpClient = new HttpClient();
            var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
            return await SaveImageAsync(imageBytes);        
        }

        public async Task<string> SaveImageFromBase64Async(string input)
        {
            var base64Data = input.Contains(",")
                ? input.Substring(input.IndexOf(",") + 1)
                : input;

            byte[] imageBytes = Convert.FromBase64String(base64Data);

            return await SaveImageAsync(imageBytes);
        }
        public async Task<IFormFile> GetImageAsync(string name)
        {

            var size = 400;
            var dir = Path.Combine(Directory.GetCurrentDirectory(), configuration["ImagesDir"]!);
            var path = Path.Combine(dir, $"{size}_{name}");

            if (File.Exists(path))
            {
                var memoryStream = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    await stream.CopyToAsync(memoryStream);
                }

                memoryStream.Position = 0;
                var file = new FormFile(memoryStream, 0, memoryStream.Length, "file", $"{size}_{name}")
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg" // або визначати MIME тип динамічно
                };

                return file;
            }
            return null;
        }

        public async Task<string> SaveImageAsync(IFormFile file)
        {
            using MemoryStream ms = new();
            await file.CopyToAsync(ms);
            var bytes = ms.ToArray();

            var imageName = await SaveImageAsync(bytes);
            return imageName;
        }

        private async Task<string> SaveImageAsync(byte[] bytes)
        {
            string imageName = $"{Path.GetRandomFileName()}.webp";
            var sizes = configuration.GetRequiredSection("ImageSizes").Get<List<int>>();

            Task[] tasks = sizes
                .AsParallel()
                .Select(s => SaveImageAsync(bytes, imageName, s))
                .ToArray();

            await Task.WhenAll(tasks);

            return imageName;
        }

        private async Task SaveImageAsync(byte[] bytes, string name, int size)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), configuration["ImagesDir"]!,
                $"{size}_{name}");
            using var image = Image.Load(bytes);
            image.Mutate(async imgConext =>
            {
                imgConext.Resize(new ResizeOptions
                {
                    Size = new Size(size, size),
                    Mode = ResizeMode.Max
                });
                await image.SaveAsync(path, new WebpEncoder());
            });
        }
    }
}
