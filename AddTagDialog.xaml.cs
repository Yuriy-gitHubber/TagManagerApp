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

namespace TagManagerApp
{
    /// <summary>
    /// Логика взаимодействия для AddTagDialog.xaml
    /// </summary>
    public partial class AddTagDialog : Window
    {
        public string SelectedType { get; private set; }
        public object InputValue { get; private set; }

        public AddTagDialog()
        {
            InitializeComponent();
        }

        private void cbType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SelectedType = (cbType.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();
            if (SelectedType == "none")
            {
                lblValue.Visibility = Visibility.Collapsed;
                txtValue.Visibility = Visibility.Collapsed;
            }
            else
            {
                lblValue.Visibility = Visibility.Visible;
                txtValue.Visibility = Visibility.Visible;
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedType != "none")
            {
                try
                {
                    if (SelectedType == "int")
                    {
                        InputValue = int.Parse(txtValue.Text);
                    }
                    else if (SelectedType == "double")
                    {
                        InputValue = double.Parse(txtValue.Text);
                    }
                    else if (SelectedType == "bool")
                    {
                        InputValue = bool.Parse(txtValue.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при вводе значения: " + ex.Message);
                    return;
                }
            }

            DialogResult = true;
        }
    }
}
