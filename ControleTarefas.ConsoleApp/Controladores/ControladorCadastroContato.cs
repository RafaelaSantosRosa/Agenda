using ControleTarefas.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleTarefas.ConsoleApp.Controladores
{
    public class ControladorCadastroContato
    {
        public static void IniciaConexao(SqlConnection conexaoComBanco)
        {
            string enderecoDBTarefa =
                @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefa;Integrated Security=True;Pooling=False";

            conexaoComBanco.ConnectionString = enderecoDBTarefa;
            conexaoComBanco.Open();
        }

        public string Insere(Contato contato, List<Contato> contatosCadastrados)
        {
            string resultadoValidacao = contato.Validar();

            if (resultadoValidacao == "VALIDO")
            {
                SqlConnection conexaoComBanco = new SqlConnection();

                IniciaConexao(conexaoComBanco);

                SqlCommand comandoInsercao = new SqlCommand();
                comandoInsercao.Connection = conexaoComBanco;

                string sqlInsercao = ScriptInsercao();

                comandoInsercao.CommandText = sqlInsercao;
                
                Parametros(contato, comandoInsercao);

                contatosCadastrados.Add(contato);

                comandoInsercao.ExecuteNonQuery();

                conexaoComBanco.Close();
            }
            return resultadoValidacao;
        }

        public string Editar(Contato contato, List<Contato> contatosCadastrados)
        {
            string resultadoValidacao = contato.Validar();

            if (resultadoValidacao == "VALIDO")
            {
                SqlConnection conexaoComBanco = new SqlConnection();

                IniciaConexao(conexaoComBanco);

                SqlCommand comandoAtualizacao = new SqlCommand();
                comandoAtualizacao.Connection = conexaoComBanco;

                string sqlAtualizacao = ScriptAtualizacao();

                comandoAtualizacao.CommandText = sqlAtualizacao;

                comandoAtualizacao.Parameters.AddWithValue("ID", contato.Id);

                Parametros(contato, comandoAtualizacao);

                EditarNaLista(contato.Id, contato, contatosCadastrados);

                comandoAtualizacao.ExecuteNonQuery();

                conexaoComBanco.Close();
            }
            return resultadoValidacao;
        }


        public void Deleta(List<Contato> contatosCadastrados)
        {
            Contato contato = new Contato();

            SqlConnection conexaoComBanco = new SqlConnection();

            IniciaConexao(conexaoComBanco);

            SqlCommand comandoExclusao = new SqlCommand();
            comandoExclusao.Connection = conexaoComBanco;

            string sqlExclusao = ScriptExclusao();

            comandoExclusao.CommandText = sqlExclusao;

            comandoExclusao.Parameters.AddWithValue("ID", contato.Id);

            contatosCadastrados.Remove(contatosCadastrados.Find(x => x.Id == contato.Id));

            comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }

        public List<Contato> VisualizarPorCargo()
        {

            SqlConnection conexaoComBanco = new SqlConnection();

            IniciaConexao(conexaoComBanco);

            SqlCommand comandoSelecionarTodos = new SqlCommand();
            comandoSelecionarTodos.Connection = conexaoComBanco;

            string sqlSelecao = @"SELECT 
                                    [ID], 
                                    [NOME], 
                                    [TELEFONE], 
                                    [EMAIL], 
                                    [EMPRESA], 
                                    [CARGO] 
                                FROM TBCONTATO ";

            comandoSelecionarTodos.CommandText = sqlSelecao;

            SqlDataReader leitorContatos = comandoSelecionarTodos.ExecuteReader();
            
            List<Contato> contatos = AdicionarContatos(leitorContatos);

            conexaoComBanco.Close();
            return contatos;
        }


        private void EditarNaLista(int id, Contato contato, List<Contato> contatosCadastrados)
        {
            for (int i = 0; i < contatosCadastrados.Count; i++)
            {
                if (contatosCadastrados[i].Id == id)
                    contatosCadastrados[i] = contato;
                break;
            }
        }

        private static List<Contato> AdicionarContatos(SqlDataReader leitorContatos)
        {
            List<Contato> contatos = new List<Contato>();

            while (leitorContatos.Read())
            {
                int id = Convert.ToInt32(leitorContatos["ID"]);
                string nome = Convert.ToString(leitorContatos["NOME"]);
                string telefone = Convert.ToString(leitorContatos["TELEFONE"]);
                string email = Convert.ToString(leitorContatos["EMAIL"]);
                string empresa = Convert.ToString(leitorContatos["EMPRESA"]);
                string cargo = Convert.ToString(leitorContatos["CARGO"]);

                contatos.Add(new Contato(nome, telefone, email, empresa, cargo));
            }

            return contatos;
        }

        private static void Parametros(Contato contato, SqlCommand comandoInsercao)
        {
            comandoInsercao.Parameters.AddWithValue("NOME", contato.Nome);
            comandoInsercao.Parameters.AddWithValue("TELEFONE", contato.Telefone);
            comandoInsercao.Parameters.AddWithValue("EMAIL", contato.Email);
            comandoInsercao.Parameters.AddWithValue("EMPRESA", contato.Empresa);
            comandoInsercao.Parameters.AddWithValue("CARGO", contato.Cargo);
        }

        #region SCRIPT
        private static string ScriptInsercao()
        {
            return @"insert into TBContato
	                                (
		                                [NOME], 
		                                [TELEFONE], 
		                                [EMAIL], 
		                                [EMPRESA],
		                                [CARGO]
	                                )
	                                VALUES 
	                                (
		                                    @NOME, 
		                                    @TELEFONE, 
		                                    @EMAIL, 
		                                    @EMPRESA,
		                                    @CARGO	  
	                                );
	                                SELECT SCOPE_IDENTITY();";
        }

        private static string ScriptAtualizacao()
        {
            return @"UPDATE TBCONTATO 
	                                    SET	
		                                    [NOME] =  @NOME, 
		                                    [TELEFONE]= @TELEFONE, 
		                                    [EMAIL] = @EMAIL,
	                                        [EMPRESA] = @EMPRESA,
		                                    [CARGO] = @CARGO	
	                                    WHERE 
		                                    [ID] = 1";
        }


        private static string ScriptExclusao()
        {
            return @"DELETE FROM TBCONTATO 
	                                WHERE 
		                                [ID] = 1";
        }
        #endregion
    }
}
