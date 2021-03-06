﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WgSystems.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, ForeignKey("Category")]
        public int CategoryId { get; set; }


        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }
    }
}
