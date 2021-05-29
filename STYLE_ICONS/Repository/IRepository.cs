using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T GetByID(int Id);
        List<T> GetByFk(string fk);
        void Create(T t);
        void Delete(int Id);
        void Update(T t);
       

    }
}
