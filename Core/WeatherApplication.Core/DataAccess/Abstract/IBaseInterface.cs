using System;
using System.Linq.Expressions;
using WeatherApplication.Core.Entities.Concrete;

namespace WeatherApplication.Core.DataAccess.Abstract;
public interface IBaseInterface<T> where T : BaseEntity, new()
{
    void AddAsync(T entity);
    void UpdateAsync(T entity);
    void DeleteAsync(T entity);
    T GetById(int id);
    List<T> GetAllAsync(Expression<Func<T, bool>>? filter = null);
}