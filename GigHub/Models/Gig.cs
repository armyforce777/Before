using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigHub.Models
{

    /*
     * Anything changed to domain model have to change to sql server
     * we need to add a migration first and then update the database 
     * for this domain class change.
     */
    public class Gig
    {
        public int Id { get; set; }
        
        public ApplicationUser Artist { get; set; }

        //The reason why ArtistId is string type is because in ApplicationUser
        //the Id is string data type. Each user is uniquely identified using a guide,
        //which stores as a string.
        [Required]
        public string  ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        
        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }
    }
}