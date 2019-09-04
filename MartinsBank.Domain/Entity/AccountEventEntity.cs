using MartinsBank.Domain.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartinsBank.Domain.Entity
{
    public enum eEventType : int
    {
        Credit = 1,
        Debt = 2
    } 

    public class AccountEventEntity
    {
        [BsonRepresentation( BsonType.ObjectId )]
        public string Id { get; set; }
        public int AccountId { get; set; }
        public eEventType Type { get; set; }
        public DateTime EventDate { get; set; }
        public double Value { get; set; }

        public AccountEventEntity( )
        {   
        }

        public AccountEventEntity( AccountEventModel accountEventModel, int accountId)
        {
            this.AccountId = accountId;
            this.Type = accountEventModel.Type;
            this.EventDate = accountEventModel.EventDate;
            this.Value = accountEventModel.Value;           
        }
    }
}
