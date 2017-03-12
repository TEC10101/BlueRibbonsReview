using System;
using Blue_Ribbon.DAL;
using System.ComponentModel.DataAnnotations;            

namespace Blue_Ribbon.Models
{
    public class ReviewLog
    {
        /// <summary>
        /// This is the Id for each review, this is the primary key
        /// </summary>
        [Key]
        public int ReviewLogId { get; set; }

        /// <summary>
        /// The ASIN "Amazon Standard Identification Number"
        /// </summary>
        public string ASIN { get; set; }

        /// <summary>
        /// Refers to the vendor/seller/API used for that particular item that's needing review
        /// </summary>
        public int WebsiteAPIId { get; set; }

        /// <summary>
        /// When customer presses "Get coupon code" for the in-house reviews system.
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SelectedDate { get; set; }

        /// <summary>
        /// Once user presses Submit button on their review
        /// </summary>
        public bool CustomerReviewed { get; set; } = false;

        /// <summary>
        /// When user submits reviews and the client-side checks pass
        /// </summary>
        public bool? AutomaticValidation { get; set; }

        /// <summary>
        /// Triggers by server-side checks if 3 rating or less or foul language to show up in Admin Dashboard
        /// </summary>
        public bool? NeedsAdminReview { get; set; }

        /// <summary>
        /// Once Admin reviews, this will be set true
        /// </summary>
        public bool? AdminReviewed { get; set; }

        /// <summary>
        /// This is the switch to have the review publicly acessible
        /// </summary>
        public bool DisplayReview { get; set; } = false;

        /// <summary>
        /// A rating of 1-5, Five being the highest
        /// </summary>
        public int? Rating { get; set; }

        /// <summary>
        /// The date the user created the review
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateReviewed { get; set; }

        //TODO implement the [Email] annotation similar to the implementation in Vendor.cs
        /// <summary>
        /// Fkey between customer.Email ReviewLog.Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Review title
        /// </summary>
        public string ReviewSubject { get; set; } = "";

        /// <summary>
        /// Minimum of 70 words, represents the customer review
        /// </summary>
        public string ReviewBody { get; set; } = "";

        /// <summary>
        /// Yes is True
        /// </summary>
        public bool? WouldBuyAgain { get; set; }

        /// <summary>
        /// Yes is True
        /// </summary>
        public bool? RecToFriend { get; set; }

        private BRContext db = new BRContext();
    }
}