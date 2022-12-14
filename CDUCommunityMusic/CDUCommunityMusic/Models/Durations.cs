// using required library
using System.ComponentModel.DataAnnotations;
namespace CDUCommunityMusic.Models
{
    //Model: Durations
    public class Durations
    {
        //Principle key
        public int Id { get; set; }
        [Required,DataType(DataType.Duration)]
        public string Minutes { get; set; }
        //defining datatype for cost
        [Required,DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        
    }
}
