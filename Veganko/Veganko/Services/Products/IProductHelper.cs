﻿using System;
using System.Collections.Generic;
using System.Text;
using Veganko.Models;

namespace Veganko.Services.Products
{
    public interface IProductHelper
    {
        void AddImageUrls(Product product);
    }
}
