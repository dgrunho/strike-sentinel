using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrikeSentinel.Model
{
    public class Greve : INotifyPropertyChanged
    {
        public Greve(byte[] icon, string tipo, DateTime datainicio, DateTime datafim, string observacoes, bool tododia, DateTime? horainicio, DateTime? horafim, bool cancelada, string empresa, string sourcelink)
        {
            Icon = icon;
            Tipo = tipo;
            DataInicio = datainicio;
            DataFim = datafim;
            Observacoes = observacoes;
            TodoDia = tododia;
            HoraInicio = horainicio;
            HoraFim = horafim;
            Cancelada = cancelada;
            Empresa = empresa;
            SourceLink = sourcelink;
        }


        byte[] _Icon;
        public byte[] Icon
        {
            get { return _Icon; }
            set
            {
                if (_Icon != value)
                {
                    _Icon = value;
                    RaisePropertyChanged("Icon");
                }
            }
        }

        string _Tipo;
        public string Tipo
        {
            get { return _Tipo; }
            set
            {
                if (_Tipo != value)
                {
                    _Tipo = value;
                    RaisePropertyChanged("Tipo");
                }
            }
        }


        DateTime _DataInicio;
        public DateTime DataInicio
        {
            get { return _DataInicio; }
            set
            {
                if (_DataInicio != value)
                {
                    _DataInicio = value;
                    RaisePropertyChanged("DataInicio");
                }
            }
        }

        DateTime _DataFim;
        public DateTime DataFim
        {
            get { return _DataFim; }
            set
            {
                if (_DataFim != value)
                {
                    _DataFim = value;
                    RaisePropertyChanged("DataFim");
                }
            }
        }

        string _Observacoes;
        public string Observacoes
        {
            get { return _Observacoes; }
            set
            {
                if (_Observacoes != value)
                {
                    _Observacoes = value;
                    RaisePropertyChanged("Observacoes");
                }
            }
        }

        bool _TodoDia;
        public bool TodoDia
        {
            get { return _TodoDia; }
            set
            {
                if (_TodoDia != value)
                {
                    _TodoDia = value;
                    RaisePropertyChanged("_TodoDia");
                }
            }
        }

        DateTime? _HoraInicio;
        public DateTime? HoraInicio
        {
            get { return _HoraInicio; }
            set
            {
                if (_HoraInicio != value)
                {
                    _HoraInicio = value;
                    RaisePropertyChanged("HoraInicio");
                }
            }
        }

        DateTime? _HoraFim;
        public DateTime? HoraFim
        {
            get { return _HoraFim; }
            set
            {
                if (_HoraFim != value)
                {
                    _HoraFim = value;
                    RaisePropertyChanged("HoraFim");
                }
            }
        }


        bool _Cancelada;
        public bool Cancelada
        {
            get { return _Cancelada; }
            set
            {
                if (_Cancelada != value)
                {
                    _Cancelada = value;
                    RaisePropertyChanged("Cancelada");
                }
            }
        }

        string _Empresa;
        public string Empresa
        {
            get { return _Empresa; }
            set
            {
                if (_Empresa != value)
                {
                    _Empresa = value;
                    RaisePropertyChanged("Empresa");
                }
            }
        }

        string _SourceLink;
        public string SourceLink
        {
            get { return _SourceLink; }
            set
            {
                if (_SourceLink != value)
                {
                    _SourceLink = value;
                    RaisePropertyChanged("SourceLink");
                }
            }
        }


        void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
