using AviaBooking.Models;
using Bogus;
using DevExpress.Mvvm;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Bogus.DataSets.Name;

namespace AviaBooking.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public Visibility PassengerListVisibility { get; set; }

        public int AdultsCount { get; set; }
        public int ChildrenCount { get; set; }
        public int InfantsCount { get; set; }
        public string PassengersCountButton { get; set; }

        public bool EconomyRadioButton { get; set; }
        public bool ComfortRadioButton { get; set; }
        public bool BusinessRadioButton { get; set; }
        public bool FirstClassRadioButton { get; set; }

        public Flight flight { get; set; }
        public MainViewModel()
        {
            PassengerListVisibility = Visibility.Collapsed;
            AdultsCount = 1;
            ChildrenCount = 0;
            InfantsCount = 0;
            EconomyRadioButton = true;
            ComfortRadioButton = false;
            BusinessRadioButton = false;
            FirstClassRadioButton = false;
            PassengersCountButton = "1 adult, эконом";

            flight = new Flight();
            flight.Departure = "Minsk";
            flight.DepartureDate = DateTime.Now;
            flight.ArrivalDate = DateTime.Now.AddDays(7);
            flight.Class = "Economy";


        }

        // Выпадающий список
        public ICommand PassengerListClick
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    PassengersCountButton = "";
                    if (PassengerListVisibility == Visibility.Collapsed)
                    {
                        PassengerListVisibility = Visibility.Visible;
                    }
                    else
                    {
                        PassengerListVisibility = Visibility.Collapsed;
                    }
                    if (AdultsCount > 0)
                    {
                        if (AdultsCount == 1)
                            PassengersCountButton += AdultsCount + " adult, ";
                        else
                            PassengersCountButton += AdultsCount + " adults, ";
                    }
                    if (ChildrenCount > 0)
                    {
                        if (ChildrenCount == 1)
                            PassengersCountButton += ChildrenCount + " child, ";
                        else
                            PassengersCountButton += ChildrenCount + " children, ";
                    }
                    if (InfantsCount > 0)
                    {
                        if (InfantsCount == 1)
                            PassengersCountButton += InfantsCount + " infant, ";
                        else
                            PassengersCountButton += InfantsCount + " infants, ";
                    }
                    if (AdultsCount == 0 && ChildrenCount == 0 && InfantsCount == 0)
                        PassengersCountButton += "0 passengers, ";
                    if (EconomyRadioButton == true)
                    {
                        PassengersCountButton += "эконом";
                        flight.Class = "Economy";
                    }
                    if (ComfortRadioButton == true)
                    {
                        PassengersCountButton += "комфорт";
                        flight.Class = "Comfort";
                    }
                    if (BusinessRadioButton == true)
                    {
                        PassengersCountButton += "бизнесс";
                        flight.Class = "Business";
                    }    
                    if (FirstClassRadioButton == true)
                    {
                        PassengersCountButton += "первый класс";
                        flight.Class = "First Class";
                    }    

                });
            }
        }

        // Увеличить число пассажиров
        #region Увеличить число пассажиров
        public ICommand IncreaseAdultsCount
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    AdultsCount++;

                });
            }
        }
        public ICommand IncreaseChildrenCount
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    ChildrenCount++;

                });
            }
        }
        public ICommand IncreaseInfantsCount
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    InfantsCount++;

                });
            }
        }
        #endregion

        // Уменьшить число пассажиров
        #region Уменьшить число пассажиров
        public ICommand DecreaseAdultsCount
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if(AdultsCount > 1) 
                    AdultsCount--;

                });
            }
        }
        public ICommand DecreaseChildrenCount
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (ChildrenCount > 0)
                        ChildrenCount--;

                });
            }
        }
        public ICommand DecreaseInfantsCount
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (InfantsCount > 0)
                        InfantsCount--;

                });
            }
        }
        #endregion

        public ICommand FindFlightButton
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    int sumOfPassengers = AdultsCount + ChildrenCount;
                    if (!string.IsNullOrEmpty(flight.Departure) && !string.IsNullOrEmpty(flight.Destination) &&
                    flight.DepartureDate >= DateTime.Today && flight.ArrivalDate >= DateTime.Today &&
                    sumOfPassengers >= 1 && !string.IsNullOrEmpty(flight.Class))
                    {
                        TicketViewModel ticketsViewModel = new TicketViewModel(flight, AdultsCount, ChildrenCount, InfantsCount);
                        MainWindow mainWindowFirst = (MainWindow)Application.Current.MainWindow;
                        Header mainFrameFirst = (Header)mainWindowFirst.FindName("Header");
                        var accountViewModel = mainFrameFirst.DataContext as HeaderViewModel;

                        if (accountViewModel != null)
                        {
                            var tickets = accountViewModel.tickets as Tickets;
                            tickets.DataContext = ticketsViewModel;

                            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                            Frame mainFrame = (Frame)mainWindow.FindName("MainFrame");
                            mainFrame.Navigate(tickets);
                        }

                    }
                    if (flight.DepartureDate < DateTime.Today && flight.ArrivalDate < DateTime.Today)
                        MessageBox.Show("Введенные даты некорректны");
                    else
                    {
                        MessageBox.Show("Заполните все поля корректно");
                    }



                });
            }
        }
    }
}
