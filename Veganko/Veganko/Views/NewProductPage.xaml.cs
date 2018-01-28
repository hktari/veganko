﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Veganko.Models;
using System.Linq;
using Veganko.Extensions;
using System.Collections.ObjectModel;
using Veganko.ViewModels;

namespace Veganko.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewProductPage : BaseContentPage
    {
        public Dictionary<ProductType, List<ProductClassifier>> ClassifierDictionary => new Dictionary<ProductType, List<ProductClassifier>>
        {
            {
                ProductType.Hrana, new List<ProductClassifier> 
                {
                    ProductClassifier.Vegeterijansko,
                    ProductClassifier.Vegansko,
                    ProductClassifier.GlutenFree,
                    ProductClassifier.RawVegan,
                    ProductClassifier.Pesketarijansko}
            },
            {
                ProductType.Pijaca, new List<ProductClassifier>
                {
                    ProductClassifier.Vegeterijansko,
                    ProductClassifier.Vegansko,
                    ProductClassifier.GlutenFree,
                    ProductClassifier.RawVegan,
                    ProductClassifier.Pesketarijansko
                }
            },
            {
                ProductType.Kozmetika, new List<ProductClassifier>
                {
                    ProductClassifier.Vegeterijansko,
                    ProductClassifier.Vegansko,
                    ProductClassifier.CrueltyFree
                }
            }
        };

        NewProductViewModel vm;

        public NewProductPage()
        {
            InitializeComponent();

            BindingContext = vm = new NewProductViewModel();
            TypePicker.ItemsSource = Enum.GetNames(typeof(ProductType));
            TypePicker.SelectedIndexChanged += TypePickerSelectedIndexChanged;
        }

        private void TypePickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            var type = (ProductType)Enum.Parse(typeof(ProductType), picker.SelectedItem as string, true);
            SelectableProductClassifierView.Source = ClassifierDictionary[type] ?? new List<ProductClassifier>();
        }

        void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", vm.Product);
            var mainPage = App.Current.MainPage as TabbedPage;
            mainPage.CurrentPage = mainPage.Children[0];
        }

        async void Scan_Clicked(object sender, EventArgs e)
        {
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();

            var result = await scanner.Scan();

            if (result != null)
            {
                vm.Product.Barcode = result.Text;
                await DisplayAlert("Obvestilo", "Skeniranje končano !", "OK");
            }
            else
            {
                await DisplayAlert("Obvestilo", "Napaka pri skeniranju !", "OK");
            }
        }
    }
}