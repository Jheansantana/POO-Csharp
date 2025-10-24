using System;
using System.Collections.Generic;

namespace CadastroClientes
{
    class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public Cliente(int id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Nome: {Nome}, Email: {Email}";
        }
    }

    class ClienteRepositorio
    {
        private List<Cliente> clientes = new List<Cliente>();
        private int proximoId = 1;

        public void AdicionarCliente(string nome, string email)
        {
            Cliente cliente = new Cliente(proximoId, nome, email);
            clientes.Add(cliente);
            proximoId++;
        }

        public List<Cliente> ObterTodos()
        {
            return clientes;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ClienteRepositorio repositorio = new ClienteRepositorio();
            bool executando = true;

            while (executando)
            {
                Console.WriteLine("\n=== Sistema de Cadastro de Clientes ===");
                Console.WriteLine("1 - Cadastrar cliente");
                Console.WriteLine("2 - Listar clientes");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");
                string? opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.Write("Digite o nome do cliente: ");
                        string nome = Console.ReadLine() ?? "";

                        Console.Write("Digite o email do cliente: ");
                        string email = Console.ReadLine() ?? "";

                       
                        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(email))
                        {
                            Console.WriteLine("Nome e email não podem ser vazios.");
                        }
                        else
                        {
                            repositorio.AdicionarCliente(nome, email);
                            Console.WriteLine("✅ Cliente cadastrado com sucesso!");
                        }
                        break;

                    case "2":
                        var clientes = repositorio.ObterTodos();
                        if (clientes.Count == 0)
                        {
                            Console.WriteLine("Nenhum cliente cadastrado.");
                        }
                        else
                        {
                            Console.WriteLine("\n--- Lista de Clientes ---");
                            foreach (var cliente in clientes)
                            {
                                Console.WriteLine(cliente);
                            }
                        }
                        break;

                    case "0":
                        executando = false;
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }

            Console.WriteLine("Sistema encerrado.");
        }
    }
}
