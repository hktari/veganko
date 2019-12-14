﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veganko.Extensions;
using Veganko.Services.Http;
using Veganko.ViewModels.PasswordRecovery;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Veganko.Views.PasswordRecovery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ValidateOTPPage : ContentPage
    {
        private readonly ForgotPasswordViewModel vm;

        public ValidateOTPPage(ForgotPasswordViewModel vm)
        {
            InitializeComponent();
            BindingContext = this.vm = vm;
        }
    }
}