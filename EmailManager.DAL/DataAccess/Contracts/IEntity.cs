using System;

namespace EmailManager.DAL.DataAccess.Contracts
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}
