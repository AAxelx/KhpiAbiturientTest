using MimeKit;
using System.Threading.Tasks;

namespace QuestionManager.BLL.Services.Abstractions
{
    public interface ILetterService
    {
        bool Send(MimeMessage message);
    }
}
