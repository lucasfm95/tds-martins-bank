using MartinsBank.Domain.Entity;
using System;

namespace MartinsBank.Domain.Model
{
    public class AccountEventModel
    {
        public eEventType Type { get; set; }
        public DateTime EventDate { get; set; }
        public double Value { get; set; }

        public bool IsValidModel( out string messageError )
        {
            if ( Type == 0 )
            {
                messageError = "tipo de movimentação invalido";
                return false;
            }
            else if ( EventDate == null || this.EventDate < DateTime.Now.AddDays( -1 ) )
            {
                messageError = "Data da movimentação invalida";
                return false;
            }
            else if ( this.Value < 0 )
            {
                messageError = "valor da movimentação invalido";
                return false;
            }
            else
            {
                messageError = null;
                return true;
            }
        }
    }
}
