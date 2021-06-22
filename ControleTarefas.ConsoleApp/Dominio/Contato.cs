
using ControleTarefas.ConsoleApp.Dominio;
using System.Collections.Generic;

namespace ControleTarefas.ConsoleApp
{
    public class Contato : EntidadeBase
    {
        int id;
        string nome;
        string email;
        string telefone;
        string empresa;
        string cargo;

        public Contato()
        {
        }

        public List<Contato> contatosCadastrados = new List<Contato>();

        public Contato(string nome, string email, string telefone, string empresa, string cargo)
        {
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
            this.empresa = empresa;
            this.cargo = cargo;
        }

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Email { get => email; set => email = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string Empresa { get => empresa; set => empresa = value; }
        public string Cargo { get => cargo; set => cargo = value; }

        public override string Validar()
        {
            if(telefone.Length < 9)
            {
                return "Número de telefone inválido!";
            }
            return "VALIDO";
        }
    }
}
