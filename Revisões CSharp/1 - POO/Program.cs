using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _1___POO
{
    // Classe base, comum a todos os empregados
    class Empregado
    {
        private string nome;

        public Empregado(string nomeDaPessoa)
        {
            nome = nomeDaPessoa;
        }

        public void MostraNome()
        {
            Console.WriteLine("{0}", nome);
        }

        // Método preparado para ser alterado por classes derivadas
        public virtual void MostraFuncao()
        {
            Console.WriteLine("Empregado");
        }
    }

    // Classe patrao
    internal class Patrao : Empregado
    {
        public Patrao(string nomeDoPatrao) : base(nomeDoPatrao)
        {
        }

        // Nova implementação da funcionalidade "MostraFuncao"
        public override void MostraFuncao()
        {
            Console.WriteLine("Patrao");
        }
    }

    class Program
    {
        private static void Main()
        {
            // Uma pequena tabela dos trabalhadores da empresa
            Empregado[] trabalhadores =
            {
                new Empregado("Zé Maria"),
                new Empregado("António Carlos"),
                new Patrao("José António")
            };

            // Mostra o nome e a função de todos os trabalhadores
            foreach (Empregado t in trabalhadores)
            {
                t.MostraNome();
                t.MostraFuncao();
                Console.WriteLine();
            }
        }
    }
}
