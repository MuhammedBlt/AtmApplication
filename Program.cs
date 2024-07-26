using System;

class Program
{
    static void Main()
    {
        ATM atm = new ATM();
        atm.Start();
    }
}

public class ATM
{
    private double balance; 
    private double userBalance; 
    private bool isUserLoggedIn;

    private string[] userNames; 
    private string[] userPasswords; 
    private int userCount; 

    public ATM()
    {
        balance = 1000; 
        userBalance = 0; 
        isUserLoggedIn = false;

        userNames = new string[10]; 
        userPasswords = new string[10]; 
        userCount = 0; 
    }

    public void Start()
    {
        while (true)
        {
            Console.WriteLine("ATM Menüsü:");
            Console.WriteLine("1. Üye Ol");
            Console.WriteLine("2. Giriş Yap");
            Console.WriteLine("3. Çıkış");
            Console.Write("Seçiminizi yapın: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Register();
                    break;
                case "2":
                    Login();
                    break;
                case "3":
                    Console.WriteLine("Çıkış yapılıyor...");
                    return;
                default:
                    Console.WriteLine("Geçersiz seçim. Tekrar deneyin.");
                    break;
            }
        }
    }

    public void Register()
    {

        if (userCount >= userNames.Length)
        {
            Console.WriteLine("Kullanıcı kapasitesi dolmuş.");
            return;
        }

        Console.Write("Kullanıcı adı: ");
        string username = Console.ReadLine();
        Console.Write("Şifre: ");
        string password = Console.ReadLine();

        for (int i = 0; i < userCount; i++)
        {
            if (userNames[i] == username)
            {
                Console.WriteLine("Kullanıcı adı zaten var.");
                return;
            }
        }
        userNames[userCount] = username;
        userPasswords[userCount] = password;
        userCount++;

        Console.WriteLine("Üyelik başarıyla oluşturuldu.");
    }

    private void Login()
    {
        Console.Write("Kullanıcı adı: ");
        string username = Console.ReadLine();
        Console.Write("Şifre: ");
        string password = Console.ReadLine();

        for (int i = 0; i < userCount; i++)
        {
            if (userNames[i] == username && userPasswords[i] == password)
            {
                isUserLoggedIn = true;
                UserMenu();
                return;
            }
        }

        Console.WriteLine("Geçersiz kullanıcı adı veya şifre.");
    }

    private void UserMenu()
    {
        while (isUserLoggedIn)
        {
            Console.Clear();
            Console.WriteLine("Kullanıcı Menüsü:");
            Console.WriteLine("1. Para Yatır");
            Console.WriteLine("2. Para Çek");
            Console.WriteLine("3. Çıkış");
            Console.Write("Seçiminizi yapın: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Deposit();
                    break;
                case "2":
                    Withdraw();
                    break;
                case "3":
                    isUserLoggedIn = false;
                    Console.WriteLine("Çıkış yapılıyor...");
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim. Tekrar deneyin.");
                    break;
            }
        }
    }

    private void Deposit()
    {
        Console.Write("Yatırmak istediğiniz tutarı girin: ");
        if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
        {
            userBalance += amount;
            balance += amount;
            Console.WriteLine($"{amount} TL başarıyla yatırıldı.");
        }
        else
        {
            Console.WriteLine("Geçersiz tutar.");
        }
        Console.ReadKey();
    }

    private void Withdraw()
    {
        Console.Write("Çekmek istediğiniz tutarı girin: ");
        if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
        {
            if (amount > userBalance)
            {
                Console.WriteLine("Yetersiz bakiye.");
                return;
            }

            if (amount > balance)
            {
                Console.WriteLine("ATM'de yeterli para yok.");
                return;
            }

            userBalance -= amount;
            balance -= amount;
            Console.WriteLine($"{amount} TL başarıyla çekildi.");
        }
        else
        {
            Console.WriteLine("Geçersiz tutar.");
        }
        Console.ReadKey();
    }
}
