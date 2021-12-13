using System;

namespace QuestionManager.BLL.Models.Requests
{
    public class RemoveUserRequest
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
    }
}
