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

            Greves.Add(new Greve(null, "Comboios", DateTime.Now.AddDays(0), DateTime.Now.AddDays(1), "bla bla", true, null, null, false, "CP", "http://google.com"));
            Greves.Add(new Greve(null, "Metro", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), "bla bla", true, null, null, false, "Metro de Lisboa", "http://google.com"));
            Greves.Add(new Greve(null, "Autocarro", DateTime.Now.AddDays(2), DateTime.Now.AddDays(3), "bla bla", true, null, null, false, "Carris", "http://google.com"));
            Greves.Add(new Greve(null, "Hospitais", DateTime.Now.AddDays(5), DateTime.Now.AddDays(6), "bla bla", true, null, null, false, "Centro Hospitalar do médio Tejo", "http://google.com"));
            Greves.Add(new Greve(null, "Educação", DateTime.Now.AddDays(20), DateTime.Now.AddDays(21), "bla bla", true, null, null, false, "Professores", "http://google.com"));
        }

        #endregion

        #region "Commands"

        public RelayCommand cmdNewProject => new RelayCommand(delegate (object o)
        {
            Task.Factory.StartNew(() => PropertiesMessageQueue.Enqueue("Click"));

        });

        #endregion
    }
}
