﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Gighub.Models
{
    public class Gig
    {
        public int Id { get; set; }

        [Required]
        public ApplicationUser Artist { get; set; }

        public DateTime DateTime { get; set; }

        [StringLength(255)]
        [Required]
        public string Venue { get; set; }

        [Required]
        public Genre Genre { get; set; }


    }



}