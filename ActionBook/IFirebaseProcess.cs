using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ActionBook
{
    public interface IFirebaseProcess
    {
        void CreateNewUser(string handle,string email, string password);
        bool IsUserSignedIn();
        void ChangeUserPassword(string newPassword);
      
        void ChangeUserEmail(string newEmail);
        void Login(string email, string password, bool isEmail);
        string GetCurrentUID();
        void Logout();
        void GetImageSize(Image input);
        
    }
}
