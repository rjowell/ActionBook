using Foundation;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using UIKit;

namespace ActionBookShare
{

    /*$photoURL=$_POST["photoURL"];
$headline=$_POST["headline"];
$contentURL=$_POST["contentURL"];
$userID=$_POST["userID"];
$contentType=$_POST["type"];
$page=$_POST["page"];  postContent.php
*/

    public class PickerController : UIPickerViewModel
    {


        List<String[]> pickerItems { get; set; }
        List<UILabel> newPickerThings { get; set; }
        List<string> newIndex { get; set; }
        public string selectedItem { get; set; }




        public string getSelectedItem()
        {
            return selectedItem;
        }


        /*public PickerController(List<String[]> items)
        {
            pickerItems = items;
        }*/

        public PickerController(List<UILabel> things, List<string> indexes)
        {
            newPickerThings = things;
            newIndex = indexes;
        }


        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            //return pickerItems.Count;
            return newPickerThings.Count;
        }

        public override nfloat GetRowHeight(UIPickerView pickerView, nint component)
        {
            return 22f;
        }

        public override UIView GetView(UIPickerView pickerView, nint row, nint component, UIView view)
        {
            return newPickerThings[(int)row];
        }
        /*public override string GetTitle(UIPickerView picker, nint row, nint component)
        {
            return pickerItems[(int)row][1];
        }*/
        public override void Selected(UIPickerView picker, nint row, nint component)
        {
            //selectedItem = pickerItems[(int)picker.SelectedRowInComponent(0)][0];
            selectedItem = newIndex[(int)picker.SelectedRowInComponent(0)];
        }
    }


    public partial class FinalizeViewController : UIViewController
    {
        public UIImage inputImage;
        public string storyHeadline;
        public string imageURL;
        public string pageURL;
        public string currentUserId;
        WebClient client;
        List<string[]> pages = new List<string[]>();
        List<UILabel> newPickerItems = new List<UILabel>();
        List<string> pickerIndex = new List<string>();
        //List<KeyValuePair<string,string>> pages = new List<KeyValuePair<string,string>();


        public FinalizeViewController (IntPtr handle) : base (handle)
        {
            
        }

        string contentType;
        public void ChangeButton(object sender, EventArgs e)
        {
            UIButton sentButton = (UIButton)sender;
            
            if(sentButton.Tag==0)
            {
                contentType = "0";

                learnButton.Selected = true;
                learnButton.Layer.BorderColor = UIColor.Red.CGColor;
                learnButton.Layer.BorderWidth = 3;
                //learnBackground.BackgroundColor = UIColor.SystemBlueColor;

                doButton.Selected = false;
                doButton.Layer.BorderColor = UIColor.Clear.CGColor;
                doButton.Layer.BorderWidth = 3;

                giveButton.Selected = false;
                giveButton.Layer.BorderColor = UIColor.Clear.CGColor;
                giveButton.Layer.BorderWidth = 3;

            }
            else if(sentButton.Tag==1)
            {

                contentType = "1";

                learnButton.Selected = false;
                learnButton.Layer.BorderColor = UIColor.Clear.CGColor;
                learnButton.Layer.BorderWidth = 3;
                //learnBackground.BackgroundColor = null;

                doButton.Selected = true;
                doButton.Layer.BorderColor = UIColor.Red.CGColor;
                doButton.Layer.BorderWidth = 3;

                giveButton.Selected = false;
                giveButton.Layer.BorderColor = UIColor.Clear.CGColor;
                giveButton.Layer.BorderWidth = 3;
            }
            else
            {
                contentType = "2";

                learnButton.Selected = false;
                learnButton.Layer.BorderColor = UIColor.Clear.CGColor;
                learnButton.Layer.BorderWidth = 3;

                doButton.Selected = false;
                doButton.Layer.BorderColor = UIColor.Clear.CGColor;
                doButton.Layer.BorderWidth = 3;

                giveButton.Selected = true;
                giveButton.Layer.BorderColor = UIColor.Red.CGColor;
                giveButton.Layer.BorderWidth = 3;
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //learnBackground.BackgroundColor = null;

            //giveBackground.BackgroundColor = null;
            //doBackground.BackgroundColor = null;
            var userDefaults = new NSUserDefaults("group.com.cvxtech.ActionBook", NSUserDefaultsType.SuiteName);
            currentUserId = userDefaults.StringForKey("current_user");
            client = new WebClient();
            
           
            JArray data=JArray.Parse(client.DownloadString("https://www.cvx4u.com/ActionBook/getPage.php?userID=" + currentUserId));
            foreach(JObject thing in data)
            {
                string[] thisThing = new string[2];
                thisThing[0] = thing.GetValue("PageID").ToString();
                thisThing[1] = thing.GetValue("Title").ToString();
                UILabel currentLabel = new UILabel();
                currentLabel.Text= thing.GetValue("Title").ToString();
                currentLabel.Font= UIFont.FromName("MyriadPro-Bold", 19f);
                currentLabel.TextColor = UIColor.FromRGB(214f,255f,214f);
                currentLabel.TextAlignment = UITextAlignment.Center;
                newPickerItems.Add(currentLabel);
                pickerIndex.Add(thing.GetValue("PageID").ToString());

                pages.Add(thisThing);
            }
            Console.WriteLine(newPickerItems.Count);
            //Console.WriteLine(data);
            //debuggingLabelOne.Text = pages.Count.ToString();
            //pagePicker.Model = new PickerController(pages);
            pagePicker.Model = new PickerController(newPickerItems, pickerIndex);
            finalizePostLabel.Font= UIFont.FromName("MyriadPro-Bold", 23f);
            chooseContentLabel.Font= UIFont.FromName("MyriadPro-Bold", 23f);
            choosePageLabel.Font= UIFont.FromName("MyriadPro-Bold", 23f);
            submitButton.Font=UIFont.FromName("MyriadPro-Bold", 23f);
            cancelButton.Font = UIFont.FromName("MyriadPro-Bold", 23f);
            learnButton.SetBackgroundImage(UIImage.FromFile("Images/LearnIcon.png"),UIControlState.Normal);
            learnButton.Tag = 0;
            learnButton.TouchDown += ChangeButton;
            doButton.SetBackgroundImage(UIImage.FromFile("Images/ActIcon.png"), UIControlState.Normal);
            doButton.Tag = 1;
            doButton.TouchDown += ChangeButton;

            giveButton.SetBackgroundImage(UIImage.FromFile("Images/GiveIcon.png"), UIControlState.Normal);
            giveButton.Tag = 2;
            giveButton.TouchDown += ChangeButton;

            selectedImage.Image = inputImage;
            
            headlineLabel.Text = storyHeadline;
            headlineLabel.Font= UIFont.FromName("MyriadPro-Bold", 19f);

            cancelButton.TouchDown += (sender, e) =>
              {
                  ExtensionContext.CompleteRequest(null, null);
              };



            submitButton.TouchDown += (sender,e) =>
                {

                    if (contentType != null)
                    {
                        NameValueCollection submissionData = new NameValueCollection();
                        submissionData.Set("photoURL", imageURL);
                        submissionData.Set("headline", storyHeadline);
                        submissionData.Set("contentURL", pageURL);
                        submissionData.Set("userID", currentUserId);
                        submissionData.Set("page", pages[(int)pagePicker.SelectedRowInComponent(0)][0]);
                        submissionData.Set("type", contentType);

                        client.UploadValues("https://www.cvx4u.com/ActionBook/postContent.php", submissionData);

                        ExtensionContext.CompleteRequest(null, null);
                    }
                };
            }
        }
    }
 