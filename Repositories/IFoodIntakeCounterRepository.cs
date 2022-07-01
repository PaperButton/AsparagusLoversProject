namespace AsparagusLoversProject.Repositories
{
    public interface IFoodIntakeCounterRepository<TEntity, TLover>
    {
        /// <summary>
        /// Get all food intake counter records
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetFoodIntakeCounters();

        /// <summary>
        /// Get one counter record by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetFoodIntakeCounterById(Guid id);

        /// <summary>
        /// Save one food intake counter according to the received model to the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Guid SaveFoodIntakeCounter(Guid loverID);

        /// <summary>
        /// Remove from the database one food intake counter according to the received model
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteFoodIntakeCounter(TLover lover);

       
    }
}
