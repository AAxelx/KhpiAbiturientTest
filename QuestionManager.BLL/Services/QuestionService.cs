using QuestionManager.BLL.Models;
using QuestionManager.BLL.Models.Responses;
using QuestionManager.BLL.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuestionManager.BLL.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUserService _userService;
        private readonly IGoogleSheetsService _sheetsService;
        private readonly IMessageService _messageService;
        private readonly ILetterService _letterService;

        public QuestionService(IUserService userService, IGoogleSheetsService sheetsService, IMessageService messageService, ILetterService letterService)
        {
            _userService = userService;
            _sheetsService = sheetsService;
            _messageService = messageService;
            _letterService = letterService;
        }

        public GetAllQuestionsResponse GetAll()
        {
            var questions = _sheetsService.GetAllQuestions();

            if (questions != null)
            {
                var random = new Random();
                var numbersList = new List<int>();
                var countEasyQuestions = 0;
                var countMediumQuestions = 0;
                var countHardQuestions = 0;
                var result = new List<QuestionModel>();

                do
                {
                    var count = questions.Count();
                    var questionNumber = random.Next(count);

                    if (!numbersList.Contains(questionNumber))
                    {
                        var question = questions[questionNumber];
                        var complexity = int.Parse((string)question[2]);

                        if (complexity == 1 && countEasyQuestions != 4)
                            countEasyQuestions++;
                        else if (complexity == 2 && countMediumQuestions != 4)
                        {
                            countMediumQuestions++;

                            var temp = question[1];
                            question[1] = question[3];
                            question[3] = question[4];
                            question[4] = temp;
                        }
                        else if (complexity == 3 && countHardQuestions != 4)
                        {
                            countHardQuestions++;

                            var temp = question[1];
                            question[1] = question[4];
                            question[4] = question[3];
                            question[3] = temp;
                        }
                        else
                            continue;

                        numbersList.Add(questionNumber);
                        result.Add(new QuestionModel()
                        {
                            Question = (string)question[0],
                            FirstOption = (string)question[1],
                            SecondOption = (string)question[3],
                            ThirdOption = (string)question[4],
                            Id = int.Parse((string)question[5])
                        });
                    }
                }
                while (result.Count != 12);

                return new GetAllQuestionsResponse() { Questions = result };
            }

            throw new Helpers.KeyNotFoundException("Questions wasn't found");
        }
                
        public AddResultResponse SaveResult(List<AnswearModel> questions, string email)
        {
            _userService.GetByEmail(email);
            var score = CalculatePoints(questions);
            var user = new string[4] { email, score.ToString(), "", "" };
            var googleSheetsSuccess = _sheetsService.AddUser(user);

            var textForLetter = _sheetsService.GetMessage();
            var message = _messageService.CreateMessageDetails(email, textForLetter);
            var success = _letterService.Send(message);

            return new AddResultResponse() { Success = true };
        }

        public int CalculatePoints(List<AnswearModel> answears)
        {
            var questions = _sheetsService.GetAllQuestions();
            var score = 0;

            foreach (var answear in answears)
            {
                var question = questions.FirstOrDefault(q => q[5].ToString() == answear.Id.ToString());

                if (answear == null)
                    throw new Helpers.KeyNotFoundException("Question not found");

                if (answear.Answear == (string)question[1])
                    score += int.Parse((string)question[2]);
            }

            return score;
        }
    }
}
