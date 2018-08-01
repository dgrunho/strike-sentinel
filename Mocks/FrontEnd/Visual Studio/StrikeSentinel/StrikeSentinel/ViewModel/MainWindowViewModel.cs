using MaterialDesignThemes.Wpf;
using StrikeSentinel.Helpers;
using System;
using System.Collections.Generic;
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
