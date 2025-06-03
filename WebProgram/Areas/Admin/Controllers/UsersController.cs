using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProgram.Data.Entities.Identity;
using WebProgram.Areas.Admin.Models.Users;

namespace WebProgram.Areas.Admin.Controllers;

[Area("Admin")]
public class UsersController(UserManager<UserEntity> userManager, 
    IMapper mapper) : Controller
{
    public async Task<IActionResult> Index()
    {
        var model = await userManager.Users
            .ProjectTo<UserItemViewModel>(mapper.ConfigurationProvider)
            .ToListAsync();

        return View(model);
    }

}
