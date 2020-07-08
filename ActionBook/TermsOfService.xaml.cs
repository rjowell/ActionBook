using System;
using System.Collections.Generic;
using System.Net;
using Xamarin.Forms;

namespace ActionBook
{
    public partial class TermsOfService : ContentPage
    {
        public TermsOfService()
        {
            InitializeComponent();
            WebClient client = new WebClient();
            infoLabel.Text = client.DownloadString("https://www.cvx4u.com/ActionBook/app_assets/toc");
        }

        public void GoBack(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }

        public void GoAhead(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateAccount());
        }
    }
}
