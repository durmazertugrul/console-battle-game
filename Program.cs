using System.Numerics;

public enum Division
{
    Warrior = 0,
    Mage = 1,
    Archer = 2,
    Tank = 3
}

public enum EnemyDivision
{
    Spider = 0,
    Goblin = 1,
    Orc = 2,
    Dragon = 3
}

class Battlefield
{
    public Player player1;
    public Enemy enemy1;

    public Battlefield(Player player, Enemy enemy)
    {
        this.player1 = player;
        this.enemy1 = enemy;
    }


    public void Fight()
    {
        Console.WriteLine("                 The War Begins!");
        int count = 1;
        while (player1.IsPlayerAlive && enemy1.IsEnemyAlive)
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"                    {count}. TURN");
            player1.Attack(enemy1);
            if (!enemy1.IsEnemyAlive)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{player1.playerName} Has Won The Battle");
                Console.ResetColor();
                break;
            }
            enemy1.Attack(player1);
            if (!player1.IsPlayerAlive)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{enemy1.enemyName} Has Won The Battle");
                Console.ResetColor();
                break;
            }
            count++;
            Console.WriteLine("-------------------------------------------");
        }
    }
}


class Enemy
{
    public Random random;
    public string enemyName;
    private int enemyHealth;
    private int enemyStrength;
    public bool IsEnemyAlive = true;
    private EnemyDivision division;


    public void Attack(Player player)
    {
        Console.WriteLine();
        int enemyDamage = random.Next(0, (enemyStrength / 2) + 1);
        player.Health -= enemyDamage;
        if (enemyDamage == enemyStrength / 2)
        {
            Console.Write($"{enemyName} dealt {enemyDamage} damage.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" Critical Hit!");
            Console.ResetColor();
            Console.WriteLine();
        }
        else
        {
            Console.Write($"{enemyName} dealt {enemyDamage} damage.");
            Console.WriteLine();
        }

        Console.WriteLine($"{player.playerName} HP: {player.Health}  ");

    }

    public int EnemyHealth
    {
        get { return enemyHealth; }
        set
        {
            enemyHealth = value;

            if (enemyHealth <= 0) IsEnemyAlive = false;
        }
    }

    public int EnemyStrength
    {
        get { return enemyStrength; }
        set { enemyStrength = value; }
    }

    public Enemy(string name, int health, int strength, EnemyDivision division)
    {
        this.random = new Random();
        this.enemyName = name;
        this.enemyHealth = health;
        this.enemyStrength = strength;
        this.division = division;

        switch (division)
        {
            case EnemyDivision.Spider:
                this.enemyHealth += 10;
                this.enemyStrength += 5;
                break;
            case EnemyDivision.Goblin:
                this.enemyHealth += 12;
                this.enemyStrength += 8;
                break;
            case EnemyDivision.Orc:
                this.enemyHealth += 15;
                this.enemyStrength += 10;
                break;
            case EnemyDivision.Dragon:
                this.enemyHealth += 30;
                this.enemyStrength += 15;
                break;
        }

    }

    public void PrintEnemyInfo()
    {
        Console.WriteLine($"{enemyName}'s Info:");
        ColoredPrint("Health: ", ConsoleColor.Red);
        Console.Write(enemyHealth);
        ColoredPrint("  Strength: ", ConsoleColor.Red);
        Console.Write(enemyStrength);
        ColoredPrint("  Division: ", ConsoleColor.Red);
        Console.Write(division);
        Console.WriteLine();
    }

    private void ColoredPrint(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();
    }
}



class Player
{
    public Random random;
    public string playerName;
    private int playerHealth;
    private int playerStrength;
    public bool IsPlayerAlive = true;
    private Division division;




    public void Attack(Enemy enemy)
    {
        Console.WriteLine();
        int playerDamage = random.Next(0, (playerStrength / 2) + 1);
        enemy.EnemyHealth -= playerDamage;
        if (playerDamage == playerStrength / 2)
        {
            Console.Write($"{playerName} dealt {playerDamage} damage.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" Critical Hit!");
            Console.ResetColor();
            Console.WriteLine();
        }
        else
        {
            Console.Write($"{playerName} dealt {playerDamage} damage.");
            Console.WriteLine();
        }

        Console.WriteLine($"{enemy.enemyName} HP: {enemy.EnemyHealth}  ");

    }

    public int Health
    {
        get { return playerHealth; }
        set
        {
            playerHealth = value;

            if (playerHealth <= 0) IsPlayerAlive = false;
        }
    }

    public int Strength
    {
        get { return playerStrength; }
        set { playerStrength = value; }
    }

    public Player(string name, int health, int strength, Division division)
    {
        this.random = new Random();
        this.playerName = name;
        this.playerHealth = health;
        this.playerStrength = strength;
        this.division = division;

        switch (division)
        {
            case Division.Warrior:
                this.playerHealth += 10;
                this.playerStrength += 20;
                break;
            case Division.Mage:
                this.playerHealth += 20;
                this.playerStrength += 10;
                break;
            case Division.Archer:
                this.playerHealth += 10;
                this.playerStrength += 10;
                break;
            case Division.Tank:
                this.playerHealth += 30;
                this.playerStrength += 8;
                break;
        }

    }

    public void PrintInfo()
    {
        Console.WriteLine($"{playerName}'s Info:");
        ColoredPrint("Health: ", ConsoleColor.Green);
        Console.Write(playerHealth);
        ColoredPrint("  Strength: ", ConsoleColor.Green);
        Console.Write(playerStrength);
        ColoredPrint("  Division: ", ConsoleColor.Green);
        Console.Write(division);
        Console.WriteLine();
    }

    private void ColoredPrint(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();
    }
}
class Program
{
    static void Main(string[] args)
    {
        Random rnd = new Random();
        Player player1 = new Player("Subaru", rnd.Next(50, 101), rnd.Next(10, 21), (Division)rnd.Next(0, 4));
        Enemy enemy1 = new Enemy("Elsa", rnd.Next(50, 101), rnd.Next(10, 21), (EnemyDivision)rnd.Next(0, 4));
        Battlefield battle = new Battlefield(player1, enemy1);



        player1.PrintInfo();
        Console.WriteLine();
        enemy1.PrintEnemyInfo();
        Console.WriteLine("-------------------------------------------");

        battle.Fight();





        Console.ReadKey();
    }
}

