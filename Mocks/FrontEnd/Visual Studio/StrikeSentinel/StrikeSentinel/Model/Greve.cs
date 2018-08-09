using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrikeSentinel.Model
{
    public class Greve : INotifyPropertyChanged
    {
        public Greve(byte[] icon, string tipo, DateTime datainicio, DateTime datafim, string observacoes, bool tododia, string estado, string estadodescr, string cor, string empresa, string sourcelink)
        {
            Icon = icon;
            Tipo = tipo;
            DataInicio = datainicio;
            DataFim = datafim;
            Observacoes = observacoes;
            TodoDia = tododia;
            Estado = estado;
            EstadoDescr = estadodescr;
            Cor = cor;
            Empresa = empresa;
            SourceLink = sourcelink;
            SetDateName();
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

        string _Estado;
        public string Estado
        {
            get { return _Estado; }
            set
            {
                if (_Estado != value)
                {
                    _Estado = value;
                    RaisePropertyChanged("Estado");
                }
            }
        }

        string _EstadoDescr;
        public string EstadoDescr
        {
            get { return _EstadoDescr; }
            set
            {
                if (_EstadoDescr != value)
                {
                    _EstadoDescr = value;
                    RaisePropertyChanged("EstadoDescr");
                }
            }
        }

        string _Cor;
        public string Cor
        {
            get { return _Cor; }
            set
            {
                if (_Cor != value)
                {
                    _Cor = value;
                    RaisePropertyChanged("Cor");
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

        string _DateGroup;
        public string DateGroup
        {
            get { return _DateGroup; }
            set
            {
                if (_DateGroup != value)
                {
                    _DateGroup = value;
                    RaisePropertyChanged("DateGroup");
                }
            }
        }

        void SetDateName() {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            DateTime Today = DateTime.Now;
            DateTime Tomorrow = DateTime.Parse(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            if (Today  >= DataInicio && Today <= DataFim)
            {
                DateGroup = "Hoje";
            } else if (Tomorrow >= DataInicio && Tomorrow <= DataFim)
            {
                DateGroup = "Amanhã";
            }
            else
            {
                if (DataInicio.Year == Today.Year)
                {
                    DateGroup = textInfo.ToTitleCase(DataInicio.ToString("MMMM", CultureInfo.CreateSpecificCulture("pt")));
                }
                else 
                {
                    DateGroup = textInfo.ToTitleCase(DataInicio.ToString("MMMM", CultureInfo.CreateSpecificCulture("pt"))) + " " + DataInicio.Year;
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
