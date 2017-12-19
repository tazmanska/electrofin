using System;
using System.Collections.Generic;
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

            return model;
        }

        public IEnumerable<TModel> All(Expression<Func<TModel, bool>> predicate = null)
        {
            using (var db = new LiteRepository(_connectionString))
            {
                if (predicate != null)
                {
                    return db.Fetch(predicate);
                }

                return db.Fetch<TModel>();
            }
        }

        public TModel GetById(int id)
        {
            using (var db = new LiteRepository(_connectionString))
            {
                return db.FirstOrDefault<TModel>(x => x.Id == id);
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
                var entity = db.FirstOrDefault<TModel>(x => x.Id == id);
                if (entity == null)
                {
                    return false;
                }

                entity.Removed = true;
                return db.Update(entity);
            }
        }
    }
}