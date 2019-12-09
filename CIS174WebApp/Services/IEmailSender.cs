using System.Threading.Tasks;

namespace CIS174WebApp.Services
{
  public   interface IEmailSender
    {
        
        Task SendEmailAsync(string email, string subject, string message);
    }
}
