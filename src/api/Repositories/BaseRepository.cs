using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using api.Dtos;
using LiteDB;

namespace api.Repositories
{
    public class BaseRepository<TModel> where TModel : BaseDto
    {
        private readonly string _connectionString = ".\\db\\database.db";

        public TModel Add(TModel model)
        {
            using (var db = new LiteRepository(_connectionString))
            {
                db.Insert(model);
            }

            AddTags(model.Tags);

            return model;
        }

        public IEnumerable<TModel> All(Expression<Func<TModel, bool>> filter = null)
        {
            return AllRecords(filter ?? (x => true));
        }

        private IEnumerable<TModel> AllRecords(Expression<Func<TModel, bool>> predicate)
        {
            using (var db = new LiteRepository(_connectionString))
            {              
                return db.Fetch(predicate).Where(x => !x.Removed);
            }
        }

        public TModel GetById(int id)
        {
            using (var db = new LiteRepository(_connectionString))
            {
                return db.FirstOrDefault<TModel>(x => x.Id == id && !x.Removed);
            }
        }

        public bool Update(TModel model)
        {
            using (var db = new LiteRepository(_connectionString))
            {
                return db.Update(model);
            }
        }

        public bool Remove(int id)
        {
            using (var db = new LiteRepository(_connectionString))
            {
                var entity = db.FirstOrDefault<TModel>(x => x.Id == id && !x.Removed);
                if (entity == null)
                {
                    return false;
                }

                entity.Removed = true;
                return db.Update(entity);
            }
        }

        private void AddTags(string[] tags)
        {
            var tagsDtos = tags?.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => new TagDto() { Name = x, NormalizedName = x.ToLowerInvariant() });
            if (tagsDtos != null && tagsDtos.Any())
            {
                AddTagIfNotExists(tagsDtos);
            }
        }

        private void AddTagIfNotExists(IEnumerable<TagDto> tags)
        {
            using (var db = new LiteRepository(_connectionString))
            {
                foreach (var tag in tags)
                {
                    var exists = db.FirstOrDefault<TagDto>(x => x.NormalizedName == tag.NormalizedName);
                    if (exists == null)
                    {
                        db.Insert<TagDto>(tag);
                    }
                }                
            }
        }
    }
}