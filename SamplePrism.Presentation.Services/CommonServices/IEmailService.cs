﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamplePrism.Services
{
    public interface IEmailService
    {
        void SendEmail(string smtpServerAddress, string smtpUser, string smtpPassword, int smtpPort,
                       string toEmailAddress, string ccEmailAddresses, string fromEmailAddress, string subject,
                       string body, string fileName, bool deleteFile, bool bypassSslErrors);

        void SendEMailAsync(string smtpServerAddress, string smtpUser, string smtpPassword, int smtpPort,
                            string toEmailAddress, string ccEmailAddresses, string fromEmailAddress, string subject,
                            string body, string fileName, bool deleteFile, bool byPassSslErrors);
    }
}
