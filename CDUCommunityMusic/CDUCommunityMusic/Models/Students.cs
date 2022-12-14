// using required library
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace CDUCommunityMusic.Models
{
    public class Students
    {
       //Principle key
        public int Id { get; set; }

        //ensuring user input and customizing field display name 
        [Required, Display(Name = "Given Name")]

        //limiting user input to 400 char
        [StringLength(400)]
        public string FirstName { get; set; }

        //Customize error message
        [Required(ErrorMessage = "Last Name Must be Entered"), Display(Name = "Last Name")]
        [StringLength(400)]
        public string LastName { get; set; }

        //Adding strings for Full Name 
        [Display(Name = "Full Name")]
        public  string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Required, DataType(DataType.Date), Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }
        public int Age  
        { 
            get {
                //Calculating current age 
                int age = DateTime.Now.Year - DOB.Year;
                if (DateTime.Now.DayOfYear < DOB.DayOfYear)
                    age--;

                return age;
            }
        }


        [ForeignKey("Gender"), Display(Name = "Gender Type")]
        public int GenderId { get; set; }
        // Navigation properties to Model: Genders
        public virtual Genders Gender { get; set; }

        [Required, Display(Name = "Parents/Gurdian Name")]
        [StringLength(200)]
        public string GurdianName { get; set; }

        [Required, DataType(DataType.EmailAddress), Display(Name = "Payment Contact Email")]        
        public string EmailAddress { get; set; }

        [Required, DataType(DataType.PhoneNumber),  Display(Name = "Payment Contact Telephone")]
        //Ensuring Australian Mobile no.
        [RegularExpression(@"(?:\+?61)?(?:\(0\)[23478]|\(?0?[23478]\)?)\d{8}", ErrorMessage = "Entered phone format is not valid.")]
        public string ContactNumber { get; set; }


        // Navigation properties to Model: Lessons
        public virtual List <Lessons> Lessons { get; set; }

        
      
    }
}
