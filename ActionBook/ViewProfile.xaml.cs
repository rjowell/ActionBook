using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace ActionBook
{
    /* [{
 		"PageID": "100",
 		"Title": "My Great Page",
 		"Description": "This is an AWESOME Page",
 		"ContentPieces": ["1000"]
 	},
 	{
 		"PageID": "101",
 		"Title": "Another Great Page",
 		"Description": "It's ANOTHER Awesome Page",
 		"Content Pieces": "1003"
 	}
 ]*/

    public class Page
        {
            public event PropertyChangedEventHandler PropertyChanged;

        private string pageID { get; set; }
        public string PageID
        {
            get { return pageID; }
            set
            {
                pageID = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("PageID"));
            }
        }

        private string pageTitle { get; set; }
        public string PageTitle
        {
            get { return pageTitle; }
            set
            {
                pageTitle = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("PageTitle"));
            }
        }
            
         private string[] firstPics { get; set; }
        public string[] FirstPics
        {
            get { return firstPics; }
            set
            {
                firstPics = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("FirstPics"));
            }
        }

        private bool firstPicVisible { get; set; }
        public bool FirstPicVisible
        {
            get { return firstPicVisible; }
            set
            {
                firstPicVisible = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("FirstPicVisible"));
            }
        }

        private bool secondPicVisible { get; set; }
        public bool SecondPicVisible
        {
            get { return secondPicVisible; }
            set
            {
                secondPicVisible = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SecondPicVisible"));
            }
        }

        private bool thirdPicVisible { get; set; }
        public bool ThirdPicVisible
        {
            get { return thirdPicVisible; }
            set
            {
                thirdPicVisible = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ThirdPicVisible"));
            }
        }

        private bool fourthPicVisible { get; set; }
        public bool FourthPicVisible
        {
            get { return fourthPicVisible; }
            set
            {
                fourthPicVisible = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("FourthPicVisible"));
            }
        }




        public Page(string pageIDin, string pageTitleIn, string firstPicsIn, JArray firstPicsIn1)
            {
                pageID = pageIDin;
                pageTitle = pageTitleIn;
            string[] picList = firstPicsIn1.ToObject<string[]>();
            //Console.WriteLine(newList.Length);
            //string[] picList = firstPicsIn.Replace("[","").Replace("]","").Replace("\"","").Replace("\n","").Replace("  ","").Split(',');
            //string[] newList = firstPicsIn1.ToObject<string[]>();
            Console.WriteLine(picList);
            Console.WriteLine(picList.Length);
            string photoPath = "https://www.cvx4u.com/ActionBook/photos/";
            if(picList.Length<4)
            {
                firstPics = new string[picList.Length];
            }
            else
            {
                firstPics = new string[4];
            }
            int index = 0;

            foreach (string thing in picList)
            {
                
                if (index < 4 && index!=picList.Length)
                {
                    Console.WriteLine(photoPath + picList[index] + ".jpg");
                    if (picList[index].Length == 4)
                    {
                        firstPics[index] = photoPath + picList[index] + ".jpg";
                    }
                    else
                    {
                        firstPics[index] = photoPath + picList[index].Substring(1) + ".jpg";
                    }
                    if(index==0)
                    {
                        firstPicVisible = true;
                    }
                    if(index==1)
                    {
                        secondPicVisible = true;
                    }
                    if(index==2)
                    {
                        thirdPicVisible = true;
                    }
                    if(index==3)
                    {
                        fourthPicVisible = true;
                    }
                    index++;
                }
                else break;
            }
            

            //firstPics = new string[4];
            Console.WriteLine("consolr here");
            Console.WriteLine(firstPics.Length);
        }
        }


    public partial class ViewProfile : ContentPage
    {
        WebClient client = new WebClient();
        ObservableCollection<Page> pageList = new ObservableCollection<Page>();
        string userID;
        byte[] profilePicData;

        void OpenPage(Object sender, EventArgs e)
        {
            RelativeLayout sendingLayout = (RelativeLayout)sender;
            Console.WriteLine("id iss");
            Navigation.PushAsync(new ViewPage(userID,sendingLayout.AutomationId));
            Console.WriteLine(sendingLayout.AutomationId);
        }

        public async void PictureSelect(object sender, EventArgs e)
        {
            Stream stream = await DependencyService.Get<GetPhoto>().GetDevicePhoto();
            if (stream != null)
            {
                MemoryStream bms = new MemoryStream();
                stream.CopyTo(bms);
                byte[] picData = bms.ToArray();
                viewBackImage.Source = ImageSource.FromStream(() => new MemoryStream(picData));
                profilePicData = picData;
            }

            //profilePicData = picData;

        }

        

        public void CloseWindow(object sender, EventArgs e)
        {
            createPageWindow.IsVisible = false;
            //confirmNameLabel.Text = pageNameEntry.Text;
            pageNameEntry.IsVisible = true;
            confirmNameLabel.IsVisible = false;
            pageNameEntry.Text = "";
            submitButton.IsVisible = true;
            confirmButton.IsVisible = false;
            statusLabel.Text = "Enter Page Name";
        }

        public void ShowWindow(object sender, EventArgs e)
        {
            createPageWindow.IsVisible = true;
        }

        public void SubmitNewPage(object sender, EventArgs e)
        {

            if (profilePicData == null)
            {
                selectPicture.TextColor = Color.LightPink;
            }
            else
            {
                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("userID", App.currentUser.userID);
                nvc.Add("pageName", confirmNameLabel.Text);
                nvc.Add("backgroundImageBytes", Convert.ToBase64String(profilePicData));
                WebClient thisclient = new WebClient();
                string newData = System.Text.Encoding.Default.GetString(thisclient.UploadValues("https://www.cvx4u.com/ActionBook/createPage.php", nvc));
                Console.WriteLine(newData);
                LoadData();
                createPageWindow.IsVisible = false;
            }


           

        }

        public void SubmitPageName(object sender, EventArgs e)
        {
            if(pageNameEntry.Text==null)
            {
                pageNameEntry.BackgroundColor = Color.LightPink;
            }
            else
            {
                confirmNameLabel.Text = pageNameEntry.Text;
                pageNameEntry.IsVisible = false;
                confirmNameLabel.IsVisible = true;
                submitButton.IsVisible = false;
                confirmButton.IsVisible = true;
                statusLabel.Text = "Confirm Page Name";
            }
        }

        protected override void OnAppearing()
        {
            Console.WriteLine("poop");
            Console.WriteLine(backgroundPhoto.Width);
            Console.WriteLine("poop");
            Console.WriteLine(backgroundPhoto.Source);
        }

        public void LoadData()
        {
            pageList.Clear();
            string pagePhotos = client.DownloadString("https://www.cvx4u.com/ActionBook/getPage.php?isGetPage=0&mode=fourPhotos&userID=" + userID);
            Console.WriteLine("toys3");
            backgroundPhoto.Source = "https://www.cvx4u.com/ActionBook/userPhotos/B" + userID.Substring(1) + ".png";
            userPhotoDisplay.Source= "https://www.cvx4u.com/ActionBook/userPhotos/U" + userID.Substring(1) + ".png";
        
            Console.WriteLine(pagePhotos);
            JArray pageItems = JArray.Parse(pagePhotos);
            //Console.WriteLine(pageItems.);
            foreach (JObject thisObj in pageItems)
            {
                //JObject theThing = JObject.Parse(thisObj);
                pageList.Add(new Page(thisObj.GetValue("PageID").ToString(), thisObj.GetValue("Title").ToString(), thisObj.GetValue("ContentPieces").ToString(), JArray.Parse(thisObj.GetValue("ContentPieces").ToString())));
            }

            Console.WriteLine(pageList.Count);

            pageListDisplay.ItemsSource = null;
            pageListDisplay.ItemsSource = pageList;


            //DependencyService.Get<IFirebaseProcess>().GetImageSize(backgroundPhoto);


            //backgroundPhoto.ScaleX = 768 / backgroundPhoto.Width;
            //backgroundPhoto.ScaleY= mainStack.Width / backgroundPhoto.Width;
            BindingContext = this;
        }

        public void GoToHelp(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HelpPage());
        }

        public ViewProfile(string profileID, string profileName)
        {
            InitializeComponent();
            userID = profileID;
            Console.WriteLine("toys1");
            mainMenu.IsVisible = false;
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
            editProfile.Clicked += (s, e) =>
            {
                Navigation.PushAsync(new EditProfile());
            };
            logoutButton.Clicked += (sender, arg) => {

                DependencyService.Get<IFirebaseProcess>().Logout();

            };
            
            goBack.Clicked += (s, e) =>
            {
                Navigation.PushAsync(new MainPage());
            };
            if (App.currentUser.userID==profileID)
            {
                addPageButton.IsVisible = true;
            }
            else
            {
                addPageButton.IsVisible = false;
            }
            Console.WriteLine("toys2");
            confirmButton.IsVisible = false;
            confirmNameLabel.IsVisible = false;
            createPageWindow.IsVisible = false;
            userNameDisplay.Text=profileName;
            string pagePhotos = client.DownloadString("https://www.cvx4u.com/ActionBook/getPage.php?isGetPage=0&mode=fourPhotos&userID="+profileID);
            Console.WriteLine("toys3");
            backgroundPhoto.Source = "https://www.cvx4u.com/ActionBook/userPhotos/B" + profileID.Substring(1) + ".png";
            Console.WriteLine(pagePhotos);
            JArray pageItems = JArray.Parse(pagePhotos);
            //Console.WriteLine(pageItems.);
            foreach(JObject thisObj in pageItems)
            {
                //JObject theThing = JObject.Parse(thisObj);
                pageList.Add(new Page(thisObj.GetValue("PageID").ToString(), thisObj.GetValue("Title").ToString(), thisObj.GetValue("ContentPieces").ToString(),JArray.Parse(thisObj.GetValue("ContentPieces").ToString())));
            }

            Console.WriteLine(pageList.Count);

            
            pageListDisplay.ItemsSource = pageList;

            
            //DependencyService.Get<IFirebaseProcess>().GetImageSize(backgroundPhoto);
          
            
            //backgroundPhoto.ScaleX = 768 / backgroundPhoto.Width;
            //backgroundPhoto.ScaleY= mainStack.Width / backgroundPhoto.Width;
            BindingContext = this;
            
            

        }
    }
}
