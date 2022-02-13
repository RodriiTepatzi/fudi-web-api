using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace fudi_web_api.Areas.Api.Services
{
    public interface IBaseRepository<T> where T : class, new()
    {
        Task<string> Get(string record);
        List<T> GetAll();
        T Add(T record);
        bool Update(string id, T values);
        bool Delete(string record);
    }
}
