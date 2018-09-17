using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace StrikeSentinelAPI.Models
{
    public class Greve
    {

        public Greve(string id,string tipo, DateTime datainicio, DateTime datafim, string observacoes, bool tododia, string estado, string estadodescr, string cor, string empresa, string sourcelink)
        {
            Id = id;
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

        string _Id;
        public string Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
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
                }
            }
        }


        void SetDateName()
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            DateTime Today = DateTime.Now;
            DateTime Tomorrow = DateTime.Parse(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            if (Today >= DataInicio && Today <= DataFim)
            {
                DateGroup = "Hoje";
            }
            else if (Tomorrow >= DataInicio && Tomorrow <= DataFim)
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

    }

    public class GroupGreve
    {

        public GroupGreve(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public GroupGreve(string id, string name, Greve greve)
        {
            Id = id;
            Name = name;
            Greves.Add(greve);
        }

        public GroupGreve(string id, string name, List<Greve> greves)
        {
            Id = id;
            Name = name;
            Greves.AddRange(greves.ToArray());
        }

        string _Id;
        public string Id
        {
            get { return _Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                }
            }
        }

        string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                }
            }
        }

        List<Greve> _Greves = new List<Greve>();
        public List<Greve> Greves
        {
            get { return _Greves; }
            set
            {
                if (_Greves != value)
                {
                    _Greves = value;
                }
            }
        }
    }
}
