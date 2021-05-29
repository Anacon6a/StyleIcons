using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly UserManager<StoreUser> _userManager;
        private readonly SignInManager<StoreUser> _signInManager;
    

        public AccountController(UserManager<StoreUser> userManager, SignInManager<StoreUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //[HttpGet("{id}")]
        //[Route("api/Account/order")]
        //public async Task<IActionResult> GetStoreUser([FromRoute] string id)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var user = _userManager
        //        .(x => x.Idcategory == id)
        //               .Include("Product")
        //               .ToListAsync(); 

        //    var user = await _context.StoreUser.SingleOrDefaultAsync(x => x.Id == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(user);
        //}

        [HttpPost]
        [Route("api/Account/Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                StoreUser user = new StoreUser {  FirstName = model.FirstName, LastName = model.LastName, MiddleName = model.MiddleName, PhoneNumber = "7" + model.PhoneNumber, Email = model.Email, UserName = "7" + model.PhoneNumber};
                // Добавление нового пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "user");
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    var msg = new
                    {
                        message = "Регистрация прошла успешна. Добро пожаловать, " + user.FirstName + " " + user.MiddleName
                    };
                    return Ok(msg);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    var errorMsg = new
                    {
                        error = "Пользователь не зарегистрирован",
                        allerror = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                    };
                    return Ok(errorMsg);
                }
            }
            else
            {
                var errorMsg = new
                {
                    error = "Пользователь не зарегистрирован",
                    allerror = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                };
                return Ok(errorMsg);
            }
        }

        [HttpPost]
        [Route("api/Account/Login")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                StoreUser user = await _userManager.FindByNameAsync(model.Login);
                if (user == null) user = await _userManager.FindByEmailAsync(model.Login);

                if (user == null)
                {
                    ModelState.AddModelError("", "Пользователь с данным логином не найден");
                    var errorMsg = new
                    {
                        error = "Вход не выполнен",
                        allerror = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                    };
                    return Ok(errorMsg);
                }
                else
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        var msg = new
                        {
                            message = "Добро пожаловать,<br>" + user.FirstName + " " + user.MiddleName
                        };
                        return Ok(msg);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Неправильный пароль");
                        var errorMsg = new
                        {
                            error = "Вход не выполнен",
                            allerror = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                        };
                        return Ok(errorMsg);
                    }
                }
            }
            else
            {
                var errorMsg = new
                {
                    error = "Вход не выполнен",
                    allerror = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                };
                return Ok(errorMsg);
            }

        }

        [HttpPost]
        [Route("api/account/logoff")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            // Удаление куки
            await _signInManager.SignOutAsync();
            var msg = new
            {
                message = "Выполнен выход."
            };
            return Ok(msg);
        }

        [HttpPost]
        [Route("api/Account/isAuthenticated")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LogisAuthenticatedOff()
        {
            StoreUser usr = await GetCurrentUserAsync();
            var type = usr == null ? null : await _userManager.GetRolesAsync(usr);
            var name = usr != null ? usr.FirstName + " " + usr.MiddleName : "";
            var idu = usr != null ? usr.Id : "";
            var message = usr == null ? "Вы Гость. Пожалуйста, выполните вход." : "Рады вас видеть, " + name;
            var msg = new
            {
                message,
                type,
                name,
                idu
            };
            return Ok(msg);

        }
       
       
        private Task<StoreUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        
    }
}