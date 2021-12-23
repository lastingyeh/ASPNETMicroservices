using System.Threading.Tasks;
using Identity.API.Models;
using Identity.API.Services;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IRedirectService _redirectService;
        public HomeController(IIdentityServerInteractionService interaction, IRedirectService redirectService)
        {
            _redirectService = redirectService;
            _interaction = interaction;
        }

        public IActionResult Index(string returnUrl)
        {
            return View();
        }

        public IActionResult ReturnToOriginalApplication(string returnUrl)
        {
            if (returnUrl != null)
            {
                return Redirect(_redirectService.ExtractRedirectUriFromReturnUrl(returnUrl));
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();
            var message = await _interaction.GetErrorContextAsync(errorId);

            if (message != null)
            {
                vm.Error = message;
            }

            return View("Error", vm);
        }
    }
}