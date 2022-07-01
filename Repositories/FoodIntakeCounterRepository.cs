using AsparagusLoversProject.Domain;

namespace AsparagusLoversProject.Repositories
{
    public class FoodIntakeCounterRepository: IFoodIntakeCounterRepository<IFoodIntakeCounter, ILover> 
    {
        private readonly AppDbContext _appDbContext;

        /// <summary>
        /// This variable is needed in order to use a class that implements the IFoodIntakeCounter interface when saving or deleting an entry when accessing the context.
        /// </summary>
        private IFoodIntakeCounter _foodIntakeCounter;

        public FoodIntakeCounterRepository(AppDbContext appDbContext, IFoodIntakeCounter foodIntakeCounter)
        {
            _appDbContext = appDbContext;
            _foodIntakeCounter = foodIntakeCounter;
        }

       

        public void DeleteFoodIntakeCounter(ILover lover)
        {
           _appDbContext.FoodIntakeCounters.Remove(_appDbContext.FoodIntakeCounters.Single(x => x.LoverID == lover.LoverID));
            _appDbContext.Lovers.Remove(_appDbContext.Lovers.Single(x=>x.LoverID == lover.LoverID)); 
            _appDbContext.SaveChanges();
        }

        public IFoodIntakeCounter GetFoodIntakeCounterById(Guid id)
        {
            return _appDbContext.FoodIntakeCounters.Single(x => x.LoverID == id);
        }

        public IQueryable<IFoodIntakeCounter> GetFoodIntakeCounters()
        {
            return _appDbContext.FoodIntakeCounters.OrderByDescending(x => x.LastFoodEatenDateTime);
        }

        public Guid SaveFoodIntakeCounter(Guid loverID)
        {
            _foodIntakeCounter.LoverID = loverID;
            if (_appDbContext.FoodIntakeCounters.Where(x => x.LoverID == loverID).ToList().Count == 0)
            {
                _foodIntakeCounter.NumberOfMealsOfFood = 1;
                _foodIntakeCounter.RecordId = Guid.NewGuid();
                _appDbContext.Entry(_foodIntakeCounter).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            }
            else
            {
                _foodIntakeCounter = _appDbContext.FoodIntakeCounters.Single(x => x.LoverID == loverID);
                _foodIntakeCounter.NumberOfMealsOfFood += 1;//this is the counter of asparagus eaten        
                _appDbContext.Entry(_foodIntakeCounter).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            _foodIntakeCounter.LastFoodEatenDateTime = DateTime.Now;
            _appDbContext.SaveChanges();

            return _foodIntakeCounter.LoverID;
        }
    }
}
