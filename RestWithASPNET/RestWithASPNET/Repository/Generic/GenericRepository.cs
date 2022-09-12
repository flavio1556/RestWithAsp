using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Models.Base;
using RestWithASPNET.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNET.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MySQLContext _context;
        protected DbSet<T> _dataSet;
        public GenericRepository(MySQLContext context)
        {
            _context = context;
            _dataSet = _context.Set<T>();
        }
        public T FindById(long id)
        {
            try
            {
                return _dataSet.FirstOrDefault(x => x.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<T> FindAll()
        {
            try
            {
                return _dataSet.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public T Create(T item)
        {
            try
            {
                _dataSet.Add(item);
                save();
                return item;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public T Update(T item)
        {
            if(!Exists(item.Id)) return null;
            try
            {
                var result = FindById(item.Id);
                _dataSet.Update(result).CurrentValues.SetValues(item);
                save();
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Delete(long id)
        {
            if (Exists(id))
            {
                try
                {
                    var result = FindById(id);

                    _dataSet.Remove(result);
                    save();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            
        }

        public bool Exists(long id) => _dataSet.Any(x => x.Id.Equals(id));

        protected void save() => _context.SaveChanges();

        public List<T> FindWithPagedSearch(string query)
        {
            return _dataSet.FromSqlRaw<T>(query).ToList();
        }

        public int GetCount(string query)
        {
            var result = "";
            using  (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    result = command.ExecuteScalar().ToString();
                }
            }
           return int.Parse(result);
        }
    }
}
