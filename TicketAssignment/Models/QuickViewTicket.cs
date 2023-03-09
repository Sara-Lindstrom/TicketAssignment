using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketAssignment.Models.Entities;

namespace TicketAssignment.Models
{
    public class QuickViewTicket
    {
        public int TicketId { get; set; }
        public string Title { get; set; } = null!;
        public StatusEnum Status { get; set; }
        public DateTime CreatedTime { get; set; }
        public SeverityEnum Severity { get; set; }
        public string TimeRemaining { get; set; }
    }
}
