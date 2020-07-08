using System;
using System.Collections.Generic;
using System.IO;
using ActionBook;
//using Plugin.Media;
using Xamarin.Forms;

namespace ActionBook
{
    public partial class CreateAccount : ContentPage
    {
        public CreateAccount()
        {
            InitializeComponent();

            userHandle.Text = App.newUser.handle;
            userEMail.Text = App.newUser.email;
            userPassword.Text = App.newUser.password;


            MessagingCenter.Subscribe<IFirebaseProcess, int>(this, "CreateError", (sender, arg) =>
            {
                if(arg==1)
                {
                    //bad format
                    Console.WriteLine("issue 1");
                    statusMessage.Text = "The E-mail address is badly formatted";
                }
                else if(arg==2)
                {
                    //already in use
                    Console.WriteLine("issue 2");
                    statusMessage.Text = "The E-mail address is already in use";
                }
                else if(arg==3)
                {
                    Console.WriteLine("issue 3");
                    statusMessage.Text = "Handle already in use";
                }
                else
                {

                    //NavigationPage page = new NavigationPage(this);
                    //Console.WriteLine("booogers good");
                    Navigation.PushAsync(new ChoosePhotos());
                    //this.Navigation.PushAsync(new MainPage());

                    
                }

            });
           
        }

        public void GoBack(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }

        async void OpenPhotoWindow(object sender, EventArgs e)
        {
            /*Stream stream = await DependencyService.Get<GetPhoto>().GetDevicePhoto();


            if(stream!=null)
            {
                //testImage.Source = ImageSource.FromStream(() => stream);
            }

            //Console.WriteLine(stream);*/


        }

        public void GoToPhotoPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChoosePhotos());
        }

        public void CreateNewAccount(object sender, EventArgs e)
        {
            bool moveOn=true;
            if(userEMail.Text=="" || userPassword.Text=="" || userHandle.Text=="" || userEMail.Text==null || userPassword.Text==null || userHandle.Text=="")
            {
                moveOn = false;
                statusMessage.Text = "Some Fields Are Blank";
            }
            

            if (moveOn == true)
            {

                App.newUser.handle = userHandle.Text;
                App.newUser.email = userEMail.Text;
                App.newUser.password = userPassword.Text;
                DependencyService.Get<IFirebaseProcess>().CreateNewUser(userHandle.Text, userEMail.Text, userPassword.Text);
            }
        }
    }
}
