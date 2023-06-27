using AviaBooking.View;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AviaBooking.ViewModels
{
    public partial class HeaderViewModel : ViewModelBase
    {
        public MainPage mainPage { get; set; }
        public Tickets tickets { get; set; }
        public Reviews reviews { get; set; }
        public Account account { get; set; }

        public HeaderViewModel() 
        { 
            mainPage = new MainPage();
            tickets = new Tickets();
            reviews = new Reviews();
            account = new Account();
        }


        // Кнопка личного кабинета
        public ICommand MainPageClick
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                    Frame mainFrame = (Frame)mainWindow.FindName("MainFrame");
                    mainFrame.Navigate(mainPage);

                });
            }
        }
        // Кнопка личного кабинета
        public ICommand TicketsPageClick
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                    Frame mainFrame = (Frame)mainWindow.FindName("MainFrame");
                    mainFrame.Navigate(tickets);

                });
            }
        }
        // Кнопка личного кабинета
        public ICommand ReviewsPageClick
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                    Frame mainFrame = (Frame)mainWindow.FindName("MainFrame");
                    mainFrame.Navigate(reviews);

                });
            }
        }
        // Кнопка личного кабинета
        public ICommand AccountPageClick
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                    Frame mainFrame = (Frame)mainWindow.FindName("MainFrame");
                    mainFrame.Navigate(account);

                });
            }
        }

    }

}
