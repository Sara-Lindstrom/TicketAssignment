using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketAssignment.Models.Entities;

[Index(nameof(Email), IsUnique = true)]

public class ClientEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(20)")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(320)")]
    public string Email { get; set; } = null!;

    [Required]
    [Column(TypeName = "char(13)")]
    public string PhoneNumber { get; set; } = null!;

    public ICollection<TicketEntity> Tickets { get; set; } = null!;
}
