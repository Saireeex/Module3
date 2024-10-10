    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;

    namespace ex_3
    {
        public delegate void TaskDelegate(string taskDescription);
        public class TaskManager
        {
            private List<Task> tasks = new List<Task>();

            public void AddTask(string description, TaskDelegate taskDelegate)
            {
                tasks.Add(new Task(description, taskDelegate));
                MessageBox.Show($"Задача добавлена: {description}");
            }

            public void ExecuteAllTasks()
            {
                foreach (var task in tasks)
                {
                    task.Execute();
                }
            }
        }
        public class Task
        {
            public string Description { get; }
            private TaskDelegate TaskAction { get; }

            public Task(string description, TaskDelegate taskAction)
            {
                Description = description;
                TaskAction = taskAction;
            }

            public void Execute()
            {
                TaskAction?.Invoke(Description);
            }
        }
        public static class TaskHandlers
        {
            public static void SendNotification(string taskDescription)
            {
                MessageBox.Show($"Отправлено уведомление: {taskDescription}");
            }

            public static void LogToJournal(string taskDescription)
            {
                MessageBox.Show($"Запись в журнал: {taskDescription}");
            }

            public static void PrintToConsole(string taskDescription)
            {
                Console.WriteLine($"Вывод на консоль: {taskDescription}");
            }
        }

        /// <summary>
        /// Логика взаимодействия для MainWindow.xaml
        /// </summary>
        public partial class MainWindow : Window
        {
            private TaskManager taskManager = new TaskManager();

            public MainWindow()
            {
                InitializeComponent();
            }

            private void OnAddTask(object sender, RoutedEventArgs e)
            {
                string taskDescription = TaskDescriptionTextBox.Text;

                if (TaskTypeComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    TaskDelegate taskDelegate = null;

                    switch (selectedItem.Content.ToString())
                    {
                        case "Отправить уведомление":
                            taskDelegate = TaskHandlers.SendNotification;
                            break;
                        case "Запись в журнал":
                            taskDelegate = TaskHandlers.LogToJournal;
                            break;
                        case "Вывод на консоль":
                            taskDelegate = TaskHandlers.PrintToConsole;
                            break;
                    }

                    if (taskDelegate != null)
                    {
                        taskManager.AddTask(taskDescription, taskDelegate);
                    }
                    else
                    {
                        MessageBox.Show("Выберите тип задачи.");
                    }
                }
            }

            // Обработчик события для кнопки "Выполнить все задачи"
            private void OnExecuteTasks(object sender, RoutedEventArgs e)
            {
                taskManager.ExecuteAllTasks();
            }
        }
    }