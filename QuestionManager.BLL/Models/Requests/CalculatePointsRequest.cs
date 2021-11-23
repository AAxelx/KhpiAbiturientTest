using System.Collections.Generic;

namespace QuestionManager.BLL.Models.Requests
{
    public class CalculatePointsRequest
    {
        public List<AnswearModel> Questions { get; set; }
        public string Email { get; set; }
    }
}
