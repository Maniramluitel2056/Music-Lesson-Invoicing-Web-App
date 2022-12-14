using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CDUCommunityMusic.Models
{
    public class Letter
    {
        public int Id { get; set; }

        [ForeignKey("Students"), Display(Name = "Student")]
        public int StudentId { get; set; }
        // Navigation properties to Model: Students
        public virtual Students Students { get; set; }
        
        [Required, Display(Name = "Reference ID.")]
        public string Reference
        {
            get
            {
                String lastName;
                if (Students != null) lastName = Students.LastName;
                else lastName = new String("unknown");
                return DateTime.Now.Year + lastName + Id.ToString();
            }
        }
        
        public enum Status
        {
            Paid,
            Unpaid
        }
      public Status Payment { get; set; }

    public string InitialComment { get; set; }
    public string BankName { get; set; }
    public string AccountName { get; set; }
    public string AccountNumber { get; set; }

    public string BSB { get; set; }

    public enum Terms
    {
        Term1,
        Term2,
        Term3,
        Term4
    }
    public Terms Term { get; set; }

    public enum Semester
    {
        Semester1,
        Semester2
    }
    public Semester CurrentSemestern { get; set; }
    public string CurrentYear { get; set; }
    public DateTime TermStartDate { get; set; }
    public decimal TotalCost { get; set;}
    public List<Lessons> Lessons { get; set; }

    }
}
