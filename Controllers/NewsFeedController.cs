using AsparagusLoversProject.Domain;
using AsparagusLoversProject.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AsparagusLoversProject.Controllers
{
    public class NewsFeedController : Controller
    {
        private readonly AsparagusLoverRepository<ILover> _asparagusLoversRepository;
        private readonly IFoodIntakeCounterRepository<IFoodIntakeCounter,ILover> _foodIntakeCounterRepository;

        public NewsFeedController(AsparagusLoverRepository<ILover>  asparagusLoversRepository, IFoodIntakeCounterRepository<IFoodIntakeCounter, ILover> foodIntakeCounterRepository)
        {
            _asparagusLoversRepository = asparagusLoversRepository;
            _foodIntakeCounterRepository = foodIntakeCounterRepository;
        }
      
        public IActionResult ShowNewsFeed()
        {
            ViewBag.Lovers = _asparagusLoversRepository.GetLovers();
            ViewBag.FoodIntakeCounters = _foodIntakeCounterRepository.GetFoodIntakeCounters();
            return View();
        }
    }
}
