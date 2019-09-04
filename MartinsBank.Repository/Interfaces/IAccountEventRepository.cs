using MartinsBank.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartinsBank.Repository.Interfaces
{
    public interface IAccountEventRepository
    {
        List<AccountEventEntity> FindAll( );
        bool Insert( AccountEventEntity p_Obj );
    }
}
