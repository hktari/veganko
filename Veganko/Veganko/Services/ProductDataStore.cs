﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Veganko.Models;
using Xamarin.Forms;
using XamarinImageUploader;

[assembly: Xamarin.Forms.Dependency(typeof(Veganko.Services.ProductDataStore))]
namespace Veganko.Services
{
    class ProductDataStore : IDataStore<Product>
    {
        List<Product> products;

        public async Task<bool> AddItemAsync(Product item)
        {
            await App.MobileService.GetTable<Product>().InsertAsync(item);
            return true;
        }

        public async Task<bool> DeleteItemAsync(Product item)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetItemsAsync(bool forceRefresh = false)
        {
            var products = await App.MobileService.GetTable<Product>().ToListAsync();
            foreach (var product in products)
            {
                if (product.ImageName == null)
                {
                    product.Image = ImageSource.FromFile("icon.png");
                }
                else
                {
                    product.ImageData = await ImageManager.GetImage(product.ImageName);
                    product.Image = ImageSource.FromStream(() => new MemoryStream(product.ImageData));
                }
            }
            return products;
        }
        
        public Task<bool> UpdateItemAsync(Product item)
        {
            throw new NotImplementedException();
        }
    }
}
