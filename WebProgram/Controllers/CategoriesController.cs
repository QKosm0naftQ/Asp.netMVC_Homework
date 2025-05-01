using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProgram.Data;
using WebProgram.Data.Entities;
using WebProgram.Interface;
using WebProgram.Models.Category;

namespace WebProgram.Controllers
{
    public class CategoriesController(AppProgramDbContext context, IMapper mapper , IImageService imageService) : Controller
    {
        public IActionResult Index()
        {
            var model = mapper.ProjectTo<CategoryItemViewModel>(context.Categories).ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateViewModel model)
        {
            var item = await context.Categories.SingleOrDefaultAsync(x => x.Name == model.Name);
            if(item != null)
            {
                ModelState.AddModelError("Name", "Категорія з такою назвою вже існує");
                return View(model);
            }

            item = mapper.Map<CategoryEntity>(model);
            item.ImageUrl = await imageService.SaveImageAsync(model.ImageFile);
            await context.Categories.AddAsync(item);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var model = mapper.Map<CategoryEditViewModel>(category);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryEditViewModel model)
        {
            var item = await context.Categories.FirstOrDefaultAsync(c => c.Id == model.Id);
            if (item == null)
            {
                return NotFound();
            }
            var duplicate = await context.Categories
           .FirstOrDefaultAsync(x => x.Name == model.Name && x.Id != model.Id);
            if (duplicate != null)
            {
                ModelState.AddModelError("Name", "Вже така категорія існує");
                return View(model);
            }
            item = mapper.Map(model, item);
            item.ImageUrl = await imageService.SaveImageAsync(model.ImageFile);

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await context.Categories.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            context.Categories.Remove(item);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




    }
}
