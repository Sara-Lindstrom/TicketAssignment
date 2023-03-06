using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TicketAssignment.Models;
using TicketAssignment.Models.Entities;
using TicketAssignment.Services;

namespace TicketAssignment.MVVM.ViewModels;

internal partial class TicketSpecificViewModel : ObservableObject
{
	DatabaseService _databaseService = new DatabaseService();

    private FullTicket _ticket;

	[ObservableProperty]
	private string title = string.Empty;

    [ObservableProperty]
    private string description = string.Empty;

    [ObservableProperty]
    private StatusEnum status;

    [ObservableProperty]
    private int statusId;

    [ObservableProperty]
    private string[] statusEnum;

    [ObservableProperty]
    private DateTime createdTime;

    [ObservableProperty]
    private string firstName = string.Empty;

    [ObservableProperty]
    private string lastName = string.Empty;

    [ObservableProperty]
    private string email = string.Empty;

    [ObservableProperty]
    private string phoneNumber = string.Empty;

    [ObservableProperty]
    private string severity = string.Empty;

    //[ObservableProperty]
    //private string[] severityEnum;

    [ObservableProperty]
    private TimeSpan? timeSpan;

    [ObservableProperty]
    private ICollection<CommentEntity>? comments;

    [RelayCommand]
    private async Task SaveTicketAltercations()
    {
        if(_ticket.Status != Status)
        {
            _ticket.Status = Status;
            await _databaseService.UpdateTicketStatusAsync(_ticket);
        }
    }

    public TicketSpecificViewModel()
	{

    }

	public TicketSpecificViewModel(int ticketId)
	{
        //severityEnum = Enum.GetNames(typeof(SeverityEnum));
        statusEnum = Enum.GetNames(typeof(StatusEnum));

        var SelectedTicket = Task.Run(async () => await _databaseService.GetTicketWithComments(ticketId));

        _ticket = new FullTicket
        {
            TicketId = ticketId,
            Title = SelectedTicket.Result.Title,
            Description = SelectedTicket.Result.Description,
            Status = SelectedTicket.Result.Status,
            CreatedTime = SelectedTicket.Result.CreatedTime,
            FirstName = SelectedTicket.Result.FirstName,
            LastName = SelectedTicket.Result.LastName,
            Email = SelectedTicket.Result.Email,
            PhoneNumber = SelectedTicket.Result.PhoneNumber,
            Severity = SelectedTicket.Result.Severity
        };

        title = SelectedTicket.Result.Title;
        description = SelectedTicket.Result.Description;
        status = SelectedTicket.Result.Status;
        createdTime= SelectedTicket.Result.CreatedTime;
        firstName = SelectedTicket.Result.FirstName;
        lastName = SelectedTicket.Result.LastName;
        email = SelectedTicket.Result.Email;
        phoneNumber = SelectedTicket.Result.PhoneNumber;
        severity = SelectedTicket.Result.Severity.ToString();

        if(SelectedTicket.Result.TimeSpan != null)
        {
            timeSpan = SelectedTicket.Result.TimeSpan;
        }

        if(SelectedTicket.Result.Comments.Count != 0)
        {
            comments = SelectedTicket.Result.Comments;
        }

        if (Enum.TryParse(Status.ToString(), out StatusEnum parsedValue))
        {
            statusId = (int)parsedValue;
        }
    }
}
