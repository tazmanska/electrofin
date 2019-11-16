using System;
using System.Linq;
using System.Linq.Expressions;
using api.Dtos;
using api.Extensions;
using api.Repositories;

namespace api.Services
{
    public abstract class BaseService<TModel> where TModel : BaseDto
    {
        private BaseRepository<TModel> _repository = new BaseRepository<TModel>();

        public BaseRepository<TModel> Repository => _repository;

        public virtual DataListDto<TModel> GetAll(Expression<Func<TModel, bool>> filter = null)
        {
            filter = filter ?? ((x) => true);

            return Repository.All(filter)
                                .OrderBy(x => x.Name)
                                .ToPagedData();
        }

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

        public virtual DataListDto<TModel> FindByTagsOrName(string[] tags, string name)
        {
            return Repository.All(x => (tags == null || tags.Length == 0 || x.Tags == null || x.Tags.Any(t => tags.Contains(t))) || (name == null || x.Name.Contains(name)))
                             .OrderBy(x => x.Name)
                             .ToPagedData();
        }
    }
}