using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;

namespace Uniza.Namedays.EditorGuiApp
{
    /// <summary>
    /// Main window of the app.
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NamedayCalendar _calendar;
        private const string Version = "1.0";

        public MainWindow()
        {
            _calendar = new NamedayCalendar();
            var fi = new FileInfo("namedays-sk.csv");
            _calendar.Load(fi);
            InitializeComponent();

            CalendarG.DisplayDate = DateTime.Now.Date;
            Celebrates.Content = CalendarG.DisplayDate.ToString("dd.MM.yyyy") + " celebrates:";
            var celebrs = _calendar[CalendarG.DisplayDate];
            foreach (var name in celebrs)
            {
                Celebrators.Items.Add(name);
            }

            var dateFormat = new DateTimeFormatInfo();
            dateFormat = new CultureInfo("sk-SK").DateTimeFormat;
            MonthsBox.Items.Add("");
            for (var i = 1; i <= 12; i++)
            {
                MonthsBox.Items.Add(dateFormat.GetMonthName(i));
            }

            CountLabel.Content = "Count 0 / 0";

            MonthsBox.SelectionChanged += FilterChanged;
            RegexFilterBox.PreviewKeyUp += FilterChanged;

            // initially disable all manipulation buttons
            Disable_Buttons();
            ShowOnCalBt.IsEnabled = false; // TODO does not disable the button any way...

            // when nameday is chosen, enable buttons
            NamedaysListBox.GotMouseCapture += Enable_Buttons;

            // when focus on combo box or filter box, disable buttons
            MonthsBox.GotFocus += Disable_Buttons;
            RegexFilterBox.GotFocus += Disable_Buttons;

        }

        private void Menu_New_Click(object sender, RoutedEventArgs e)
        {
            if (_calendar.Any())
            {
                var option = MessageBox.Show("Do you wish to clear the calendar?", "Clear calendar", MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question, MessageBoxResult.No);
                if (option != MessageBoxResult.Yes) return;
                _calendar.Clear();
                NamedaysListBox.Items.Clear();
                Update_Count();
            }
            else
            {
                MessageBox.Show("Calendar is already empty", "Calendar is empty", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void Menu_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new()
            {
                Multiselect = false,
                Filter = "CSV file (*.csv)|*.csv|All files (*.*)|*.*"
            };
            fileDialog.ShowDialog();
            if (fileDialog.FileName.Any())
            {
                FileInfo fi = new(fileDialog.FileName);
                _calendar.Load(fi);
                NamedaysListBox.Items.Clear();
                Update_Count();
            }
            else
            {
                MessageBox.Show("No file was chosen!", "No input file", MessageBoxButton.OKCancel,
                    MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }

        private void Menu_SaveAs_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*"
            };
            saveFileDialog.ShowDialog();

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

        private void Menu_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Menu_About_Click(object sender, RoutedEventArgs e)
        {
            const string text = "Namedays\n" +
                                "Version " + Version + "\n" +
                                "Copyright (c) 2023 David Kučera\n" +
                                "\n" +
                                "This is an app for editing and viewing namedays.";
            MessageBox.Show(text, "About application", MessageBoxButton.OK);
        }

        private void Calendar_Changed(object? sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (!CalendarG.SelectedDate.HasValue) return;
            var date = CalendarG.SelectedDate.Value;
            Celebrators.Items.Clear();
            Celebrates.Content = date.ToString("dd.MM.yyyy") + " celebrates:";
            var celebrs = _calendar[date];
            foreach (var name in celebrs)
            {
                Celebrators.Items.Add(name);
            }
            Mouse.Capture(null);
        }

        private void Today_Click(object sender, RoutedEventArgs e)
        {
            var date = DateTime.Now.Date;
            CalendarG.DisplayDate = date;
            CalendarG.SelectedDate = date;
            Celebrators.Items.Clear();
            Celebrates.Content = date.ToString("dd.MM.yyyy") + " celebrates:";
            var celebrs = _calendar[date];
            foreach (var name in celebrs)
            {
                Celebrators.Items.Add(name);
            }
        }

        private void Clear_Filter_Click(object sender, RoutedEventArgs e)
        {
            RegexFilterBox.Text = "";
            MonthsBox.SelectedIndex = 0;
            Disable_Buttons(sender, e);
            Update_Count();
        }

        private void FilterChanged(object? sender, EventArgs e)
        {
            IEnumerable<Nameday> menaRegex;
            if (sender != null && sender.GetType() == typeof(TextBox) && ((KeyEventArgs)e).Key == Key.Enter)
            {
                menaRegex = _calendar.GetNamedays(RegexFilterBox.Text);
            }
            else
            {
                menaRegex = _calendar.GetNamedays();
            }

            var namesMonth = _calendar.GetNamedays();
            if (MonthsBox.SelectedIndex != 0)
            {
                namesMonth = _calendar.GetNamedays(MonthsBox.SelectedIndex);
            }

            var intersect = namesMonth.Intersect(menaRegex);

            NamedaysListBox.Items.Clear();
            foreach (var nameday in intersect)
            {
                NamedaysListBox.Items.Add(nameday);
            }

            Update_Count();
        }

        private void Update_Count()
        {
            var count = NamedaysListBox.Items.Count;
            var total = _calendar.GetNamedays().Count();
            CountLabel.Content = "Count: " + count + " / " + total;
        }

        private void Add_Date_Click(object sender, RoutedEventArgs e)
        {
            var newNameday = new AddWindow();
            newNameday.ShowDialog();

            var date = newNameday.DatePicker.SelectedDate;
            var name = newNameday.TextBox.Text;

            if (date != null && name != "")
            {
                _calendar.Add(date.Value.Day, date.Value.Month, name);
            }

            Update_Count();
            FilterChanged(sender, e);
        }

        private void Edit_Date_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Nameday)NamedaysListBox.SelectedItem;
            var editWindow = new EditWindow(selectedItem.DayMonth.ToDateTime(), selectedItem.Name);
            editWindow.Show();

            if (editWindow.DatePicker.SelectedDate != null)
            {
                _calendar.Remove(selectedItem.Name);
                var day = editWindow.DatePicker.SelectedDate.Value.Day;
                var month = editWindow.DatePicker.SelectedDate.Value.Month;
                var dayMonth = new DayMonth(day, month);
                var name = editWindow.TextBox.Text;
                _calendar.Add(dayMonth, name);
            }
            FilterChanged(sender, e);
            Disable_Buttons();
        }

        private void Remove_Date_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Nameday)NamedaysListBox.SelectedItem;
            var removeMb = MessageBox.Show("Do you really want to remove selected nameday (" + selectedItem.Name + ")?", "Remove nameday", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (removeMb == MessageBoxResult.Yes)
            {
                _calendar.Remove(selectedItem.Name);
            }
            FilterChanged(sender, e);
            Disable_Buttons();
        }

        private void Show_On_Calendar_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Nameday)NamedaysListBox.SelectedItem;
            CalendarG.SelectedDate = selectedItem.DayMonth.ToDateTime();
            CalendarG.DisplayDate = selectedItem.DayMonth.ToDateTime();
        }

        private void Disable_Buttons()
        {
            EditBt.IsEnabled = false;
            RemoveBt.IsEnabled = false;
            ShowOnCalBt.IsEnabled = false;
        }

        private void Disable_Buttons(object sender, RoutedEventArgs e)
        {
            Disable_Buttons();
        }

        private void Enable_Buttons(object sender, EventArgs e)
        {
            EditBt.IsEnabled = true;
            RemoveBt.IsEnabled = true;
            ShowOnCalBt.IsEnabled = true;
        }

    }
}
