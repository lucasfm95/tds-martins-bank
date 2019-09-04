using MartinsBank.Domain.Entity;
using MartinsBank.Repository.Base;
using MartinsBank.Repository.Context.Interfaces;
using MartinsBank.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartinsBank.Repository
{
    public class AccountEventRepository : Repository<AccountEventEntity> , IAccountEventRepository
    {
        public AccountEventRepository( IConnectionFactory p_ConnectionFactory ) : base( p_ConnectionFactory, "atlas-dsv-ticketing", "AccountEvent")
        {

        }
    }
}
