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

namespace ShopManagement.ViewsForAdmin.Stats
{
    /// <summary>
    /// Interaction logic for CategoriesStatsView.xaml
    /// </summary>
    public partial class CategoriesStatsView : Window
    {
        public CategoriesStatsView()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
