using ControleTarefas.ConsoleApp.Controladores;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ControleTarefas.ConsoleApp.Telas
{
    public class TelaPrincipal: TelaBase
    {
        private readonly ControladorCadastroTarefa controladorCadastroTarefa;
        private readonly ControladorCadastroContato controladorCadastroContato;

        private readonly TelaTarefa telaTarefa;
        private readonly TelaContato telaContato;

        public TelaPrincipal() : base("Tela Principal")
        {
            controladorCadastroTarefa = new ControladorCadastroTarefa();
            controladorCadastroContato = new ControladorCadastroContato();

            telaTarefa = new TelaTarefa(controladorCadastroTarefa);
            telaContato = new TelaContato(controladorCadastroContato);
        }

        public TelaBase ObterTela()
        {
            ConfigurarTela("Escolha uma opção: ");

            TelaBase telaSelecionada = null;
            string opcao;
            do
            {
                Console.WriteLine("Digite 1 para o Cadastro de Tarefas");
                Console.WriteLine("Digite 2 para o Cadastro de Contatos");

                Console.WriteLine("Digite S para Sair");
                Console.WriteLine();
                Console.Write("Opção: ");
                opcao = Console.ReadLine();
                opcao.ToUpper();

                if (opcao == "1")
                    telaSelecionada = telaTarefa;

                if (opcao == "2")
                    telaSelecionada = telaContato;

            } while (OpcaoInvalida(opcao));

            return telaSelecionada;
        }

        private bool OpcaoInvalida(string opcao)
        {
            if (opcao != "1" && opcao != "2" && opcao != "S" && opcao != "s")
            {
                ApresentarMensagem("Opção inválida", TipoMensagem.Erro);
                return true;
            }
            return false;
        }

    }
}
