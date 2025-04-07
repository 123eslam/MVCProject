using Demo.DAL.Entities.Identity;

namespace Demo.BLL.Common.Services.EmailSettings
{
    public interface IEmailSettings
    {
        public void SendEmail(Email email);
    }
}
