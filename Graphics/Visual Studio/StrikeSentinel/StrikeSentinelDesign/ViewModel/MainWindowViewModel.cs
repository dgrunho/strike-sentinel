using MaterialDesignThemes.Wpf;
using StrikeSentinel.Helpers;
using StrikeSentinel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrikeSentinel.ViewModel
{
    class MainWindowViewModel : ViewModelBase

    {
        #region "Properties"

        string _Titulo = "Strike Sentinel";
        public string Titulo
        {
            get
            {
                return _Titulo;
            }
            set
            {
                if (_Titulo != value)
                {
                    _Titulo = value;
                    RaisePropertyChanged("Titulo");
                }
            }
        }

        bool _IsDarkTheme = false;
        public bool IsDarkTheme
        {
            get
            {
                return _IsDarkTheme;
            }
            set
            {
                if (_IsDarkTheme != value)
                {
                    _IsDarkTheme = value;
                    RaisePropertyChanged("IsDarkTheme");
                    new PaletteHelper().SetLightDark(_IsDarkTheme);
                }
            }
        }

        bool _IsMainToggleReturn = false;
        public bool IsMainToggleReturn
        {
            get
            {
                return _IsMainToggleReturn;
            }
            set
            {
                if (_IsMainToggleReturn != value)
                {
                    _IsMainToggleReturn = value;
                    if (_IsMainToggleReturn == true && SelectedTabIndex == 0)
                    {
                        IsMainMenuOpen = true;
                    }
                    
                    else if (_IsMainToggleReturn == false && IsMainMenuOpen == true)
                    {
                        IsMainMenuOpen = false;
                    }else if (_IsMainToggleReturn == false && SelectedTabIndex != 0)
                    {
                        SelectedTabIndex = 0;
                        GreveSelecionada = null;
                    }
                    RaisePropertyChanged("IsMainToggleReturn");
                }
            }
        }

        bool _IsMainMenuOpen = false;
        public bool IsMainMenuOpen
        {
            get
            {
                return _IsMainMenuOpen;
            }
            set
            {
                if (_IsMainMenuOpen != value)
                {
                    _IsMainMenuOpen = value;
                    RaisePropertyChanged("IsMainMenuOpen");
                }
            }
        }

        int _SelectedTabIndex = 0;
        public int SelectedTabIndex
        {
            get
            {
                return _SelectedTabIndex;
            }
            set
            {
                if (_SelectedTabIndex != value)
                {
                    _SelectedTabIndex = value;
                    RaisePropertyChanged("SelectedTabIndex");
                }
            }
        }

        ObservableCollection<Greve> _Greves = new ObservableCollection<Greve>();
        public ObservableCollection<Greve> Greves
        {
            get
            {
                return _Greves;
            }
            set
            {
                if (_Greves != value)
                {
                    _Greves = value;
                    RaisePropertyChanged("Greves");
                }
            }
        }


        Greve _GreveSelecionada = null;
        public Greve GreveSelecionada
        {
            get
            {
                return _GreveSelecionada;
            }
            set
            {
                if (_GreveSelecionada != value)
                {
                    _GreveSelecionada = value;
                    RaisePropertyChanged("GreveSelecionada");
                    if (_GreveSelecionada != null)
                    {
                        SelectedTabIndex = 1;
                        IsMainToggleReturn = true;
                    }
                }
            }
        }


        SnackbarMessageQueue _PropertiesMessageQueue = new SnackbarMessageQueue();
        public SnackbarMessageQueue PropertiesMessageQueue
        {
            get
            {
                return _PropertiesMessageQueue;
            }
            set
            {
                if (_PropertiesMessageQueue != value)
                {
                    _PropertiesMessageQueue = value;
                    RaisePropertyChanged("PropertiesMessageQueue");
                }
            }
        }
        #endregion

        #region "Initialization"

        public MainWindowViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                
            }

            DateTime Today = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            Greves.Add(new Greve(System.IO.File.ReadAllBytes("Recursos\\IconesEmpresas\\CP.png"), "Comboios", Today, Today.AddDays(1).AddMilliseconds(-1), "bla bla", true, "Check","Confirmada","Green", "CP", "http://google.com"));
            Greves.Add(new Greve(System.IO.File.ReadAllBytes("Recursos\\IconesEmpresas\\Metro.png"), "Metro", Today.AddDays(1), Today.AddDays(3).AddMilliseconds(-1), "bla bla", true, "Cancel", "Cancelada", "Red", "Metro de Lisboa", "http://google.com"));
            Greves.Add(new Greve(System.IO.File.ReadAllBytes("Recursos\\IconesEmpresas\\Carris.jpg"), "Autocarro", Today.AddDays(2).AddHours(10), Today.AddDays(2).AddHours(14), "bla bla", false, "Help", "A Confirmar", "GoldenRod", "Carris", "http://google.com"));
            Greves.Add(new Greve(System.IO.File.ReadAllBytes("Recursos\\IconesEmpresas\\CHMT.png"), "Hospitais", DateTime.Now.AddDays(5), DateTime.Now.AddDays(7).AddHours(4), "bla bla", false, "Help", "A Confirmar", "GoldenRod", "Centro Hospitalar do médio Tejo", "http://google.com"));
            Greves.Add(new Greve(System.IO.File.ReadAllBytes("Recursos\\IconesEmpresas\\Professor.png"), "Educação", DateTime.Now.AddDays(20), DateTime.Now.AddDays(21), "bla bla", true, "Help", "A Confirmar", "GoldenRod", "Professores", "http://google.com"));
        }

        #endregion

        #region "Commands"

        public RelayCommand cmdNewProject => new RelayCommand(delegate (object o)
        {
            Task.Factory.StartNew(() => PropertiesMessageQueue.Enqueue("Click"));

        });

        //public RelayCommand AbrirItem => new RelayCommand(delegate (object o)
        //{
        //    GreveSelecionada = null;
        //    IsMainToggleReturn = true;
        //    Task.Factory.StartNew(() => PropertiesMessageQueue.Enqueue("Abriu"));

        //});

        #endregion
    }
}
