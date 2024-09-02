using Microsoft.Win32;
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

namespace TagManagerApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TagStorage storage = new TagStorage();

        public MainWindow()
        {
            InitializeComponent();
            TagTreeView.DataContext = storage;
        }

        private void MenuItem_Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                storage.LoadFromFile(openFileDialog.FileName);
                TagTreeView.ItemsSource = storage.Root.DirectChildren;
            }
        }

        private void MenuItem_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                storage.SaveToFile(saveFileDialog.FileName);
            }
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_AddTag_Click(object sender, RoutedEventArgs e)
        {
            if (TagTreeView.SelectedItem is TagItem selectedTag)
            {
                var dialog = new InputDialog("Введите имя нового тега:");
                if (dialog.ShowDialog() == true)
                {
                    string name = dialog.Input;
                    var addTagDialog = new AddTagDialog();
                    if (addTagDialog.ShowDialog() == true)
                    {
                        string type = addTagDialog.SelectedType;
                        object value = addTagDialog.InputValue;

                        var newTag = new TagItem(name, value);
                        selectedTag.AddChildTag(newTag);
                        TagTreeView.ItemsSource = null;
                        TagTreeView.ItemsSource = storage.Root.GetDirectChildren();
                    }
                }
            }
        }

        private void MenuItem_RenameTag_Click(object sender, RoutedEventArgs e)
        {
            if (TagTreeView.SelectedItem is TagItem selectedTag)
            {
                var dialog = new InputDialog("Введите новое имя тега:", selectedTag.TagName);
                if (dialog.ShowDialog() == true)
                {
                    selectedTag.TagName = dialog.Input;
                    TagTreeView.ItemsSource = null;
                    TagTreeView.ItemsSource = storage.Root.DirectChildren;
                }
            }
        }

        private void MenuItem_DeleteTag_Click(object sender, RoutedEventArgs e)
        {
            if (TagTreeView.SelectedItem is TagItem selectedTag && selectedTag.ParentTag != null)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить этот тег и все его потомки?", "Подтверждение удаления", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    selectedTag.ParentTag.RemoveChildTag(selectedTag.TagName);
                    TagTreeView.ItemsSource = null;
                    TagTreeView.ItemsSource = storage.Root.DirectChildren;
                }
            }
        }

        private void TagTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // Возможно, потребуется обновить интерфейс или выполнить действия при изменении выбранного тега
        }
    }

}
