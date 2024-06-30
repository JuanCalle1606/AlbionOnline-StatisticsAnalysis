﻿using StatisticsAnalysisTool.Common;
using StatisticsAnalysisTool.EventValidations;
using StatisticsAnalysisTool.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace StatisticsAnalysisTool.Views;

/// <summary>
/// Interaction logic for ConsoleWindowViewModel.xaml
/// </summary>
public partial class EventValidationWindow
{
    private static bool _isWindowMaximized;

    public EventValidationWindow()
    {
        InitializeComponent();
        DataContext = new EventValidationViewModel();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e) => Close();

    private void MinimizeButton_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

    private void MaximizedButton_Click(object sender, RoutedEventArgs e)
    {
        if (_isWindowMaximized)
        {
            WindowState = WindowState.Normal;
            Utilities.CenterWindowOnScreen(this);
            MaximizedButton.Content = 1;
            _isWindowMaximized = false;
        }
        else
        {
            WindowState = WindowState.Maximized;
            MaximizedButton.Content = 2;
            _isWindowMaximized = true;
        }
    }

    private void Hotbar_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
            DragMove();
    }

    private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2 && WindowState == WindowState.Normal)
        {
            WindowState = WindowState.Maximized;
            return;
        }

        if (e.ClickCount == 2 && WindowState == WindowState.Maximized) WindowState = WindowState.Normal;
    }
    
    private void BtnResetConsole_Click(object sender, RoutedEventArgs e)
    {
        EventValidator.Reset();
    }
}