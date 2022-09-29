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
using WpfApp4.DB;

namespace WpfApp4.MyForms
{
    /// <summary>
    /// Логика взаимодействия для UserAcauntWindow.xaml
    /// </summary>
    public partial class UserAcauntWindow : Window
    {
        private int _acauntId;
        private DB.Acaunt _Acaunt;
        private bool isStartFlag = false;
        public bool isSafe = false;
        public UserAcauntWindow(int acaunt)
        {
            InitializeComponent();
            _acauntId = acaunt;
            this.Loaded += UserAcauntWindow_Loaded;
        }

        private void UserAcauntWindow_Loaded(object sender, RoutedEventArgs e)
        {
            btSave.Visibility = Visibility.Collapsed;

            DB.MyContext myContext = new DB.MyContext();
            try
            {
                _Acaunt = myContext.Acaunts.Single(x => x.AcauntId == _acauntId);
                this.Title = $"Chenge profile {_Acaunt.AcauntName}";
                imageAcaunt.Source = GetImage(_Acaunt.PathImage);
                tbName.Text = _Acaunt.AcauntName;
                dgAcauntImage.ItemsSource = myContext.EnterControls.Where(x => x.AcauntId == _acauntId).OrderBy(x => x.DateTimeEnterControlId).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private ImageSource GetImage(string pathImage)
        {
            Uri uri = new Uri(@"pack://application:,,,/AcauntImage/" + pathImage);
            var bm = new BitmapImage(uri);
            return bm;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(isStartFlag==false)
            {
                isStartFlag= true;
                return;
            }
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            _Acaunt.AcauntName = tbName.Text;

            DB.MyContext myContext = new DB.MyContext();
            try
            {
                myContext.Acaunts.Update(_Acaunt);
                myContext.SaveChanges();
                isStartFlag = false;
                isSafe = true;
                MessageBox.Show("Resave ib Data Base");
                UserAcauntWindow_Loaded(null, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btClear_Click(object sender, RoutedEventArgs e)
        {
            var resDialog = MessageBox.Show("Delete?", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resDialog == MessageBoxResult.No)
                return;
            try
            {
                var enterSelect = dgAcauntImage.SelectedItem;
                MyContext myContext = new MyContext();
                List<DB.EnterControl> removList = new List<DB.EnterControl>();
                foreach (var row in enterSelect)
                {
                    DB.EnterControl enterControl = row as DB.EnterControl;

                    if(enterControl != null)
                    {
                        removList.Add(enterControl);
                    }
                }
                myContext.EnterControls.RemoveRange(removList);
                myContext.SaveChanges();

                MessageBox.Show("All flock in colt - delete");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error delete data in data base \n" + ex.Message);
            }
            finally
            {
                isStartFlag = false;
                isSafe = true;
                UserAcauntWindow_Loaded(null, null);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MyForms.ImageBox imageBox = new ImageBox();

            if (imageBox.ShowDialog() == true)
            {
                DB.MyContext myContext = new DB.MyContext();
                try
                {
                    _Acaunt.PathImage = imageBox.SelectImage.Name;
                    myContext.Acaunts.Update(_Acaunt);
                    myContext.SaveChanges();
                    isSafe = true;
                    UserAcauntWindow_Loaded(null, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
