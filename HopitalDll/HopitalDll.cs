using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopitalDll
{
    interface IHopitalDataBase<T,PK>
    {
        List<T> FindAll();
        T FindById(PK id);
        void Create(T obj);
        void Update(T obj);
        void Delete(PK id);
    }
    interface IHopitalTableCreation
    {

    }
}
