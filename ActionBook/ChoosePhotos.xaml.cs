using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using Xamarin.Forms;

namespace ActionBook
{
    public partial class ChoosePhotos : ContentPage
    {
        public ChoosePhotos()
        {
            InitializeComponent();
            userName.Text = App.currentUser.currentHandle;
            
        }




        public void GoBack(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateAccount());
        }

        public void SubmitPhotos(object sender, EventArgs e)
        {

            if (profilePicData == null)
            {
                errorLabel.IsVisible = true;
            }
            else
            {
                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("profileImageBytes", Convert.ToBase64String(profilePicData));
                if (backgroundPicStream != null)
                {
                    //MemoryStream backgroundMs = new MemoryStream();
                    //backgroundPicStream.CopyTo(backgroundMs);
                    nvc.Add("backgroundImageBytes", Convert.ToBase64String(backgroundPicData));
                }
                WebClient client = new WebClient();
                nvc.Add("userID", App.currentUser.userID);
                Console.WriteLine("IS is" + App.currentUser.userID);
                client.UploadValues("https://www.cvx4u.com/ActionBook/uploadUserPhotos.php", nvc);
                Navigation.PushAsync(new MainPage());
            }

        }

        Stream profilePicStream;
        Stream backgroundPicStream;
        byte[] profilePicData;
        byte[] backgroundPicData;

        public async void PhotoSelection(object sender, EventArgs e)
        {

            Console.WriteLine(profilePicStream);
            Stream stream = await DependencyService.Get<GetPhoto>().GetDevicePhoto();
            Console.WriteLine("cancel post");
            Console.WriteLine(stream);
            MemoryStream pms = new MemoryStream();
            MemoryStream bms = new MemoryStream();
            stream.CopyTo(pms);
            byte[] picData = pms.ToArray();
            Console.WriteLine("poopdog");
            Console.WriteLine(picData.Length);
            
            if (stream!=null)
            {
                //testImage.Source = ImageSource.FromStream(() => stream);
            }

            Button sentButton = (Button)sender;
            if(sentButton.ClassId=="profile")
            {
                
                profilePicData = picData;
                viewProfileImage.Source = ImageSource.FromStream(()=>new MemoryStream(picData));


            }
            else
            {
                
                backgroundPicData = picData;
                
                viewBackImage.Source = ImageSource.FromStream(() => stream);
                
            }
           
        }
    }
}
