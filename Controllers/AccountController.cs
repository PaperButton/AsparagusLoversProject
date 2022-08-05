using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AsparagusLoversProject.ViewModels;
using AsparagusLoversProject.Models;
using Microsoft.AspNetCore.Identity;
using AsparagusLoversProject.Domain;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AsparagusLoversProject.Repositories;

namespace AsparagusLoversProject.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        private readonly AsparagusLoverRepository<ILover> _loversRepository;
        private readonly IFoodIntakeCounterRepository<IFoodIntakeCounter, ILover> _foodIntakeCounterRepository;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
                                IConfiguration configuration, AsparagusLoverRepository<ILover> loversRepository, 
                                IFoodIntakeCounterRepository<IFoodIntakeCounter, ILover> foodIntakeCounterRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;

            _loversRepository = loversRepository;
            _foodIntakeCounterRepository = foodIntakeCounterRepository;
        }
        [HttpGet]
        public IActionResult RegisterVK()
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallBackVK), "Account", "/Home/Index");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("VK", redirectUrl);
            return Challenge(properties, "VK");
            return View();
        }
       /* [HttpPost]
        public async Task<IActionResult> RegisterVK(RegisterViewModel model)
        {
            


            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser {UserName=model.Email, FirstName="Череповец", LastName="Васицкий" };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);

                    var accessTokenUrl = VkHelpers.GetAccessTokenUrl(_configuration["Authentication:VK:AppId"], _configuration["Authentication:VK:AppSecret"]);
                    var responseStrAccessToken = VkHelpers.GetRequest(accessTokenUrl);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }*/


        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallBackVK()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            string access_token = info.AuthenticationTokens.First(tokens=> tokens.Name == "access_token").Value;
            string resUrl = VkHelpers.GetVKUserNameDataUrl(info.Principal.FindFirstValue(ClaimTypes.NameIdentifier).ToString(), access_token);
            var res =  VkHelpers.GetRequest(resUrl);
            var deserializedVKUserNameData = Newtonsoft.Json.JsonConvert.DeserializeObject<VKUserNameData>(res);

            if (info == null)
            {
                return RedirectToAction("Index", "Home");
            }

            GetLoverDataForEatingViewModel inputLoverData = new GetLoverDataForEatingViewModel();
            inputLoverData.LoverFname = System.Convert.ToString(deserializedVKUserNameData.VKUserNameDataItems.Single().UserName);
            inputLoverData.ProviderId = 2;
            inputLoverData.ExternalId = System.Convert.ToString(deserializedVKUserNameData.VKUserNameDataItems.Single().UserId);

            Guid loverId = _loversRepository.SaveLover(inputLoverData);
            _foodIntakeCounterRepository.SaveFoodIntakeCounter(loverId);
            return RedirectToAction("", "NewsFeed");
        }
    }
}