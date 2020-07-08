using System;
using Xamarin.Forms;
using ActionBook;
[assembly: Dependency(typeof(IFirebaseAuth))]
namespace ActionBook.Droid
{
    public class AndroidFirebaseAuth: IFirebaseAuth
    {
        public AndroidFirebaseAuth()
        {
        }

        public void CreateNewUser(string handle, string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
