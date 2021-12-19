using QuestionManager.DAL.DataAccess.Contracts;
using System;

namespace QuestionManager.DAL.DataAccess.Implementations.Entities
{
    public class QuestionEntity
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answear { get; set; }
        public int Complexity { get; set; }
        public string SecondOption { get; set; }
        public string ThirdOption { get; set; }
    }
}
