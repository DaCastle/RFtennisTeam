using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RFtennisTeam.Models
{
    public class MatchResults
    {
        public int Id { get; set; }
        [Display(Name = "Player 1")]
        public string player1 { get; set; }
        [Display(Name = "Player 2")]
        public string player2 { get; set; }
        [Display(Name = "Match Date")]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        [Display(Name = "Set Scores")]
        public string setScores { get; set; }
        [Display(Name = "Added By")]
        public string addedBy { get; set; }
    }
}