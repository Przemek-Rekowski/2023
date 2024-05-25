public class Program
{
    public static void Main()
    {
        // Tworzenie obiektu klasy Osoba
        Osoba osoba = new Osoba("Jan Kowalski");
        Console.WriteLine($"Osoba: {osoba.ImięNazwisko}");

        // Tworzenie obiektu klasy Prostokąt
        Prostokąt prostokąt = new Prostokąt(5.0, 10.0);
        Console.WriteLine($"Prostokąt: bokA = {prostokąt.BokA}, bokB = {prostokąt.BokB}");

        // Tworzenie obiektu klasy Wektor
        Wektor wektor = new Wektor(3);
        wektor[0] = 1.0;
        wektor[1] = 2.0;
        wektor[2] = 3.0;
        Console.WriteLine($"Wektor: [{wektor[0]}, {wektor[1]}, {wektor[2]}], długość = {wektor.Długość}");

        // Tworzenie obiektu klasy ProduktSpożywczyNapój
        ProduktSpożywczyNapój<int> napój = new ProduktSpożywczyNapój<int>
        {
            Nazwa = "Sok Pomarańczowy",
            CenaNetto = 5.99m,
            KategoriaVAT = "A",
            Kalorie = 50,
            Waga = 1.0m,
            Objętość = 1
        };
        Console.WriteLine($"Napój: {napój.Nazwa}, Cena brutto: {napój.CenaBrutto}, Kalorie: {napój.Kalorie}, Waga: {napój.Waga}, Objętość: {napój.Objętość}");

        // Tworzenie obiektu klasy Wielopak
        Wielopak wielopak = new Wielopak
        {
            Produkt = napój,
            Ilość = 6,
            CenaNetto = napój.CenaNetto * 6
        };
        Console.WriteLine($"Wielopak: {wielopak.Produkt.Nazwa}, Ilość: {wielopak.Ilość}, Cena brutto: {wielopak.CenaBrutto}");
    }
}