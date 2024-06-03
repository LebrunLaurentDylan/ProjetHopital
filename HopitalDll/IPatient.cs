using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopitalData;

namespace HopitalDll
{
    public interface IPatient
    {
        void AddPatient(Patients patient);
        Patients GetNextPatient();
        void EntrerPatient(IObserver observer);
        void SortirPatient(IObserver observer);
        void NotifyPatient();
    }
}
