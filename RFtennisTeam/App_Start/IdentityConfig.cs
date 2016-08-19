using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using RFtennisTeam.Models;
using SendGrid;
using System.Net;
using System.Configuration;
using System.Diagnostics;
//using SendGrid.Helpers.Mail;

namespace RFtennisTeam
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            message.Subject = "UWRF Tennis Club";
            //message.Body = "<html><body style=\"background-image:url(https://www.uwrf.edu/news/images/UWRFRedBG271.png);\">This is my message</body></html>";
            await configSendGridasync(message);
        }

        // Use NuGet to install SendGrid (Basic C# client lib) 
        private async Task configSendGridasync(IdentityMessage message)
        {

                  var myMessage = new SendGridMessage();
                  myMessage.AddTo(message.Destination);
                  myMessage.AddCc("dakota.castleberg18@gmail.com");
                  myMessage.From = new System.Net.Mail.MailAddress(
                                      "dakota.castleberg18@gmail.com", "Dakota Castleberg");
                  myMessage.Subject = message.Subject;
                  myMessage.Text = message.Body;
                  myMessage.Html = message.Body;

                  var credentials = new NetworkCredential(
                             ConfigurationManager.AppSettings["mailAccount"],
                             ConfigurationManager.AppSettings["mailPassword"]
                             );

                  // Create a Web transport for sending email.
                  var transportWeb = new Web(credentials);// Web(credentials);

            // Send the email.
            if (transportWeb != null)
            {
                await transportWeb.DeliverAsync(myMessage);
            }
            else
            {
                Trace.TraceError("Failed to create Web transport.");
                await Task.FromResult(0);
            }


            /* // Returning null, why?
 String apiKey = Environment.GetEnvironmentVariable("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", EnvironmentVariableTarget.User);
 dynamic sg = new SendGrid.SendGridAPIClient(apiKey);

 Email from = new Email("test@sendgrid.com");
 Email to = new Email("dakota.castleberg18@gmail.com");
 String subject = "Sending with SendGrid is Fun";
 Content content = new Content("text/plain", "and easy to do anywhere, even with C#");
 Mail mail = new Mail(from, subject, to, content);

 dynamic response = sg.client.mail.send.beta.post(requestBody: mail.Get());

 // Send the email.
 if (5 == 5)
 {
     Trace.TraceError("Failed to create Web transport.");
     await Task.FromResult(0);
 }

*/
        }

    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
