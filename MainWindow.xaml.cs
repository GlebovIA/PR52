﻿using System.Windows;
using System.Windows.Controls;

namespace PR52
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow init;
        public MainWindow()
        {
            InitializeComponent();
            init = this;
            OpenPages(new Pages.Main());
        }
        public void OpenPages(Page page) => frame.Navigate(page);
    }
}
