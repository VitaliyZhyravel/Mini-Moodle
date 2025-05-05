
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mini_Moodle.Models.Domain
{
    public class Course
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime CreatedAt { get; set; } = DateTime.Now.Date;
        [MaxLength(40)]
        public string CreatedBy { get; set; } 

        public List<Lesson> Lessons { get; set; }
    }
}
