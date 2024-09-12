using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IBaseRepository<T,E>
    {
        List<T> FindAll();
        T? FindById(E id);

        T Create(T entity);

        T Update(T entity);

        void Delete(E id);
    }
}
