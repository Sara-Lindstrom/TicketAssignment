
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


    //public async Task<FullTicket> GetTicketWithComments()
    //{

    //}
}
