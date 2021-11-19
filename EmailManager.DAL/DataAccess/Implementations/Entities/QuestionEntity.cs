using EmailManager.DAL.DataAccess.Contracts;
using System;

namespace EmailManager.DAL.DataAccess.Implementations.Entities
{
    public class QuestionEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public bool Correct { get; set; }
        public int Complexity { get; set; }
    }
}
