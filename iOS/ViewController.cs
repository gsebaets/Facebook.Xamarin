using System;
using Facebook.LoginKit;
using Facebook.CoreKit;
using UIKit;
using System.Drawing;
using CoreGraphics;
using Google.SignIn;

namespace Login.iOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        LoginButton loginView;
        ProfilePictureView pictureView;
        UILabel nameLabel;


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // If was send true to Profile.EnableUpdatesOnAccessTokenChange method
            // this notification will be called after the user is logged in and
            // after the AccessToken is gotten
            Profile.Notifications.ObserveDidChange((sender, e) =>
            {

                if (e.NewProfile == null)
                    return;

                nameLabel.Text = e.NewProfile.Name;
            });

            // Set the Read and Publish permissions you want to get
            loginView = new LoginButton(new CGRect(80, 500, 220, 46))
            {
                LoginBehavior = LoginBehavior.Native,
            };

            // Handle actions once the user is logged in
            loginView.Completed += (sender, e) =>
            {
                if ((e.Error != null))
                {
                    // Handle if there was an error
#pragma warning disable CS0618
                    new UIAlertView("Login", e.Error.Description, null, "Ok", null).Show();
#pragma warning restore CS0618
                    return;
                }

                if (e.Result.IsCancelled)
                {
                    // Handle if the user cancelled the login request
#pragma warning disable CS0618 // Type or member is obsolete
                    new UIAlertView("Login", "The user cancelled the login", null, "Ok", null).Show();
#pragma warning restore CS0618 // Type or member is obsolete
                    return;
                }

                // Handle your successful login
#pragma warning disable CS0618
                new UIAlertView("Login", "Success!!", null, "Ok", null).Show();
#pragma warning restore CS0618
            };

            // Handle actions once the user is logged out
            loginView.LoggedOut += (sender, e) =>
            {
                // Handle your logout
#pragma warning disable CS0618
                new UIAlertView("Logout", "Loging-Out!!", null, "Ok", null).Show();
#pragma warning restore CS0618
            };

            //UserProfilePic
            pictureView = new ProfilePictureView(new CGRect(80, 100, 220, 220));

            //UseName
            nameLabel = new UILabel(new Rectangle(80, 300, 220, 220));


            // Add views to main view
            View.AddSubview(loginView);
            View.AddSubview(pictureView);
            View.AddSubview(nameLabel);

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use. 
        }
    }
}