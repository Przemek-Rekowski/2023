﻿using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Produkt
{
    private string nazwa;
    private decimal cenaNetto;
    private string kategoriaVAT;

    protected static readonly Dictionary<string, decimal> VATRates = new Dictionary<string, decimal>
    {
        { "A", 0.23m },
        { "B", 0.08m },
        { "C", 0.05m },
        { "D", 0.00m }
    };

    protected static readonly HashSet<string> Countries = new HashSet<string>
    {
        "Polska",
        "Niemcy",
        "Francja",
        "Włochy"
    };

    public string Nazwa
    {
        get { return nazwa; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Nazwa nie może być pusta.");
            nazwa = value;
        }
    }

    public virtual decimal CenaNetto
    {
        get { return cenaNetto; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Cena netto nie może być ujemna.");
            cenaNetto = value;
        }
    }

    public virtual string KategoriaVAT
    {
        get { return kategoriaVAT; }
        set
        {
            if (!VATRates.ContainsKey(value))
                throw new ArgumentException("Niepoprawna kategoria VAT.");
            kategoriaVAT = value;
        }
    }

    public virtual decimal CenaBrutto
    {
        get { return CenaNetto * (1 + VATRates[KategoriaVAT]); }
    }

    public virtual string KrajPochodzenia
    {
        get { throw new NotImplementedException(); }
        set
        {
            if (!Countries.Contains(value))
                throw new ArgumentException("Niepoprawny kraj pochodzenia.");
        }
    }
}

public abstract class ProduktSpożywczy<T> : Produkt where T : struct, IComparable, IComparable<T>
{
    private T kalorie;

    protected static readonly HashSet<string> SpożywczeKategoriiVAT = new HashSet<string>
    {
        "A", "B"
    };

    protected static readonly HashSet<string> AlergenySet = new HashSet<string>
    {
        "Gluten",
        "Orzechy",
        "Soja",
        "Mleko"
    };

    public new string KategoriaVAT
    {
        get { return base.KategoriaVAT; }
        set
        {
            if (!SpożywczeKategoriiVAT.Contains(value))
                throw new ArgumentException("Niepoprawna kategoria VAT dla produktu spożywczego.");
            base.KategoriaVAT = value;
        }
    }

    public T Kalorie
    {
        get { return kalorie; }
        set
        {
            if (Comparer<T>.Default.Compare(value, default(T)) < 0)
                throw new ArgumentException("Kalorie nie mogą być ujemne.");
            kalorie = value;
        }
    }

    public virtual HashSet<string> Alergeny
    {
        get { throw new NotImplementedException(); }
        set
        {
            if (value.Except(AlergenySet).Any())
                throw new ArgumentException("Niepoprawny alergen w zestawie.");
        }
    }
}

public class ProduktSpożywczyNaWagę<T> : ProduktSpożywczy<T> where T : struct, IComparable, IComparable<T>
{

}

public class ProduktSpożywczyPaczka<T> : ProduktSpożywczy<T> where T : struct, IComparable, IComparable<T>
{
    private decimal waga;

    public decimal Waga
    {
        get { return waga; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Waga nie może być ujemna.");
            waga = value;
        }
    }
}

public class ProduktSpożywczyNapój<T> : ProduktSpożywczyPaczka<T> where T : struct, IComparable, IComparable<T>
{
    private T objętość;

    public T Objętość
    {
        get { return objętość; }
        set
        {
            if (Comparer<T>.Default.Compare(value, default(T)) < 0)
                throw new ArgumentException("Objętość nie może być ujemna.");
            objętość = value;
        }
    }
}

public class Wielopak : Produkt
{
    private Produkt produkt;
    private ushort ilość;

    public Produkt Produkt
    {
        get { return produkt; }
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(Produkt));
            produkt = value;
        }
    }

    public ushort Ilość
    {
        get { return ilość; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Ilość musi być większa od zera.");
            ilość = value;
        }
    }

    public override decimal CenaNetto
    {
        get { return base.CenaNetto; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Cena netto nie może być ujemna.");
            base.CenaNetto = value;
        }
    }

    public override decimal CenaBrutto
    {
        get { return CenaNetto * (1 + VATRates[Produkt.KategoriaVAT]); }
    }

    public override string KategoriaVAT
    {
        get { return Produkt.KategoriaVAT; }
    }

    public override string KrajPochodzenia
    {
        get { return Produkt.KrajPochodzenia; }
        set
        {
            Produkt.KrajPochodzenia = value;
        }
    }
}
