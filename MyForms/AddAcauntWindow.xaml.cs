using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace WpfApp4.MyForms
{
    /// <summary>
    /// Логика взаимодействия для AddAcauntWindow.xaml
    /// </summary>
    public partial class AddAcauntWindow : Window
    {
        public AddAcauntWindow()
        {
            InitializeComponent();
            this.Loaded += AddAcauntWindow_Loaded;
        }

        private void AddAcauntWindow_Loaded(object sender, RoutedEventArgs e)
        {
            cbImage.ItemsSource = GetImage();
        }
        private List<ModelComboxImageAcaunt> GetImage()
        {
            List<ModelComboxImageAcaunt> acaunts = new List<ModelComboxImageAcaunt>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            string ddStart = assembly.Location;
            string dirImage = System.IO.Path.GetDirectoryName(ddStart);
            dirImage = dirImage + "/AcauntImage";
            var files = Directory.GetFiles(dirImage);

            var absolutPathFele = files.Where(x => x.ToLower().EndsWith(".png") || x.ToLower().EndsWith(".jpeg") || x.ToLower().EndsWith(".jpg")).ToArray();

            foreach (var file in absolutPathFele)
            {
                var name = System.IO.Path.GetFileName(file);
                var path = @"pack://application:,,,/AcauntImage/" + name;
                var newImage = new ModelComboxImageAcaunt() { Name = name, Path = path };
                acaunts.Add(newImage);
            }
            return acaunts;
        }
        private void btAddUser_Click(object sender, RoutedEventArgs e)
        {
            DB.MyContext myContext = new DB.MyContext();
            try
            {
                var newAcaunt = new DB.Acaunt();
                newAcaunt.Name = tbName.Text;
                var image = cbImage.SelectedItem as ModelComboxImageAcaunt;
                if (image != null)
                    newAcaunt.PathImage = image.Name;
                else
                {
                    MessageBox.Show("Select Image");
                    return;
                }
                myContext.Acaunts.Add(newAcaunt);
                btAddUser.IsEnabled = false;
                myContext.SaveChanges();
                btAddUser.IsEnabled = true;
                MessageBox.Show("object add in BD");
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btAddUser.IsEnabled = true;
            }
        }
    }
    public class ModelComboxImageAcaunt
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
