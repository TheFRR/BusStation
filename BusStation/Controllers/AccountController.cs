using BusStation.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusStation.Controllers
{
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(UserManager<User> userManager,
        SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.logger = logger;
        }

        [HttpPost]
        [Route("api/Account/Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            logger.LogInformation("Вызов post запроса api/Account/Register"); 
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = model.Email,
                    UserName = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "user");
                    await _signInManager.SignInAsync(user, false);
                    var msg = new
                    {
                        message = "Добавлен новый пользователь: " + user.UserName
                    };
                    logger.LogInformation(msg.message);
                    return Ok(msg);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty,
                        error.Description);
                    }
                    var errorMsg = new
                    {
                        message = "Пользователь не добавлен.",
                        error = ModelState.Values.SelectMany(e =>
                        e.Errors.Select(er => er.ErrorMessage))
                    };
                    logger.LogError(errorMsg.message);
                    return BadRequest(errorMsg);
                }
            }
            else
            {
                var errorMsg = new
                {
                    message = "Неверные входные данные.",
                    error = ModelState.Values.SelectMany(e =>
                    e.Errors.Select(er => er.ErrorMessage))
                };
                logger.LogError(errorMsg.message);
                return BadRequest(errorMsg);
            }
        }

        [HttpPost]
        [Route("api/Account/Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            logger.LogInformation("Вызов post запроса api/Account/Login");
            if (ModelState.IsValid)
            {
                var result =
                await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var msg = new
                    {
                        message = "Здравствуйте,  " +
                        model.Email + "!"
                    };
                    logger.LogInformation(msg.message);
                    return Ok(msg);
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин/пароль");
                    var errorMsg = new
                    {
                        message = "Здравствуйте, гость!",
                        error = ModelState.Values.SelectMany(e =>
                        e.Errors.Select(er => er.ErrorMessage))
                    };
                    logger.LogError(errorMsg.message);
                    return BadRequest(errorMsg);
                }
            }
            else
            {
                var errorMsg = new
                {
                    message = "Вход не выполнен.",
                    error = ModelState.Values.SelectMany(e =>
                    e.Errors.Select(er => er.ErrorMessage))
                };
                logger.LogError(errorMsg.message);
                return BadRequest(errorMsg);
            }
        }

        [HttpPost]
        [Route("api/Account/LogOff")]
        public async Task<IActionResult> LogOff()
        {
            logger.LogInformation("Вызов post запроса api/Account/LogOff");
            await _signInManager.SignOutAsync();
            var msg = new
            {
                message = "Вы вышли из аккаунта."
            };
            logger.LogInformation(msg.message);
            return Ok(msg);
        }

        [HttpPost]
        [Route("api/Account/isAuthenticated")]
        public async Task<IActionResult> LogisAuthenticatedOff()
        {
            logger.LogInformation("Вызов post запроса api/Account/isAuthenticated");
            User usr = await GetCurrentUserAsync();
            var message = usr == null ? "Здравствуйте, гость!" : "Здравствуйте, " + usr.UserName + "!";
            var msg = new
            {
                message
            };
            logger.LogInformation(msg.message);
            return Ok(msg);
        }

        public Task<User> GetCurrentUserAsync()
        {
            var user = _userManager.GetUserAsync(HttpContext.User);
            return user;
        }

        [HttpPost]
        [Route("api/Account/currentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            logger.LogInformation("Вызов post запроса api/Account/currentUser");
            User user = await GetCurrentUserAsync();
            if (user != null)
            {
                logger.LogInformation("OK");
                return Ok(user);
            }
            else
            {
                logger.LogError("BadRequest");
                return BadRequest();
            }
        }
    }
}

