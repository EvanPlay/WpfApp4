﻿using System;
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

namespace WpfApp4
{
    /// <summary>
    /// Логика взаимодействия для EnterSystem.xaml
    /// </summary>
    public partial class EnterSystem : Window
    {
        public EnterSystem()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded();
        }

        private RoutedEventHandler MainWindow_Loaded()
        {
            Controllers.EnterControlController enterControlController = new Controllers.EnterControlController();
            lEnterContent.ItemsSource.enterControlController.GetEntryControlTheLastFiveDays();
        }

        private void GoToAcaunt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
