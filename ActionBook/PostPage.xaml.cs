using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace ActionBook
{
    public partial class PostPage : ContentPage
    {
        WebClient client = new WebClient();

        string currentPostID;

        

        protected override void OnAppearing()
        {


           

            Console.WriteLine(currentPostID);
            JObject postData = JObject.Parse(client.DownloadString("https://www.cvx4u.com/ActionBook/getPost.php?postID=" + currentPostID));
            postImage.Source = "https://www.cvx4u.com/ActionBook/photos/" + currentPostID + ".jpg";
            Console.WriteLine("https://www.cvx4u.com/ActionBook/getLikes.php&postID=P" + currentPostID);
            string likeData = client.DownloadString(new Uri("https://www.cvx4u.com/ActionBook/getLikes.php?postID=P" + currentPostID));
            
            goBack.Clicked += (s, e) =>
              {
                  Navigation.PopAsync();
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
            goBack.Clicked += (s, e) =>
            {
                Navigation.PushAsync(new MainPage());
            };
            editProfile.Clicked += (s, e) =>
            {
                Navigation.PushAsync(new EditProfile());
            };
            logoutButton.Clicked += (sender, arg) => {

                DependencyService.Get<IFirebaseProcess>().Logout();

            };
            JObject likes = JObject.Parse(likeData);
            Console.WriteLine(likes);
            JArray likeArray = JArray.Parse(likes.GetValue("Likes").ToString());
            JArray dontLikeArray = JArray.Parse(likes.GetValue("DontLikes").ToString());
            JArray shareArray = JArray.Parse(likes.GetValue("Shares").ToString());
            storyTitle.Text = postData.GetValue("Title").ToString();
            
            TapGestureRecognizer linkTgp = new TapGestureRecognizer();
            linkTgp.Tapped += (s, e) =>
            {
                Browser.OpenAsync(new Uri(postData.GetValue("URL").ToString()),BrowserLaunchMode.SystemPreferred);
            };
            storyTitle.GestureRecognizers.Add(linkTgp);
            profileLogo.Source = postData.GetValue("ProfilePhoto").ToString();
            profileLogo.ClassId= postData.GetValue("UserID").ToString();
            Console.WriteLine("posted name");
            Console.WriteLine(postData.GetValue("PostedBy").ToString());
            if (profileLogo.AutomationId == null)
            {
                profileLogo.AutomationId = postData.GetValue("PostedBy").ToString();
            }
            linkToProfile.Text = postData.GetValue("PostedBy").ToString();
            linkToProfile.ClassId = postData.GetValue("UserID").ToString();
            
            linkToPage.Text = postData.GetValue("PageName").ToString();
            linkToPage.ClassId = postData.GetValue("UserID").ToString();
            if (linkToPage.AutomationId == null)
            {
                linkToPage.AutomationId = postData.GetValue("PostPage").ToString();
            }
            storySource.Text = new Uri(postData.GetValue("URL").ToString()).Host;
            typeImage.Source = App.typeImages[Convert.ToInt32(postData.GetValue("Type").ToString())];
            likeCount.Text = likeArray.Count.ToString();
            Console.WriteLine("likes"+ postData.GetValue("Likes").ToString());
            likeButton.Text = postData.GetValue("Likes").ToString();
            dontLikeCount.Text = dontLikeArray.Count.ToString();
            dontLikeButton.Text = postData.GetValue("DontLikes").ToString();
            Console.WriteLine("dlikes" + postData.GetValue("DontLikes").ToString());
            shareCount.Text = shareArray.Count.ToString();
            Console.WriteLine(likeArray);
            Console.WriteLine(App.currentUser.userID);
            Console.WriteLine(likeArray.Contains(App.currentUser.userID));
            if (likes.GetValue("Likes").ToString().Contains(App.currentUser.userID))
            {
                likeCount.Text += "✔";
                dontLikeButton.BackgroundColor = Color.Black;
                dontLikeButton.Opacity = 0.2f;
                dontLikeButton.IsEnabled = false;
            }
            if (likes.GetValue("DontLikes").ToString().Contains(App.currentUser.userID))
            {
                dontLikeCount.Text += "✔";
                likeButton.BackgroundColor = Color.Black;
                likeButton.Opacity = 0.2f;
                likeButton.IsEnabled = false;
            }
            if (likes.GetValue("Shares").ToString().Contains(App.currentUser.userID))
            {
                shareCount.Text += "✔";
            }
        }

        public void SetContent(string postID)
        {
            currentPostID = postID;

        }

        public void ShowFlagMenu(object sender, EventArgs e)
        {
            flagMenu.IsVisible = true;
        }

        public void ProcessIssue(object sender, EventArgs e)
        {
            flagMenu.IsVisible = false;
            if (((Button)sender).ClassId == "offensive")
            {
               if (client.DownloadString("https://www.cvx4u.com/ActionBook/processFlags.php?postID=" + currentPostID + "&userID=" + App.currentUser.userID + "&flagType=0") == "1")
                {
                    flagWindowLabel.Text = "Thanks! We'll take a look!";
                    flagWindow.IsVisible = true;
                   
                }
            }
            if (((Button)sender).ClassId == "wrong")
            {
                Console.WriteLine("wrong");
                if (client.DownloadString("https://www.cvx4u.com/ActionBook/processFlags.php?postID=" + currentPostID + "&userID=" + App.currentUser.userID + "&flagType=1") == "1")
                {
                    flagWindowLabel.Text = "Thanks! We'll take a look!";
                    flagWindow.IsVisible = true;
                   
                }
            }
            if (((Button)sender).ClassId == "hideposts")
            {
                Navigation.PushAsync(new MainPage());

            }
        }

        public PostPage(string postID)
        {
            currentPostID = postID;
            InitializeComponent();
            mainMenu.IsVisible = false;
            
        }

        public void DismissFlagWindow(object sender, EventArgs e)
        {
            flagWindow.IsVisible = false;
        }

        async void OpenProfilePage(object sender, EventArgs args)
        {

            try
            {
                Button sentButton = (Button)sender;

                await Navigation.PushAsync(new ViewProfile(sentButton.ClassId, sentButton.Text));
            }
            catch (InvalidCastException ice)
            {
                ImageButton sentButton = (ImageButton)sender;
                await Navigation.PushAsync(new ViewProfile(sentButton.ClassId, sentButton.AutomationId));
            }


        }

        public void ShowLikesDontShares(object sender, EventArgs e)
        {

            //WebClient client = new WebClient();
            Console.WriteLine(currentPostID);
            Button senderButton = (Button)sender;
            Console.WriteLine("Button status");
            Console.WriteLine(senderButton.Text);
            if (senderButton.ClassId == "like")
            {
                
                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("userID", App.currentUser.userID);
                nvc.Add("postID", currentPostID);
                if (likeCount.Text.IndexOf('✔') == -1)
                    nvc.Add("type", "Likes");
                else
                    nvc.Add("type", "Likes-");
                string newLikes = Encoding.UTF8.GetString(client.UploadValues("https://www.cvx4u.com/ActionBook/postLikes.php", nvc));

                if (likeCount.Text.IndexOf('✔') == -1)
                {
                    likeCount.Text = newLikes + "✔";
                    likeButton.Text= newLikes;
                    likeButton.IsEnabled = true;
                    likeButton.BackgroundColor = Color.Transparent;
                    likeButton.Opacity = 1.0f;

                    dontLikeButton.IsEnabled = false;
                    dontLikeButton.BackgroundColor = Color.Black;
                    dontLikeButton.Opacity = 0.2f;
                }
                else
                {
                    likeCount.Text = newLikes;
                    likeButton.Text = newLikes;
                    dontLikeButton.IsEnabled = true;
                    dontLikeButton.BackgroundColor = Color.Transparent;
                    dontLikeButton.Opacity = 1.0f;

                    likeButton.IsEnabled = true;
                    likeButton.BackgroundColor = Color.Transparent;
                    likeButton.Opacity = 1.0f;

                   
                }

            


            }
            else if (senderButton.ClassId == "dontLike")
            {
                Console.WriteLine("dontlike");
                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("userID", App.currentUser.userID);
                nvc.Add("postID", currentPostID);
                if (dontLikeCount.Text.IndexOf('✔') == -1)
                    nvc.Add("type", "DontLikes");
                else
                    nvc.Add("type", "DontLikes-");
                string newDontLikes = Encoding.UTF8.GetString(client.UploadValues("https://www.cvx4u.com/ActionBook/postLikes.php", nvc));

               
                        if (dontLikeCount.Text.IndexOf('✔') == -1)
                        {
                            dontLikeCount.Text = newDontLikes + "✔";
                            dontLikeButton.Text = newDontLikes;
                            likeButton.IsEnabled = false;
                            likeButton.BackgroundColor = Color.Black;
                            likeButton.Opacity = 0.2f;

                            dontLikeButton.IsEnabled = true;
                            dontLikeButton.BackgroundColor = Color.Transparent;
                            dontLikeButton.Opacity = 1.0f;
                        }

                        else
                        {
                            dontLikeButton.Text = newDontLikes;
                            dontLikeCount.Text = newDontLikes;
                            likeButton.IsEnabled = true;
                            likeButton.BackgroundColor = Color.Transparent;
                            likeButton.Opacity = 1f;

                            dontLikeButton.IsEnabled = true;
                            dontLikeButton.BackgroundColor = Color.Transparent;
                            dontLikeButton.Opacity = 1f;
                        }

                
            }
            else if (senderButton.ClassId == "share")
            {

                Console.WriteLine("share");
                /*
                if (senderButton.Text.IndexOf('✔') != -1)
                {
                    NameValueCollection nvc = new NameValueCollection();
                    nvc.Add("userID", App.currentUser.userID);
                    nvc.Add("postID", senderButton.AutomationId);
                    nvc.Add("type", "Shares-");
                    string newShares = Encoding.UTF8.GetString(client.UploadValues("https://www.cvx4u.com/ActionBook/postLikes.php", nvc));
                    /*foreach (PostItem thisThing in postItems)
                    {
                        //Console.WriteLine(thisThing.ContentID);
                        if (thisThing.ContentID.Equals(senderButton.AutomationId))
                        {
                            //Console.WriteLine("it worrrrredd");
                            thisThing.Shares = newShares;
                        }
                    }*/
                /*
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

            */
            }
        }
    }
}
