using System;
using System.Linq;

public class Wektor
{
    private double[] współrzędne;
    public Wektor(byte wymiar)
    {
        if (wymiar < 1)
            throw new ArgumentException("Wymiar musi być większy niż 0.");
        współrzędne = new double[wymiar];
    }

    public Wektor(params double[] współrzędne)
    {
        if (współrzędne == null || współrzędne.Length == 0)
            throw new ArgumentException("Wektor musi mieć przynajmniej jedną współrzędną.");
        this.współrzędne = (double[])współrzędne.Clone();
    }

    public double Długość
    {
        get { return Math.Sqrt(IloczynSkalarny(this, this)); }
    }

    public byte Wymiar
    {
        get { return (byte)współrzędne.Length; }
    }

    public double this[byte index]
    {
        get
        {
            if (index >= Wymiar)
                throw new IndexOutOfRangeException("Indeks poza zakresem.");
            return współrzędne[index];
        }
        set
        {
            if (index >= Wymiar)
                throw new IndexOutOfRangeException("Indeks poza zakresem.");
            współrzędne[index] = value;
        }
    }

    public static double IloczynSkalarny(Wektor V, Wektor W)
    {
        if (V.Wymiar != W.Wymiar)
            return double.NaN;
        return V.współrzędne.Zip(W.współrzędne, (v, w) => v * w).Sum();
    }

    public static Wektor Suma(params Wektor[] Wektory)
    {
        if (Wektory == null || Wektory.Length == 0)
            throw new ArgumentException("Musi być przynajmniej jeden wektor.");
        byte wymiar = Wektory[0].Wymiar;
        if (Wektory.Any(wektor => wektor.Wymiar != wymiar))
            throw new ArgumentException("Wszystkie wektory muszą mieć ten sam wymiar.");
        double[] suma = new double[wymiar];
        foreach (var wektor in Wektory)
        {
            for (int i = 0; i < wymiar; i++)
            {
                suma[i] += wektor.współrzędne[i];
            }
        }
        return new Wektor(suma);
    }

    public static Wektor operator +(Wektor V, Wektor W)
    {
        return Suma(V, W);
    }

    public static Wektor operator -(Wektor V, Wektor W)
    {
        if (V.Wymiar != W.Wymiar)
            throw new ArgumentException("Wektory muszą mieć ten sam wymiar.");
        double[] różnica = new double[V.Wymiar];
        for (int i = 0; i < V.Wymiar; i++)
        {
            różnica[i] = V.współrzędne[i] - W.współrzędne[i];
        }
        return new Wektor(różnica);
    }

    public static Wektor operator *(Wektor V, double skalar)
    {
        double[] wynik = new double[V.Wymiar];
        for (int i = 0; i < V.Wymiar; i++)
        {
            wynik[i] = V.współrzędne[i] * skalar;
        }
        return new Wektor(wynik);
    }

    public static Wektor operator *(double skalar, Wektor V)
    {
        return V * skalar;
    }

    public static Wektor operator /(Wektor V, double skalar)
    {
        if (skalar == 0)
            throw new DivideByZeroException("Skalar nie może być zerem.");
        double[] wynik = new double[V.Wymiar];
        for (int i = 0; i < V.Wymiar; i++)
        {
            wynik[i] = V.współrzędne[i] / skalar;
        }
        return new Wektor(wynik);
    }
}
