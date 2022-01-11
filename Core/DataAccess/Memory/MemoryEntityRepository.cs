using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Core.DataAccess.Memory
{
    public class MemoryEntityRepository<T> : IEntityRepository<T>
        where T : class, IEntity, new()
    {
        List<T> _entities;
        public MemoryEntityRepository()
        {
            _entities = new List<T>();
        }
        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);
        }

        public T Get(Func<T, bool> filter)
        {
            return _entities.SingleOrDefault(filter);
        }

        public List<T> GetAll(Func<T, bool> filter = null)
        {
            return filter == null ? _entities : _entities.Where(filter).ToList();
        }

        public void Update(T entity)
        {
            int index = _entities.FindIndex(e => e.Id == entity.Id);
            if (index != -1)
            {
                _entities[index] = entity;
            }
        }
    }
}
