﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Solitaire.DataAccess.Models;
using Solitaire.DataAccess.Repositories.IRepositories;
using Solitaire.DataAccess.Services.IServices;
using Solitaire.ViewModels;

namespace Solitaire.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(
            ILogger<AccountController> logger,
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Authenticate([FromBody]AuthRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Model not valid.");

                var user = await _userManager.FindByNameAsync(model.UserName)
                    ?? throw new UnauthorizedAccessException("Username or password wrong!");

                var isValidPwd = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!isValidPwd)
                    throw new UnauthorizedAccessException("Username or password wrong!");

                var accessToken = _tokenService.CreateToken(user);
                await _unitOfWork.SaveAsync();

                return Ok(new AuthResponse
                {
                    Username = user.UserName ?? "",
                    Email = user.Email ?? "",
                    Base64String = "data:image/svg+xml;base64," + user.Base64Picture,
                    Token = accessToken
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Model not valid.");

                if (!request.Password.Equals(request.PasswordConfirm))
                    throw new Exception("Passwords do not match.");

                var userExists = await _userManager.FindByNameAsync(request.UserName);
                if (userExists is not null)
                    throw new Exception("Username already in use.");

                var base64Picture = await GetProfilePicture(request.UserName);

                var newUser = new ApplicationUser()
                {
                    UserName = request.UserName,
                    Base64Picture = base64Picture,
                    SolitaireSessionId = null
                };

                var result = await _userManager.CreateAsync(newUser, request.Password);
                if (!result.Succeeded)
                    throw new Exception("An error occured.");

                return Ok($"User \"{request.UserName}\" successfully created.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("check")]
        public async Task<IActionResult> CheckIfUserNameIsValid(string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);

                if (user is null)
                    return Ok("Username valid.");
                else
                    return BadRequest("Username already in use.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        private static string ConvertToBase64(Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }

        private async Task<string> GetProfilePicture(string userName)
        {
            string base64String = "";

            using (HttpClient client = new HttpClient())
            {
                string requestURI = "https://source.boringavatars.com/beam/120/" + userName;

                var stream = await client.GetStreamAsync(requestURI);
                base64String = ConvertToBase64(stream);
            }

            return base64String;
        }
    }
}
