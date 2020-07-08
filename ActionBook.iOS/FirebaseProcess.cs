using System;
using ActionBook;
using Xamarin.Forms;
using Firebase.Auth;
using Firebase.Core;
[assembly: Dependency(typeof(IFirebaseAuth))]
namespace ActionBook.iOS
{
    public class iosFirebaseAuth: IFirebaseAuth
    {
        

        public void CreateNewUser(string handle, string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
