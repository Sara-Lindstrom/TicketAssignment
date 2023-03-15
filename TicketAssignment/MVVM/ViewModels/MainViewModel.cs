
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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
    private async Task RemoveTicket(object sender)
    {
        var removeTicket = sender as QuickViewTicket;

        //pop up confirmating
        string messageBoxText = $"Are You Sure You Want to Remove This Ticket:\n\n{removeTicket.Title}\n{removeTicket.Status}, {removeTicket.Severity}?";
        string caption = "Delete Warning";

        MessageBoxButton button = MessageBoxButton.YesNo;
        MessageBoxImage icon = MessageBoxImage.Warning;
        MessageBoxResult result;

        result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

        if (result == MessageBoxResult.Yes)
        {
            await DatabaseService.RemoveTicketAsync(removeTicket.TicketId);
        }
    }

    //Radio Button
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

    //Change View
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
