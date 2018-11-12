﻿using Diarista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Diarista.Services
{
    public class NotificacaoService
    {
        private static EmailConfiguracao Email = new EmailConfiguracao
        {
            From = "email@gmail.com",
            Name = "Nome do Usuario",
            Password = "<senha_do_email>"
        };
        public void NotificarDiarista(Servico servico)
        {
            var body = $"<h2>Olá, {servico.Diarista.Nome}, </h2><p>Você recebeu uma proposta de serviço para trabalhar em: {servico.Casa.Endereco}, acesse o sistema para responder.</p>";

            var mm = new MailMessage(Email.From, servico.Diarista.Usuario.Email, $"Nova proposta de {servico.Contratante.Nome}", body)
            {
                IsBodyHtml = true
            };

            using (var client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Email.From, Email.Password)
            })
            {
                try
                {
                    client.Send(mm);
                }
                catch (Exception ex)
                {
                    //throw;
                }
            }
        }

        public class EmailConfiguracao
        {
            public string From { get; set; }
            public string Name { get; set; }
            public string Password { get; set; }
        }
    }
}