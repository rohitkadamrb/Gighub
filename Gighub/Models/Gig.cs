using System;
using System.ComponentModel.DataAnnotations;

namespace Gighub.Models
{
    public class Gig
    {
        public int Id { get; set; }


        public ApplicationUser Artist { get; set; }
        [Required]
        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        [StringLength(255)]
        [Required]
        public string Venue { get; set; }


        public Genre Genre { get; set; }
        [Required]
        public byte GenreId { get; set; }

        public bool IsCancelled { get; set; }
    }



}