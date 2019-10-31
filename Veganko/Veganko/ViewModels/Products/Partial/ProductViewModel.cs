﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Veganko.Models;
using Xamarin.Forms;

namespace Veganko.ViewModels.Products.Partial
{
    public class ProductViewModel : BaseViewModel
    {
        public ProductViewModel()
        {

        }

        public ProductViewModel(ProductViewModel productViewModel)
        {
            Update(productViewModel);
        }

        public ProductViewModel(Product product)
        {
            Update(product);
        }

        public string Id { get; set; }

        public byte[] ImageBase64Encoded { get; set; }

        // ??
        public ProductState State { get; set; }

        public string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string brand;
        public string Brand
        {
            get => brand;
            set => SetProperty(ref brand, value);
        }

        public string barcode;
        public string Barcode
        {
            get => barcode;
            set => SetProperty(ref barcode, value);
        }

        public string imageName;
        public string ImageName
        {
            get => imageName;
            set => SetProperty(ref imageName, value);
        }

        public ImageSource image;
        public ImageSource Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }

        public string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public ObservableCollection<ProductClassifier> productClassifiers;
        public ObservableCollection<ProductClassifier> ProductClassifiers
        {
            get => productClassifiers;
            set => SetProperty(ref productClassifiers, value);
        }

        public ProductType type;
        public ProductType Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }

        public int rating;
        public int Rating
        {
            get => rating;
            set => SetProperty(ref rating, value);
        }

        public void Update(ProductViewModel productViewModel)
        {
            Rating = productViewModel.Rating;
            Type = productViewModel.Type;
            ProductClassifiers = new ObservableCollection<ProductClassifier>(productViewModel.ProductClassifiers);
            Description = productViewModel.Description;
            ImageBase64Encoded = productViewModel.ImageBase64Encoded;
            Image = productViewModel.Image;
            ImageName = productViewModel.ImageName;
            Barcode = productViewModel.Barcode;
            Brand = productViewModel.Brand;
            Name = productViewModel.Name;
            Id = productViewModel.Id;
        }

        public void Update(Product product)
        {
            Rating = product.Rating;
            Type = product.Type;
            ProductClassifiers = product.ProductClassifiers;
            Description = product.Description;
            ImageBase64Encoded = product.ImageBase64Encoded;
            Image = ImageSource.FromStream(() => new MemoryStream(ImageBase64Encoded));
            ImageName = null;
            Barcode = product.Barcode;
            Brand = product.Brand;
            Name = product.Name;
            Id = product.Id;
        }

        public void MapToModel(Product product)
        {
            product.Rating = Rating;
            product.Type = Type;
            product.ProductClassifiers = ProductClassifiers;
            product.Description = Description;
            product.ImageBase64Encoded = ImageBase64Encoded;
            product.Barcode = Barcode;
            product.Brand = Brand;
            product.Name = Name;
            product.Id = Id;
        }
    }
}