﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Veganko.Common.Models.Products
{
    public class ProductModRequest
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        /// <summary>
        /// The reference to the product being edited with this mod request.
        /// </summary>
        public string ExistingProductId { get; set; }

        public ProductModRequestAction Action { get; set; }
        
        public DateTime Timestamp { get; set; }
        
        [Required]
        public UnapprovedProduct UnapprovedProduct { get; set; }
        
        public string ChangedFields { get; set; }
    }
}
