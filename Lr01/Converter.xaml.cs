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
using System.Windows.Shapes;

namespace Lr01
{
    /// <summary>
    /// Логика взаимодействия для Converter.xaml
    /// </summary>
    public partial class Converter : Window
    {
        public Converter()
        {
            InitializeComponent();
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка, что текстовое поле не пустое
            if (string.IsNullOrEmpty(valueTextBox.Text))
            {
                resultLabel.Content = "Введите значение длины.";
                return;
            }

            // Проверка, что выбрано начальное измерение
            if (startUnitComboBox.SelectedItem == null)
            {
                resultLabel.Content = "Выберите начальное измерение.";
                return;
            }

            // Проверка, что выбрано конечное измерение
            if (!endUnitMeterRadioButton.IsChecked.HasValue || !endUnitKilometerRadioButton.IsChecked.HasValue ||
                !endUnitInchRadioButton.IsChecked.HasValue || !endUnitFeetRadioButton.IsChecked.HasValue ||
                (!endUnitMeterRadioButton.IsChecked.Value && !endUnitKilometerRadioButton.IsChecked.Value &&
                !endUnitInchRadioButton.IsChecked.Value && !endUnitFeetRadioButton.IsChecked.Value))
            {
                resultLabel.Content = "Выберите конечное измерение.";
                return;
            }

            // Получение значения длины из текстового поля
            if (!double.TryParse(valueTextBox.Text, out double value))
            {
                resultLabel.Content = "Некорректное значение длины.";
                return;
            }

            // Выполнение конвертации
            double result = 0;
            string startUnit = ((ComboBoxItem)startUnitComboBox.SelectedItem).Content.ToString();
            string endUnit = "";

            if (endUnitMeterRadioButton.IsChecked.Value)
            {
                endUnit = "Метры";
                result = ConvertToMeter(value, startUnit);
            }
            else if (endUnitKilometerRadioButton.IsChecked.Value)
            {
                endUnit = "Километры";
                result = ConvertToKilometer(value, startUnit);
            }
            else if (endUnitInchRadioButton.IsChecked.Value)
            {
                endUnit = "Дюймы";
                result = ConvertToInch(value, startUnit);
            }
            else if (endUnitFeetRadioButton.IsChecked.Value)
            {
                endUnit = "Футы";
                result = ConvertToFeet(value, startUnit);
            }

            // Вывод результата
            resultLabel.Content = $"{value} {startUnit} = {result} {endUnit}";
        }

        private double ConvertToMeter(double value, string unit)
        {
            switch (unit)
            {
                case "Метры":
                    return value;
                case "Километры":
                    return value * 1000;
                case "Дюймы":
                    return value * 0.0254;
                case "Футы":
                    return value * 0.3048;
                default:
                    throw new ArgumentException("Некорректное начальное измерение.");
            }
        }

        private double ConvertToKilometer(double value, string unit)
        {
            switch (unit)
            {
                case "Метры":
                    return value / 1000;
                case "Километры":
                    return value;
                case "Дюймы":
                    return value * 0.0000254;
                case "Футы":
                    return value * 0.0003048;
                default:
                    throw new ArgumentException("Некорректное начальное измерение.");
            }
        }

        private double ConvertToInch(double value, string unit)
        {
            switch (unit)
            {
                case "Метры":
                    return value / 0.0254;
                case "Километры":
                    return value / 0.0000254;
                case "Дюймы":
                    return value;
                case "Футы":
                    return value * 12;
                default:
                    throw new ArgumentException("Некорректное начальное измерение.");
            }
        }

        private double ConvertToFeet(double value, string unit)
        {
            switch (unit)
            {
                case "Метры":
                    return value / 0.3048;
                case "Километры":
                    return value / 0.0003048;
                case "Дюймы":
                    return value / 12;
                case "Футы":
                    return value;
                default:
                    throw new ArgumentException("Некорректное начальное измерение.");
            }
        }

        private void ValueTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Разрешение ввода только цифр и точки
            e.Handled = !IsNumericInput(e.Text);
        }

        private bool IsNumericInput(string text)
        {
            foreach (char c in text)
            {
                if (!char.IsDigit(c) && c != '.')
                {
                    return false;
                }
            }
            return true;
        }
    }
}


