using System;
using System.Collections.Generic;
using System.Linq; // CORREÇÃO: necessário para usar o método Average() em CalcularMediaIdade

// ============================================================
// PONTO DE ENTRADA: menu principal do sistema
// ============================================================
class Program
{
    static void Main()
    {
        PetShop petShop = new PetShop();
        int opcao;

        // Repete o menu até o usuário escolher 0 (Sair)
        do
        {
            Console.WriteLine("\n=== Pet Shop Majulle ===");
            Console.WriteLine("1 - Cadastrar pet");
            Console.WriteLine("2 - Listar pets cadastrados");
            Console.WriteLine("3 - Média da Idade dos Pets");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            // TryParse evita erro se o usuário digitar algo que não seja número
            if (!int.TryParse(Console.ReadLine(), out opcao))
                opcao = -1; // valor inválido: cai no "default" do switch

            switch (opcao)
            {
                case 1:
                    petShop.CadastrarPet();
                    break;

                case 2:
                    petShop.ListarPets();
                    break;

                case 3:
                    petShop.CalcularMediaIdade(); // (indentação corrigida)
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

// ============================================================
// CLASSE BASE: características comuns a todos os animais
// ============================================================
class Animal
{
    public string Nome;
    public int Idade;

    public Animal(string nome, int idade)
    {
        Nome = nome;
        Idade = idade;
    }

    // "virtual": as classes filhas podem sobrescrever (override) este método
    public virtual void EmitirSom()
    {
        Console.WriteLine("Som de animal");
    }

    public virtual void ExibirInfo()
    {
        Console.WriteLine($"Nome: {Nome}, Idade: {Idade}");
    }
}

// ============================================================
// CLASSES DERIVADAS: cada animal tem seu atributo, som e exibição
// ============================================================
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

// Outro: para qualquer animal que não seja cachorro, gato ou hamster
// (coelho, papagaio, tartaruga etc.). O usuário informa a espécie e o som.
class Outro : Animal
{
    public string Especie;
    public string Som;

    public Outro(string nome, int idade, string especie, string som) : base(nome, idade)
    {
        Especie = especie;
        Som = som;
    }

    public override void EmitirSom()
    {
        // Se o usuário não informou um som, mostra uma mensagem padrão
        if (string.IsNullOrWhiteSpace(Som))
            Console.WriteLine("(esse animal não faz barulho)");
        else
            Console.WriteLine(Som);
    }

    public override void ExibirInfo()
    {
        Console.WriteLine($"[{Especie}] Nome: {Nome}, Idade: {Idade}");
    }
}

// ============================================================
// PET SHOP: guarda e gerencia a lista de animais
// ============================================================
class PetShop
{
    // Lista que armazena todos os pets cadastrados (de qualquer tipo)
    private List<Animal> animais = new List<Animal>();

    public void CadastrarPet()
    {
        Console.WriteLine("Qual tipo de pet deseja cadastrar? (1-Cachorro, 2-Gato, 3-Hamster, 4-Outro)");

        // CORREÇÃO: antes usava int.Parse, que fechava o programa com entrada inválida.
        // Agora o tipo é validado ANTES de pedir nome e idade (aceita de 1 a 4).
        if (!int.TryParse(Console.ReadLine(), out int tipo) || tipo < 1 || tipo > 4)
        {
            Console.WriteLine("Tipo inválido!");
            return; // volta ao menu
        }

        Console.Write("Nome: ");
        string nome = Console.ReadLine() ?? "";

        // Não permite nome vazio
        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Nome inválido!");
            return;
        }

        Console.Write("Idade: ");

        // CORREÇÃO: TryParse no lugar de int.Parse, e não aceita idade negativa
        if (!int.TryParse(Console.ReadLine(), out int idade) || idade < 0)
        {
            Console.WriteLine("Idade inválida!");
            return;
        }

        // Pede o dado específico de cada tipo e cria o objeto correspondente
        switch (tipo)
        {
            case 1:
                Console.Write("Raça: ");
                string raca = Console.ReadLine() ?? "";
                animais.Add(new Cachorro(nome, idade, raca));
                break;

            case 2:
                Console.Write("Cor: ");
                string cor = Console.ReadLine() ?? "";
                animais.Add(new Gato(nome, idade, cor));
                break;

            case 3:
                Console.Write("Gênero: ");
                string genero = Console.ReadLine() ?? "";
                animais.Add(new Hamster(nome, idade, genero));
                break;

            case 4:
                // Animal de outra espécie: o usuário digita qual é
                Console.Write("Espécie (ex: Coelho, Papagaio, Tartaruga): ");
                string especie = Console.ReadLine() ?? "";

                // A espécie é obrigatória, senão a listagem ficaria sem identificação
                if (string.IsNullOrWhiteSpace(especie))
                {
                    Console.WriteLine("Espécie inválida!");
                    return;
                }

                // O som é opcional: pode deixar vazio apertando Enter
                Console.Write("Som que ele faz (opcional, Enter para pular): ");
                string som = Console.ReadLine() ?? "";

                animais.Add(new Outro(nome, idade, especie, som));
                break;

            default:
                // Segurança: o tipo já foi validado acima, então não deve chegar aqui
                Console.WriteLine("Tipo inválido!");
                return;
        }

        Console.WriteLine("Pet cadastrado com sucesso!");
    }

    // Mostra as informações e o som de cada pet cadastrado
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
            pet.ExibirInfo();  // chama a versão da classe filha (polimorfismo)
            pet.EmitirSom();
        }
    }

    // Calcula a média das idades de todos os pets
    public void CalcularMediaIdade()
    {
        // Evita calcular média de uma lista vazia
        if (animais.Count == 0)
        {
            Console.WriteLine("Nenhum pet cadastrado para calcular a média.");
            return;
        }

        double media = animais.Average(p => p.Idade); // precisa do System.Linq
        Console.WriteLine($"A média de idade dos pets é: {media:F2} anos");
    }
}
