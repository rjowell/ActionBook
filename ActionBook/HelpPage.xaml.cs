using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ActionBook
{
    public partial class HelpPage : ContentPage
    {
        public void GoToHelp(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HelpPage());
        }

        public HelpPage()
        {
            InitializeComponent();
            mainMenu.IsVisible = false;
            goBack.Clicked += (s, e) =>
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
            editProfile.Clicked += (s, e) =>
            {
                Navigation.PushAsync(new EditProfile());
            };
            logoutButton.Clicked += (sender, arg) => {

                DependencyService.Get<IFirebaseProcess>().Logout();

            };
        }
    }
}
