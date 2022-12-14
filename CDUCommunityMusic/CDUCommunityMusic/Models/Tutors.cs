using Microsoft.Build.Framework;

namespace CDUCommunityMusic.Models
{
    //model : Tutors
    public class Tutors
    {
        //Principle key
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
