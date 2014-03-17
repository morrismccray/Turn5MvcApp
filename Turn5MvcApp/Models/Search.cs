using System.ComponentModel.DataAnnotations;

namespace Turn5MvcApp.Models
{
    public class Search
    {
        public Search()
        {
            Sort = "Price:ASC";
        }

        //The form must contain simple javascript validation to ensure the text box is not empty and contains at least 3 characters 
        [Required, MinLength(3, ErrorMessage = "Search Term field must contain at least 3 characters")]
        public string Term { get; set; }

        public string Sort { get; set; }
        public SearchXML Results { get; set; }
    }
}