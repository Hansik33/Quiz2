using Quiz2.ViewModels;
using System.IO;
using System.Windows.Controls;

namespace Quiz2.Views
{
    public partial class ChangeCategoryView : UserControl
    {
        public ChangeCategoryView()
        {
            InitializeComponent();
            DataContext = new ChangeCategoryViewModel();
        }

        private void ChooseCategory(object sender, System.Windows.RoutedEventArgs e)
        {
            var givenObject = sender as Button;

            var chosenCategory = givenObject.Tag.ToString();

            File.WriteAllText("CHOSENCATEGORY.temp", chosenCategory);
        }
    }
}