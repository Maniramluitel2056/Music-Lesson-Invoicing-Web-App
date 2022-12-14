// using required library
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace CDUCommunityMusic.Models
{
    //Model : Lessons
    public class Lessons
    {
        //Principle Key
        public int Id { get; set; }
        
        [Required, ForeignKey("Students"), Display(Name = "Student")]
        public int StudentId { get; set; }
        // Navigation properties to Model: Students
        public virtual Students Students { get; set; }

        [Required,ForeignKey("Instruments"), Display(Name = "Instrument")]
        public int InstrumentId { get; set; }

        // Navigation properties to Model: Instruments
        public virtual Instrument Instruments { get; set; }

        [Required, ForeignKey("Tutors"), Display(Name = "Tutor")]
        public int TutorId { get; set; }
        // Navigation properties to Model: Tutors
        public virtual Tutors Tutors { get; set; }

        [Required, ForeignKey("Durations"), Display(Name = "Duration")]
        public int DurationsId { get; set; }
        // Navigation properties to Model: Durations
        public virtual Durations Durations { get; set; }

        [Required, Display(Name = "Lesson date and time")]
        [DataType(DataType.DateTime)]
        public DateTime DateNtime { get; set; }
        public int Term
        {
            get
            {
                //Obtain Term for lesson based on Lesson date
                DateTime firstday = new DateTime(DateNtime.Year, 1, 1);
                int numDays = (DateNtime - firstday).Days;
                return (int)(Math.Truncate((double)(numDays / ((DateTime.IsLeapYear(DateTime.Now.Year)? 366 : 365 /4)))) +1);
                
            }
        }
        [Display(Name = "Lesson Term and Year")]
        public string TermNyear
        {
            get
            {
                //adding strings Term and DateNTime for TermNYear
                return "Term " + Term + " Year " + DateNtime.Year;
            }
        }



        
        public bool PaymentStatus { get; set; }

    }
}