using api.Dtos;
using api.Repositories;

namespace api.Services
{
    public abstract class BaseService<TModel> where TModel : BaseDto
    {
        private BaseRepository<TModel> _repository = new BaseRepository<TModel>();

        public BaseRepository<TModel> Repository => _repository;

        public virtual TModel Get(int id)
        {
            return Repository.GetById(id);
        }

        public virtual TModel Create(TModel model)
        {
            return Repository.Add(model);
        }

        public virtual bool Remove(int id)
        {
            return Repository.Remove(id);
        }

        public virtual bool Update(TModel model)
        {
            return Repository.Update(model);
        }
    }
}