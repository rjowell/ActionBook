using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using Xamarin.Forms;

namespace ActionBook
{
    public partial class EditProfile : ContentPage
    {
        WebClient client = new WebClient();

        public void ChangeUserHandle(string newHandle)
        {


            NameValueCollection nvc = new NameValueCollection();
            nvc.Set("userID", App.currentUser.userID);
            nvc.Set("newHandle", newHandle);
            string response = System.Text.Encoding.UTF8.GetString(client.UploadValues("https://www.cvx4u.com/ActionBook/updateHandle.php", nvc));
            NameValueCollection sent = new NameValueCollection();
            if (response == "USER_ALREADY_EXISTS")
            {
                Console.WriteLine("it already exists");

            }
            else
            {
                Console.WriteLine("Success");
            }

        }

        public void GoBack(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        public EditProfile()
        {
            InitializeComponent();
            
            handleEntry.Text = App.currentUser.currentHandle;
            handleEntry.IsEnabled = false;
            handleCancel.IsVisible = false;
            handleSubmit.IsVisible = false;
            eMailCancel.IsVisible = false;
            eMailSubmit.IsVisible = false;
            passwordCancel.IsVisible = false;
            passwordSubmit.IsVisible = false;

            handleCancel.Clicked += (s, e) =>
             {
                 handleEntry.Text = App.currentUser.currentHandle;
                 handleEntry.IsEnabled = false;
                 handleEdit.IsVisible = true;
                 handleCancel.IsVisible = false;
                 handleSubmit.IsVisible = false;
             };

            handleEdit.Clicked += (sender, e) =>
             {
                 handleEntry.IsEnabled = true;
                 handleEdit.IsVisible = false;
                 handleCancel.IsVisible = true;
                 handleSubmit.IsVisible = true;
             };

            logoutButton.Clicked += (sender, arg) => {

                DependencyService.Get<IFirebaseProcess>().Logout();

            };

            handleSubmit.Clicked += (s, e) =>
              {
                  NameValueCollection nvc = new NameValueCollection();
                  nvc.Set("userID", App.currentUser.userID);
                  nvc.Set("newHandle", handleEntry.Text);
                  string response = System.Text.Encoding.UTF8.GetString(client.UploadValues("https://www.cvx4u.com/ActionBook/updateHandle.php", nvc));
                 
                  if (response == "USER_ALREADY_EXISTS")
                  {
                      //MessagingCenter.Subscribe<IFirebaseProcess, NameValueCollection> (this, "EditProfileError", (sender, arg) =>
                      errorLabel.Text = "Handle Already Exists";

                  }
                  else
                  {
                      
                      handleEdit.IsVisible = true;
                      handleCancel.IsVisible = false;
                      handleSubmit.IsVisible = false;
                      App.currentUser.currentHandle = handleEntry.Text;
                      handleEntry.IsEnabled = false;
                      errorLabel.Text = "Handle Changed Successfully";
                  }
              };


            eMailSubmit.Clicked += (s, e) =>
             {
                 DependencyService.Get<IFirebaseProcess>().ChangeUserEmail(eMailEntry.Text);
             };

            eMailCancel.Clicked += (s, e) =>
            {
                eMailEntry.Text = App.currentUser.currentEmail;
                eMailEntry.IsEnabled = false;
                eMailEdit.IsVisible = true;
                eMailCancel.IsVisible = false;
                eMailSubmit.IsVisible = false;
            };

            eMailEdit.Clicked += (sender, e) =>
            {
                eMailEntry.IsEnabled = true;
                eMailEdit.IsVisible = false;
                eMailCancel.IsVisible = true;
                eMailSubmit.IsVisible = true;
            };


            passwordCancel.Clicked += (s, e) =>
            {
                passwordEntry.Text = "********";
                passwordEntry.IsEnabled = false;
                passwordEdit.IsVisible = true;
                passwordCancel.IsVisible = false;
                passwordSubmit.IsVisible = false;
            };


            passwordEdit.Clicked += (sender, e) =>
            {
                passwordEntry.IsEnabled = true;
                passwordEdit.IsVisible = false;
                passwordCancel.IsVisible = true;
                passwordSubmit.IsVisible = true;
            };

            eMailEntry.Text = App.currentUser.currentEmail;
            eMailEntry.IsEnabled = false;
            passwordEntry.Text = "*********";
            passwordEntry.IsEnabled = false;
            mainMenu.IsVisible = false;
            logoutButton.Clicked += (sender, arg) => {

                DependencyService.Get<IFirebaseProcess>().Logout();

            };

            mainButton.Clicked += (s, e) =>
            {
                Navigation.PushAsync(new MainPage());
            };

            TapGestureRecognizer menuTgp = new TapGestureRecognizer();
            menuTgp.Tapped += (s, e) =>
            {
                if (mainMenu.IsVisible == true)
                {
                    mainMenu.IsVisible = false;
                }
                else
                {
                    mainMenu.IsVisible = true;
                }
            };
            menuButton.GestureRecognizers.Add(menuTgp);
            logoutButton.Clicked += (sender, arg) => {

                DependencyService.Get<IFirebaseProcess>().Logout();

            };

            MessagingCenter.Subscribe<IFirebaseProcess, NameValueCollection> (this, "EditProfileError", (sender, arg) =>
            {
                if (arg["changeEmail"]!=null)
                {
                    if(arg["changeEmail"]=="1")
                    {
                        Console.WriteLine("email bas format");
                        errorLabel.Text = "E-mail is baddly formatted";
                        eMailEntry.Text = App.currentUser.currentEmail;
                    }
                    else if(arg["changeEmail"]=="2")
                    {
                        Console.WriteLine("email taken");
                        errorLabel.Text = "E-mail already in use";
                        eMailEntry.Text = App.currentUser.currentEmail;
                    }
                    else
                    {
                        Console.WriteLine("email success");
                        NameValueCollection eMailNvc = new NameValueCollection();
                        eMailNvc.Set("userID", App.currentUser.userID);
                        eMailNvc.Set("newEmail", eMailEntry.Text);
                        client.UploadValues("https://www.cvx4u.com/ActionBook/updateEmail.php", eMailNvc);
                        errorLabel.Text = "E-Mail Successfully Changed";
                    }
                }
                if(arg["changePassword"]!=null)
                {
                    errorLabel.Text = "Password changed";
                }
                if(arg["changeHandle"]!=null)
                {
                    if (arg["changeHandle"] == "1")
                    {
                        //handle exists
                    }
                 
                    else
                    {
                        //success
                    }
                }
            });
        }
    }
}
