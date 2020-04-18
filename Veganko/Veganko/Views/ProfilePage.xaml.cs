﻿using Veganko.Extensions;
using Autofac;
using System;
using Veganko.Other;
using Veganko.Services.Auth;
using Veganko.Services.Http;
using Veganko.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using Veganko.ViewModels.Products.ModRequests.Partial;

namespace Veganko.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : BaseContentPage
    {
        private ProfileViewModel vm;
        public ProfilePage()
        {
            InitializeComponent();
            vm = (ProfileViewModel)BindingContext;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            Debug.Assert(Parent != null);

            // Icon has to be bound to the NavigationPage that is the parent of ProfilePage
            if (Device.RuntimePlatform == Device.iOS)
            {
                var parentNavPage = this.Parent as Xamarin.Forms.NavigationPage;
                if (parentNavPage != null)
                { 
                    parentNavPage.IconImageSource = new FontImageSource { FontFamily = "Material Icons", Glyph = MaterialDesignIcons.AccountCircle };
                }
            }
            else
            {
                Parent?.SetBinding(Xamarin.Forms.NavigationPage.IconImageSourceProperty, new Binding("AvatarImage", source: vm));
            }
        }

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            App.IoC.Resolve<IAuthService>().Logout();
            App.Current.MainPage = new Xamarin.Forms.NavigationPage(new Loginpage());
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                await vm.SaveProfile();
                await this.Inform("Profilna spremenjena !");

            }
            catch (ServiceException ex)
            {
                await this.Err(ex.StatusCodeDescription);
            }
        }

        void OnDeleteProductClicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            vm.DeleteProdModReqCommand.Execute((ProductModRequestViewModel)mi.CommandParameter);
        }
        private void ProductSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args?.SelectedItem == null)
            {
                return;
            }

            vm.ProductSelectedCommand.Execute(args.SelectedItem);

            listView.SelectedItem = null;
        }
    }
}