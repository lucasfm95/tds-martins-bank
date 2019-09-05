using MartinsBank.Repository.Context.Interfaces;
using MartinsBank.Repository.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MartinsBank.Repository.Base
{
    public abstract class Repository<T> : IRepository<T>
    {
        private readonly IMongoCollection<T> m_Collection;

        public Repository( IConnectionFactory p_ConnectionFactory, string p_DatabaseName, string p_CollectionName )
        {
            m_Collection = p_ConnectionFactory.GetDatabase( p_DatabaseName ).GetCollection<T>( p_CollectionName );
        }

        /// <summary>
        /// Retorna todos os documentos da collection
        /// </summary>
        /// <returns>Lista de objetos</returns>
        public List<T> FindAll( )
        {
            try
            {
                return m_Collection.AsQueryable<T>( ).ToList( );
            }
            catch ( Exception ex)
            {
                throw;
            }
            
        }

        /// <summary>
        /// Retorna a collection
        /// </summary>
        /// <returns>Mongo collection</returns>
        protected IMongoCollection<T> GetCollection( )
        {
            return m_Collection;
        }

        /// <summary>
        /// Insere um novo objeto
        /// </summary>
        /// <param name="p_Obj">Objeto</param>
        public bool Insert( T p_Obj )
        {
            try
            {
                m_Collection.InsertOne( p_Obj );
                return true;
            }
            catch ( Exception ex)
            {
                throw new Exception("", ex);
            }

        }
    }
}
