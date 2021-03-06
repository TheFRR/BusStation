using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория
    /// </summary>
    /// <typeparam name="T">Модель данных</typeparam>
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task<int> Add(T item);
        void Update(T item);
        void Delete(int id);
    }
}
