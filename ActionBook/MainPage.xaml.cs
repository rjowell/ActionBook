
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ActionBook
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer


    public class PostItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string userName { get; set; }
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("UserName"));
            }
        }

        private string userID { get; set; }
        public string UserID
        { get { return userID; }
            set
            {
                userID = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("UserID"));
            }
        }

        private string postCaption { get; set; }
        public string PostCaption
        {
            get { return postCaption; }
            set
            {
                postCaption = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("PostCaption"));
            }
        }

        private string userPhoto { get; set; }
        public string UserPhoto
        {
            get { return userPhoto; }
            set
            {
                userPhoto = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("UserPhoto"));
            }
        }

        private string linkURL { get; set; }
        public string LinkURL
        {
            get { return linkURL; }
            set
            {
                linkURL = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("LinkURL"));
            }
        }

        private string imageURL { get; set; }
        public string ImageURL
        {
            get { return imageURL; }
            set
            {
                imageURL = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ImageURL"));
            }
        }

        private string contentID { get; set; }

        public string ContentID
        {
            get { return contentID; }
            set
            {
                contentID = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ContentID"));
            }
        }


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

        private string pageName { get; set; }
        public string PageName
        {
            get { return pageName; }
            set
            {
                pageName = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("PageName"));
            }
        }

        private string likes { get; set; }
        public string Likes
        {
            get { return likes; }
            set
            {
                likes = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Likes"));
            }
        }

        private string dontLikes { get; set; }
        public string DontLikes
        {
            get { return dontLikes; }
            set
            {
                dontLikes = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("DontLikes"));
            }
        }

        private string shares { get; set; }
        public string Shares
        {
            get { return shares; }
            set
            {
                shares = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Shares"));
            }
        }

        private string postDate { get; set; }
        public string PostDate
        {
            get { return postDate; }
            set
            {
                postDate = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("PostDate"));
            }
        }

        private bool likesOn { get; set; }
        public bool LikesOn
        {
            get { return likesOn; }
            set
            {
                likesOn = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("LikesOn"));
            }
        }


        private bool gestureOn { get; set; }
        public bool GestureOn
        {
            get { return gestureOn; }
            set
            {
                gestureOn = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("GestureOn"));
            }
        }

        private bool sensitiveOn { get; set; }
        public bool SensitiveOn
        {
            get { return sensitiveOn; }
            set
            {
                sensitiveOn = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SensitiveOn"));
            }
        }


        private bool dontLikesOn { get; set; }
        public bool DontLikesOn
        {
            get { return dontLikesOn; }
            set
            {
                dontLikesOn = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("DontLikesOn"));
            }
        }

        private Color likeButtonBackground { get; set; }
        public Color LikeButtonBackground
        {
            get { return likeButtonBackground; }
            set
            {
                likeButtonBackground = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("LikeButtonBackground"));
            }
        }

        private Color dontLikeButtonBackground { get; set; }
        public Color DontLikeButtonBackground
        {
            get { return dontLikeButtonBackground; }
            set
            {
                dontLikeButtonBackground = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("DontLikeButtonBackground"));
            }
        }

        private float likeButtonOpacity { get; set; }
        public float LikeButtonOpacity
        {
            get { return likeButtonOpacity; }
            set
            {
                likeButtonOpacity = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("LikeButtonOpacity"));
            }
        }

        private float dontLikeButtonOpacity { get; set; }
        public float DontLikeButtonOpacity
        {
            get { return dontLikeButtonOpacity; }
            set
            {
                dontLikeButtonOpacity = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("DontLikeButtonOpacity"));
            }
        }

        private string typeImageSource { get; set; }
        public string TypeImageSource
        {
            get { return typeImageSource; }
            set
            {
                typeImageSource = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("TypeImageSource"));
            }
        }

        private bool flagMenuVisible { get; set; }
        public bool FlagMenuVisible
        {
            get { return flagMenuVisible; }
            set
            {
                flagMenuVisible = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("FlagMenuVisible"));
            }
        }
        private double shareOpacity { get; set; }
        public double ShareOpacity
        {
            get { return shareOpacity; }
            set
            {
                shareOpacity = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ShareOpacity"));
            }
        }

        private bool shareEnabled { get; set; }
        public bool ShareEnabled
        {
            get { return shareEnabled; }
            set
            {
                shareEnabled = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ShareEnabled"));
            }
        }

        private string picBackgroundColor { get; set; }
        public string PicBackgroundColor
        {
            get { return picBackgroundColor; }
            set
            {
                picBackgroundColor = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("PicBackgroundColor"));
            }
        }

        private bool isShare { get; set; }
        public bool IsShare
        {
            get { return isShare; }
            set
            {
                isShare = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsShare"));
            }
        }

        //#D7F9D8
        //#e9fce9






        public void AddCheck(int input)
        {
            if(input==0)
            {
                likes += "✔";
                Console.WriteLine("likes poop");
                likesOn = true;
                dontLikesOn = false;
                likeButtonBackground = Color.Transparent;
                dontLikeButtonBackground = Color.Black;
                likeButtonOpacity = 1.0f;
                dontLikeButtonOpacity = 0.2f;
            }
            else if(input==1)
            {
                dontLikes += "✔";
                Console.WriteLine("dont likes poop");
                likesOn = false;
                dontLikesOn = true;
                likeButtonBackground = Color.Black;
                dontLikeButtonBackground = Color.Transparent;
                likeButtonOpacity = 0.2f;
                dontLikeButtonOpacity = 1.0f;
            }
            else
            {
                shares += "✔";
            }
        }

        public PostItem(string contentIDinput, string userNameInput, string userIDInput, string userPhotoURL, string postCaptionInput, string dateInput, string linkURLinput, string imageURLInput, string pageIDinput, string pageNameInput, string likesIn, string dontLikesIn, string sharesIn, string typeIn, string explicitIn, bool isShared)
        {
            userName = userNameInput;
            contentID = contentIDinput;
            userID = userIDInput;
            isShare = isShared;
            if(isShared==true)
            {
                picBackgroundColor = "#F1FEF5";
            }
            else
            {
                picBackgroundColor = "#D7F9D8";
            }


            if(userIDInput==App.currentUser.userID)
            {
                shareOpacity = 0.4;
                shareEnabled = false;
            }
            else
            {
                shareOpacity = 1.0;
                shareEnabled = true;
            }
            flagMenuVisible = false;

            if(explicitIn=="0")
            {
                sensitiveOn = false;
                gestureOn = true;
            }
            else
            {
                sensitiveOn = true;
                gestureOn = false;
            }
            
            userPhoto = userPhotoURL;
            postCaption = postCaptionInput;
            pageID = pageIDinput;
            pageName = pageNameInput;
            linkURL = linkURLinput;
            likeButtonBackground = Color.Transparent;
            dontLikeButtonBackground = Color.Transparent;
           
            imageURL = imageURLInput;
            likes = likesIn;
            Console.WriteLine("hamburger");
            dontLikes = dontLikesIn;
            likesOn = true;
            likeButtonOpacity = 1.0f;
            dontLikeButtonOpacity = 1.0f;
            Console.WriteLine("hamburger0");
            shares = sharesIn;
            dontLikesOn = true;
            Console.WriteLine("hamburger1");
            postDate = dateInput;
            //11252019053253
            string[] months = { "","Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            postDate = "Posted On: "+months[Convert.ToInt32(dateInput.Substring(4, 2))] + " " + dateInput.Substring(6, 2);

            //DateTime dt = DateTime.Parse(dateInput);
            Console.WriteLine("hamburge2");
            //Console.WriteLine(dt.Month);
            if (typeIn=="0")
            {
                typeImageSource = "https://www.cvx4u.com/ActionBook/app_assets/LearnIcon.png";
            }
            else if(typeIn=="1")
            {
                typeImageSource = "https://www.cvx4u.com/ActionBook/app_assets/ActIcon.png";
            }
            else
            {
                typeImageSource = "https://www.cvx4u.com/ActionBook/app_assets/GiveIcon.png";
            }



        }

    }

    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        //private ObservableCollection<PostItem> postItems;
        //public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<PostItem> postItems { get; set; }
        public ObservableCollection<Button> likeButtons { get; set; }
        public ObservableCollection<Button> dontButtons { get; set; }
        

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.Current.MainPage = App.loginPage;
            LoadData();
            
            
            
        }

        WebClient client = new WebClient();
        Label shareToPageCount;
        Label likeCount;
        public static readonly BindableProperty CountProperty = BindableProperty.Create("Count", typeof(string), typeof(MainPage), null);
        Label dontLikeCount;

        public string CountString
        {
            get { return (String)GetValue(CountProperty); }
            set { SetValue(CountProperty, value); }
        }

        public void GoToHelp(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HelpPage());
        }

        public void ShowSensitiveButton(object sender, EventArgs e)
        {
            ((Button)sender).IsVisible = false;
            foreach(PostItem thisThing in postItems)
            {
                if(thisThing.ContentID == ((Button)sender).ClassId)
                {
                    thisThing.GestureOn = true;
                }
            }
        }

        



        public void LoadData()
        {
            //itemList.IsVisible = false;
            postItems.Clear();
            itemList.ItemsSource = null;
            string rawData = client.DownloadString("https://www.cvx4u.com/ActionBook/getFeed.php?userID="+App.currentUser.userID);
            string likeData = client.DownloadString("https://www.cvx4u.com/ActionBook/getLikes.php");
            JObject likes = JObject.Parse(likeData);
            JArray objects = JArray.Parse(rawData);

            foreach (JObject thisThing in objects)
            {


                postItems.Add(new PostItem(thisThing.GetValue("ID").ToString(),
                    thisThing.GetValue("PostedBy").ToString(), thisThing.GetValue("UserID").ToString(),
                    thisThing.GetValue("ProfilePhoto").ToString(), thisThing.GetValue("Title").ToString(),
                    thisThing.GetValue("PostDate").ToString(), thisThing.GetValue("URL").ToString(),
                    thisThing.GetValue("Photo").ToString(), thisThing.GetValue("PostPage").ToString(),
                    thisThing.GetValue("PageName").ToString(), thisThing.GetValue("Likes").ToString(),
                    thisThing.GetValue("DontLikes").ToString(), thisThing.GetValue("Shares").ToString(),
                    thisThing.GetValue("Type").ToString(),thisThing.GetValue("isExplicit").ToString(),false));

            }

            foreach (PostItem thing in postItems)
            {
                JObject currentItem = JObject.Parse(likes.GetValue("P" + thing.ContentID).ToString());
                List<string> likeArray = JArray.Parse(currentItem.GetValue("Likes").ToString()).ToObject<List<string>>();
                List<string> dontLikeArray = JArray.Parse(currentItem.GetValue("DontLikes").ToString()).ToObject<List<string>>();
                List<string> shareArray = JArray.Parse(currentItem.GetValue("Shares").ToString()).ToObject<List<string>>();
                string currentString = "P" + thing.ContentID;
                if (currentItem.GetValue("Likes").ToString().Contains(App.currentUser.userID) == true)
                {
                    thing.AddCheck(0);
                }
                if (currentItem.GetValue("DontLikes").ToString().Contains(App.currentUser.userID) == true)
                {
                   thing.AddCheck(1);
                }
                if (currentItem.GetValue("Shares").ToString().Contains(App.currentUser.userID) == true)
                {
                   thing.AddCheck(2);
                }
                
            }
            
            itemList.ItemsSource = postItems;
       
            BindingContext = this;
            
            itemList.IsVisible = true;
            loadingWindow.IsVisible = false;
        }

        
        public MainPage()
        {
            
            postItems = new ObservableCollection<PostItem>();

           
            
            
        Console.WriteLine("step 7");

            InitializeComponent();

            itemList.RefreshCommand = new Command(() => {

                itemList.IsRefreshing = true;
                LoadData();
                itemList.IsRefreshing = false;

            });

            TapGestureRecognizer flagTgp = new TapGestureRecognizer();
            flagTgp.Tapped += (s, e) =>
            {


              
            };

            mainMenu.IsVisible = false;
            logoutButton.Clicked += (sender, arg) => {

                DependencyService.Get<IFirebaseProcess>().Logout();

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
            MessagingCenter.Subscribe<IFirebaseProcess>(this, "Logout", (sender) => {

                Console.WriteLine("bopper11");
                
                Navigation.PopToRootAsync();
                //Navigation.PushAsync(new Login());

            });

            editPages.Clicked+= (s,e)=>
            {
                //Navigation.PushAsync(new EditPages());
               Navigation.PushAsync(new ViewProfile(App.currentUser.userID, App.currentUser.currentHandle));
            };

            editProfile.Clicked += (s, e) =>
            {
                Navigation.PushAsync(new EditProfile());
            };


            

            menuButton.GestureRecognizers.Add(menuTgp);
            //itemList.ItemTemplate = itemDataTemplate;
            Console.WriteLine("step 7-1");
            Console.WriteLine(postItems.Count);
            //itemList.ItemsSource = postItems;
            Console.WriteLine("step 7-2");
            //BindingContext = this;
            //LoadData();
            Console.WriteLine("step 7-3");
            pickerLayout.IsVisible = false;
            
            shareCancel.Clicked += (s, e) => { pickerLayout.IsVisible = false; };




            userIdentifier.Text = "Welcome, " + App.currentUser.currentHandle;


        }

       /* public void OpenProfile(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewProfile());
        }*/

        public void ShowLikesDontShares(object sender, EventArgs e)
        {

            //WebClient client = new WebClient();
            
            Button senderButton = (Button)sender;
            Console.WriteLine("Button status");
            Console.WriteLine(senderButton.Text);
            if (senderButton.ClassId=="like")
            {
                Console.WriteLine("like");
                Console.WriteLine(senderButton.AutomationId);

                NameValueCollection nvc = new NameValueCollection();
                    nvc.Add("userID", App.currentUser.userID);
                    nvc.Add("postID", senderButton.AutomationId);
                    if(senderButton.Text.IndexOf('✔') == -1)
                        nvc.Add("type", "Likes");
                    else
                        nvc.Add("type", "Likes-");
                    string newLikes = Encoding.UTF8.GetString(client.UploadValues("https://www.cvx4u.com/ActionBook/postLikes.php", nvc));

                    foreach (PostItem thisThing in postItems)
                    {
                        //Console.WriteLine(thisThing.ContentID);
                        if (thisThing.ContentID.Equals(senderButton.AutomationId))
                        {
                        //Console.WriteLine("it worrrrredd");

                            if (senderButton.Text.IndexOf('✔') == -1)
                            {
                                thisThing.Likes = newLikes + "✔";
                                thisThing.LikesOn = true;
                                thisThing.LikeButtonBackground = Color.Transparent;
                                thisThing.LikeButtonOpacity = 1.0f;
                                
                                thisThing.DontLikesOn = false;
                                thisThing.DontLikeButtonBackground = Color.Black;
                                thisThing.DontLikeButtonOpacity = 0.2f;
                            }
                            else
                            {
                                thisThing.Likes = newLikes;
                                thisThing.DontLikesOn = true;
                                thisThing.DontLikeButtonBackground = Color.Transparent;
                                thisThing.DontLikeButtonOpacity = 1f;

                                thisThing.LikesOn = true;
                                thisThing.LikeButtonBackground = Color.Transparent;
                                thisThing.LikeButtonOpacity = 1f;
                        }
                    }
                }
                
               
            }
            else if(senderButton.ClassId=="dontLike")
            {
                Console.WriteLine("dontlike");
                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("userID", App.currentUser.userID);
                nvc.Add("postID", senderButton.AutomationId);
                if (senderButton.Text.IndexOf('✔') == -1)
                    nvc.Add("type", "DontLikes");
                else
                    nvc.Add("type", "DontLikes-");
                string newDontLikes= Encoding.UTF8.GetString(client.UploadValues("https://www.cvx4u.com/ActionBook/postLikes.php", nvc));

                foreach (PostItem thisThing in postItems)
                {
                    //Console.WriteLine(thisThing.ContentID);
                    if (thisThing.ContentID.Equals(senderButton.AutomationId))
                    {
                        //Console.WriteLine("it worrrrredd");
                        if (senderButton.Text.IndexOf('✔') == -1)
                        {
                            thisThing.DontLikes = newDontLikes + "✔";
                            thisThing.LikesOn = false;
                            thisThing.LikeButtonBackground = Color.Black;
                            thisThing.LikeButtonOpacity = 0.2f;

                            thisThing.DontLikesOn = true;
                            thisThing.DontLikeButtonBackground = Color.Transparent;
                            thisThing.DontLikeButtonOpacity = 1.0f;
                        }
                            
                        else
                        {
                            thisThing.DontLikes = newDontLikes;
                            thisThing.LikesOn = true;
                            thisThing.LikeButtonBackground = Color.Transparent;
                            thisThing.LikeButtonOpacity = 1f;

                            thisThing.DontLikesOn = true;
                            thisThing.DontLikeButtonBackground = Color.Transparent;
                            thisThing.DontLikeButtonOpacity = 1f;
                        }
                            
                        //thisThing.DontLikes = newDontLikes + "✔";
                    }
                }
            }
            else if(senderButton.ClassId == "share")
            {

                Console.WriteLine("share");

                if (senderButton.Text.IndexOf('✔') != -1)
                {
                    NameValueCollection nvc = new NameValueCollection();
                    nvc.Add("userID", App.currentUser.userID);
                    nvc.Add("postID", senderButton.AutomationId);
                    nvc.Add("type", "Shares-");
                    string newShares = Encoding.UTF8.GetString(client.UploadValues("https://www.cvx4u.com/ActionBook/postLikes.php", nvc));
                    foreach (PostItem thisThing in postItems)
                    {
                        //Console.WriteLine(thisThing.ContentID);
                        if (thisThing.ContentID.Equals(senderButton.AutomationId))
                        {
                            //Console.WriteLine("it worrrrredd");
                            thisThing.Shares = newShares;
                        }
                    }

                }
                else
                {

                    JArray pageData = JArray.Parse(client.DownloadString("https://www.cvx4u.com/ActionBook/getPages.php?userID=" + App.currentUser.userID));
                    List<string> pageNames = new List<string>();
                    List<string> pageIDs = new List<string>();
                    foreach (JObject pageItem in pageData)
                    {
                        pageNames.Add(pageItem.GetValue("Title").ToString());
                        pageIDs.Add(pageItem.GetValue("PageID").ToString());
                    }
                    pagePicker.ItemsSource = pageNames;
                    pickerLayout.IsVisible = true;
                    Console.WriteLine("spaghetti");
                    shareSubmit.Clicked += (s, ei) => {

                        NameValueCollection nvc = new NameValueCollection();
                        nvc.Add("userID", App.currentUser.userID);
                        nvc.Add("postID", senderButton.AutomationId);
                        nvc.Add("type", "Share");
                        nvc.Add("postPage", pageIDs[pagePicker.SelectedIndex]);

                        string newShares = Encoding.UTF8.GetString(client.UploadValues("https://www.cvx4u.com/ActionBook/postLikes.php", nvc));

                        foreach (PostItem thisThing in postItems)
                        {
                            //Console.WriteLine(thisThing.ContentID);
                            if (thisThing.ContentID.Equals(senderButton.AutomationId))
                            {
                                //Console.WriteLine("it worrrrredd");
                                thisThing.Shares = newShares + "✔";
                            }
                        }
                        pickerLayout.IsVisible = false;

                    };
                }
                
              
            }
        }


        public void ProcessIssue(object sender, EventArgs e)
        {
            if(((Button)sender).ClassId=="offensive")
            {
                //Console.WriteLine("offensive");
                //Console.WriteLine(client.DownloadString("https://www.cvx4u.com/ActionBook/processFlags.php?postID=" + ((Button)sender).AutomationId + "&userID=" + App.currentUser.userID + "&flagType=0"));
                if (client.DownloadString("https://www.cvx4u.com/ActionBook/processFlags.php?postID="+((Button)sender).AutomationId+"&userID="+App.currentUser.userID+"&flagType=0")=="1")
                {
                    flagWindowLabel.Text = "Thanks! We'll take a look!";
                    flagWindow.IsVisible = true;
                    foreach(PostItem thing in postItems)
                    {
                        if(thing.ContentID == ((Button)sender).AutomationId)
                        {
                            thing.FlagMenuVisible = false;
                        }
                    }
                }
            }
            if (((Button)sender).ClassId == "wrong")
            {
                Console.WriteLine("wrong");
                if (client.DownloadString("https://www.cvx4u.com/ActionBook/processFlags.php?postID=" + ((Button)sender).AutomationId + "&userID=" + App.currentUser.userID + "&flagType=1") == "1")
                {
                    flagWindowLabel.Text = "Thanks! We'll take a look!";
                    flagWindow.IsVisible = true;
                    foreach (PostItem thing in postItems)
                    {
                        if (thing.ContentID == ((Button)sender).AutomationId)
                        {
                            thing.FlagMenuVisible = false;
                        }
                    }
                }
            }
            if (((Button)sender).ClassId == "hideposts")
            {
                Console.WriteLine("hide");
                foreach (PostItem item in postItems.ToList())
                    {
                        if(item.ContentID == ((Button)sender).AutomationId)
                        {
                            if (client.DownloadString("https://www.cvx4u.com/ActionBook/processUserBlocks.php?blockedUserID=" + item.UserID + "&userID=" + App.currentUser.userID) == "1")
                            {
                                postItems.Remove(item);
                            }
                        }
                    }
                flagWindowLabel.Text = "Thanks! We've removed this user's posts";
                flagWindow.IsVisible = true;
                foreach (PostItem thing in postItems)
                {
                    if (thing.ContentID == ((Button)sender).AutomationId)
                    {
                        thing.FlagMenuVisible = false;
                    }
                }

            }
        }

        public void DismissFlagWindow(object sender, EventArgs e)
        {
            flagWindow.IsVisible = false;
        }

        public void ShowFlagMenu(object sender, EventArgs e)
        {
            ImageButton sentButton = (ImageButton)sender;
            foreach(PostItem things in postItems)
            {
                if(things.ContentID==sentButton.ClassId)
                {
                    if(things.FlagMenuVisible==false)
                    {
                        things.FlagMenuVisible = true;
                    }
                    else
                    {
                        things.FlagMenuVisible = false;
                    }
                }
            }
         
        }

        public void ShowInfo(object sender, EventArgs e)
        {
            Button sent = (Button)sender;
            Console.WriteLine(sent.ClassId);
            PostPage page = new PostPage(sent.ClassId);
            Console.WriteLine("cheese");
            page.SetContent(sent.ClassId);
            Console.WriteLine("burns");
            Navigation.PushAsync(page);
        }

        public async void OpenPage(object sender, EventArgs e)
        {
            Console.WriteLine("dweeb");
            Button senderButton = (Button)sender;
            Console.WriteLine("poop in pants");
            await Navigation.PushAsync(new ViewPage(senderButton.ClassId,senderButton.AutomationId));
        }

        async void OpenProfilePage(object sender, EventArgs args)
        {
            
            try
            { Button sentButton = (Button)sender;
                
                await Navigation.PushAsync(new ViewProfile(sentButton.ClassId, sentButton.Text));
            }
            catch(InvalidCastException ice)
            {
                ImageButton sentButton = (ImageButton)sender;
                await Navigation.PushAsync(new ViewProfile(sentButton.ClassId, sentButton.AutomationId));
            }
            
            
        }
    }
}
