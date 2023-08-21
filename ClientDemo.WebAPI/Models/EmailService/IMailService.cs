using System.Threading.Tasks;

namespace TravelNinjaz.B2B.WebAPI.Models.EmailService
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendEmailAsync_2(MailRequest mailRequest);

    }
}
