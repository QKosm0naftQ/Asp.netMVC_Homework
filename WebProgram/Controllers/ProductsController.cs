using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebProgram.Data;
using WebProgram.Models.Product;

namespace WebProgram.Controllers;

public class ProductsController(AppProgramDbContext context, 
    IMapper mapper) : Controller
{
    // GET
    public IActionResult Index() 
    {
        ViewBag.Title = "Категорії";
        var model = mapper.ProjectTo<ProductItemViewModel>(context.Products).ToList();
        return View(model);
    }
}