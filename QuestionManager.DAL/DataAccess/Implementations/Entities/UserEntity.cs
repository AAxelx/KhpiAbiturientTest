﻿using QuestionManager.DAL.DataAccess.Contracts;
using System;

namespace QuestionManager.DAL.DataAccess.Implementations.Entities
{
    public class UserEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public int Score { get; set; }
    }
}
