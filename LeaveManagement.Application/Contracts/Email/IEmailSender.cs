using LeaveManagement.Application.Contracts.Models.Email;

namespace LeaveManagement.Application.Contracts.Email;

public interface IEmailSender
{
    Task<bool> SendEmail(EmailMessage email);
}