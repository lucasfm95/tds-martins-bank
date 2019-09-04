using MartinsBank.Domain.Entity;
using MartinsBank.Repository.Base;
using MartinsBank.Repository.Context.Interfaces;
using MartinsBank.Repository.Interfaces;
using MongoDB.Driver;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartinsBank.Repository
{
    public class AccountEventRepository : Repository<AccountEventEntity>, IAccountEventRepository
    {
        public AccountEventRepository( IConnectionFactory p_ConnectionFactory ) : base( p_ConnectionFactory, "atlas-dsv-ticketing", "AccountEvent" )
        {

        }

        public List<AccountEventEntity> FindAllByAccount( int p_AccountId )
        {
            try
            {
                return GetCollection( ).FindAsync( ( a ) => a.AccountId == p_AccountId ).Result.ToList( ).OrderBy( ( b => b.EventDate ) ).ToList( );
            }
            catch ( Exception ex)
            {

                throw;
            }
        }

        public List<AccountEventEntity> FindAllByAccountAndYear( int p_AccountId, int p_Year )
        {
            try
            {
                //return GetCollection( ).FindAsync( ( a ) => a.AccountId == p_AccountId && a.EventDate.Date.Year == p_Year ).Result.ToList( ).OrderBy( ( b => b.EventDate ) ).ToList( );
                return GetCollection( ).FindAsync( ( a ) => a.AccountId == p_AccountId ).Result.ToList( ).Where( a => a.EventDate.Date.Year == p_Year ).OrderBy( ( b => b.EventDate ) ).ToList( );
            }
            catch ( Exception ex )
            {

                throw;
            }

        }
    }
}
