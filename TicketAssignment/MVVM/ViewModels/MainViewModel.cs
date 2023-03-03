
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketAssignment.Models;
using TicketAssignment.Models.Entities;
using TicketAssignment.MVVM.ViewModels;
using TicketAssignment.Services;

namespace TicketAssignment.MVVM.ModelView;

internal partial class MainViewModel : ObservableObject
{
    private DatabaseService _databaseService;

    [ObservableProperty]
    private ObservableObject currentViewModel;

    [ObservableProperty]
    private ObservableCollection<QuickViewTicket> ticketList;

    [ObservableProperty]
    private QuickViewTicket selectedTicket;



    public MainViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
        currentViewModel = new TicketSpecificViewModel();
        Task.Run(async () => await populateTicketList());
    }

    private async Task populateTicketList()
    {
        TicketList = await _databaseService.GetAllTicketsAsync();
    }

    [RelayCommand]
    private void GoToAddTicket()
    {
        CurrentViewModel = new AddTicketViewModel();
    }

    [RelayCommand]
    private void GoToTicketSpecific()
    {
        if(SelectedTicket!= null)
        {
            CurrentViewModel = new TicketSpecificViewModel(SelectedTicket.TicketId);
        }
        else
        {
            CurrentViewModel = new TicketSpecificViewModel();
        }
    }
}
