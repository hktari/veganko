﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using ZXing.Mobile;
using Microsoft.WindowsAzure.MobileServices;
using Veganko.Services;
using System.Threading.Tasks;
using System.Runtime.Remoting.Contexts;
using FFImageLoading.Forms.Platform;
using Android.Content;
using Android.Provider;
using Java.IO;
using Android.Support.V4.Content;
using Java.Text;
using Java.Util;
using Android.Graphics;
using Android.Util;
using System.IO;
using Java.Nio;
using System.Runtime.InteropServices;
using Android.Media;

namespace Veganko.Droid
{
    [Activity(Label = "Veganko", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public partial class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IAuthenticate
    {
        public static MainActivity Context { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);

            Context = this;
            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);
            ImageCircleRenderer.Init();
            CachedImageRenderer.Init(true);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            // Initialize the scanner first so it can track the current context
            MobileBarcodeScanner.Initialize(this.Application);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);
            CurrentPlatform.Init();
            // Initialize the authenticator before loading the app.
            App.Init((IAuthenticate)this);

            LoadApplication(new App());
        }
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            OnRequestPermissionsResult_PhotoPicking(requestCode, permissions, grantResults);
        }

        #region FB_Auth
        // Define a authenticated user.
        private MobileServiceUser user;
        
        public async Task<bool> Authenticate()
        {
            var success = false;
            var message = string.Empty;
            try
            {
                // Sign in with Facebook login using a server-managed flow.
                user = await App.MobileService.LoginAsync(this,
                    MobileServiceAuthenticationProvider.Facebook, "facebook");
                if (user != null)
                {
                    message = string.Format("you are now signed-in as {0}.",
                        user.UserId);
                    success = true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            // Display the success or failure message.
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetMessage(message);
            builder.SetTitle("Sign-in result");
            builder.Create().Show();

            return success;
        }
        #endregion
    }
}

