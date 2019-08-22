﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegankoService.Models;

namespace VegankoService.Data
{
    public interface IProductRepository
    {
        PagedList<Product> GetAll(int page, int count = 10);
        Product Get(string id);
        void Create(Product product);
        void Update(Product product);
        void Delete(string id);
    }
}