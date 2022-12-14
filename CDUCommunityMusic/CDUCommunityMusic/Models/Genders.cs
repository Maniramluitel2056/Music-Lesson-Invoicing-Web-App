// using required library
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CDUCommunityMusic.Models
{
    //model:Genders
    public class Genders
    { 
        //principle key
        public int Id { get; set; }

        [Required, Display(Name = "Types of Gender")]
        public string Gender { get; set; }
        
    }
}
