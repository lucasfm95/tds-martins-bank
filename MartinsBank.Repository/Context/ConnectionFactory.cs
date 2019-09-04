using MartinsBank.Repository.Context.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartinsBank.Repository.Context
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string m_ConnectionString;

        public ConnectionFactory( string p_ConnectionString )
        {
            m_ConnectionString = p_ConnectionString;
        }

        /// <summary>
        /// Retorna o mongo client
        /// </summary>
        /// <returns>Mongo client</returns>
        public IMongoClient GetClient( )
        {
            return new MongoClient( m_ConnectionString );
        }

        /// <summary>
        /// Retorna o database do mongoDB de acordo com o client e nome do database informados
        /// </summary>
        /// <param name="p_MongoClient">Client do mongo</param>
        /// <param name="p_DatabaseName">Nome do datebase</param>
        /// <returns>Database do mongo</returns>
        public IMongoDatabase GetDatabase( IMongoClient p_MongoClient, string p_DatabaseName )
        {
            return p_MongoClient.GetDatabase( p_DatabaseName );
        }

        /// <summary>
        /// Retorna o database de acordo com o nome informado
        /// </summary>
        /// <param name="p_DatabaseName">Nome do database</param>
        /// <returns>Database do mongo</returns>
        public IMongoDatabase GetDatabase( string p_DatabaseName )
        {
            IMongoClient client = GetClient( );
            return client.GetDatabase( p_DatabaseName );
        }
    }
}
