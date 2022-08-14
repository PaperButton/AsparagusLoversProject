using AsparagusLoversProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AsparagusLoversProject.Domain;
using AsparagusLoversProject.Repositories;
using AsparagusLoversProject.Filters;
using AsparagusLoversProject.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace AsparagusLoversProject.Controllers
{
    public class HomeController : Controller
    {
   
        private readonly AsparagusLoverRepository<ILover> _loversRepository;
        private readonly IFoodIntakeCounterRepository<IFoodIntakeCounter,ILover> _foodIntakeCounterRepository;

        public HomeController(AsparagusLoverRepository<ILover> loversRepository, IFoodIntakeCounterRepository<IFoodIntakeCounter, ILover> foodIntakeCounterRepository)
        {
            _loversRepository = loversRepository;
            _foodIntakeCounterRepository = foodIntakeCounterRepository;
        }
      
        public IActionResult Index()
        {
            return View();
        }

        

        public IActionResult SetNewUserVK()
        {
            return RedirectToAction("RegisterVK", "Account");
        }

  

        public IActionResult SaveEatingAsparagus()
        {
            GetLoverDataForEatingViewModel inputLoverData = new GetLoverDataForEatingViewModel();
            inputLoverData.ProviderId = 1;
            return View(inputLoverData);
        }

        [PrepareNewLoverForValidating]
        [HttpPost]
        public IActionResult SaveEatingAsparagus(GetLoverDataForEatingViewModel inputLoverData)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            Guid loverId = _loversRepository.SaveLover(inputLoverData);
            _foodIntakeCounterRepository.SaveFoodIntakeCounter(loverId);
            return RedirectToAction("","NewsFeed");
            
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}