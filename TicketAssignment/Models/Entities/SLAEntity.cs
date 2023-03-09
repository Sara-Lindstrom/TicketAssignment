using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketAssignment.Models.Entities;

public class SLAEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public SeverityEnum Severity { get; set; }

    public ICollection<TicketEntity> Tickets { get; set; } = null!;
}

public enum SeverityEnum
{
    Minor,
    Important,
    Urgent
}
