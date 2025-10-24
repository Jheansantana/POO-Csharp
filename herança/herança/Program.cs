using System;

public class Pessoa
{
    public required string Nome { get; set; }
    public int Idade { get; set; }

    public void Apresentar()
    {
        Console.WriteLine($"Olá, meu nome é {Nome} e tenho {Idade} anos.");
    }
}


public class Aluno : Pessoa
{
    public required string Curso { get; set; }

    public void Estudar()
    {
        Console.WriteLine($"{Nome} está estudando {Curso}.");
    }
}

public class Professor : Pessoa
{
    public required string Disciplina { get; set; }

    public void Ensinar()
    {
        Console.WriteLine($"{Nome} está ensinando {Disciplina}.");
    }
}

class Program
{
    static void Main()
    {
        Aluno aluno = new Aluno
        {
            Nome = "Carlos",
            Idade = 20,
            Curso = "Engenharia"
        };

        aluno.Apresentar();  
        aluno.Estudar();   

        Console.WriteLine();

        Professor professor = new Professor
        {
            Nome = "Ana",
            Idade = 45,
            Disciplina = "Matemática"
        };

        professor.Apresentar();  
        professor.Ensinar(); 
    }
}
