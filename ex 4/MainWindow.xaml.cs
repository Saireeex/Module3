using System;
using System.Collections.Generic;
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

namespace ex_4
{
    public class DataItem
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public DataItem(DateTime date, string description)
        {
            Date = date;
            Description = description;
        }

        public override string ToString()
        {
            return $"{Date.ToShortDateString()} - {Description}";
        }
    }
    public partial class MainWindow : Window
    {
        private List<DataItem> allDataItems;
        private List<DataItem> filteredDataItems;
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            allDataItems = new List<DataItem>
            {
                new DataItem(new DateTime(2024, 10, 1), "Отчет о продажах"),
                new DataItem(new DateTime(2024, 10, 2), "Собрание команды"),
                new DataItem(new DateTime(2024, 10, 1), "Подготовка документов"),
                new DataItem(new DateTime(2024, 10, 3), "Встреча с клиентом")
            };

            filteredDataItems = new List<DataItem>(allDataItems);
            UpdateListBox();
        }

        private void UpdateListBox()
        {
            DataListBox.ItemsSource = null;
            DataListBox.ItemsSource = filteredDataItems;
        }

        private void OnApplyKeywordFilter(object sender, RoutedEventArgs e)
        {
            string keyword = KeywordTextBox.Text;
            if (!string.IsNullOrEmpty(keyword))
            {
                // Используем делегат для фильтрации по ключевому слову
                filteredDataItems = allDataItems.Where(item => Filters.FilterByKeyword(item, keyword)).ToList();
                UpdateListBox();
            }
        }

        private void OnResetFilter(object sender, RoutedEventArgs e)
        {
            filteredDataItems = new List<DataItem>(allDataItems);
            UpdateListBox();
        }
    }

    public static class Filters
    {
        public static bool FilterByKeyword(DataItem item, string keyword)
        {
            return item.Description.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}