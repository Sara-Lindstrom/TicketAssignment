using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketAssignment.Models.Entities;

namespace TicketAssignment.MVVM.ViewModels;

public partial class AddTicketViewModel : ObservableObject
{
    [ObservableProperty]
    private string title = string.Empty;

    [ObservableProperty]
    private string description = string.Empty;

    [ObservableProperty]
    private StatusEnum status = StatusEnum.NotStarted;

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
    private TimeSpan timeSpan = TimeSpan.FromHours(1);

    [ObservableProperty]
    private string[] severityEnum;

    [ObservableProperty]
    private string comment = string.Empty;

    [RelayCommand]
    private void AddToDataBase()
    {

    }

    public AddTicketViewModel()
    {
        severityEnum = Enum.GetNames(typeof(SeverityEnum));
    }
}
