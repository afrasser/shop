﻿using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Data.Entities
{
    public class OrderDetailTmp : IEntity
    {
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Product Product { get; set; }

        // Use this filed to store price history
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value { get; set; }
    }
}
