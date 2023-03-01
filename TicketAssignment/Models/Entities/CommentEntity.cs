
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketAssignment.Models.Entities;

internal class CommentEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(max)")]
    public string Comment { get; set; } = null!;

    [Required]
    public DateTime CreatedTime { get; set; }

    [Required]
    public int TicketId { get; set; }
    public TicketEntity Ticket { get; set; } = null!;
}
