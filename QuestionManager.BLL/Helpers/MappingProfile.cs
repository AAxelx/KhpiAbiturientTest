using AutoMapper;
using QuestionManager.BLL.Models;
using QuestionManager.DAL.DataAccess.Implementations.Entities;

namespace QuestionManager.BLL.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<QuestionEntity, QuestionModel>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Question, opt => opt.MapFrom(src => src.Question))
                .ForMember(dst => dst.FirstOption, opt => opt.MapFrom(src => src.Answear))
                .ForMember(dst => dst.SecondOption, opt => opt.MapFrom(src => src.SecondOption))
                .ForMember(dst => dst.ThirdOption, opt => opt.MapFrom(src => src.ThirdOption));
        }
    }
}
