using AutoMapper;
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
            var questions = await _dbRepository.GetAllAsync<QuestionEntity>();

            if(questions != null)
            {
                var easyResult = new List<QuestionModel>();
                var mediumResult = new List<QuestionModel>();
                var hardResult = new List<QuestionModel>();

                foreach (var question in questions)
                {
                    if(question.Complexity == 1)
                        easyResult.Add(_mapper.Map<QuestionModel>(question));
                    else if(question.Complexity == 2)
                        mediumResult.Add(_mapper.Map<QuestionModel>(question));
                    else
                        hardResult.Add(_mapper.Map<QuestionModel>(question));
                }

                return new GetAllQuestionsResponse() { EasyQuestions = easyResult, MediumQuestions = mediumResult, HardQuestions = hardResult};
            }

            return new AddQuestionResponse() { Message = "Questions wasn't found" };
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

            return new AddQuestionResponse() { Message = "The question was not added" };
        }

        public async Task<IServiceResponse> DeleteAsync(Guid questionId)
        {
            var isRemoved = await _dbRepository.RemoveAsync<QuestionEntity>(questionId);

            if(isRemoved)
            {
                return new DeleteResponse() { IsRemoved = isRemoved };
            }

            return new DeleteResponse() { Message = "The question was not deleteded" };
        }

        public async Task<IServiceResponse> CalculatePointsAsync(List<AnswearModel> questions, string email)
        {
            var questionEntities = await _dbRepository.GetAllAsync<QuestionEntity>();
            var score = 0;

            foreach(var question in questions)
            {
                var entity = questionEntities.FirstOrDefault(q => q.Id == question.Id);

                if (entity == null)
                    throw new Helpers.KeyNotFoundException("Question not found");

                if (question.Answear == entity.Answear)
                    score += entity.Complexity;
            }

            var SqlSuccess = (AddResultResponse)await _userService.AddResultAsync(email, score);

            if (SqlSuccess.Success)
            {
                var user = new string[4] {email, score.ToString(), "", ""};

                var googleSheetsSuccess = _sheetsService.AddUser(user);
                //var result = _emailService.SendLetter(email);

                if (googleSheetsSuccess)
                {
                    return new CalculatePointsResponse() { Success = SqlSuccess.Success };
                }
            }

            return new DeleteResponse() { Message = SqlSuccess.Message };
        }
    }
}
