using AutoMapper;
using QuestionManager.BLL.Helpers;
using QuestionManager.BLL.Models;
using QuestionManager.BLL.Models.Responses;
using QuestionManager.BLL.Models.Responses.Abstractions;
using QuestionManager.BLL.Services.Abstractions;
using QuestionManager.DAL.DataAccess.Contracts;
using QuestionManager.DAL.DataAccess.Implementations.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionManager.BLL.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IDbRepository _dbRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IGoogleSheetsService _sheetsService;
        private readonly IEmailService _emailService;

        public QuestionService(IDbRepository dbRepository, IMapper mapper, IUserService userService, IGoogleSheetsService sheetsService, IEmailService emailService)
        {
            _dbRepository = dbRepository;
            _mapper = mapper;
            _userService = userService;
            _sheetsService = sheetsService;
            _emailService = emailService;
        }

        public async Task<IServiceResponse> GetAllAsync()
        {
            var questions = (List<QuestionEntity>) await _dbRepository.GetAllAsync<QuestionEntity>();

            if(questions != null)
            {
                var random = new Random();
                var numbersList = new List<int>();
                var result = new List<QuestionModel>();
                var countEasyQuestions = 0;
                var countMediumQuestions = 0;
                var countHardQuestions = 0;

                do
                {
                    var count = questions.Count();
                    var questionNumber = random.Next(count);

                    if (!numbersList.Contains(questionNumber))
                    {
                        if (questions[questionNumber].Complexity == 1 && countEasyQuestions != 4)
                            countEasyQuestions++;
                        else if (questions[questionNumber].Complexity == 2 && countMediumQuestions != 4)
                            countMediumQuestions++;
                        else if (questions[questionNumber].Complexity == 3 && countHardQuestions != 4)
                            countHardQuestions++;
                        else
                            continue;

                        numbersList.Add(questionNumber);
                        result.Add(_mapper.Map<QuestionModel>(questions[questionNumber]));
                    }
                }
                while (result.Count != 12);

                return new GetAllQuestionsResponse() { Questions = result };
            }

            throw new Helpers.KeyNotFoundException("Questions wasn't found");
        }

        public async Task<IServiceResponse> AddAsync(string question, string answear, int complexity, string secondOption, string thirdOption)
        {
            var result = await _dbRepository.AddAsync(new QuestionEntity()
            {
                Id = Guid.NewGuid(),
                Question = question,
                Complexity = complexity,
                Answear = answear,
                SecondOption = secondOption,
                ThirdOption = thirdOption
            });

            if(result != null)
            {
                var questionModel = _mapper.Map<QuestionModel>(result);

                return new AddQuestionResponse() { AddedQuestion = questionModel };
            }

            throw new AppException("The question was not added");
        }

        public async Task<IServiceResponse> DeleteAsync(Guid questionId)
        {
            var isRemoved = await _dbRepository.RemoveAsync<QuestionEntity>(questionId);

            if(isRemoved)
            {
                return new DeleteResponse() { IsRemoved = isRemoved };
            }

            throw new Helpers.KeyNotFoundException("The question was not deleteded");
        }

        public async Task<IServiceResponse> SaveResultAsync(List<AnswearModel> questions, string email)
        {
            var score = await CalculatePointsAsync(questions);

            await _userService.AddResultAsync(email, score);

            var user = new string[4] { email, score.ToString(), "", "" };

            var googleSheetsSuccess = _sheetsService.AddUser(user);
            //var result = _emailService.SendLetter(email);

            return new CalculatePointsResponse() { Success = googleSheetsSuccess };
        }

        public async Task<int> CalculatePointsAsync(List<AnswearModel> questions)
        {
            var questionEntities = await _dbRepository.GetAllAsync<QuestionEntity>();
            var score = 0;

            foreach (var question in questions)
            {
                var entity = questionEntities.FirstOrDefault(q => q.Id == question.Id);

                if (entity == null)
                    throw new Helpers.KeyNotFoundException("Question not found");

                if (question.Answear == entity.Answear)
                    score += entity.Complexity;
            }

            return score;
        }
    }
}
