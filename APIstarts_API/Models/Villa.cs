﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIstarts_API.Models
{
    public class Villa      //veritabanında yer alan nesne gibi veri tutar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int Occupancy { get; set; }

        public int Sqft { get; set; }

        public string Details { get; set; }

        public double Rate { get; set; }

        public string ImageUrl { get; set; }

        public string Amenity { get; set; }


    }
}
