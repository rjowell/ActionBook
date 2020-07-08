using System;
using System.Collections.Generic;
using System.Net.Mail;
//using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace ActionBook
{
    public partial class Login : ContentPage
    {

        

        public Login()
        {
            InitializeComponent();
            Console.WriteLine("login1");
            loginIndicator.IsRunning = false;
            Console.WriteLine("login1");
            //DependencyService.Get<IFirebaseProcess>().Logout();
            MessagingCenter.Subscribe<App, string>(this, "OpenMainPage", (sender,arg) =>
            {
                loginIndicator.IsRunning = false;
                errorMessage.Text = "";
                handleOrEmail.Text = null;
                password.Text = null;
                Navigation.PushAsync(new MainPage());
            });

            handleOrEmail.Completed += (s, e) => {

                password.Focus();

            };

            MessagingCenter.Subscribe<IFirebaseProcess, int>(this, "LoginError", (sender, arg) =>
               {

                   loginIndicator.IsRunning = false;
                   if (arg==1)
                   {
                       //bad handle
                       errorMessage.Text = "That handle does not exist";
                   }
                   else if(arg==2)
                   {
                       //bad email
                       errorMessage.Text = "That e-mail does not exist";
                   }
                   else if(arg==3)
                   {
                       errorMessage.Text = "Password Incorrect";
                   }
                   else
                   {
                       Navigation.PushAsync(new MainPage());
                   }



               });
        }

        public void LoginUser(object sender, EventArgs e)
        {
            bool isItEmail;
            loginIndicator.IsRunning = true;
            if (handleOrEmail.Text == null || password.Text == null)
            {
                errorMessage.Text = "One Field Is Blank";
                loginIndicator.IsRunning = false;
            }
            else
            {
                try
                {
                    MailAddress m = new MailAddress(handleOrEmail.Text);

                    isItEmail = true;
                }
                catch (FormatException)
                {
                    isItEmail = false;
                }


                DependencyService.Get<IFirebaseProcess>().Login(handleOrEmail.Text, password.Text, isItEmail);
            }
        }

        public void OpenCreateAccount(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TermsOfService());
        }
    }
}
