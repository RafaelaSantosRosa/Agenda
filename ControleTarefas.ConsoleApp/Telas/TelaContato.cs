using ControleTarefas.ConsoleApp.Controladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleTarefas.ConsoleApp.Telas
{
    public class TelaContato: TelaBase, ICadastravel
    {
        private readonly ControladorCadastroContato controladorCadastroContato;

        public List<Contato> contatosCadastrados = new List<Contato>();
        public TelaContato(ControladorCadastroContato controladorCadastroContato) : base("Contato")
        {
            this.controladorCadastroContato = controladorCadastroContato;
        }

        public void InserirNovoRegistro()
        {
            ConfigurarTela("Inserindo um novo Contato...");

            Contato contato = ObterContato();

            string resultadoValidacao = controladorCadastroContato.Insere(contato, contatosCadastrados);

            if (resultadoValidacao == "VALIDO")
                ApresentarMensagem("Contato inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public bool VisualizarRegistros(TipoVisualizacao tipo)
        {
            if (tipo == TipoVisualizacao.VisualizandoTela)
                ConfigurarTela("Visualizando Contatos...");

            string configuracaoColunasTabela = "{0,-10} | {1,-55} | {2,-35}";

            MontarCabecalhoTabela(configuracaoColunasTabela, "Id", "Nome", "Telefone");

            List<Contato> contatos = controladorCadastroContato.VisualizarPorCargo();

            foreach (Contato contato in contatos)
            {
                Console.WriteLine(configuracaoColunasTabela, contato.Id, contato.Nome, contato.Telefone);
            }
            return true;
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando um Contato...");

            bool temRegistros = VisualizarRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número do contato que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Contato contato = ObterContato();

            string resultadoValidacao = controladorCadastroContato.Editar(contato, contatosCadastrados);

            if (resultadoValidacao == "VALIDO")
                ApresentarMensagem("Contato editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                EditarRegistro();
            }

        }

        public void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo um contato...");

            bool temRegistros = VisualizarRegistros(TipoVisualizacao.Pesquisando);

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número do contato que deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());

            controladorCadastroContato.Deleta(contatosCadastrados);

        }

        protected Contato ObterContato()
        {
            Console.Write("Digite o nome do contato: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o telefone do contato: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite o email do contato: ");
            string email = Console.ReadLine();

            Console.Write("Digite a empresa do contato: ");
            string empresa = Console.ReadLine();

            Console.Write("Digite o cargo do contato: ");
            string cargo = Console.ReadLine();

            return new Contato(nome, telefone, email, empresa, cargo);
        }
    }
}
