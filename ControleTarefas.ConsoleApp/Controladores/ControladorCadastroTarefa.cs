using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;



namespace ControleTarefas.ConsoleApp
{
    public class ControladorCadastroTarefa
    {
        public static void IniciaConexao(SqlConnection conexaoComBanco)
        {
            string enderecoDBTarefa =
                @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefa;Integrated Security=True;Pooling=False";

            conexaoComBanco.ConnectionString = enderecoDBTarefa;
            conexaoComBanco.Open();
        }

        public string Insere(Tarefa tarefa, List<Tarefa> tarefasCadastradas)
        {
            string resultadoValidacao = tarefa.Validar();

            if (resultadoValidacao == "VALIDO")
            {
                SqlConnection conexaoComBanco = new SqlConnection();

                IniciaConexao(conexaoComBanco);

                SqlCommand comandoInsercao = new SqlCommand();
                comandoInsercao.Connection = conexaoComBanco;

                string sqlInsercao = ScriptInsercao();

                comandoInsercao.CommandText = sqlInsercao;

                Parametros(tarefa, comandoInsercao);
                
                tarefasCadastradas.Add(tarefa);

                comandoInsercao.ExecuteNonQuery();

                conexaoComBanco.Close();
            }
            return resultadoValidacao;
        }
     
        public string Editar(Tarefa tarefa, List<Tarefa> tarefasCadastradas)
        {
            string resultadoValidacao = tarefa.Validar();

            if (resultadoValidacao == "VALIDO")
            {
                SqlConnection conexaoComBanco = new SqlConnection();

                IniciaConexao(conexaoComBanco);

                SqlCommand comandoAtualizacao = new SqlCommand();
                comandoAtualizacao.Connection = conexaoComBanco;

                string sqlAtualizacao = ScriptAtualizacao();

                comandoAtualizacao.CommandText = sqlAtualizacao;

                comandoAtualizacao.Parameters.AddWithValue("ID",tarefa.Id);
                Parametros(tarefa, comandoAtualizacao);

                EditarNaLista(tarefa.Id, tarefa, tarefasCadastradas);

                comandoAtualizacao.ExecuteNonQuery();

                conexaoComBanco.Close();
            }
            return resultadoValidacao;
        }

        public void Deleta(List<Tarefa> tarefasCadastradas)
        {
            Tarefa tarefa = new Tarefa();

            SqlConnection conexaoComBanco = new SqlConnection();

            IniciaConexao(conexaoComBanco);

            SqlCommand comandoExclusao = new SqlCommand();
            comandoExclusao.Connection = conexaoComBanco;

            string sqlExclusao = ScriptExclusao();

            comandoExclusao.CommandText = sqlExclusao;

            comandoExclusao.Parameters.AddWithValue("ID", tarefa.Id);

            tarefasCadastradas.Remove(tarefasCadastradas.Find(x => x.Id == tarefa.Id));

            comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }

        
        public List<Tarefa> VisualizarTarefasConcluidas()
        {
            SqlConnection conexaoComBanco = new SqlConnection();

            IniciaConexao(conexaoComBanco);

            SqlCommand comandoSelecionarTodos = new SqlCommand();
            comandoSelecionarTodos.Connection = conexaoComBanco;

            string sqlSelecao = ScriptVisualizarTarefasConcluidas();

            comandoSelecionarTodos.CommandText = sqlSelecao;

            SqlDataReader leitorTarefas = comandoSelecionarTodos.ExecuteReader();

            List<Tarefa> tarefas = AdicionarTarefas(leitorTarefas);

            foreach (var item in tarefas)
            {
                Console.WriteLine(item.Titulo);
            }

            conexaoComBanco.Close();
            return tarefas;
        }

        
        public void VisualizarTarefaPendente()
        {
            SqlConnection conexaoComBanco = new SqlConnection();

            IniciaConexao(conexaoComBanco);

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao = ScriptVisualizarTarefasPendentes();

            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorTarefas = comandoSelecao.ExecuteReader();

            List<Tarefa> tarefas = AdicionarTarefas(leitorTarefas);

            foreach (var item in tarefas)
            {
                Console.WriteLine(item.Titulo);

            }

            conexaoComBanco.Close();
        }       

