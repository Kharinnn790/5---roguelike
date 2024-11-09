// See https://aka.ms/new-console-template for more information
using System.Numerics;
public class Player
{
    public string Name { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }
    public Aid AidKit { get; set; }
    public Weapon Weapon { get; set; }
    public int Score { get; set; }
    public Player(string name)
    {
        Name = name;
        MaxHealth = 100;
        CurrentHealth = MaxHealth;
        Score = 0;
    }
    public void Heal()
    {
        if (AidKit != null)
        {
            CurrentHealth += AidKit.RecoveryAmount;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            Console.WriteLine($"{Name} использовал аптечку {AidKit.Name}. Здоровье восстановлено до {CurrentHealth}hp.");
            AidKit = null; // Аптечка используется
        }
        else
        {
            Console.WriteLine("У вас больше нет аптечек!");
        }
    }
    public void Attack(Enemy enemy)
    {
        if (Weapon != null)
        {
            enemy.CurrentHealth -= Weapon.Damage;
            Console.WriteLine($"{Name} ударил противника {enemy.Name}. У противника осталось {enemy.CurrentHealth}hp.");
        }
    }
}
public class Enemy
{
    public string Name { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; set; }
    public Weapon Weapon { get; set; }
    public Enemy(string name, int health, Weapon weapon)
    {
        Name = name;
        MaxHealth = health;
        CurrentHealth = MaxHealth;
        Weapon = weapon;
    }
    public void Attack(Player player)
    {
        player.CurrentHealth -= Weapon.Damage;
        Console.WriteLine($"{Name} ударил {player.Name}. У игрока осталось {player.CurrentHealth}hp.");
    }
}
public class Aid
{
    public string Name { get; set; }
    public int RecoveryAmount { get; set; }
    public Aid(string name, int recoveryAmount)
    {
        Name = name;
        RecoveryAmount = recoveryAmount;
    }
}
public class Weapon
{
    public string Name { get; set; }
    public int Damage { get; set; }
    public int Durability { get; set; }
    public Weapon(string name, int damage, int durability)
    {
        Name = name;
        Damage = damage;
        Durability = durability;
    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Добро пожаловать, БОЕЦ..");
        Console.Write("Назови себя: ");
        string playerName = Console.ReadLine();
        Player player = new Player(playerName);
        // Генерация оружия и аптечки
        player.Weapon = new Weapon("Повелитель осколков", 20, 100);
        player.AidKit = new Aid("Средняя аптечка", 10);
        Console.WriteLine("");
        Console.WriteLine($"Ваше имя {player.Name}!");
        Console.WriteLine("");
        Console.WriteLine($"Вам был ниспослан {player.Weapon.Name} ({player.Weapon.Damage}), а также {player.AidKit.Name} ({player.AidKit.RecoveryAmount}hp). У вас {player.CurrentHealth}hp.");
        Random random = new Random();
        string[] enemyNames = { "Варвар", "Скелет", "Демон", "Гоблин" };
        Weapon enemyWeapon = new Weapon("Экскалибур", 10, 100);
        while (player.CurrentHealth > 0)
        {
            string enemyName = enemyNames[random.Next(enemyNames.Length)];
            Enemy enemy = new Enemy(enemyName, random.Next(30, 70), enemyWeapon);
            Console.WriteLine($"{player.Name} встречает врага {enemy.Name} ({enemy.CurrentHealth}hp), у врага на поясе сияет оружие {enemy.Weapon.Name} ({enemy.Weapon.Damage})");
            while (enemy.CurrentHealth > 0 && player.CurrentHealth > 0)
            {
                Console.WriteLine("Что вы будете делать?");
                Console.WriteLine("");
                Console.WriteLine("1. Ударить");
                Console.WriteLine("2. Зассать и пропустить ход");
                Console.WriteLine("3. Использовать аптечку");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        player.Attack(enemy);
                        if (enemy.CurrentHealth > 0)
                        {
                            enemy.Attack(player);
                        }
                        break;
                    case "2":
                        Console.WriteLine($"{player.Name} зассал.");
                        enemy.Attack(player);
                        break;
                    case "3":
                        player.Heal();
                        enemy.Attack(player);
                        break;
                    default:
                        Console.WriteLine("Неверный выбор, попробуйте снова.");
                        break;
                }
            }
            if (player.CurrentHealth <= 0)
            {
                Console.WriteLine("");
                Console.WriteLine("GAME OVER!");
            }
            else
            {
                player.Score += 10; // Получаем очки за победу
                Console.WriteLine($"{player.Name} победил {enemy.Name}! Вы получаете 10 очков! Текущий счет: {player.Score}");
                Console.WriteLine("");
            }
        }
    }
}
//Console.WriteLine("Добро пожаловать, БОЕЦ...");
//Console.WriteLine("");
//Console.WriteLine("Введите ваше имя: ");;
//string name = Console.ReadLine();
//Console.WriteLine("Ваше имя " + name);
//Console.WriteLine("Вам был ниспослан меч Экскалибур (48), а также средняя аптечка (10hp).\r\nУ вас 120hp.");
//Console.ReadLine();
//Console.WriteLine("Вы встречаете врага Кракер (70hp), у врага на поясе сияет оружие теней (12)");
//Console.WriteLine(name + ", как вы поступите?");
//Console.WriteLine("");
//Console.WriteLine("1. Зассать и быть убитым");
//Console.WriteLine("2. Убить его и разрезать на кусочки к чертовой матери");
//Console.WriteLine("3. Использовать аптечку");
//Console.WriteLine("");
