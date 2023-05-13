using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Uniza.Namedays.EditorGuiApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NamedayCalendar _calendar;
        private const double _version = 1.0;

        public MainWindow()
        {
            _calendar = new NamedayCalendar();
            FileInfo fi = new FileInfo("names.csv");
            _calendar.Load(fi);
            InitializeComponent();
            calendarG.DisplayDate = DateTime.Now.Date;
            Celebrates.Content = calendarG.DisplayDate.ToString("dd.MM.yyyy") + " celebrates:";
            var celebrs = _calendar[calendarG.DisplayDate];
            foreach (var name in celebrs)
            {
                Celebrators.Items.Add(name);
            }
        }

        private void Menu_New_Click(object sender, RoutedEventArgs e)
        {
            if (_calendar.Any())
            {
                var option = MessageBox.Show("Do you wish to clear the calendar?", "Clear calendar", MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question, MessageBoxResult.No);
                if (option == MessageBoxResult.Yes)
                {
                    _calendar.Clear();
                }
            }
            else
            {
                MessageBox.Show("Calendar is already empty", "Calendar is empty", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void Menu_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "CSV file (*.csv)|*.csv|All files (*.*)|*.*";
            if (fileDialog.ShowDialog() == false)
            {
                if (fileDialog.FileName.Any())
                {
                    FileInfo fi = new(fileDialog.FileName);
                    _calendar.Load(fi);
                }
                else
                {
                    MessageBox.Show("No file was chosen!", "No input file", MessageBoxButton.OKCancel,
                        MessageBoxImage.Error, MessageBoxResult.OK);
                }
                
            }
        }

        private void Menu_SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new();
            saveFileDialog.Filter = "CSV file (*.csv)|*.csv|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == false)
            {
                if (saveFileDialog.FileName.Any())
                {
                    FileInfo fi = new(saveFileDialog.FileName);
                    _calendar.Write(fi);
                }
                else
                {
                    MessageBox.Show("No file was chosen!", "No output file", MessageBoxButton.OKCancel,
                        MessageBoxImage.Error, MessageBoxResult.OK);
                }
            }
        }

        private void Menu_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Menu_About_Click(object sender, RoutedEventArgs e)
        {
            // TODO make new WPF window
            var text = "Namedays\n" +
                       "Version " + _version + "\n" +
                       "Copyright (c) 2023 David Kučera\n" +
                       "\n" +
                       "This is an app for editing and viewing namedays.";
            MessageBox.Show(text, "About application", MessageBoxButton.OK);
        }

        private void Calendar_Changed(object? sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            var cal = sender as Calendar;
            if (cal.SelectedDate.HasValue)
            {
                DateTime date = cal.SelectedDate.Value;
                Celebrators.Items.Clear();
                Celebrates.Content = date.ToString("dd.MM.yyyy") + " celebrates:";
                var celebrs = _calendar[date];
                foreach (var name in celebrs)
                {
                    Celebrators.Items.Add(name);
                }
            }
        }

        private void Today_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Now.Date;
            calendarG.DisplayDate = date;
            calendarG.SelectedDate = date;
            Celebrators.Items.Clear();
            Celebrates.Content = date.ToString("dd.MM.yyyy") + " celebrates:";
            var celebrs = _calendar[date];
            foreach (var name in celebrs)
            {
                Celebrators.Items.Add(name);
            }
        }
    }
}
