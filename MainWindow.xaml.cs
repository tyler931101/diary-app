using Diary.Data;
using Diary.Models;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace Diary
{
    public partial class MainWindow : Window
    {
        private DateTime selectedDate;

        public MainWindow()
        {
            InitializeComponent();

            calendar.SelectedDate = DateTime.Today;
            selectedDate = DateTime.Today;

            LoadEntryForDate(selectedDate);
        }

        private void Calendar_SelectedDatesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (calendar.SelectedDate.HasValue)
            {
                selectedDate = calendar.SelectedDate.Value;
                LoadEntryForDate(selectedDate);
            }
        }

        private void LoadEntryForDate(DateTime date)
        {
            using var db = new DiaryContext();
            var entry = db.DiaryEntries.FirstOrDefault(e => e.CreatedAt.Date == date.Date);

            if (entry != null)
            {
                txtTitle.Text = entry.Title;
                txtContent.Text = entry.Content;
            }
            else
            {
                txtTitle.Text = "";
                txtContent.Text = "";
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using var db = new DiaryContext();
            var entry = db.DiaryEntries.FirstOrDefault(e => e.CreatedAt.Date == selectedDate.Date);

            if (entry == null)
            {
                entry = new DiaryEntry
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = txtTitle.Text,
                    Content = txtContent.Text,
                    CreatedAt = selectedDate,
                };
                db.DiaryEntries.Add(entry);
            }
            else
            {
                entry.Title = txtTitle.Text;
                entry.Content = txtContent.Text;
            }

            db.SaveChanges();
            MessageBox.Show("Diary saved!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}