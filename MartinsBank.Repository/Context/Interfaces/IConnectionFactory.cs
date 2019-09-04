using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartinsBank.Repository.Context.Interfaces
{
    public interface IConnectionFactory
    {
        IMongoClient GetClient( );
        IMongoDatabase GetDatabase( IMongoClient p_MongoClient, string p_DatabaseName );
        IMongoDatabase GetDatabase( string p_DatabaseName );
    }
}
