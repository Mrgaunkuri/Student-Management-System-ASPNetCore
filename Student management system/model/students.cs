using System.ComponentModel.DataAnnotations;

namespace Student_management_system.model
{
    public class students
    {
        public int Id { get; set; }

        [Required, StringLength(100)] 
        public string FullName { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }

        [Required, StringLength(15)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Enrollment Number")]
        public string EnrollmentNumber { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Address")]
        public string address { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

    }
}
