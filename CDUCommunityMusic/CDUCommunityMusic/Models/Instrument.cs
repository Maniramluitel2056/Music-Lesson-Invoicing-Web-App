// using required library
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CDUCommunityMusic.Models
{
    //model : Gender
    public class Instrument
    {
        //principle key
        public int Id { get; set; }
        [Required, Display(Name = "Name of Instrument")]
        public string Name { get; set; }
    }
}
