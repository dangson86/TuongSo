﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TuongSo.Views
{
    /// <summary>
    /// Interaction logic for UserInput.xaml
    /// </summary>
    public partial class UserInput : UserControl
    {
        public static readonly DependencyProperty UserInputDayProperty = DependencyProperty.Register("Day", typeof(string), typeof(UserInput));
        public static readonly DependencyProperty UserInputMonthProperty = DependencyProperty.Register("Month", typeof(string), typeof(UserInput));
        public static readonly DependencyProperty UserInputYearProperty = DependencyProperty.Register("Year", typeof(string), typeof(UserInput));

        public static readonly DependencyProperty CustomerNameProperty = DependencyProperty.Register("CustomerName", typeof(string), typeof(UserInput));
        public static readonly DependencyProperty NicknameProperty = DependencyProperty.Register("NickName", typeof(string), typeof(UserInput));



        public string NickName
        {
            get { return (string)GetValue(NicknameProperty); }
            set { SetValue(NicknameProperty, value); }
        }

        public string CustomerName
        {
            get => (string)GetValue(CustomerNameProperty);
            set => SetValue(CustomerNameProperty, value);
        }

        public string Day
        {
            get => GetValue(UserInputDayProperty) as string;
            set => SetValue(UserInputDayProperty, value);
        }
        public string Month
        {
            get => GetValue(UserInputMonthProperty) as string;
            set => SetValue(UserInputMonthProperty, value);
        }
        public string Year
        {
            get => GetValue(UserInputYearProperty) as string;
            set => SetValue(UserInputYearProperty, value);
        }
        public UserInput()
        {
            InitializeComponent();
        }
    }
}
