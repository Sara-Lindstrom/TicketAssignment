using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketAssignment.Models;
using TicketAssignment.Models.Entities;
using TicketAssignment.Services;

namespace TicketAssignment.MVVM.ViewModels;

public partial class AddTicketViewModel : ObservableObject
{
    private DatabaseService databaseService = new DatabaseService();

    [ObservableProperty]
    private string title = string.Empty;

    [ObservableProperty]
    private string description = string.Empty;

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

    [RelayCommand]
    private async Task AddToDataBase()
    {
        if(Title != string.Empty || Description != string.Empty || FirstName != string.Empty || LastName != string.Empty || Email != string.Empty || PhoneNumber != string.Empty )
        {
            FullTicket _newTicket = new FullTicket{
                Title = Title,
                Description = Description,
                Status= StatusEnum.NotStarted,
                CreatedTime= DateTime.Now,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Severity = Severity,
            };

            await databaseService.AddNewTicketAsync(_newTicket);

            Title = string.Empty;
            Description = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
        }

    }

    [RelayCommand]
    private void Cancel()
    {
        Title = string.Empty;
        Description = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        PhoneNumber = string.Empty;
    }

    public AddTicketViewModel()
    {
        severityEnum = Enum.GetNames(typeof(SeverityEnum));
    }
}
