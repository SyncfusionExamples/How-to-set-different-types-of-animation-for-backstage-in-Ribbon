using Microsoft.Win32;
using Syncfusion.Windows.Shared;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace BackStage
{
    public class ViewModel : NotificationObject
    {
        /// <summary>
        /// Maintains the command for backstage animation duration.
        /// </summary>
        private ICommand animationDurationCommand;

        /// <summary>
        ///  Maintains the collection of font.family.
        /// </summary>
        private ObservableCollection<Model> fontFamilyList = new ObservableCollection<Model>();

        /// <summary>
        ///  Maintains the collection of font.size.
        /// </summary>
        private ObservableCollection<Model> fontSizeList = new ObservableCollection<Model>();

        /// <summary>
        /// Maintains the command for exit backstage item.
        /// </summary>
        private ICommand exitCommand;

        /// <summary>
        /// Maintains the command for close backstage item.
        /// </summary>
        private ICommand closeCommand;

        /// <summary>
        /// Maintains the command for homeTab open backstage button.
        /// </summary>
        private ICommand homeTabOpenBackstageCommand;

        /// <summary>
        /// Maintains the command for insertTab open backstage button.
        /// </summary>
        private ICommand insertTabOpenBackstageCommand;

        /// <summary>
        /// Maintains the command for closing backstage from Help tabn.
        /// </summary>
        private ICommand helpTabCloseBackstageCommand;

        /// <summary>
        /// Maintains the command for inRecentTabCloseBackStage.
        /// </summary>
        private ICommand recentTabCloseBackStageCommand;

        /// <summary>
        /// Maintains the command for inInfoBackStage.
        /// </summary>
        private ICommand infoBackStageCloseCommand;

        /// <summary>
        /// Indicates whether the backstage can be closed or not in InfoTab <see cref="ViewModel"/> class.
        /// </summary>
        public bool CancelBackStageClosingInInfoTab { get; set; }

        /// <summary>
        /// Indicates whether the backstage can be shown or not in HomeTab <see cref="ViewModel"/> class.
        /// </summary>
        public bool CancelBackStageOpeningInHomeTab { get; set; }

        /// <summary>
        /// Indicates whether the backstage can be shown or not in InsertTab <see cref="ViewModel"/> class.
        /// </summary>
        public bool CancelBackStageOpeningInInsertTab { get; set; }

        /// <summary>
        /// Indicates whether the backstage can be shown or not in HelpTab <see cref="ViewModel"/> class.
        /// </summary>
        public bool CancelBackStageClosingInHelpTab { get; set; }

        /// <summary>
        /// Indicates whether the backstage can be shown or not in RecentTab <see cref="ViewModel"/> class.
        /// </summary>
        public bool CancelBackStageClosingInRecentTab { get; set; }

        /// <summary>
        /// Maintains the ribbon properties.
        /// </summary>
        private static Ribbon ribbon = null;

        public ViewModel()
        {
            animationDurationCommand = new DelegateCommand<object>(ExcecuteAnimationCommand);
            exitCommand = new DelegateCommand<object>(ExecuteMethod);
            closeCommand = new DelegateCommand<object>(CloseExecute);
            homeTabOpenBackstageCommand = new DelegateCommand<object>(HomeTabOpenBackstageExecute);
            insertTabOpenBackstageCommand = new DelegateCommand<object>(InsertTabOpenBackStageExecute);
            helpTabCloseBackstageCommand = new DelegateCommand<object>(HelpTabCloseBackstage);
            recentTabCloseBackStageCommand = new DelegateCommand<object>(RecentTabCloseBackStage);
            infoBackStageCloseCommand = new DelegateCommand<object>(InfoBackStageClose);
            InitializeFont_FamilySize();
        }

        /// <summary>
        /// Gets or sets the command for exit backstage command button <see cref="ViewModel"/> class.
        /// </summary>      
        public ICommand ExitCommand
        {
            get
            {
                return exitCommand;
            }
        }

        /// <summary>
        /// Gets or sets the command for backstage animation duration <see cref="ViewModel"/> class.
        /// </summary>
        public ICommand AnimationDurationCommand
        {
            get
            {
                return animationDurationCommand;
            }
        }

        /// <summary>
        /// Gets or sets the command for close backstage command button <see cref="ViewModel"/> class.
        /// </summary>
        public ICommand CloseCommand
        {
            get
            {
                return closeCommand;
            }
        }

        /// <summary>
        /// Gets or sets the command to open backstage in home tab button <see cref="ViewModel"/> class.
        /// </summary>
        public ICommand HomeTabOpenBackstageCommand
        {
            get
            {
                return homeTabOpenBackstageCommand;
            }
        }

        /// <summary>
        /// Gets or sets the command to open backstage in insert tab button <see cref="ViewModel"/> class.
        /// </summary>
        public ICommand InsertTabOpenBackstageCommand
        {
            get
            {
                return insertTabOpenBackstageCommand;
            }
        }

        /// <summary>
        /// Gets or sets the command to close backstage in help backstage tab <see cref="ViewModel"/> class.
        /// </summary>
        public ICommand HelpTabCloseBackstageCommand
        {
            get
            {
                return helpTabCloseBackstageCommand;
            }
        }

        /// <summary>
        /// Gets or sets the command for button in recent backstage tab <see cref="ViewModel"/> class.
        /// </summary>
        public ICommand RecentTabCloseBackStageCommand
        {
            get
            {
                return recentTabCloseBackStageCommand;
            }
        }

        /// <summary>
        /// Gets or sets the command for button in info backstage tab <see cref="ViewModel"/> class.
        /// </summary>
        public ICommand InfoBackStageCloseCommand
        {
            get
            {
                return infoBackStageCloseCommand;
            }
        }

        /// <summary>
        /// Gets or sets the font family for the ItemSource of RibbonComboBox Control <see cref="ViewModel"/> class.
        /// </summary>
        public ObservableCollection<Model> FontFamilyList
        {
            get
            {
                return fontFamilyList;
            }
            set
            {
                fontFamilyList = value;
                RaisePropertyChanged("FontFamilyList");
            }
        }

        /// <summary>
        /// Gets or sets the font size for the ItemSource of the RibbonComboBox Control <see cref="ViewModel"/> class.
        /// </summary>
        public ObservableCollection<Model> FontSizeList
        {
            get
            {
                return fontSizeList;
            }
            set
            {
                fontSizeList = value;
                RaisePropertyChanged("FontSizeList");
            }
        }

        /// <summary>
        /// Gets the ribbon property
        /// </summary>
        /// <param name="obj">Specifies the dependency object.</param>
        /// <returns></returns>
        public static Ribbon GetRibbon(DependencyObject obj)
        {
            return (Ribbon)obj.GetValue(RibbonProperty);
        }

        /// <summary>
        /// Sets the ribbon property
        /// </summary>
        /// <param name="obj">>Specifies the dependency object.</param>
        /// <param name="value">Specifies the ribbon value.</param>
        public static void SetRibbon(DependencyObject obj, Ribbon value)
        {
            obj.SetValue(RibbonProperty, value);
        }

        /// <summary>
        /// Dependency property.
        /// </summary>
        public static readonly DependencyProperty RibbonProperty =
            DependencyProperty.RegisterAttached("Ribbon", typeof(Ribbon), typeof(ViewModel), new FrameworkPropertyMetadata(OnRibbonChanged));

        /// <summary>
        /// Method used to access the ribbon control.
        /// </summary>
        /// <param name="obj">Specifies the dependency object.</param>
        /// <param name="args">Specifies the dependency property changes event args.</param>
        public static void OnRibbonChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ribbon = obj as Ribbon;
        }
        /// <summary>
        /// Method which is used to execute the action.
        /// </summary>
        /// <param name="parameter">Specifies the parameter of execute action.</param>
        private void ExecuteMethod(object parameter)
        {
            Window backstageDemosView = parameter as Window;
            if (backstageDemosView != null)
            {
                backstageDemosView.Close();
            }
        }

        /// <summary>
        /// Method to hide the backstage for close backstage button.
        /// </summary>
        /// <param name="parameter">Specifies the object type parameter.</param>
        private void CloseExecute(object parameter)
        {
            ribbon.HideBackStage();
        }

        /// <summary>
        /// Method to execute the command for opening backstage in home tab.
        /// </summary>
        /// <param name="parameter">Specifies the object type parameter.</param>
        private void HomeTabOpenBackstageExecute(object parameter)
        {
            if (CancelBackStageOpeningInHomeTab == false)
            {
                ribbon.ShowBackStage();
            }
        }

        /// <summary>
        /// Method to execute the command for opening backstage in insert tab.
        /// </summary>
        /// <param name="parameter">Specifies the object type parameter.</param>
        private void InsertTabOpenBackStageExecute(object parameter)
        {
            if (CancelBackStageOpeningInInsertTab == false)
            {
                ribbon.ShowBackStage();
            }
        }

        /// <summary>
        /// Method to execute the recent backstage button command.
        /// </summary>
        /// <param name="parameter">Specifies the object type parameter.</param>
        private void RecentTabCloseBackStage(object parameter)
        {
            if (CancelBackStageClosingInRecentTab == false)
            {
                ribbon.HideBackStage();
            }
        }

        /// <summary>
        /// Method to execute the info backstage tab command.
        /// </summary>
        /// <param name="parameter">Specifies the object type parameter.</param>
        private void InfoBackStageClose(object parameter)
        {
            if (CancelBackStageClosingInInfoTab == false)
            {
                ribbon.HideBackStage();
            }
        }


        /// <summary>
        /// Method to execute the command for hiding backstage.
        /// </summary>
        /// <param name="parameter">Specifies the object type parameter.</param>
        private void HelpTabCloseBackstage(object parameter)
        {
            if (CancelBackStageClosingInHelpTab == false)
            {
                ribbon.HideBackStage();
            }
        }

        /// <summary>
        /// Method to execute the command for backstage animation duration.
        /// </summary>
        /// <param name="parameter">Specifies the object type parameter.</param>
        private void ExcecuteAnimationCommand(object parameter)
        {
            if (parameter != null)
            {
                if ((parameter as RibbonComboBoxItem).Content.ToString().Contains("250"))
                {
                    ribbon.BackStage.AnimationDuration = TimeSpan.FromMilliseconds(250);
                }
                else if ((parameter as RibbonComboBoxItem).Content.ToString().Contains("300"))
                {
                    ribbon.BackStage.AnimationDuration = TimeSpan.FromMilliseconds(300);
                }
                else if ((parameter as RibbonComboBoxItem).Content.ToString().Contains("350"))
                {
                    ribbon.BackStage.AnimationDuration = TimeSpan.FromMilliseconds(350);
                }
                else if ((parameter as RibbonComboBoxItem).Content.ToString().Contains("400"))
                {
                    ribbon.BackStage.AnimationDuration = TimeSpan.FromMilliseconds(400);
                }
                else if ((parameter as RibbonComboBoxItem).Content.ToString().Contains("450"))
                {
                    ribbon.BackStage.AnimationDuration = TimeSpan.FromMilliseconds(450);
                }
            }
        }
        /// <summary>
        /// Initialization of font size and font family for RibbonComboBox.
        /// </summary>
        public void InitializeFont_FamilySize()
        {
            int[] sizes = new int[15] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 28, 36, 48, 72 };
            foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
            {
                FontFamilyList.Add(new Model() { FontFamily = fontFamily.ToString() });
            }
            for (int i = 0; i < sizes.Length; i++)
            {
                FontSizeList.Add(new Model() { FontSize = sizes[i] });
            }
        }
    }
}
