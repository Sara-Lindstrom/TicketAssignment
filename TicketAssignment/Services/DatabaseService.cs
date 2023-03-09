
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Documents;
using TicketAssignment.Contexts;
using TicketAssignment.Models;
using TicketAssignment.Models.Entities;
using TicketAssignment.MVVM.ModelView;

namespace TicketAssignment.Services;

public static class DatabaseService
{
    private static DataContext _context = new DataContext();
    private static TicketService ticketService = new TicketService();
    private static ObservableCollection<QuickViewTicket> _tickets = new ObservableCollection<QuickViewTicket>();
    private static bool _showResolvedTickets = false;

    public static async Task AddNewTicketAsync(FullTicket _fullTicket)
    {
        string formatPhoneNumber = ticketService.FormatPhoneNumber(_fullTicket.PhoneNumber);

        // Client
        ClientEntity _client = new ClientEntity
        {
            FirstName = _fullTicket.FirstName,
            LastName = _fullTicket.LastName,
            Email = _fullTicket.Email,
            PhoneNumber = formatPhoneNumber,
        };

        var _clientFromDatabase = await _context.Clients.FirstOrDefaultAsync(x => x.Email== _fullTicket.Email);

        if (_clientFromDatabase == null)
        {
            _context.Add(_client);
            await _context.SaveChangesAsync();
            _clientFromDatabase = await _context.Clients.FirstOrDefaultAsync(x => x.Email == _client.Email);
        }

        var _SLA = await _context.SLAs.FirstOrDefaultAsync(x => x.Severity== _fullTicket.Severity);


        // Ticket
        TicketEntity _ticket = new TicketEntity
        {
            Title = _fullTicket.Title,
            Description = _fullTicket.Description,
            Status = _fullTicket.Status,
            CreatedTime = _fullTicket.CreatedTime,
            ClientId = _clientFromDatabase.Id,
            SLAId = _SLA.Id
        };

        _context.Add(_ticket);
        await _context.SaveChangesAsync();
        _tickets.Clear();
        await GetAllTicketsAsync(_showResolvedTickets);
    }

    public static async Task UpdateTicketStatusAsync(FullTicket editedTicket)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == editedTicket.TicketId);

        if (ticket != null)
        {
            ticket.Status = editedTicket.Status;

            _context.Update(ticket);
            await _context.SaveChangesAsync();

            _tickets.Clear();
            await GetAllTicketsAsync(_showResolvedTickets);
        }

    }

    public static async Task AddNewCommentAsync(CommentEntity newComment)
    {
        _context.Add(newComment);
        await _context.SaveChangesAsync();

        _tickets.Clear();
        await GetAllTicketsAsync(_showResolvedTickets);
    }

    public static async Task<ObservableCollection<QuickViewTicket>> GetAllTicketsAsync( bool showResolvedTickets)
    {
        _showResolvedTickets = showResolvedTickets;
        List<TicketEntity> tickets = new List<TicketEntity>();

        if (showResolvedTickets)
        {
            tickets = await _context.Tickets
                .Include(t => t.SLA)
                .ToListAsync();
        }
        else
        {
            tickets = await _context.Tickets
                .Where(x => x.Status != StatusEnum.Resolved)
                .Include(t => t.SLA)
                .ToListAsync();
        }

        _tickets.Clear();

        foreach (var ticket in tickets)
        {

            _tickets.Add(new QuickViewTicket
            {
                TicketId = ticket.Id,
                Title = ticket.Title,
                Status = ticket.Status,
                CreatedTime = ticket.CreatedTime,
                Severity = ticket.SLA.Severity,
                TimeRemaining = ticketService.SetTimeRemaining(ticket.CreatedTime, ticket.SLA.Severity)
            });
        }

        return _tickets;
    }


    public static async Task<FullTicket> GetTicketWithComments(int _id)
    {
        var _ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == _id);
        var _client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == _ticket.ClientId);
        var _SLA = await _context.SLAs.FirstOrDefaultAsync(x => x.Id == _ticket.SLAId);
        var _comments = await _context.Comments.Where(x => x.TicketId == _id).ToListAsync();

        FullTicket _returnTicket = new FullTicket
        {
            TicketId = _ticket.Id,
            Title = _ticket.Title,
            Description= _ticket.Description,
            Status = _ticket.Status,
            CreatedTime = _ticket.CreatedTime,

            FirstName = _client.FirstName,
            LastName = _client.LastName,
            Email = _client.Email,
            PhoneNumber = _client.PhoneNumber,

            Severity = _SLA.Severity,

            Comments = _comments
        };

        return _returnTicket;
    }
}
