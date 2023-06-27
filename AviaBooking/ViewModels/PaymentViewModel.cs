using AviaBooking.Models;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AviaBooking.ViewModels
{
    public class PaymentViewModel : ViewModelBase
    {
        public Flight flight { get; set; }

        public Passenger passenger { get; set; }
        public int count { get; set; }
        public PaymentViewModel() { }
        public ObservableCollection<Passenger> Passengers { get; set; }
        public string Gender { get; set; }
        public Visibility PassengersListVisibility { get; set; }
        public Visibility SuccessPaymentVisibility { get; set; }

        public PaymentViewModel(Flight flight, int count)
        {
            PassengersListVisibility = Visibility.Visible;
            SuccessPaymentVisibility = Visibility.Collapsed;
            this.flight = flight;
            this.count = count;
            Passengers = new ObservableCollection<Passenger>();
            for(int i = 0; i < count; i++)
            {
                Passenger passenger = new Passenger();
                passenger.BirthDate = DateTime.Now.AddYears(-20);
                Passengers.Add(passenger);
            }
        }

        public ICommand SubmitPayment
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    bool isOk = true;
                    bool isDate = true;
                    using (var db = new AviaBookingDbContext())
                    {
                        foreach (var pas in Passengers)
                        {
                            pas.FlightId = flight.Id;
                            char selectedGender = Convert.ToChar(pas.Gender);
                            if (!string.IsNullOrEmpty(pas.FirstName) &&
                                !string.IsNullOrEmpty(pas.LastName) &&
                                (pas.Gender == 'М' || pas.Gender == 'Ж') &&
                                pas.BirthDate.Year >= 1900 &&
                                pas.FlightId > 0)
                            {
                                MessageBox.Show(pas.BirthDate.ToString());
                                db.Passengers.Add(pas);
                            }
                            if(pas.BirthDate.Year < 1900)
                            {
                                isOk = false;
                                isDate = false;
                            }
                            else
                            {
                                isOk = false;
                            }
                        }
                        if (isOk == false)
                            if(!isDate)
                                MessageBox.Show("Введите корректную дату рождения");
                            else 
                            MessageBox.Show("Заполните все поля");
                        else
                        {
                            var payment = new Payment();

                            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                            Header mainFrame = (Header)mainWindow.FindName("Header");
                            var accountViewModel = mainFrame.DataContext as HeaderViewModel;

                            if (accountViewModel != null)
                            {
                                var account = accountViewModel.account.DataContext as AccountViewModel;
                                payment.ClientId = account.ClientID;
                                payment.FlightId = flight.Id;
                                payment.PaymentDate = DateTime.Today;
                            }
                            db.Payments.Add(payment);
                            db.SaveChanges();

                            PassengersListVisibility = Visibility.Collapsed;
                            SuccessPaymentVisibility = Visibility.Visible;
                        }

                    }

                });
            }
        }



    }
}
