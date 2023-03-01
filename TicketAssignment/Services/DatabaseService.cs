
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TicketAssignment.Contexts;
using TicketAssignment.Models;

namespace TicketAssignment.Services;

internal class DatabaseService
{
    private DataContext _context = new DataContext();

    public async Task AddNewTicketAsync()
    {

    }

    public async Task UpdateTicketStatusAsync()
    {

    }

    public async Task AddNewCommentAsync()
    {

    }

    public async Task<ObservableCollection<QuickViewTicket>> GetAllTicketsAsync()
    {
        var _tickets = new ObservableCollection<QuickViewTicket>();

        foreach ( var _ticket in await _context.Tickets.ToListAsync())
        {
            _tickets.Add(new QuickViewTicket
            {
                TicketId = _ticket.Id,
                Title= _ticket.Title,
                Status= _ticket.Status,
                CreatedTime = _ticket.CreatedTime,
                Severity = _ticket.SLA.Severity,
                TimeSpan= _ticket.SLA.TimeSpan,
            });
        }

        return _tickets;

    }

    //public async Task<FullTicket> GetTicketWithComments()
    //{

    //}
}
