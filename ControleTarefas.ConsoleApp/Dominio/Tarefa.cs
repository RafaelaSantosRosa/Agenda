using ControleTarefas.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleTarefas.ConsoleApp
{
    public class Tarefa : EntidadeBase
    {
        int id;
        string titulo;
        DateTime dataCriação;
        Nullable<DateTime> dataConclusão;
        int percentualConcluído;
        string prioridade;

        public List<Tarefa> tarefasCadastradas = new List<Tarefa>();

        Dictionary<int, string> prioridades = new Dictionary<int, string>()
        {
            {1, "Alta"},
            {2, "Normal" },
            {3, "Baixa" }
        };

        public Tarefa()
        {
        }

        public Tarefa(string titulo, DateTime dataCriação, Nullable<DateTime> dataConclusão, int percentualConcluído, string prioridade)
        {
            this.Titulo = titulo;
            this.DataCriação = dataCriação;
            this.DataConclusão = dataConclusão;
            this.PercentualConcluído = percentualConcluído;
            this.Prioridade = prioridade;
        }

        public int Id { get => id; set => id = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public DateTime DataCriação { get => dataCriação; set => dataCriação = value; }
        public Nullable<DateTime> DataConclusão { get => dataConclusão; set => dataConclusão = value; }
        public int PercentualConcluído { get => percentualConcluído; set => percentualConcluído = value; }
        public string Prioridade { get => prioridade; set => prioridade = value; }

        public override string Validar()
        {
            if(percentualConcluído > 100 || percentualConcluído < 1)
            {
                return "Percentual inválido!";
            }
            return "VALIDO";
        }
    }
}
