
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketAssignment.Models.Entities;

internal class TicketEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string Title { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(max)")]
    public string Description { get; set; } = null!;

    [Required]
    public StatusEnum Status { get; set; }

    [Required]
    public DateTime CreatedTime { get; set; }

    [Required]
    public int ClientId { get; set; }
    public ClientEntity Client { get; set; } = null!;

    [Required]
    public int SLAId { get; set; }
    public SLAEntity SLA { get; set; } = null!;

    public ICollection<CommentEntity>? Comments { get; set; }
}

enum StatusEnum
{
    NotStarted,
    Started,
    Closed
}
