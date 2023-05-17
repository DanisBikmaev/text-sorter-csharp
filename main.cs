using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TextSorter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            // Открытие диалогового окна выбора файла для чтения
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                // Чтение текста из файла
                string text = File.ReadAllText(filePath);

                // Разделение текста на слова и удаление знаков препинания
                char[] separators = { ' ', ',', '.', '!', '?', ':', ';' };
                string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                // Сортировка слов методом выбора
                List<string> sortedWords = SelectionSort(words.ToList());

                // Открытие диалогового окна выбора файла для записи
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string newFilePath = saveFileDialog.FileName;

                    // Запись отсортированного текста в новый файл
                    File.WriteAllText(newFilePath, string.Join(" ", sortedWords));

                    MessageBox.Show("Сортировка завершена и результат сохранен в новом файле.");
                }
            }
        }

        private List<string> SelectionSort(List<string> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < list.Count; j++)
                {
                    if (string.Compare(list[j], list[minIndex], StringComparison.Ordinal) < 0)
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    string temp = list[i];
                    list[i] = list[minIndex];
                    list[minIndex] = temp;
                }
            }

            return list;
        }
    }
}
