using MartinsBank.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartinsBank.Domain.Model
{
    public class AccountEventResponsePostModel
    {
        public eEventType Type { get; set; }
        public double Value { get; set; }
        public double TotalValue { get; set; }
    }
}
