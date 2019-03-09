﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Veganko.Models;

[assembly: Xamarin.Forms.Dependency(typeof(Veganko.Services.MockProductDataStore))]
namespace Veganko.Services
{
    public class MockProductDataStore : IDataStore<Product>
    {
        List<Product> items;

        public MockProductDataStore()
        {
            items = new List<Product>();
            var mockItems = new List<Product>
            {
              new Product
                {
                    Id = "0",
                    Name = "Vegan Cheese", Description = "100% VEGAN",
                    Image = "img_product_tmp_2.jpg", Rating = 5,
                    Type = ProductType.Hrana,
                    ProductClassifiers = new ObservableCollection<ProductClassifier>
                    {
                        ProductClassifier.Vegansko,
                        ProductClassifier.GlutenFree
                    }
                },
              new Product
                {
                    Id = "1",
                    Name = "Cheese Face Message Cream", Description = "Creamy",
                    Image = "img_product_tmp_2.jpg", Rating = 5,
                    Type = ProductType.Kozmetika,
                    ProductClassifiers = new ObservableCollection<ProductClassifier>
                    {
                        ProductClassifier.CrueltyFree,
                        ProductClassifier.Vegansko
                    }
                },
              new Product
                {
                    Id = "2",
                    Name = "Violife Mozarella Cheese", Description = "From soya with love",
                    Image = "img_product_tmp_2.jpg", Rating = 5,
                    Type = ProductType.Hrana,
                    ProductClassifiers = new ObservableCollection<ProductClassifier>
                    {
                        ProductClassifier.Vegansko
                    }
                },
                new Product
                {
                    Id = "3",
                    Name = "Lepotna krema", Description = "Za fajn namazane roke",
                    Image = "img_product_tmp_2.jpg",
                    Type = ProductType.Kozmetika,
                    ProductClassifiers = new ObservableCollection<ProductClassifier>
                    {
                        ProductClassifier.Vegansko,
                        ProductClassifier.GlutenFree,
                        ProductClassifier.CrueltyFree
                    }
                },
                new Product
                {
                    Id = "4",
                    Name = "Čokoladni namaz", Description = "Kdo pa nima rad nutelle... Še posebej, če je vegan.",
                    Image = "evrokrem.jpg",
                    Type = ProductType.Hrana,
                    ProductClassifiers = new ObservableCollection<ProductClassifier>
                    {
                        ProductClassifier.Vegansko,
                        ProductClassifier.GlutenFree
                    }
                },
                new Product
                {
                    Id = "5",
                    Name = "Knusprige Vollkornwaffeln", Description = "100% Vollkorn und weniger Zucker !",
                    Image = "manner.jpg",
                    Type = ProductType.Hrana,
                    ProductClassifiers = new ObservableCollection<ProductClassifier>
                    {
                        ProductClassifier.Vegansko
                    }
                },
                new Product
                {
                    Id = "6",
                    Name = "Sensitiv After Shave Balsam", Description = "MEN",
                    Image = "alverde_after_shave.jpg",
                    Type = ProductType.Kozmetika,
                    ProductClassifiers = new ObservableCollection<ProductClassifier>
                    {
                        ProductClassifier.CrueltyFree,
                        ProductClassifier.Vegansko
                    }
                },
                new Product
                {
                    Id = "7",
                    Name = "Valsoia la crema", Description = "Kremni namaz z lešniki, kakavom in sojo",
                    Brand = "VALSOIA",
                    Image = "evrokrem.jpg",
                    Type = ProductType.Hrana,
                    ProductClassifiers = new ObservableCollection<ProductClassifier>
                    {
                        ProductClassifier.Vegansko,
                        ProductClassifier.GlutenFree
                    }
                },
                new Product
                {
                    Id = "8",
                    Name = "Gourmet Arašidov Namaz s koščki",
                    Brand = "GOURMET",
                    Image = "arasidovo_maslo.jpg",
                    Type = ProductType.Hrana,
                    ProductClassifiers = new ObservableCollection<ProductClassifier>
                    {
                        ProductClassifier.Vegansko,
                        ProductClassifier.GlutenFree
                    }
                },
                new Product
                {
                    Id = "9",
                    Name = "BIO Pomarančni sok",
                    Brand = "DM",
                    Image = "dmbio_orangensaft.jpg",
                    Type = ProductType.Pijaca,
                    ProductClassifiers = new ObservableCollection<ProductClassifier>
                    {
                        ProductClassifier.Vegansko,
                        ProductClassifier.GlutenFree,
                        ProductClassifier.RawVegan,
                        ProductClassifier.Pesketarijansko
                    }
                }
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Product item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Product item)
        {
            var _item = items.Where((Product arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Product item)
        {
            var _item = items.Where((Product arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Product> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Product>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}