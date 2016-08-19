using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RFtennisTeam.Models
{
    [Table("dbo.NoteBoard")]
    public class NoteBoard
    {
        public int Id { get; set; }
        [Display(Name = "Note")]
        [Required]
        [MinLength(10)]
        public string note { get; set; }
        [Display(Name = "User")]
        public string userName { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
    }
}