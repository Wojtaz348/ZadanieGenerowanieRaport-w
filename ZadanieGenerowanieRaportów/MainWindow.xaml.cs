using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ZadanieGenerowanieRaportow
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Report> Reports { get; set; }
        public ObservableCollection<ReportItem> ReportItems { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Reports = new ObservableCollection<Report>();
            ReportItems = new ObservableCollection<ReportItem>();
            ItemsDataGrid.ItemsSource = ReportItems;
            SavedReportsListView.ItemsSource = Reports;
        }

        private void PreviewReport_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text) || ReportDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Uzupełnij wszystkie pola!");
                return;
            }

            string reportSummary = $"Tytuł: {TitleTextBox.Text}\nData: {ReportDatePicker.SelectedDate.Value:d}\n\nElementy:";
            foreach (var item in ReportItems)
            {
                reportSummary += $"\n- {item.Name}: {item.Details}";
            }

            MessageBox.Show(reportSummary, "Podgląd raportu");
        }

        private void SaveReport_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text) || ReportDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Uzupełnij wszystkie pola!");
                return;
            }

            var report = new Report
            {
                Title = TitleTextBox.Text,
                Date = ReportDatePicker.SelectedDate.Value,
                Items = new ObservableCollection<ReportItem>(ReportItems)
            };
            Reports.Add(report);

            MessageBox.Show("Raport zapisany!", "Informacja");
        }

        private void OpenReport_Click(object sender, RoutedEventArgs e)
        {
            if (SavedReportsListView.SelectedItem is not Report selectedReport)
            {
                MessageBox.Show("Wybierz raport z listy!");
                return;
            }

            string reportDetails = $"Tytuł: {selectedReport.Title}\nData: {selectedReport.Date:d}\n\nElementy:";
            foreach (var item in selectedReport.Items)
            {
                reportDetails += $"\n- {item.Name}: {item.Details}";
            }

            MessageBox.Show(reportDetails, "Szczegóły raportu");
        }

        private void ExportToPdf_Click(object sender, RoutedEventArgs e)
        {
            if (SavedReportsListView.SelectedItem is not Report selectedReport)
            {
                MessageBox.Show("Wybierz raport do eksportu!");
                return;
            }
            MessageBox.Show("Funkcja eksportu do PDF w przygotowaniu.", "Informacja");
        }
    }

    public class Report
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public ObservableCollection<ReportItem> Items { get; set; }
    }

    public class ReportItem
    {
        public string Name { get; set; }
        public string Details { get; set; }
    }
}
