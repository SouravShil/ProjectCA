using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chap_App.Models
{
    public class Message
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string message { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }

    }
}
