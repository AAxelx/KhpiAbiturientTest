using System;

namespace QuestionManager.DAL.DataAccess.Contracts
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}
