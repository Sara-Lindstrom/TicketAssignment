
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TicketAssignment.Models;
using TicketAssignment.Models.Entities;
using TicketAssignment.MVVM.ViewModels;
using TicketAssignment.Services;

namespace TicketAssignment.MVVM.ModelView;

internal partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private bool seeResolvedTickets = false;

    [ObservableProperty]
    private ObservableObject currentViewModel;

    [ObservableProperty]
    private ObservableCollection<QuickViewTicket> ticketList = new ObservableCollection<QuickViewTicket>();

    [ObservableProperty]
    private QuickViewTicket selectedTicket = new QuickViewTicket();


    public MainViewModel()
    {
        currentViewModel = new TicketSpecificViewModel();
        Task.Run(async () => await populateTicketList());
    }

    public async Task populateTicketList()
    {
        TicketList = await DatabaseService.GetAllTicketsAsync(seeResolvedTickets);
    }

    [RelayCommand]
    public async Task YesChecked()
    {
        SeeResolvedTickets = true;
        ticketList = await DatabaseService.GetAllTicketsAsync(SeeResolvedTickets);
    }

    [RelayCommand]
    public async Task NoChecked()
    {
        SeeResolvedTickets = false;
        ticketList = await DatabaseService.GetAllTicketsAsync(SeeResolvedTickets);
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
