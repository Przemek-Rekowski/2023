using System;

public class Osoba
{
    private string imie;
    private string nazwisko;

    public Osoba(string imieNazwisko)
    {
        ImięNazwisko = imieNazwisko;
    }
    public string Imię
    {
        get { return imie; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Imię nie może być puste.");
            imie = value;
        }
    }

    public string Nazwisko
    {
        get { return nazwisko; }
        set { nazwisko = value; }
    }

    public DateTime? DataUrodzenia { get; set; } = null;
    public DateTime? DataŚmierci { get; set; } = null;

    public string ImięNazwisko
    {
        get { return $"{Imię} {Nazwisko}".Trim(); }
        set
        {
            var parts = value.Split(' ');
            Imię = parts[0];
            Nazwisko = parts.Length > 1 ? parts[^1] : string.Empty;
        }
    }

    public TimeSpan? Wiek
    {
        get
        {
            if (DataUrodzenia == null)
                return null;

            DateTime endDate = DataŚmierci ?? DateTime.Now;
            return endDate - DataUrodzenia.Value;
        }
    }
}
