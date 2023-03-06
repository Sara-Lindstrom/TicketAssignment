
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using TicketAssignment.Contexts;
using TicketAssignment.Models;
using TicketAssignment.Models.Entities;
using TicketAssignment.MVVM.ModelView;

namespace TicketAssignment.Services;

internal class DatabaseService
{
    private DataContext _context = new DataContext();
    private ObservableCollection<QuickViewTicket> _tickets = new ObservableCollection<QuickViewTicket>();

    public async Task AddNewTicketAsync(FullTicket _fullTicket)
    {
        // Client
        ClientEntity _client = new ClientEntity
        {
            FirstName = _fullTicket.FirstName,
            LastName = _fullTicket.LastName,
            Email = _fullTicket.Email,
            PhoneNumber = _fullTicket.PhoneNumber,
        };

        var _clientFromDatabase = await _context.Clients.FirstOrDefaultAsync(x => x.Email== _fullTicket.Email);

        if (_clientFromDatabase == null)
        {
            _context.Add(_client);
            var id = await _context.SaveChangesAsync();
            _clientFromDatabase = await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);
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
    }

    public async Task UpdateTicketStatusAsync(FullTicket editedTicket)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == editedTicket.TicketId);
        int _id;

        if (ticket != null)
        {
            ticket.Status = editedTicket.Status;

            _context.Update(ticket);
            _id = await _context.SaveChangesAsync();

            var removeTicketFromList = _tickets.FirstOrDefault(x => x.TicketId == ticket.Id);
            _tickets.Remove(removeTicketFromList);

            var updatedTicket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == _id);

            if (updatedTicket != null)
            {
                _tickets.Add(new QuickViewTicket
                {
                    TicketId = ticket.Id,
                    Title = ticket.Title,
                    Status = ticket.Status,
                    CreatedTime = ticket.CreatedTime,
                    Severity = ticket.SLA.Severity,
                    TimeSpan = ticket.SLA.TimeSpan,
                });
            }
        }

    }

    public async Task AddNewCommentAsync()
    {

    }

    public async Task<ObservableCollection<QuickViewTicket>> GetAllTicketsAsync()
    {
        var tickets = await _context.Tickets
            .Include(t => t.SLA)
            .ToListAsync();

        foreach (var ticket in tickets)
        {
            _tickets.Add(new QuickViewTicket
            {
                TicketId = ticket.Id,
                Title = ticket.Title,
                Status = ticket.Status,
                CreatedTime = ticket.CreatedTime,
                Severity = ticket.SLA.Severity,
                TimeSpan = ticket.SLA.TimeSpan,
            });
        }

        return _tickets;
    }


    public async Task<FullTicket> GetTicketWithComments(int _id)
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
            TimeSpan = _SLA.TimeSpan,

            Comments = _comments
        };

        return _returnTicket;
    }
}