        private static List<Tarefa> AdicionarTarefas(SqlDataReader leitorTarefas)
        {
            List<Tarefa> tarefas = new List<Tarefa>();

            while (leitorTarefas.Read())
            {
                int id = Convert.ToInt32(leitorTarefas["ID"]);
                string titulo = Convert.ToString(leitorTarefas["TITULO"]);
                DateTime dataCriacao = Convert.ToDateTime(leitorTarefas["DATACRIACAO"]);
                DateTime dataConclusao = Convert.ToDateTime(leitorTarefas["DATACONCLUSAO"]);
                int percentualConclusao = Convert.ToInt32(leitorTarefas["PERCENTUALCONCLUSAO"]);
                string prioridade = Convert.ToString(leitorTarefas["PRIORIDADE"]);

                Tarefa tarefa = new Tarefa(titulo, dataCriacao, dataConclusao, percentualConclusao, prioridade);
                tarefa.Id = id;

                tarefas.Add(tarefa);
            }

            return tarefas;
        }

        private static void Parametros(Tarefa tarefa, SqlCommand comandoInsercao)
        {
            comandoInsercao.Parameters.AddWithValue("TITULO", tarefa.Titulo);
            comandoInsercao.Parameters.AddWithValue("DATACRIAÇÃO", tarefa.DataCriação);
            comandoInsercao.Parameters.AddWithValue("DATACONCLUSÃO", tarefa.DataConclusão);
            comandoInsercao.Parameters.AddWithValue("PERCENTUALCONCLUSÃO", tarefa.PercentualConcluído);
            comandoInsercao.Parameters.AddWithValue("PRIORIDADE", tarefa.Prioridade);
        }

        private void EditarNaLista(int idSelecionado, Tarefa tarefa, List<Tarefa> tarefasCadastradas)
        {
            for (int i = 0; i < tarefasCadastradas.Count; i++)
            {
                if (tarefasCadastradas[i].Id == idSelecionado)
                    tarefasCadastradas[i] = tarefa;
                break;
            }
        }


        #region SCRIPT
        private static string ScriptInsercao()
        {
            return @"INSERT INTO TBTAREFA
	                    (
		                    [TITULO], 
		                    [DATACRIAÇÃO], 
		                    [DATACONCLUSÃO], 
		                    [PERCENTUALCONCLUSÃO],
                            [PRIORIDADE]
	                    )
	                    VALUES 
	                    (
		                      @TITULO,
		                      @DATACRIAÇÃO,
		                      @DATACONCLUSÃO,
		                      @PERCENTUALCONCLUSÃO,
                              @PRIORIDADE
                        );
	                    SELECT SCOPE_IDENTITY();";
        }
        private static string ScriptAtualizacao()
        {
            return @"UPDATE TBTAREFA 
	            SET	
		            [TITULO] = @TITULO, 
		            [DATACRIAÇÃO]= @DATACRIAÇÃO, 
		            [DATACONCLUSÃO] = @DATACONCLUSÃO,
	                [PERCENTUALCONCLUSÃO] = @PERCENTUALCONCLUSÃO,
		            [PRIORIDADE] = @PRIORIDADE
	            WHERE 
		            [ID] = @ID";
        }
        private static string ScriptExclusao()
        {
            return @"DELETE FROM TBTAREFA 
	                                WHERE 
		                                [ID] = 1";
        }
        private static string ScriptVisualizarTarefasConcluidas()
        {
            return @"SELECT 
                      [ID], 
                      [TITULO], 
                      [DATACRIAÇÃO], 
                      [DATACONCLUSÃO], 
                      [PERCENTUALCONCLUSÃO],  
                      [PRIORIDADE] 
                    FROM TBTAREFA
                      WHERE
                         [PERCENTUALCONCLUSAO] = 100";
        }
        private static string ScriptVisualizarTarefasPendentes()
        {
            return @"SELECT 
                    [ID],
                    [TITULO], 
		            [DATACRIACAO], 
		            [DATACONCLUSAO] ,
	                [PERCENTUALCONCLUSAO],
                    [PRIORIDADE] 
                    FROM 
                        TBTAREFAS
                    WHERE
                        [PERCENTUALCONCLUSAO] < 100";
        }
        #endregion
    }
}
