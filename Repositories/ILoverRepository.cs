

using AsparagusLoversProject.ViewModels;

namespace AsparagusLoversProject.Repositories
{
    /// <typeparam name="TEntity">It accepts Lover class, not a repository class</typeparam>
    public interface ILoverRepository<TEntity>

    {
        /// <summary>
        /// Get all lovers of something of the same type
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetLovers();

        /// <summary>
        /// Get one lover by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetLoverById(Guid id);

        /// <summary>
        /// Save one lover of something according to the received viewModel to the database
        /// </summary>
        /// <param name="inputLoverData"></param>
        /// <returns></returns>
        public Guid SaveLover(GetLoverDataForEatingViewModel inputLoverData);

        /// <summary>
        /// Remove from the database one lover of something according to the received model
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteLover(TEntity entity);
    }
}
