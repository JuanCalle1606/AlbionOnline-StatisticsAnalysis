﻿using Ookii.Dialogs.Wpf;
using StatisticAnalysisTool.Extractor;
using StatisticAnalysisTool.Extractor.Enums;
using StatisticsAnalysisTool.Localization;
using System;

namespace StatisticsAnalysisTool.ViewModels;

public class GameDataPreparationWindowViewModel : BaseViewModel
{
    private string _title;
    private string _message;
    private string _path;
    private bool _isConfirmButtonEnabled;
    private string _errorMessage;

    public GameDataPreparationWindowViewModel()
    {
        Title = LocalizationController.Translation("SELECT_ALBION_ONLINE_MAIN_GAME_FOLDER");
        Message = LocalizationController.Translation("PLEASE_SELECT_A_CORRECT_ALBION_ONLINE_MAIN_GAME_FOLDER");
    }

    public void OpenPathSelection()
    {
        ErrorMessage = string.Empty;

        var dialog = new VistaFolderBrowserDialog()
        {
            Description = LocalizationController.Translation("SELECT_ALBION_ONLINE_MAIN_GAME_FOLDER"),
            RootFolder = Environment.SpecialFolder.Desktop,
            ShowNewFolderButton = false,
            UseDescriptionForTitle = true,
            Multiselect = false
        };

        var result = dialog.ShowDialog();

        if (result.HasValue && result.Value)
        {
            if (Extractor.IsValidMainGameFolder(dialog.SelectedPath ?? string.Empty, ServerType.Live))
            {
                Path = dialog.SelectedPath;
                IsConfirmButtonEnabled = true;
            }
            else
            {
                ErrorMessage = LocalizationController.Translation("PLEASE_SELECT_A_CORRECT_FOLDER");
                IsConfirmButtonEnabled = false;
            }
        }
    }

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            OnPropertyChanged();
        }
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged();
        }
    }

    public string Path
    {
        get => _path;
        set
        {
            _path = value;
            OnPropertyChanged();
        }
    }

    public bool IsConfirmButtonEnabled
    {
        get => _isConfirmButtonEnabled;
        set
        {
            _isConfirmButtonEnabled = value;
            OnPropertyChanged();
        }
    }

    public static string TranslationSelectMainGameFolder => LocalizationController.Translation("SELECT_MAIN_GAME_FOLDER_DOTS");
    public static string TranslationConfirm => LocalizationController.Translation("CONFIRM");
    public static string TranslationStandaloneLauncher => LocalizationController.Translation("STANDALONE_LAUNCHER");
    public static string TranslationStandaloneLauncherMessage => LocalizationController.Translation("STANDALONE_LAUNCHER_MESSAGE");
    public static string TranslationSteamLauncher => LocalizationController.Translation("STEAM_LAUNCHER");
    public static string TranslationSteamLauncherMessage => LocalizationController.Translation("STEAM_LAUNCHER_MESSAGE");
}