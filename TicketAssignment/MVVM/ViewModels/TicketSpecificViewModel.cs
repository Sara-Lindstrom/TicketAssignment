using CommunityToolkit.Mvvm.ComponentModel;
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
	DatabaseService databaseService = new DatabaseService();

	[ObservableProperty]
	private string title = string.Empty;

    [ObservableProperty]
    private string description = string.Empty;

    [ObservableProperty]
    private StatusEnum status;

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
    private SeverityEnum severity;

    [ObservableProperty]
    private string[] severityEnum;

    [ObservableProperty]
    private TimeSpan? timeSpan;

    [ObservableProperty]
    private ICollection<CommentEntity>? comments;

    public TicketSpecificViewModel()
	{

    }

	public TicketSpecificViewModel(int ticketId)
	{
        severityEnum = Enum.GetNames(typeof(SeverityEnum));

        var SelectedTicket = Task.Run(async () => await databaseService.GetTicketWithComments(ticketId));

        title = SelectedTicket.Result.Title;
        description = SelectedTicket.Result.Description;
        status = SelectedTicket.Result.Status;
        createdTime= SelectedTicket.Result.CreatedTime;
        firstName = SelectedTicket.Result.FirstName;
        lastName = SelectedTicket.Result.LastName;
        email = SelectedTicket.Result.Email;
        phoneNumber = SelectedTicket.Result.PhoneNumber;
        severity = SelectedTicket.Result.Severity;

        if(SelectedTicket.Result.TimeSpan != null)
        {
            timeSpan = SelectedTicket.Result.TimeSpan;
        }

        if(SelectedTicket.Result.Comments.Count != 0)
        {
            comments = SelectedTicket.Result.Comments;
        }		
    }
}
