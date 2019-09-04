using System;
using System.Collections.Generic;
using System.Text;

namespace MartinsBank.Repository.Interfaces
{
    public interface IRepository<T>
    {
        List<T> FindAll( );
        bool Insert( T p_Obj );
    }
}
