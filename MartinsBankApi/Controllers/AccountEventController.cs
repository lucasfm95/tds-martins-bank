using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MartinsBank.Domain.Entity;
using MartinsBank.Domain.Model;
using MartinsBank.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MartinsBankApi.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class AccountEventController : ControllerBase
    {
        private readonly IAccountEventRepository m_AccountEventRepository;

        public AccountEventController( IAccountEventRepository p_AccountEventRepository )
        {
            m_AccountEventRepository = p_AccountEventRepository;
        }

        [HttpGet( "account/{accountId}" )]
        [ProducesResponseType( typeof( List<AccountEventEntity> ), ( int ) HttpStatusCode.OK )]
        public async Task<IActionResult> Get( [FromRoute]int accountId )
        {
            return Ok( m_AccountEventRepository.FindAll( ).FindAll( ( a ) => a.AccountId == accountId ) );
        }

        /// <summary>
        /// Inseri as movimentações na conta
        /// </summary>
        /// <param name="accountId">Id da conta</param>
        /// <param name="p_AccountEventModel">Movimentação</param>
        /// <returns>Valores atualizados depois da movimentação</returns>
        [HttpPost( "account/{accountId}" )]
        [ProducesResponseType( typeof( AccountEventResponsePostModel ), ( int ) HttpStatusCode.OK )]
        public async Task<IActionResult> Post( [FromRoute]int accountId, [FromBody] AccountEventModel p_AccountEventModel )
        {
            AccountEventEntity accountEvent = new AccountEventEntity( p_AccountEventModel, accountId );

            if ( m_AccountEventRepository.Insert( accountEvent ) )
            {
                List<AccountEventEntity> accountEventEntities = m_AccountEventRepository.FindAll( );
                double totalValue = accountEventEntities.FindAll( ( a ) => a.Type == accountEvent.Type && a.AccountId == accountId ).Sum( ( b ) => b.Value );

                return Ok( new AccountEventResponsePostModel( )
                {
                    Type = accountEvent.Type,
                    Value = accountEvent.Value,
                    TotalValue = totalValue
                } );
            }
            else
            {
                return BadRequest( );
            }
        }
    }
}