using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GamePersistance;

namespace TheFipster.Munchkin.Api.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPlayerStore _playerStore;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IPlayerStore playerStore)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _playerStore = playerStore;
        }
        public IActionResult SignInWithGoogle()
        {
            var authenticationProperties = _signInManager.ConfigureExternalAuthenticationProperties("Google", Url.Action(nameof(HandleExternalLogin)));
            return Challenge(authenticationProperties, "Google");
        }

        public async Task<IActionResult> HandleExternalLogin()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

            if (!result.Succeeded) //user does not exist yet
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var newUser = await createUser(email);
                createPlayer(info.Principal, newUser);
                await addClaims(info, newUser);
                await _signInManager.SignInAsync(newUser, isPersistent: false);
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            }

            return Redirect("http://localhost:4200");
        }

        private void createPlayer(ClaimsPrincipal identity, IdentityUser newUser)
        {
            var player = new GameMaster
            {
                Email = newUser.Email,
                Id = Guid.Parse(newUser.Id),
                Name = identity.FindFirstValue(ClaimTypes.GivenName)
            };

            _playerStore.Add(player);
        }

        private async Task addClaims(ExternalLoginInfo info, IdentityUser newUser)
        {
            await _userManager.AddLoginAsync(newUser, info);
            var newUserClaims = info.Principal.Claims.Append(new Claim("userId", newUser.Id));
            await _userManager.AddClaimsAsync(newUser, newUserClaims);
        }

        private async Task<IdentityUser> createUser(string email)
        {
            var newUser = new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };
            var createResult = await _userManager.CreateAsync(newUser);
            if (!createResult.Succeeded)
                throw new Exception(createResult.Errors.Select(e => e.Description).Aggregate((errors, error) => $"{errors}, {error}"));
            return newUser;
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("http://localhost:4200");
        }
    }
}