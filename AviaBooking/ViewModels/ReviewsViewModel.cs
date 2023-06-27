using AviaBooking.Models;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class ReviewsViewModel : ViewModelBase
    {
        public ObservableCollection<Airline> Airlines { get; set; }
        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<Review> Reviews{ get; set; }
        public Airline SelectedAirline { get; set; }
        public ObservableCollection<ReviewModel> ReviewModels { get; set; }

        public Review review { get; set; }
        public Visibility ReviewMenuVisibility { get; set; }
        public int ClientID { get; set; }

        public ReviewsViewModel()
        {
            using (var db = new AviaBookingDbContext())
            {
                var airlines = db.Airlines.ToList();
                Airlines = new ObservableCollection<Airline>(airlines);
            }
            ReviewMenuVisibility = Visibility.Collapsed;
            review = new Review();

        }

        

        public ICommand SendReview
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    using (var db = new AviaBookingDbContext())
                    {
                        if(SelectedAirline == null)
                        {
                            MessageBox.Show("Выберите авиакомпанию");
                        }
                        if(SelectedAirline != null)
                        {
                            if (int.TryParse(review.Score.ToString(), out int score) && review.Text != null
                                && review.Score >= 1 && review.Score <= 5)
                            {
                                review.AirlineId = SelectedAirline.Id;
                                MessageBox.Show(review.Score + " " + review.Text);
                                review.ClientId = ClientID;
                                db.Reviews.Add(review);
                                db.SaveChanges();

                                var reviews = db.Reviews.Where(r => r.AirlineId == SelectedAirline.Id).ToList();

                                var clientIds = reviews.Select(r => r.ClientId).Distinct().ToList();
                                var clients = db.Clients.Where(c => clientIds.Contains(c.Id)).ToList();

                                ReviewModels = new ObservableCollection<ReviewModel>(
                                    reviews.Join(clients, r => r.ClientId, c => c.Id, (r, c) => new ReviewModel
                                    {
                                        ClientName = c.Name,
                                        ClientSurname = c.Surname,
                                        ReviewText = r.Text,
                                        Score = r.Score,
                                    }).ToList());
                            }
                            else if(review.Score < 1 || review.Score > 5)
                            {
                                MessageBox.Show("Введите оценку от 1 до 5");
                            }
                            else
                            {
                                MessageBox.Show("Заполните отзыв");
                            }
                        }
                        
                    }
                });
            }
        }
        public ICommand ShowReviewMenu
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                    Header mainFrame = (Header)mainWindow.FindName("Header");
                    var accountViewModel = mainFrame.DataContext as HeaderViewModel;

                    if (accountViewModel != null)
                    {
                        var account = accountViewModel.account.DataContext as AccountViewModel;

                        if(account.ClientID > 0)
                        {
                            if (ReviewMenuVisibility == Visibility.Collapsed)
                            {
                                ReviewMenuVisibility = Visibility.Visible;
                            }
                            else
                            {
                                ReviewMenuVisibility = Visibility.Collapsed;
                            }
                            ClientID = account.ClientID;
                        }
                        else
                        {
                            MessageBox.Show("Чтобы оставить отзыв войдите в аккаунт");
                        }
                    }


                    
                });
            }
        }
        public ICommand ShowReviews
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    using (var db = new AviaBookingDbContext())
                    {
                        if(SelectedAirline != null)
                        {
                            var reviews = db.Reviews.Where(r => r.AirlineId == SelectedAirline.Id).ToList();

                            var clientIds = reviews.Select(r => r.ClientId).Distinct().ToList();
                            var clients = db.Clients.Where(c => clientIds.Contains(c.Id)).ToList();

                            ReviewModels = new ObservableCollection<ReviewModel>(
                                reviews.Join(clients, r => r.ClientId, c => c.Id, (r, c) => new ReviewModel
                                {
                                    ClientName = c.Name,
                                    ClientSurname = c.Surname,
                                    ReviewText = r.Text,
                                    Score = r.Score,
                                }).ToList());
                        } else
                        {
                            MessageBox.Show("Выберите авиакомпанию");
                        }
                    }
                });
            }
        }
    }
}
