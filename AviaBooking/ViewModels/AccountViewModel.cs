using AviaBooking.Models;
using Bogus;
using DevExpress.Mvvm;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AviaBooking.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {
        public Visibility LoginPageVisibility { get; set; }
        public Visibility AccountPageVisibility { get; set; }
        public Visibility RegButtonVisibility { get; set; }
        public Visibility LogButtonVisibility { get; set; }

        private ICommand _logButton;
        private ICommand _regButton;

        public string LogNickname { get; set; }
        public string LogName { get; set; }
        public string LogSurname { get; set; }
        public string LogEmail { get; set; }
        public string LogPassword { get; set; }
        public string LogSecuredPassword { get; set; }

        public string TempNickname { get; set; }
        public string TempName { get; set; }
        public string TempSurname { get; set; }
        public string TempEmail { get; set; }
        public string TempPassword { get; set; }

        public Visibility CurrentTicketsVisibility { get; set; }
        public Visibility PersonalDataVisibility { get; set; }
        public Visibility ChangePersonalDataVisibility { get; set; }
        public Visibility HistoryVisibility { get; set; }
        public Visibility TicketsNotFoundVisibility { get; set; }

        public bool isLogin { get; set; }

        public bool isAdmin { get; set; }
        public Visibility AdminPanelVisibility { get; set; }
        public Visibility AdminButtonVisibility { get; set; }
        public Visibility FlightsMenuVisibility { get; set; }
        public Visibility ClientsMenuVisibility { get; set; }
        public Visibility PassengersMenuVisibility { get; set; }
        public ObservableCollection<Flight> Flights { get; set; }
        public Flight SelectedDeleteFlight { get; set; }

        public int AirlineId { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public int AvailableSeats { get; set; }
        public ComboBoxItem Class { get; set; }

        public Client Client { get; set; }
        public ObservableCollection<Client> Clients { get; set; }

        public int FlightNumber { get; set; }
        public ObservableCollection<Passenger> Passengers { get; set; }
        public int ClientID { get; set; }


        public AccountViewModel() 
        {
            LoginPageVisibility = Visibility.Visible;
            AccountPageVisibility = Visibility.Collapsed;
            RegButtonVisibility = Visibility.Collapsed;
            LogButtonVisibility = Visibility.Visible;

            CurrentTicketsVisibility = Visibility.Collapsed;
            PersonalDataVisibility = Visibility.Collapsed;
            ChangePersonalDataVisibility = Visibility.Collapsed;
            HistoryVisibility = Visibility.Collapsed;
            TicketsNotFoundVisibility = Visibility.Collapsed;
            AdminPanelVisibility = Visibility.Collapsed;
            AdminButtonVisibility = Visibility.Collapsed;
            isLogin = true;
            isAdmin = false;

        }

        public ICommand AdminPanelButton
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    CurrentTicketsVisibility = Visibility.Collapsed;
                    PersonalDataVisibility = Visibility.Collapsed;
                    ChangePersonalDataVisibility = Visibility.Collapsed;
                    HistoryVisibility = Visibility.Collapsed;
                    TicketsNotFoundVisibility = Visibility.Collapsed;
                    if (AdminPanelVisibility == Visibility.Collapsed)
                    {
                        AdminPanelVisibility = Visibility.Visible;
                    }
                    else
                    {
                        AdminPanelVisibility = Visibility.Collapsed;
                    }
                    FlightsMenuVisibility = Visibility.Collapsed;
                    ClientsMenuVisibility = Visibility.Collapsed;
                    PassengersMenuVisibility = Visibility.Collapsed;

                });
            }
        }

        public ICommand AddFlightButton
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (AirlineId == null)
                    {
                        MessageBox.Show("Please enter Airline Id.");
                    }

                    if (string.IsNullOrEmpty(Departure))
                    {
                        MessageBox.Show("Please enter Departure.");
                    }

                    if (string.IsNullOrEmpty(Destination))
                    {
                        MessageBox.Show("Please enter Destination.");
                    }
                    if(DepartureDate <= DateTime.Today)
                    {
                        MessageBox.Show("DepartureDate cannot be past");
                    }
                    if (DepartureDate > ArrivalDate)
                    {
                        MessageBox.Show("Departure Date cannot be later than Arrival Date.");
                    }
                    if (ArrivalDate.Subtract(DepartureDate).TotalDays > 1)
                    {
                        MessageBox.Show("The difference between Departure Date and Arrival Date cannot be more than 1 day.");
                    }

                    if (AvailableSeats <= 0)
                    {
                        MessageBox.Show("Please enter a valid number of Available Seats.");
                    } 
                    else
                    {
                        Flight flight = new Flight();
                        flight.AirlineId = AirlineId;
                        flight.Departure = Departure;
                        flight.Destination = Destination;
                        flight.DepartureDate = DepartureDate;
                        flight.ArrivalDate = ArrivalDate;
                        flight.AvailableSeats = AvailableSeats;
                        ComboBoxItem selectedItem = Class as ComboBoxItem;
                        if (selectedItem != null)
                        {
                            flight.Class = selectedItem.Content.ToString();
                        }

                        MessageBox.Show(flight.Class);
                        using (var db = new AviaBookingDbContext())
                        {
                            db.Flights.Add(flight);
                            db.SaveChanges();
                        }
                        Flights.Add(flight);
                    }

                });
            }
        }

        public ICommand FlightsMenuButton
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    FlightsMenuVisibility = Visibility.Visible;
                    ClientsMenuVisibility = Visibility.Collapsed;
                    PassengersMenuVisibility = Visibility.Collapsed;

                    using (var db = new AviaBookingDbContext())
                    {
                        var flights = db.Flights.ToList();
                        Flights = new ObservableCollection<Flight>(flights);
                    }
                });
            }
        }

        public ICommand ClientsMenuButton
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    FlightsMenuVisibility = Visibility.Collapsed;
                    ClientsMenuVisibility = Visibility.Visible;
                    PassengersMenuVisibility = Visibility.Collapsed;

                    using (var db = new AviaBookingDbContext())
                    {
                        var clients = db.Clients.ToList();
                        Clients = new ObservableCollection<Client>(clients);
                    }
                });
            }
        }
        public ICommand PassengersMenuButton
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    FlightsMenuVisibility = Visibility.Collapsed;
                    ClientsMenuVisibility = Visibility.Collapsed;
                    PassengersMenuVisibility = Visibility.Visible;

                });
            }
        }

        public ICommand DeleteFlightCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    using (var db = new AviaBookingDbContext())
                    {
                        db.Flights.Remove(SelectedDeleteFlight);
                        db.SaveChanges();
                        Flights.Remove(SelectedDeleteFlight);
                    }

                });
            }
        }

        public ICommand _changeClientRole { get; set; }
        public ICommand ChangeClientRole => _changeClientRole ??= new DelegateCommand<object>(parameter =>
        {
            if(Client != null)
            {
                using (var db = new AviaBookingDbContext())
                {
                    var client = db.Clients.FirstOrDefault(c => c.Nickname == Client.Nickname);
                    if (client.Role == "user")
                    {
                        client.Role = "admin";

                        db.SaveChanges();
                        var clients = db.Clients.ToList();
                        Clients = new ObservableCollection<Client>(clients);
                    }
                }
                
                
            }
        });


        public ICommand FindPassengers
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if(FlightNumber != null)
                    {
                        using (var db = new AviaBookingDbContext())
                        {
                            var passengers = db.Passengers
                                            .Where(p => p.FlightId == FlightNumber)
                                            .ToList();

                            Passengers = new ObservableCollection<Passenger>(passengers);
                        }
                    } 
                    else
                    {
                        MessageBox.Show("Введите номер рейса");
                    }

                });
            }
        }

        public ICommand FillDataBase
        {
            get
            {
                return new DelegateCommand(() =>
                {

                    using (var db = new AviaBookingDbContext())
                    {
                        for (int i = 0; i < 10; i++)
                        {

                            var faker = new Faker<Airline>()
                                            .RuleFor(x => x.Name, f => f.Company.CompanyName())
                                            .RuleFor(x => x.Rating, f => Math.Round(f.Random.Double(1, 5), 2));

                            Airline airline = faker.Generate();
                            db.Airlines.Add(airline);
                            db.SaveChanges();
                        }
                    }

                    using (var db = new AviaBookingDbContext())
                    {
                        for (int i = 0; i < 25; i++)
                        {
                            var faker = new Faker<Client>()
                                        .RuleFor(x => x.Nickname, f => f.Internet.UserName())
                                        .RuleFor(x => x.Name, f => f.Name.FirstName())
                                        .RuleFor(x => x.Surname, f => f.Name.LastName())
                                        .RuleFor(x => x.Email, f => f.Internet.Email())
                                        .RuleFor(x => x.Password, f => f.Internet.Password());

                            Client client = faker.Generate();
                            client.Role = "user";
                            db.Clients.Add(client);
                            db.SaveChanges();
                        }
                    }


                    using (var db = new AviaBookingDbContext())
                    {

                        var faker = new Faker<Flight>()
                                   .RuleFor(x => x.AirlineId, f => f.Random.Number(1, 10))
                                   .RuleFor(x => x.Departure, f => f.Address.Country())
                                   .RuleFor(x => x.Destination, f => f.Address.Country())
                                   .RuleFor(x => x.DepartureDate, f => f.Date.Future())
                                   .RuleFor(x => x.ArrivalDate, (f, x) => f.Date.Between(x.DepartureDate, x.DepartureDate.AddDays(1)))
                                   .RuleFor(x => x.AvailableSeats, f => f.Random.Number(50, 200))
                                   .RuleFor(x => x.Class, f => f.PickRandom(new[] { "Economy", "Comfort", "Business", "First Class" }));

                        for (int i = 0; i < 400; i++)
                        {

                            Flight flight = faker.Generate();

                            db.Flights.Add(flight);
                        }

                        db.SaveChanges();
                    }


                    using (var db = new AviaBookingDbContext())
                    {

                        var faker = new Faker<Passenger>()
                                    .RuleFor(x => x.FirstName, f => f.Person.FirstName)
                                    .RuleFor(x => x.LastName, f => f.Person.LastName)
                                    .RuleFor(x => x.BirthDate, f => f.Person.DateOfBirth)
                                    .RuleFor(x => x.FlightId, f => f.Random.Number(1, 400));

                        for (int i = 0; i < 1000; i++)
                        {


                            Passenger passenger = faker.Generate();

                            Random rand = new Random();
                            int random = rand.Next(1, 100);
                            if (random <= 50)
                            {
                                passenger.Gender = 'М';
                            }
                            else
                            {
                                passenger.Gender = 'Ж';
                            }
                            db.Passengers.Add(passenger);
                        }
                        db.SaveChanges();
                    }


                    using (var db = new AviaBookingDbContext())
                    {
                        var faker = new Faker<Payment>()
                                    .RuleFor(x => x.ClientId, f => f.Random.Number(1, 25))
                                    .RuleFor(x => x.FlightId, f => f.Random.Number(1, 400))
                                    .RuleFor(x => x.PaymentDate, f => f.Date.Past());

                        for (int i = 0; i < 100; i++)
                        {

                            Payment payment = faker.Generate();

                            db.Payments.Add(payment);
                        }
                        db.SaveChanges();
                    }


                    using (var db = new AviaBookingDbContext())
                    {
                        var faker = new Faker<Review>()
                            .RuleFor(x => x.AirlineId, f => f.Random.Number(1, 10))
                            .RuleFor(x => x.ClientId, f => f.Random.Number(1, 25))
                            .RuleFor(x => x.Score, f => f.Random.Number(1, 5));

                        for (int i = 0; i < 100; i++)
                        {

                            Review review = faker.Generate();
                            review.Text = "";
                            db.Reviews.Add(review);
                        }
                        db.SaveChanges();
                    }
                    MessageBox.Show("База данных заполнена!");
                });
            }
        }

        public ICommand _returnFlightTickets { get; set; }
        public ICommand ReturnFlightTickets => _returnFlightTickets ??= new DelegateCommand<object>(parameter =>
        {
            var box = (ListView)parameter;
            Flight selectedFlight = box.SelectedItem as Flight;
            using (var db = new AviaBookingDbContext())
            {
                var paymentId = db.Payments.FirstOrDefault(p => p.FlightId == selectedFlight.Id);
                if (paymentId != null)
                {
                    db.Payments.Remove(paymentId);
                    db.SaveChanges();

                }
                var client = db.Clients.FirstOrDefault(f => f.Nickname == LogNickname && f.Password == LogPassword);
                if (client != null)
                {

                    // Получаем все записи из таблицы Payments, где клиент совпадает с искомым клиентом.
                    var payments = db.Payments.Where(p => p.ClientId == client.Id).ToList();

                    // Получаем все рейсы, которые соответствуют найденным записям в таблице Payments.
                    var flights = new List<Flight>();
                    foreach (var payment in payments)
                    {
                        var flight = db.Flights.FirstOrDefault(f => f.Id == payment.FlightId && f.DepartureDate > DateTime.Today);
                        if (flight != null)
                        {
                            flights.Add(flight);
                        }
                    }

                    if (flights.Count > 0)
                        Flights = new ObservableCollection<Flight>(flights);
                    else
                    {
                        TicketsNotFoundVisibility = Visibility.Visible;
                        CurrentTicketsVisibility = Visibility.Collapsed;
                    }
                }
            }

        });


        #region Смена Вход/Регистрация
        public ICommand ChooseRegistrationButton
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    RegButtonVisibility = Visibility.Visible;
                    LogButtonVisibility = Visibility.Collapsed;
                    isLogin = false;
                });
            }
        }

        public ICommand ChooseLoginButton
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    RegButtonVisibility = Visibility.Collapsed;
                    LogButtonVisibility = Visibility.Visible;
                    isLogin = true;
                });
            }
        }
        #endregion

        #region Кнопки сменты видимости меню в личном кабинете
        public ICommand CurrentTicketsButton
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    CurrentTicketsVisibility = Visibility.Visible;
                    PersonalDataVisibility = Visibility.Collapsed;
                    ChangePersonalDataVisibility = Visibility.Collapsed;
                    HistoryVisibility = Visibility.Collapsed;
                    TicketsNotFoundVisibility = Visibility.Collapsed;
                    AdminPanelVisibility = Visibility.Collapsed;
                    FlightsMenuVisibility = Visibility.Collapsed;
                    ClientsMenuVisibility = Visibility.Collapsed;
                    FlightsMenuVisibility = Visibility.Collapsed;
                    using (var db = new AviaBookingDbContext())
                    {
                        var client = db.Clients.FirstOrDefault(f => f.Nickname == LogNickname && f.Password == LogPassword);
                        if (client != null)
                        {

                            // Получаем все записи из таблицы Payments, где клиент совпадает с искомым клиентом.
                            var payments = db.Payments.Where(p => p.ClientId == client.Id).ToList();

                            // Получаем все рейсы, которые соответствуют найденным записям в таблице Payments.
                            var flights = new List<Flight>();
                            foreach (var payment in payments)
                            {
                                var flight = db.Flights.FirstOrDefault(f => f.Id == payment.FlightId && f.DepartureDate > DateTime.Today);
                                if (flight != null)
                                {
                                    flights.Add(flight);
                                }
                            }

                            if (flights.Count > 0)
                                Flights = new ObservableCollection<Flight>(flights);
                            else 
                            {
                                TicketsNotFoundVisibility = Visibility.Visible;
                                CurrentTicketsVisibility = Visibility.Collapsed;
                            }
                        }
                    }
                });
            }
        }
        public ICommand PersonalDataButton
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    CurrentTicketsVisibility = Visibility.Collapsed;
                    PersonalDataVisibility = Visibility.Visible;
                    ChangePersonalDataVisibility = Visibility.Collapsed;
                    HistoryVisibility = Visibility.Collapsed;
                    TicketsNotFoundVisibility = Visibility.Collapsed;
                    AdminPanelVisibility = Visibility.Collapsed;
                    FlightsMenuVisibility = Visibility.Collapsed;
                    ClientsMenuVisibility = Visibility.Collapsed;
                    FlightsMenuVisibility = Visibility.Collapsed;
                });
            }
        }
        public ICommand ChangePersonalDataButton
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    CurrentTicketsVisibility = Visibility.Collapsed;
                    PersonalDataVisibility = Visibility.Collapsed;
                    ChangePersonalDataVisibility = Visibility.Visible;
                    HistoryVisibility = Visibility.Collapsed;
                    TicketsNotFoundVisibility = Visibility.Collapsed;
                    AdminPanelVisibility = Visibility.Collapsed;
                    FlightsMenuVisibility = Visibility.Collapsed;
                    ClientsMenuVisibility = Visibility.Collapsed;
                    FlightsMenuVisibility = Visibility.Collapsed;
                });
            }
        }
        public ICommand HistoryButton
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    CurrentTicketsVisibility = Visibility.Collapsed;
                    PersonalDataVisibility = Visibility.Collapsed;
                    ChangePersonalDataVisibility = Visibility.Collapsed;
                    HistoryVisibility = Visibility.Visible;
                    TicketsNotFoundVisibility = Visibility.Collapsed;
                    AdminPanelVisibility = Visibility.Collapsed;
                    FlightsMenuVisibility = Visibility.Collapsed;
                    ClientsMenuVisibility = Visibility.Collapsed;
                    FlightsMenuVisibility = Visibility.Collapsed;

                    using (var db = new AviaBookingDbContext())
                    {
                        var client = db.Clients.FirstOrDefault(f => f.Nickname == LogNickname && f.Password == LogPassword);
                        if (client != null)
                        {
                            
                            // Получаем все записи из таблицы Payments, где клиент совпадает с искомым клиентом.
                            var payments = db.Payments.Where(p => p.ClientId == client.Id).ToList();

                            // Получаем все рейсы, которые соответствуют найденным записям в таблице Payments.
                            var flights = new List<Flight>();
                            foreach (var payment in payments)
                            {
                                var flight = db.Flights.FirstOrDefault(f => f.Id == payment.FlightId && f.DepartureDate <= DateTime.Today);
                                if (flight != null)
                                {
                                    flights.Add(flight);
                                }
                            }


                            if (flights.Count > 0)
                                Flights = new ObservableCollection<Flight>(flights);
                            else
                            {
                                TicketsNotFoundVisibility = Visibility.Visible;
                                HistoryVisibility = Visibility.Collapsed;
                            }
                        }
                    }
                });
            }
        }

        public ICommand ShowPasswordButton
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if(LogSecuredPassword.Contains('*'))
                    {
                        LogSecuredPassword = LogPassword;
                    } else
                    {
                        LogSecuredPassword = "";
                        for(int i = 0; i < LogPassword.Length; i++)
                        {
                            LogSecuredPassword += '*';
                        }
                    }
                });
            }
        }

        public ICommand ChangeAccountDataButton
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    using (var db = new AviaBookingDbContext())
                    {
                        var client = db.Clients.FirstOrDefault(f => f.Nickname == LogNickname);
                        if (client != null)
                        {
                            if (string.IsNullOrEmpty(LogNickname) || string.IsNullOrEmpty(LogName) ||
                                string.IsNullOrEmpty(LogSurname) || string.IsNullOrEmpty(LogEmail) ||
                                string.IsNullOrEmpty(LogPassword))
                            {
                                MessageBox.Show("Please fill in all the fields.");
                            }
                            if (!string.IsNullOrEmpty(LogEmail))
                            {
                                if (!LogEmail.Contains("@") || !LogEmail.Contains("."))
                                {
                                    MessageBox.Show("Please enter a valid email address.");
                                }
                                else
                                {
                                    client.Nickname = TempNickname;
                                    client.Name = TempName;
                                    client.Surname = TempSurname;
                                    client.Email = TempEmail;
                                    client.Password = TempPassword;
                                    db.SaveChanges();
                                    MessageBox.Show("Данные изменены");

                                    LogNickname = TempNickname;
                                    LogName = TempName;
                                    LogSurname = TempSurname;
                                    LogEmail = TempEmail;
                                    LogPassword = TempPassword;

                                }
                            }
                        }

                    }
                });
            }
        }

        #endregion

        #region Функции входа/выхода
        // Функция входа
        public ICommand LogButton => _logButton ??= new DelegateCommand<object>(parameter =>
        {
            PasswordBox box = (PasswordBox)parameter;

            LogPassword = box.Password;

            if (string.IsNullOrEmpty(LogNickname) || string.IsNullOrEmpty(LogPassword))
            {
                MessageBox.Show("Please fill in all the fields.");
            } else
            {
                using (var db = new AviaBookingDbContext())
                {
                    var client = db.Clients.FirstOrDefault(f => f.Nickname == LogNickname && f.Password == box.Password);
                    if(client!= null)
                    {
                        LoginPageVisibility = Visibility.Collapsed;
                        AccountPageVisibility = Visibility.Visible;
                        CurrentTicketsVisibility = Visibility.Visible;
                        TicketsNotFoundVisibility = Visibility.Collapsed;
                        LogNickname = client.Nickname;
                        LogName = client.Name;
                        LogEmail = client.Email;
                        LogSurname = client.Surname;
                        ClientID = client.Id;
                        TempNickname = LogNickname;
                        TempName = LogName;
                        TempSurname = LogSurname;
                        TempEmail = LogEmail;
                        TempPassword = LogPassword;
                        for(int i = 0; i < LogPassword.Length; i++)
                        {
                            LogSecuredPassword += '*';
                        }

                        // Получаем все записи из таблицы Payments, где клиент совпадает с искомым клиентом.
                        var payments = db.Payments.Where(p => p.ClientId == client.Id).ToList();

                        // Получаем все рейсы, которые соответствуют найденным записям в таблице Payments.
                        var flights = new List<Flight>();
                        foreach (var payment in payments)
                        {
                            var flight = db.Flights.FirstOrDefault(f => f.Id == payment.FlightId && f.DepartureDate > DateTime.Today);
                            if (flight != null)
                            {
                                flights.Add(flight);
                            }
                        }


                        if (flights.Count > 0)
                            Flights = new ObservableCollection<Flight>(flights);
                        else
                        {
                            TicketsNotFoundVisibility = Visibility.Visible;
                            CurrentTicketsVisibility = Visibility.Collapsed;
                        }

                        if(client.Role == "admin")
                        {
                            isAdmin = true;
                            AdminButtonVisibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не найдено совпадений");
                    }
                }
            }
           



        }, parameter => parameter is PasswordBox box);

        public ICommand RegButton => _regButton ??= new DelegateCommand<object>(parameter =>
        {
            PasswordBox box = (PasswordBox)parameter;

            string LogPassword = GetSecureHash(box.SecurePassword);

            if (string.IsNullOrEmpty(LogNickname) || string.IsNullOrEmpty(LogName) ||
                        string.IsNullOrEmpty(LogSurname) || string.IsNullOrEmpty(LogEmail) ||
                        string.IsNullOrEmpty(LogPassword))
            {
                MessageBox.Show("Please fill in all the fields.");
            }
            if (!string.IsNullOrEmpty(LogEmail))
            {
                if (!LogEmail.Contains("@") || !LogEmail.Contains("."))
                {
                    MessageBox.Show("Please enter a valid email address.");
                }
                else
                {
                    using (var db = new AviaBookingDbContext())
                    {
                        var checkClient = db.Clients.FirstOrDefault(p => p.Nickname == LogNickname || p.Email == LogEmail);
                        if(checkClient == null)
                        {
                            var client = new Client();
                            client.Nickname = LogNickname;
                            client.Name = LogName;
                            client.Surname= LogSurname;
                            client.Email= LogEmail;
                            client.Password= box.Password;
                            client.Role = "user";
                            db.Clients.Add(client);
                            db.SaveChanges();

                            RegButtonVisibility = Visibility.Collapsed;
                            LogButtonVisibility = Visibility.Visible;
                        }
                        else
                        {
                            MessageBox.Show("Пользователь с таким именем или почтой уже существует");
                        }
                    }


                }
            }


        }, parameter => parameter is PasswordBox box);


        // Функция выхода
        public ICommand _exitButton { get; set; }
        public ICommand ExitButton => _exitButton ??= new DelegateCommand<object>(parameter =>
        {
            PasswordBox box = (PasswordBox)parameter;
            box.Password = "";

            CurrentTicketsVisibility = Visibility.Collapsed;
            PersonalDataVisibility = Visibility.Collapsed;
            ChangePersonalDataVisibility = Visibility.Collapsed;
            HistoryVisibility = Visibility.Collapsed;
            LoginPageVisibility = Visibility.Visible;
            AccountPageVisibility = Visibility.Collapsed;
            LogNickname = "";
            LogName = "";
            LogSurname = "";
            LogEmail = "";
            LogPassword = "";
            LogSecuredPassword = "";
            AdminButtonVisibility = Visibility.Collapsed;
            AdminPanelVisibility = Visibility.Collapsed;

        }, parameter => parameter is PasswordBox box);
        #endregion

        #region Хэширование пароля
        private string GetSecureHash(SecureString secureString)
        {
            SHA256 sha256 = SHA256.Create();
            Span<byte> hashBytes = stackalloc byte[sha256.HashSize >> 3];
            IntPtr ptr = Marshal.SecureStringToBSTR(secureString);
            unsafe
            {
                ReadOnlySpan<byte> source = new ReadOnlySpan<byte>((void*)ptr, secureString.Length * sizeof(char));
                sha256.TryComputeHash(source, hashBytes, out _);
            }
            Marshal.ZeroFreeBSTR(ptr);
            return HashToString(hashBytes);
        }

        private string HashToString(ReadOnlySpan<byte> bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
                sb.Append(bytes[i].ToString("x2"));
            return sb.ToString();
        }
        #endregion
    }
}
