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

namespace уч_5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private delegate int[] SortingMethod(int[] numbers);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnBubbleSort(object sender, RoutedEventArgs e)
        {
            PerformSort(BubbleSort);
        }
        private void OnQuickSort(object sender, RoutedEventArgs e)
        {
            PerformSort(QuickSort);
        }

        // Метод, выполняющий сортировку
        private void PerformSort(SortingMethod sortingMethod)
        {
            try
            {
                int[] numbers = NumbersTextBox.Text.Split(',')
                                    .Select(int.Parse)
                                    .ToArray();

                int[] sortedNumbers = sortingMethod(numbers);

                SortedNumbersTextBox.Text = string.Join(", ", sortedNumbers);
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые данные, разделенные запятой.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Реализация сортировки пузырьком
        private int[] BubbleSort(int[] numbers)
        {
            int n = numbers.Length;
            int[] sortedNumbers = (int[])numbers.Clone();
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (sortedNumbers[j] > sortedNumbers[j + 1])
                    {
                        int temp = sortedNumbers[j];
                        sortedNumbers[j] = sortedNumbers[j + 1];
                        sortedNumbers[j + 1] = temp;
                    }
                }
            }
            return sortedNumbers;
        }

        // Реализация быстрой сортировки
        private int[] QuickSort(int[] numbers)
        {
            if (numbers.Length <= 1)
            {
                return numbers;
            }

            int pivot = numbers[numbers.Length / 2];
            int[] left = numbers.Where(x => x < pivot).ToArray();
            int[] right = numbers.Where(x => x > pivot).ToArray();

            return QuickSort(left).Concat(new int[] { pivot }).Concat(QuickSort(right)).ToArray();
        }
    }
}
