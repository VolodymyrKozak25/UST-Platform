using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(string token, string userEmail, string subject, string emailText);
    }
}
