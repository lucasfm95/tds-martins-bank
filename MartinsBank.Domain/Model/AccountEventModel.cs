using MartinsBank.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MartinsBank.Domain.Model
{
    public class AccountEventModel
    {
        [Required]
        public eEventType Type { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        [Required]
        public double Value { get; set; }
    }
}
