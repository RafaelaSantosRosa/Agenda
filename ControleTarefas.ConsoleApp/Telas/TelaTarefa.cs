using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleTarefas.ConsoleApp.Telas
{
    public class TelaTarefa: TelaBase, ICadastravel
    {
        private readonly ControladorCadastroTarefa controladorCadastroTarefa;

        public List<Tarefa> tarefasCadastradas = new List<Tarefa>();
        
        public TelaTarefa(ControladorCadastroTarefa controladorCadastroTarefa) : base("Tarefa")
        {
            this.controladorCadastroTarefa = controladorCadastroTarefa;
        }

        
        public void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo uma nova Tarefa...");

            Tarefa tarefa = ObterTarefa();

            string resultadoValidacao = controladorCadastroTarefa.Insere(tarefa, tarefasCadastradas);

            if (resultadoValidacao == "VALIDO")
                ApresentarMensagem("Tarefa inserida com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public bool VisualizarRegistros(TipoVisualizacao tipo)
        {
            if (tipo == TipoVisualizacao.VisualizandoTela)
                ConfigurarTela("Visualizando Tarefas Concluídas...");

            string configuracaoColunasTabela = "{0,-10} | {1,-55} | {2,-35}";

            MontarCabecalhoTabela(configuracaoColunasTabela, "Id", "Nome", "Telefone");

            List<Tarefa> tarefas = controladorCadastroTarefa.VisualizarTarefasConcluidas();

            foreach (Tarefa tarefa in tarefas)
            {
                Console.WriteLine(configuracaoColunasTabela, tarefa.Id, tarefa.Titulo, tarefa.PercentualConcluído);
            }
            return true;
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando uma tarefa...");

            bool temRegistros = VisualizarRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número da tarefaque deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Tarefa tarefa = ObterTarefa();

            string resultadoValidacao = controladorCadastroTarefa.Editar(tarefa, tarefasCadastradas);

            if (resultadoValidacao == "VALIDO")
                ApresentarMensagem("Tarefa editada com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                EditarRegistro();
            }
        }


        public void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo uma tarefa...");

            bool temRegistros = VisualizarRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número da tarefa que deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());

            controladorCadastroTarefa.Deleta(tarefasCadastradas);
        }


        protected Tarefa ObterTarefa()
        {
            Console.Write("Digite o título da tarefa: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite a data de criação da tarefa: ");
            DateTime dataCriação = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Digite a data de conclusão da tarefa: ");
            Nullable<DateTime> dataConclusao = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Digite o percentual concluído da tarefa: ");
            int percentualConcluído = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite a prioridade da tarefa: ");
            string prioridade = Console.ReadLine();

            return new Tarefa(titulo, dataCriação, dataConclusao, percentualConcluído, prioridade);
        }

    }
}
