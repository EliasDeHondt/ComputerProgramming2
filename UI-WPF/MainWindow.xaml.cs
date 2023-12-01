/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// 

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PadelClubManagement.UI.WIN
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); // Initialize the components of the window
            Title = "Padel Club Management";
            Icon = new BitmapImage(new Uri("pack://application:,,,/Resources/icon.ico"));
            Background = new SolidColorBrush(Color.FromRgb(90, 214, 255));
            
            
        }
    }
}