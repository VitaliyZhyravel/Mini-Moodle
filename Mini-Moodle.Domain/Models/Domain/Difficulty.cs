using System.ComponentModel.DataAnnotations;

namespace Mini_Moodle.Models.Domain
{
    public class Difficulty 
    {
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public double DaysToExpire{ get; set; }

        List<Assignment> Assignments { get; set; }
    }
}

