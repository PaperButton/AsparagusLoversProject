using AsparagusLoversProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AsparagusLoversProject.Domain;
using AsparagusLoversProject.Repositories;
using AsparagusLoversProject.Filters;
using AsparagusLoversProject.ViewModels;

namespace AsparagusLoversProject.Controllers
{
    public class HomeController : Controller
    {
        // private readonly ILoverRepository<AsparagusLover> _loversRepository;
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

        public IActionResult Privacy()
        {
            return View();
        }

        /*public IActionResult NewsFeed()
        {
            // ViewData["AsparagusLovers"] = _asparagusLoversRepository.GetAsparagusLovers();
            var model = _asparagusLoversRepository.GetAsparagusLovers();

            return View(model);
        }*/

        public IActionResult SaveEatingAsparagus()
        {
            GetLoverDataForEatingViewModel inputLoverData = new GetLoverDataForEatingViewModel();
            return View(inputLoverData);
        }

        [PrepareNewLoverForValidating]
        [HttpPost]
        public IActionResult SaveEatingAsparagus(GetLoverDataForEatingViewModel inputLoverData)//(AsparagusLover model)
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