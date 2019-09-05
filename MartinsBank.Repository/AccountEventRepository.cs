using MartinsBank.Domain.Entity;
using MartinsBank.Repository.Base;
using MartinsBank.Repository.Context.Interfaces;
using MartinsBank.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartinsBank.Repository
{
    public class AccountEventRepository : Repository<AccountEventEntity>, IAccountEventRepository
    {
        private readonly ILogger<Repository<AccountEventEntity>> m_logger;
        public AccountEventRepository( IConnectionFactory p_ConnectionFactory, ILogger<Repository<AccountEventEntity>> p_logger ) : base( p_ConnectionFactory, "atlas-dsv-ticketing", "AccountEvent" )
        {
            m_logger = p_logger;
        }

        public List<AccountEventEntity> FindAllByAccount( int p_AccountId )
        {
            try
            {
                return GetCollection( ).FindAsync( ( a ) => a.AccountId == p_AccountId ).Result.ToList( ).OrderBy( ( b => b.EventDate ) ).ToList( );
            }
            catch ( Exception ex)
            {
                string messageError = $"Error ao buscar movimentações da conta com id {p_AccountId}";
                m_logger.LogError( ex, messageError );
                throw new Exception( messageError );
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
                string messageError = $"Error ao buscar movimentações da conta com id {p_AccountId} e ano {p_Year}";
                m_logger.LogError( ex, messageError );
                throw new Exception( messageError );
            }

        }
    }
}
