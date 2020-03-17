namespace SoftRazborki.Desktop.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Regions;

    internal class MainWindowViewModel : BindableBase
    {
        public static string MainContentRegion
        {
            get
            {
                return "MainContent";
            }
        }

        private string _title = "";

        private readonly IRegionManager regionManager;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                SetProperty(ref _title, value);
            }
        }

        public DelegateCommand ShowStartUpPageCommand
        {
            get
            {
                return new DelegateCommand(ShowStartUp);
            }
        }

        private void ShowStartUp()
        {
            Title = "SoftRazbokri";
            regionManager.RequestNavigate(MainContentRegion, "StartUp");
        }
    }
}
