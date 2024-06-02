using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopitalData;

namespace HopitalDll
{
    interface IHopital<T,PK>
    {
        List<T> FindAll();
        T FindById(PK id);
        void Create(T obj);
        void Update(T obj);
        void Delete(PK id);
    }

    interface IHopitalVisites : IHopital<Visites,int>
    {
        List<Visites> FindByPatient(int idPatient);
    }

    interface IhopitalPatients : IHopital<Patients, int>
    {

    }
    interface IhopitalAuth : IHopital<Authentification, int>
    {

    }
}
