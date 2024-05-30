using Ejercicio_AplicandoLINQ;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public enum Palo
    {
        Treboles,
        Diamantes,
        Corazones,
        Picas
    }

    public enum Rango
    {
        Dos,
        Tres,
        Cuatro,
        Cinco,
        Seis,
        Siete,
        Ocho,
        Nueve,
        Diez,
        Jack,
        Reina,
        Rey,
        As
    }

    public static void Main(string[] args)
    {
        static IEnumerable<Palo> Palos() => Enum.GetValues(typeof(Palo)) as IEnumerable<Palo>;
        static IEnumerable<Rango> Rangos() => Enum.GetValues(typeof(Rango)) as IEnumerable<Rango>;

        if ((Palos() is null) || (Rangos() is null))
            return;

        var startingDeck = (from p in Palos().LogQuery("Creacion de Palo")
                            from r in Rangos().LogQuery("Creacion de Rango")
                            select new { Palo = p, Rango = r })
                            .LogQuery("Starting Deck")
                            .ToArray();

        foreach (var c in startingDeck)
        {
            Console.WriteLine(c);
        }
        Console.WriteLine();

        var times = 0;
        var shuffle = startingDeck;

        do
        {
            /*
            shuffle = shuffle.Take(26)
                .LogQuery("Top Half")
                .InterleaveSequenceWith(shuffle.Skip(26).LogQuery("Bottom Half"))
                .LogQuery("Shuffle")
                .ToArray();
            */

            shuffle = shuffle.Skip(26)
                .LogQuery("Bottom Half")
                .InterleaveSequenceWith(shuffle.Take(26).LogQuery("Top Half"))
                .LogQuery("Shuffle")
                .ToArray();

            foreach (var c in shuffle)
            {
                Console.WriteLine(c);
            }

            times++;
            Console.WriteLine();

        } while (!startingDeck.SequenceEquals(shuffle));

        Console.WriteLine(times);
    }
}
