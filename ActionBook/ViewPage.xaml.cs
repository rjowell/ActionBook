using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using ActionBook;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace ActionBook
{
    public partial class ViewPage : ContentPage
    {
        WebClient client = new WebClient();
        
        public ObservableCollection<PostItem> postItems { get; set; }

        bool isBig;

        public void Scroll(object sender, EventArgs e)
        {
            if(isBig==false)
            {
                topLayout.HeightRequest = 80;
            }
            else
            {
                topLayout.HeightRequest = 160;
            }
        }


        public void HandleItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            

            if(e.ItemIndex==0)
            {
                isBig = true;
                Console.WriteLine("scroll true");
            }
            
        }

        public void HandleItemDisappear(object sender, ItemVisibilityEventArgs e)
        {
            

            if (e.ItemIndex == 0)
            {
                isBig = false;
                Console.WriteLine("scroll false");
            }
            
        }

        public void ShowInfo(object sender, EventArgs e)
        {
            Button sent = (Button)sender;
            Console.WriteLine(sent.ClassId);
            PostPage page = new PostPage(sent.ClassId);
            page.SetContent(sent.ClassId);
            Navigation.PushAsync(page);
        }

        public void ProcessIssue(object sender, EventArgs e)
        {
            if (((Button)sender).ClassId == "offensive")
            {
                //Console.WriteLine("offensive");
                //Console.WriteLine(client.DownloadString("https://www.cvx4u.com/ActionBook/processFlags.php?postID=" + ((Button)sender).AutomationId + "&userID=" + App.currentUser.userID + "&flagType=0"));
                if (client.DownloadString("https://www.cvx4u.com/ActionBook/processFlags.php?postID=" + ((Button)sender).AutomationId + "&userID=" + App.currentUser.userID + "&flagType=0") == "1")
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
                    if (item.ContentID == ((Button)sender).AutomationId)
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
            foreach (PostItem things in postItems)
            {
                if (things.ContentID == sentButton.ClassId)
                {
                    if (things.FlagMenuVisible == false)
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

        public void ShowLikesDontShares(object sender, EventArgs e)
        {
            Button senderButton = (Button)sender;
            Console.WriteLine("Button status");
            Console.WriteLine(senderButton.Text);
            if (senderButton.ClassId == "like")
            {
                Console.WriteLine("like");
                Console.WriteLine(senderButton.AutomationId);

                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("userID", App.currentUser.userID);
                nvc.Add("postID", senderButton.AutomationId);
                if (senderButton.Text.IndexOf('✔') == -1)
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
            else if (senderButton.ClassId == "dontLike")
            {
                Console.WriteLine("dontlike");
                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("userID", App.currentUser.userID);
                nvc.Add("postID", senderButton.AutomationId);
                if (senderButton.Text.IndexOf('✔') == -1)
                    nvc.Add("type", "DontLikes");
                else
                    nvc.Add("type", "DontLikes-");
                string newDontLikes = Encoding.UTF8.GetString(client.UploadValues("https://www.cvx4u.com/ActionBook/postLikes.php", nvc));

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
            else if (senderButton.ClassId == "share")
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



        public ViewPage(string userID, string pageID)
        {
            InitializeComponent();
            Console.WriteLine("Cheeese");
            JObject pageData = JObject.Parse(client.DownloadString("https://www.cvx4u.com/ActionBook/getPage.php?isGetPage=1&userID=" + userID+"&pageID="+pageID));
            Console.WriteLine("boobd1");
            string likeData = client.DownloadString("https://www.cvx4u.com/ActionBook/getLikes.php");
            Console.WriteLine("boobd2");
            JObject likes = JObject.Parse(likeData);
            Console.WriteLine("information");
            Console.WriteLine(pageData);
            pageName.Text = pageData.GetValue("Title").ToString();
            userName.Text= pageData.GetValue("Handle").ToString();
            pickerLayout.IsVisible = false;
            mainMenu.IsVisible = false;
            backgroundPhoto.Source = ImageSource.FromUri(new Uri("https://www.cvx4u.com/ActionBook/userPhotos/"+App.currentUser.userID+"-"+pageID+".png"));
            JArray contentPieces = JArray.Parse(pageData.GetValue("ContentPieces").ToString());
            Console.WriteLine(contentPieces.Count);
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
            postItems = new ObservableCollection<PostItem>();
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

            foreach (string content in contentPieces)
            {
                JObject thisThing;
                bool isShare;

                if (content.IndexOf('S') == -1)
                {
                    thisThing = JObject.Parse(client.DownloadString("https://www.cvx4u.com/ActionBook/getPost.php?postID=" + content));
                    isShare = false;
                }
                else
                {
                    thisThing = JObject.Parse(client.DownloadString("https://www.cvx4u.com/ActionBook/getPost.php?postID=" + content.Substring(1)));
                    isShare = true;
                }
                Console.WriteLine(thisThing);
                postItems.Add(new PostItem(thisThing.GetValue("ID").ToString(),
                    thisThing.GetValue("PostedBy").ToString(), thisThing.GetValue("UserID").ToString(),
                    thisThing.GetValue("ProfilePhoto").ToString(), thisThing.GetValue("Title").ToString(),
                    thisThing.GetValue("PostDate").ToString(), thisThing.GetValue("URL").ToString(),
                    thisThing.GetValue("Photo").ToString(), thisThing.GetValue("PostPage").ToString(),
                    thisThing.GetValue("PageName").ToString(), thisThing.GetValue("Likes").ToString(),
                    thisThing.GetValue("DontLikes").ToString(), thisThing.GetValue("Shares").ToString(), thisThing.GetValue("Type").ToString(), thisThing.GetValue("isExplicit").ToString(),isShare));
                
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

            Console.WriteLine("here jim");


            BindingContext = this;
            Console.WriteLine("here jim1");
            itemList.ItemsSource = postItems;
            

            
        }
    }
}
