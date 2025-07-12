using WebProgram.SMTP;

namespace WebProgram.Interface;

public interface ISMTPService
{
    Task<bool> SendMessage(Message message);
}