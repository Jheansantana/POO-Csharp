using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable 

namespace SistemaBancario
{
    public class Transacao
    {
        public DateTime Data { get; }
        public string Tipo { get; }
        public decimal Valor { get; }
        public string Descricao { get; }

        public Transacao(string tipo, decimal valor, string descricao = "")
        {
            Data = DateTime.Now;
            Tipo = tipo;
            Valor = valor;
            Descricao = descricao;
        }

        public override string ToString()
        {
            return $"{Data:dd/MM/yyyy HH:mm} | {Tipo} | R${Valor:F2} | {Descricao}";
        }
    }

    public class Conta
    {
        private static int proximoNumeroConta = 1000;

        public int NumeroConta { get; }
        public string Titular { get; }
        public decimal Saldo { get; private set; }
        public List<Transacao> Historico { get; }

        public Conta(string titular)
        {
            NumeroConta = proximoNumeroConta++;
            Titular = titular;
            Saldo = 0;
            Historico = new List<Transacao>();
        }

        public void Depositar(decimal valor)
        {
            Saldo += valor;
            Historico.Add(new Transacao("Depósito", valor));
        }

        public bool Sacar(decimal valor)
        {
            if (valor > Saldo)
                return false;

            Saldo -= valor;
            Historico.Add(new Transacao("Saque", valor));
            return true;
        }

        public bool Transferir(Conta destino, decimal valor)
        {
            if (valor > Saldo)
                return false;

            Saldo -= valor;
            destino.Saldo += valor;

            Historico.Add(new Transacao("Transferência Enviada", valor, $"Para conta {destino.NumeroConta}"));
            destino.Historico.Add(new Transacao("Transferência Recebida", valor, $"De conta {NumeroConta}"));
            return true;
        }

        public void ExibirExtrato()
        {
            Console.WriteLine($"\nExtrato da Conta {NumeroConta} - {Titular}:");
            foreach (var t in Historico)
                Console.WriteLine(t);
            Console.WriteLine($"Saldo atual: R${Saldo:F2}\n");
        }

        public override string ToString()
        {
            return $"Conta {NumeroConta} - Titular: {Titular} - Saldo: R${Saldo:F2}";
        }
    }

    class Program
    {
        static List<Conta> contas = new List<Conta>();

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("\n--- Sistema Bancário ---");
                Console.WriteLine("1. Criar Conta");
                Console.WriteLine("2. Listar Contas");
                Console.WriteLine("3. Depositar");
                Console.WriteLine("4. Sacar");
                Console.WriteLine("5. Transferir");
                Console.WriteLine("6. Ver Extrato");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");

                string? opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": CriarConta(); break;
                    case "2": ListarContas(); break;
                    case "3": Depositar(); break;
                    case "4": Sacar(); break;
                    case "5": Transferir(); break;
                    case "6": VerExtrato(); break;
                    case "0": return;
                    default: Console.WriteLine("Opção inválida!"); break;
                }
            }
        }

        static void CriarConta()
        {
            Console.Write("Nome do titular: ");
            string? nome = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("Nome inválido.");
                return;
            }

            var conta = new Conta(nome);
            contas.Add(conta);
            Console.WriteLine($"Conta criada com sucesso! Número da conta: {conta.NumeroConta}");
        }

        static Conta? BuscarConta()
        {
            Console.Write("Digite o número da conta: ");
            if (int.TryParse(Console.ReadLine(), out int numero))
            {
                var conta = contas.FirstOrDefault(c => c.NumeroConta == numero);
                if (conta == null)
                    Console.WriteLine("Conta não encontrada.");
                return conta;
            }
            else
            {
                Console.WriteLine("Número inválido.");
                return null;
            }
        }

        static void ListarContas()
        {
            if (contas.Count == 0)
            {
                Console.WriteLine("Nenhuma conta cadastrada.");
                return;
            }

            Console.WriteLine("\n--- Lista de Contas ---");
            foreach (var conta in contas)
                Console.WriteLine(conta);
        }

        static void Depositar()
        {
            var conta = BuscarConta();
            if (conta == null) return;

            Console.Write("Valor para depositar: R$");
            if (decimal.TryParse(Console.ReadLine(), out decimal valor) && valor > 0)
            {
                conta.Depositar(valor);
                Console.WriteLine("Depósito realizado com sucesso.");
            }
            else
            {
                Console.WriteLine("Valor inválido.");
            }
        }

        static void Sacar()
        {
            var conta = BuscarConta();
            if (conta == null) return;

            Console.Write("Valor para saque: R$");
            if (decimal.TryParse(Console.ReadLine(), out decimal valor) && valor > 0)
            {
                if (conta.Sacar(valor))
                    Console.WriteLine("Saque realizado com sucesso.");
                else
                    Console.WriteLine("Saldo insuficiente.");
            }
            else
            {
                Console.WriteLine("Valor inválido.");
            }
        }

        static void Transferir()
        {
            Console.WriteLine("Conta de origem:");
            var origem = BuscarConta();
            if (origem == null) return;

            Console.WriteLine("Conta de destino:");
            var destino = BuscarConta();
            if (destino == null || destino == origem)
            {
                Console.WriteLine("Conta de destino inválida.");
                return;
            }

            Console.Write("Valor para transferir: R$");
            if (decimal.TryParse(Console.ReadLine(), out decimal valor) && valor > 0)
            {
                if (origem.Transferir(destino, valor))
                    Console.WriteLine("Transferência realizada com sucesso.");
                else
                    Console.WriteLine("Saldo insuficiente para transferência.");
            }
            else
            {
                Console.WriteLine("Valor inválido.");
            }
        }

        static void VerExtrato()
        {
            var conta = BuscarConta();
            if (conta != null)
                conta.ExibirExtrato();
        }
    }
}
