using AviaBooking.Models;
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
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace AviaBooking.ViewModels
{
    public class TicketViewModel : ViewModelBase
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

        public ObservableCollection<Flight> Flights { get; set; }

        public string Departure { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string Class { get; set; }

        public Visibility FlightsNotFoundVisibility { get; set; }
        public Visibility FlightListVisibility { get; set; }

        public TicketViewModel() 
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

            DepartureDate = DateTime.Today;
            ArrivalDate = DateTime.Today.AddDays(7);

            FlightsNotFoundVisibility = Visibility.Collapsed;
            FlightListVisibility = Visibility.Visible;

            using (var db = new AviaBookingDbContext())
            {
                var flights = db.Flights.ToList();
                Flights = new ObservableCollection<Flight>(flights);
                if (Flights == null)
                    FlightsNotFoundVisibility = Visibility.Collapsed;
            }

        }

        public TicketViewModel(Flight flight, int adultsCount, int childrenCount, int infantsCount)
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

            Departure = flight.Departure;
            Destination = flight.Destination;
            DepartureDate = flight.DepartureDate;
            ArrivalDate = flight.ArrivalDate;
            Class = flight.Class;

            AdultsCount = adultsCount;
            ChildrenCount = childrenCount;
            InfantsCount = infantsCount;

            string message = $"Departure: {Departure}\n" +
                 $"Destination: {Destination}\n" +
                 $"Departure Date: {DepartureDate}\n" +
                 $"Arrival Date: {ArrivalDate}\n" +
                 $"Class: {Class}\n" +
                 $"Adults Count: {AdultsCount}\n" +
                 $"Children Count: {ChildrenCount}\n" +
                 $"Infants Count: {InfantsCount}";

            MessageBox.Show(message, "Flight Details");


            FindResults();



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
                    if(AdultsCount > 0)
                    {
                        if(AdultsCount == 1)
                        PassengersCountButton += AdultsCount + " adult, ";
                        else
                            PassengersCountButton += AdultsCount + " adults, ";
                    }
                    if (ChildrenCount > 0)
                    {
                        if (AdultsCount == 1)
                            PassengersCountButton += ChildrenCount + " child, ";
                        else
                            PassengersCountButton += ChildrenCount + " children, ";
                    }
                    if (InfantsCount > 0)
                    {
                        if (AdultsCount == 1)
                            PassengersCountButton += InfantsCount + " infant, ";
                        else
                            PassengersCountButton += InfantsCount + " infants, ";
                    }
                    if (AdultsCount == 0 && ChildrenCount == 0 && InfantsCount == 0)
                        PassengersCountButton += "0 passengers, ";
                    if (EconomyRadioButton == true)
                    {
                        PassengersCountButton += "эконом";
                        Class = "Economy";
                    }
                    if (ComfortRadioButton == true)
                    {
                        PassengersCountButton += "комфорт";
                        Class = "Comfort";
                    }
                    if (BusinessRadioButton == true)
                    {
                        PassengersCountButton += "бизнесс";
                        Class = "Business";
                    }
                    if (FirstClassRadioButton == true)
                    {
                        PassengersCountButton += "первый класс";
                        Class = "First Class";
                    }

                });
            }
        }

        public ICommand FindResultsFlights
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    FindResults();

                });
            }
        }
        public void FindResults()
        {
            using (var db = new AviaBookingDbContext())
            {
                int sumCount = AdultsCount + ChildrenCount;
                var flights = db.Flights
                                .Where(f => f.Departure == Departure && f.Destination == Destination &&
                                f.DepartureDate == DepartureDate && f.ArrivalDate == ArrivalDate &&
                                f.AvailableSeats >= sumCount && 
                                f.Class == Class)
                                .ToList();
                Flights = new ObservableCollection<Flight>(flights);
                if (Flights.Count == 0)
                {
                    FlightsNotFoundVisibility = Visibility.Visible;
                    FlightListVisibility = Visibility.Collapsed;
                }
                else
                {
                    FlightsNotFoundVisibility = Visibility.Collapsed;
                    FlightListVisibility = Visibility.Visible;
                }
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
                    if (AdultsCount > 1)
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

        public ICommand _selectFlightCommand { get; set; }
        public ICommand selectFlightCommand => _selectFlightCommand ??= new DelegateCommand<object>(parameter =>
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            Header mainFrame = (Header)mainWindow.FindName("Header");
            var accountViewModel = mainFrame.DataContext as HeaderViewModel;

            if (accountViewModel != null)
            {
                var account = accountViewModel.account.DataContext as AccountViewModel;
                if(account.ClientID > 0)
                {
                    var flight = (Flight)parameter;
                    int sumOfPassengers = AdultsCount + ChildrenCount;
                    PaymentViewModel paymentViewModel = new PaymentViewModel(flight, sumOfPassengers);
                    PaymentPage paymentPage = new PaymentPage();
                    paymentPage.DataContext = paymentViewModel;


                    MainWindow mainWindowSecond = (MainWindow)Application.Current.MainWindow;
                    Frame mainFrameSecond = (Frame)mainWindowSecond.FindName("MainFrame");
                    mainFrameSecond.Navigate(paymentPage);
                }
                else
                {
                    MessageBox.Show("Войдите в аккаунт");
                }
            }

        });

    }
}
