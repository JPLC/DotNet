using System.ComponentModel.DataAnnotations.Schema;
using TrelloModel;

namespace TrelloMVC.Models
{
    public class UserContent
    {
        public int Id { get; set; }
        public string Access { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId{ get; set; }
        [ForeignKey("Board")]
        public int BoardId { get; set; }
    }
}