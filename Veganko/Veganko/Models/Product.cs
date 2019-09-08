﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using Veganko.Models.JsonConverters;
using System.IO;

namespace Veganko.Models
{
    public enum ProductState
    {
        PendingApproval,
        Approved
    }
    /// <summary>
    /// Classifiers to describe the type of the product, food or cosmetics
    /// </summary>
    public enum ProductClassifier
    {
        NOT_SET = 1,
        Vegansko = 2,
        Vegeterijansko = 4,
        GlutenFree = 8,
        RawVegan = 16,
        Pesketarijansko = 32,
        CrueltyFree = 64,
        Bio = 128,
        SoyFree = 256,
        NutFree = 512
    }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProductType
    {
        [EnumMember(Value = "NOT_SET")]
        NOT_SET,
        [EnumMember(Value = "FOOD")]
        Hrana,
        [EnumMember(Value = "BEVERAGE")]
        Pijaca,
        [EnumMember(Value = "COSMETIC")]
        Kozmetika
    }
    public class Product
    {
        public string Id { get; set; }

        public ProductState State { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public string Barcode { get; set; }

        [JsonIgnore]
        public string ImageName { get; set; }

        // TODO
        //public ImageSource Image => ImageSource.FromStream(() => new MemoryStream())
        [JsonIgnore]
        public ImageSource Image { get; set; }

        [JsonConverter(typeof(Base64StringToByteArrayConverter))]
        public byte[] ImageBase64Encoded { get; set; }

        public string Description { get; set; }

        [JsonConverter(typeof(DecimalProductClassifierListConverter))]
        public ObservableCollection<ProductClassifier> ProductClassifiers { get; set; }

        public ProductType Type { get; set; }

        public int Rating { get; set; }
        
        public Product()
        {
            Name = Brand = Barcode = Description = "";
            //Comments = new ObservableCollection<Comment>();
        }
    }
}
