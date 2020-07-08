using System;
using System.Collections.Specialized;
using System.Net;
using Newtonsoft.Json.Linq;
//using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ActionBook
{

    public class NewUser
    {
        public string handle { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }

    public class CurrentUser
    {
        public string currentEmail { get; set; }
        public string currentHandle { get; set; }
        public string firebaseID { get; set; }
        public string userID { get; set; }
        
    }

    


    public partial class App : Application
    {
        public static CurrentUser currentUser;
        public static NewUser newUser;
        
        WebClient client = new WebClient();
        public static string[] typeImages = new string[] { "https://www.cvx4u.com/ActionBook/app_assets/LearnIcon.png", "https://www.cvx4u.com/ActionBook/app_assets/ActIcon.png", "https://www.cvx4u.com/ActionBook/app_assets/GiveIcon.png" };

        public static void PostLikes(string type,string post)
        {
            NameValueCollection postData = new NameValueCollection();
            WebClient client = new WebClient();
            postData.Add("userID", App.currentUser.userID);
            postData.Add("postID", post);
            postData.Add("type", type);
            client.UploadValues(new Uri("https://www.cvx4u.com/ActionBook/postLikes.php"), postData);
        }


        public void OpenPage(string firebaseID)
        {

         


            NameValueCollection paramList = new NameValueCollection();
            paramList.Add("id", firebaseID);
            Console.WriteLine(firebaseID);
            Uri postURL = new Uri("https://www.cvx4u.com/ActionBook/getUserInfoByFirebaseID.php");
            //hello
            byte[] responseResult = client.UploadValues(postURL, paramList);
            Console.WriteLine(System.Text.Encoding.UTF8.GetString(responseResult));
            JObject responseText = JObject.Parse(System.Text.Encoding.UTF8.GetString(responseResult));

            Console.WriteLine(responseText);

            App.currentUser.currentHandle = responseText.GetValue("Handle").ToString();
            App.currentUser.currentEmail = responseText.GetValue("EMail").ToString();
            App.currentUser.firebaseID = firebaseID;
            App.currentUser.userID = responseText.GetValue("UserID").ToString();

            Console.WriteLine(App.currentUser.currentHandle + " " + App.currentUser.currentEmail + " " + App.currentUser.firebaseID + " " + App.currentUser.userID);
            //MainPage = new NavigationPage(new MainPage());

            MessagingCenter.Send<App, string>(this, "OpenMainPage", null);
        }


        public static NavigationPage loginPage;
        public static NavigationPage mainPage;

        public App()
        {
            currentUser = new CurrentUser();
            newUser = new NewUser();
            MessagingCenter.Subscribe<IFirebaseProcess, string>(this, "LoginSuccess", (sender, firebaseID) => {

                Console.WriteLine("loginsuccess1");
                Uri signInURL = new Uri("https://www.cvx4u.com/ActionBook/signInOutUser.php");
                Console.WriteLine("loginsuccess1");
                NameValueCollection signInParams = new NameValueCollection();
                Console.WriteLine("loginsuccess2");
                signInParams.Add("mode", "signIn");
                signInParams.Add("id", firebaseID);
                client.UploadValues(signInURL, signInParams);
                Console.WriteLine("loginsuccess3");
                OpenPage(firebaseID);
                //MessagingCenter.Send<App, string>(this, "OpenMainPage", null);
                /*NameValueCollection paramList = new NameValueCollection();
                paramList.Add("id", firebaseID);
                //Console.WriteLine(Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid);
                Uri postURL = new Uri("https://www.cvx4u.com/ActionBook/getUserInfoByFirebaseID.php");

                byte[] responseResult = client.UploadValues(postURL, paramList);
                Console.WriteLine(System.Text.Encoding.UTF8.GetString(responseResult));
                JObject responseText = JObject.Parse(System.Text.Encoding.UTF8.GetString(responseResult));

                Console.WriteLine(responseText);

                App.currentUser.currentHandle = responseText.GetValue("Handle").ToString();
                App.currentUser.currentEmail = responseText.GetValue("EMail").ToString();
                App.currentUser.firebaseID = firebaseID;
                App.currentUser.userID = responseText.GetValue("UserID").ToString();

                Console.WriteLine(App.currentUser.currentHandle + " " + App.currentUser.currentEmail + " " + App.currentUser.firebaseID + " " + App.currentUser.userID);


                MessagingCenter.Send<App,string> (this, "OpenMainPage",null);*/



            });
            InitializeComponent();




            /* public string currentEmail { get; set; }
         public string currentHandle { get; set; }
         public string firebaseID { get; set; }
         public string userID { get; set; }*/

            loginPage = new NavigationPage(new Login());
            mainPage = new NavigationPage(new MainPage());
            /*
            currentUser.currentEmail = "russ.jowell@gmail.com";
            currentUser.currentHandle = "rsjowell";
            currentUser.firebaseID = "X4SDay077Yc6S3UE6SqLl9OKVHs2";
            currentUser.userID = "U1000";

            MainPage = mainPage;*/


        
            //MainPage = loginPage;
            //Console.WriteLine(DependencyService.Get<IFirebaseProcess>().GetCurrentUID());
            if(DependencyService.Get<IFirebaseProcess>().GetCurrentUID()!=null)
            {
                Console.WriteLine("moving on");
                OpenPage(DependencyService.Get<IFirebaseProcess>().GetCurrentUID());
                MainPage = mainPage;
                
                //MainPage = new NavigationPage(new Login());
            }
            else { 

            

            MainPage = loginPage;

            }

        }

        public void CheckIfUserSignedIn()
        {

        }


       

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
