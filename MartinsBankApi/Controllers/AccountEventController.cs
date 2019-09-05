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
    [Route( "api/account/{accountId}/[controller]" )]
    [ApiController]
    public class AccountEventController : ControllerBase
    {
        private readonly IAccountEventRepository m_AccountEventRepository;

        public AccountEventController( IAccountEventRepository p_AccountEventRepository )
        {
            m_AccountEventRepository = p_AccountEventRepository;
        }

        /// <summary>
        /// Lista todas as movimentações de uma conta em ordem cronológica
        /// </summary>
        /// <param name="accountId">Id da conta</param>
        /// <param name="year">Filtro para ano</param>
        /// <returns>Lista de movimentações</returns>
        [HttpGet]
        [ProducesResponseType( typeof( List<AccountEventEntity> ), ( int ) HttpStatusCode.OK )]
        public async Task<IActionResult> Get( [FromRoute]int accountId, [FromQuery] int? year )
        {
            if ( year.HasValue )
            {
                return Ok( m_AccountEventRepository.FindAllByAccountAndYear( accountId, year.Value ) );
            }
            else
            {
                return Ok( m_AccountEventRepository.FindAllByAccount( accountId ) );
            }
        }

        /// <summary>
        /// Inseri as movimentações na conta
        /// </summary>
        /// <param name="accountId">Id da conta</param>
        /// <param name="p_AccountEventModel">Movimentação</param>
        /// <returns>Valores atualizados depois da movimentação</returns>
        [HttpPost]
        [ProducesResponseType( typeof( AccountEventResponsePostModel ), ( int ) HttpStatusCode.OK )]
        [ProducesResponseType( typeof( string ), ( int ) HttpStatusCode.BadRequest )]
        public async Task<IActionResult> Post( [FromRoute]int accountId, [FromBody] AccountEventModel p_AccountEventModel )
        {
            string error;
            if ( ! p_AccountEventModel.IsValidModel( out error ) )
            {
                return BadRequest( error );
            }

            AccountEventEntity accountEvent = new AccountEventEntity( p_AccountEventModel, accountId );

            List<AccountEventEntity> accountEventEntities = m_AccountEventRepository.FindAllByAccount( accountId );
            double totalValue = (accountEventEntities.FindAll( ( a ) => a.Type == eEventType.Credit ).Sum( ( b ) => b.Value ) - accountEventEntities.FindAll( ( a ) => a.Type == eEventType.Debt ).Sum( ( b ) => b.Value ));

            if ( p_AccountEventModel.Type == eEventType.Debt && p_AccountEventModel.Value > totalValue )
            {
                return BadRequest( "Valor informado para débito é maior que o saldo" );
            }
            else if ( p_AccountEventModel.Type == eEventType.Debt )
            {
                totalValue = totalValue - p_AccountEventModel.Value;
            }
            else if ( p_AccountEventModel.Type == eEventType.Credit )
            {
                totalValue = totalValue + p_AccountEventModel.Value;
            }

            if ( m_AccountEventRepository.Insert( accountEvent ) )
            {
                return Ok( new AccountEventResponsePostModel( )
                {
                    Type = accountEvent.Type,
                    Value = accountEvent.Value,
                    TotalValue = totalValue
                } );
            }
            else
            {
                return BadRequest( "Falha ao inserir movimentação da conta" );
            }
        }
    }
}