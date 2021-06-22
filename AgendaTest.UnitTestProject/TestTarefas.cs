using ControleTarefas.ConsoleApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AgendaTest.UnitTestProject
{
    [TestClass]
    public class TestTarefas
    {
        ControladorCadastroTarefa controladorCadastroTarefa = new ControladorCadastroTarefa();
        Tarefa percentual = new Tarefa();
        List<Tarefa> tarefasCadastradas = new List<Tarefa>();

        [TestMethod]
        public void TestPercentualZero()
        {
            //percentual = '0';
            //Assert.AreEqual("Percentual inválido!", controladorCadastroTarefa.Insere(percentual, tarefasCadastradas));
        }
    }
}
