using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Blue_Ribbon.DAL;

namespace Blue_Ribbon.Models
{
    public class ReviewLog
    {
        
        public int ReviewLogId { get; set; }
        public int ASIN { get; set; }
        public int WebsiteAPIId { get; set; }
        public DateTime DateCodeGiven { get { return DateTime.Now; } }
        /*[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateCodeGiven { get; set; } */
        public bool CustomerReviewed { get; set; }
        public bool AutomaticValidation { get; set; }
        public bool AdminReviewed { get; set; }
        public bool DisplayReview { get; set; }
        public int Rating { get; set; }
        public DateTime DateReviewed { get { return DateTime.Now; } }
        public string CustomerId { get; set; }
        public string ReviewSubject { get; set; }
        public string ReviewBody { get; set; }
        public bool WouldBuyAgain { get; set; }
        public bool RecToFriend { get; set; }

        private BRContext db = new BRContext();
    }

   

    
}