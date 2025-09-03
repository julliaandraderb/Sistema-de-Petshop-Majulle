using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        PetShop petShop = new PetShop();
        int opcao;

        do
        {
            Console.WriteLine("=== Pet Shop Majulle ===");
            Console.WriteLine("1 - Cadastrar pet");
            Console.WriteLine("2 - Listar pets cadastrados");
            Console.WriteLine("3 - Média da Idade dos Pets");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
            
            if (!int.TryParse(Console.ReadLine(), out opcao))
                opcao = -1;

            switch (opcao)
            {
                case 1:
                    petShop.CadastrarPet();
                    break;

                case 2:
                    petShop.ListarPets();
                    break;

                case 3: 
                       petShop.CalcularMediaIdade();
                    break;

                case 0:
                    Console.WriteLine("Saindo do sistema...");
                    break;

                default:
                    Console.WriteLine("Opção inválida, tente novamente!");
                    break;
            }

        } while (opcao != 0);
    }
}

class Animal
{
    public string Nome;
    public int Idade;

    public Animal(string nome, int idade)
    {
        Nome = nome;
        Idade = idade;
    }

    public virtual void EmitirSom()
    {
        Console.WriteLine("Som de animal");
    }

    public virtual void ExibirInfo()
    {
        Console.WriteLine($"Nome: {Nome}, Idade: {Idade}");
    }
}

class Cachorro : Animal
{
    public string Raca;

    public Cachorro(string nome, int idade, string raca) : base(nome, idade)
    {
        Raca = raca;
    }

    public override void EmitirSom()
    {
        Console.WriteLine("Au au!");
    }

    public override void ExibirInfo()
    {
        Console.WriteLine($"[Cachorro] Nome: {Nome}, Idade: {Idade}, Raça: {Raca}");
    }
}

class Gato : Animal
{
    public string Cor;

    public Gato(string nome, int idade, string cor) : base(nome, idade)
    {
        Cor = cor;
    }

    public override void EmitirSom()
    {
        Console.WriteLine("Miau!");
    }

    public override void ExibirInfo()
    {
        Console.WriteLine($"[Gato] Nome: {Nome}, Idade: {Idade}, Cor: {Cor}");
    }
}

class Hamster : Animal
{
    public string Genero;

    public Hamster(string nome, int idade, string genero) : base(nome, idade)
    {
        Genero = genero;
    }

    public override void EmitirSom()
    {
        Console.WriteLine("Chii-chii!");
    }

    public override void ExibirInfo()
    {
        Console.WriteLine($"[Hamster] Nome: {Nome}, Idade: {Idade}, Gênero: {Genero}");
    }
}

class PetShop
{
    private List<Animal> animais = new List<Animal>();

    public void CadastrarPet()
    {
        Console.WriteLine("Qual tipo de pet deseja cadastrar? (1-Cachorro, 2-Gato, 3-Hamster)");
        int tipo = int.Parse(Console.ReadLine());

        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        Console.Write("Idade: ");
        int idade = int.Parse(Console.ReadLine());

        switch (tipo)
        {
            case 1:
                Console.Write("Raça: ");
                string raca = Console.ReadLine();
                animais.Add(new Cachorro(nome, idade, raca));
                break;

            case 2:
                Console.Write("Cor: ");
                string cor = Console.ReadLine();
                animais.Add(new Gato(nome, idade, cor));
                break;

            case 3:
                Console.Write("Gênero: ");
                string genero = Console.ReadLine();
                animais.Add(new Hamster(nome, idade, genero));
                break;

            default:
                Console.WriteLine("Tipo inválido!");
                break;
        }
    }

    public void ListarPets()
    {
        if (animais.Count == 0)
        {
            Console.WriteLine("Nenhum pet cadastrado.");
            return;
        }

        Console.WriteLine("\n--- Pets Cadastrados ---");
        foreach (var pet in animais)
        {
            pet.ExibirInfo();
            pet.EmitirSom();
        }
    }
    
    public void CalcularMediaIdade()
    {
        if (animais.Count == 0)
        {
            Console.WriteLine("Nenhum pet cadastrado para calcular a média.");
            return;
        }

        double media = animais.Average(p => p.Idade);
        Console.WriteLine($"A média de idade dos pets é: {media:F2} anos");
    }
}