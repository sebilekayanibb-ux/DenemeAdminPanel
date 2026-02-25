using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace DenemeAdminPanel.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // appsettings.json dosyasından verileri alıyoruz
            var apiKey = _config["SendGrid:ApiKey"];
            var fromEmail = _config["SendGrid:FromEmail"];
            var fromName = _config["SendGrid:FromName"];

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, fromName);
            var to = new EmailAddress(email);

            // Mail içeriğini oluştur
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);

            // Gönderimi yap
            var response = await client.SendEmailAsync(msg);

            // Eğer gönderim başarılı değilse hata fırlat (debug yapabilmek için)
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Body.ReadAsStringAsync();
                throw new Exception($"SendGrid Hatası: {response.StatusCode}. Detay: {body}");
            }
        }
    }
}