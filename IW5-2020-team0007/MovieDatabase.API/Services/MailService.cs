﻿using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MovieDatabase.API.Models.Email;
using System;
using System.Threading.Tasks;

namespace MovieDatabase.API.Services
{
    public class MailService
    {
        private EmailSettings EmailSettings { get; }
        private string PathBase { get; }

        public MailService(IOptions<EmailSettings> options, IConfiguration configuration)
        {
            EmailSettings = options.Value;
            PathBase = configuration["PathBase"];
        }

        public async Task SendRegisterEmailAsync(string username, string authCode, string email)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(EmailSettings.SenderAddress));
            message.To.Add(new MailboxAddress(email));
            message.Subject = $"Registrace uživatele {username} do filmové databáze.";
            message.Body = new TextPart(TextFormat.Plain)
            {
                Text = CreateRegisterBody(username, authCode)
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(EmailSettings.SmtpAddress, EmailSettings.Port, true);

            if (!string.IsNullOrEmpty(EmailSettings.SmtpUsername))
                await client.AuthenticateAsync(EmailSettings.SmtpUsername, EmailSettings.SmtpPassword);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        private string CreateRegisterBody(string username, string authCode)
        {
            return $"Vážený uživateli {username}.\nOvěření vašeho účtu provedete následujícím odkazem:\n\n{CreateConfirmURL(authCode)}";
        }

        private string CreateConfirmURL(string authCode)
        {
            var baseUri = new Uri(PathBase);
            return new Uri(baseUri, $"users/register?code={authCode}").ToString();
        }
    }
}
