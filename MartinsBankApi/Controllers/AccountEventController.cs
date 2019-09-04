using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MartinsBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountEventController : ControllerBase
    {
        
        [HttpPost( "account/{accountId}/credit" )]
        public IActionResult PostCredit( [FromRoute]int accountId )
        {
            return Ok( );
        }

        [HttpPost( "account/{accountId}/debt" )]
        public IActionResult PostDebt( [FromRoute]int accountId )
        {
            return Ok( );
        }
    }
}