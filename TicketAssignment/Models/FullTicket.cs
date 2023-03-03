using System;
using System.Collections.Generic;
using TicketAssignment.Models.Entities;

namespace TicketAssignment.Models;

class FullTicket
{
    //Ticket
    public int TicketId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public StatusEnum Status { get; set; }
    public DateTime CreatedTime { get; set; }

    //Client
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    //SLA
    public SeverityEnum Severity { get; set; }
    public TimeSpan? TimeSpan { get; set; }

    //Comment
    public ICollection<CommentEntity>? Comments { get; set; }
}
