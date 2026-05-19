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
using System.IO;
using System.Linq;

namespace pr17_Savitsin
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Task1(object sender, RoutedEventArgs e) //Кнопка задание 1
        {
            PanelTask1.Visibility = Visibility.Visible;
            PanelTask2.Visibility = Visibility.Hidden;
            ReadFile();

        }

        private void Task2(object sender, RoutedEventArgs e) //Кнопка задание 2
        {
            PanelTask1.Visibility = Visibility.Hidden;
            PanelTask2.Visibility = Visibility.Visible;
        }

        private void Exit(object sender, RoutedEventArgs e) //Кнопка выход
        {
            this.Close();
        }

        string fileName = "file.txt";
        private void ReadFile() //Чтение текстового файла
        {
            if (File.Exists(fileName))
            {
                string text = File.ReadAllText(fileName);
                txtText.Text = text;
            }
            else
            {
                MessageBox.Show("Текстовый файл не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSearch(object sender, RoutedEventArgs e) //Кнопка поиск слова
        {
            if (txtText.Text != "")
            {
                if (txtWord.Text != "")
                {
                    char[] separator = { ' ', ',', '.', ':' };
                    string[] words = txtText.Text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    var result = from word in words where txtWord.Text.ToLower() == word.ToLower() select word; //слова из текста, совпадающие с введённом словом пользователя
                    txtResult.Text = $"Количество слов '{txtWord.Text}': {result.Count()}";
                }
                else
                {
                    MessageBox.Show("Введите слово", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Текстовый файл не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnStart(object sender, RoutedEventArgs e) //Кнопка начать
        {
            if (txtText2.Text != "")
            {
                if (txtText2.Text.Contains('/'))
                {
                    string text = txtText2.Text;

                    var Digit = from digit in txtText2.Text where char.IsDigit(digit) select digit; //Числа из текста

                    var beforeSlash = txtText2.Text.TakeWhile(c => c != '/'); //Элементы до /

                    var afterSlash = txtText2.Text.SkipWhile(c => c != '/').Skip(1); //Элементы после /

                    string ItemsAfterSlash = "";
                    foreach (var item in afterSlash)
                    {
                        ItemsAfterSlash += item;
                    }
                    string ItemsAfterSlashCopy = "";
                    for (int i = 0; i < ItemsAfterSlash.Length; i++)
                    {
                        if (char.IsUpper(ItemsAfterSlash[i])) //Изменяет регистр с верхнего на нижний
                        {
                            ItemsAfterSlashCopy += char.ToLower(ItemsAfterSlash[i]);
                        }
                        else if (char.IsLower(ItemsAfterSlash[i])) //Изменяет регистр с нижнего на верхний
                        {
                            ItemsAfterSlashCopy += char.ToUpper(ItemsAfterSlash[i]); 
                        }
                        else //Добавляет элементы другого типа
                        {
                            ItemsAfterSlashCopy += ItemsAfterSlash[i];
                        }
                    }

                    string ItemsBeforeSlash = "";
                    foreach (var item in beforeSlash)
                    {
                        ItemsBeforeSlash += item;
                    }

                    string[] afterSlashArray = ItemsAfterSlashCopy.Split(' '); //Массив элементов до /
                    string[] beforeSlashArray = ItemsBeforeSlash.Split(' '); //Массив элементов после /

                    txtResult2_1.Text = $"Количество цифр: {Digit.Count()}";
                    txtResult2_2.Text = $"Элементы до /: {string.Join("", beforeSlashArray)}";
                    txtResult2_3.Text = $"Изменённые элементы после /: {string.Join("", afterSlashArray)}";

                    string[] finalArray = beforeSlashArray.Concat(afterSlashArray).ToArray(); //Объединённые массивы элементов до / и после /

                    using (StreamWriter sw = File.CreateText("info.txt"))
                    {
                        foreach (var item in finalArray)
                        {
                            sw.WriteLine(item);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Текст должен содержать /", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Введите текст", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
