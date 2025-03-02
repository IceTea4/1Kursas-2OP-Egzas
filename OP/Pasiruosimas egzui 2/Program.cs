//moket: Lambda išraiškų taikymas. Išplėtimo metodai.Anoniminiai metodai. Anoniminiai tipai. Objektinė sintaksė.Užklausų sintaksė.
// Užklausos. Užklausų operatoriai (seka->seka, seka->elementas ar reikšmė, generavimas). Grupavimas.Sumavimas grupėje.
// Duomenų iš kelių rinkiniųišrinkimas, naudojant ryšio laukus. Operatoriaus letgalimybės. Užklausų sintaksių naudojimo ypatumai.

using System;

namespace Pasiruosimas_egzui_2
{
    //antra uzduotis
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Player> k = new List<Player>
            {
                new Player { number = "10", surname = "Surn", name = "Name", inches = 75},
                new Player { number = "23", surname = "Sur", name = "Nam", inches = 70},
                new Player { number = "5", surname = "Su", name = "Na", inches = 72},
                new Player { number = "7", surname = "S", name = "N", inches = 73},
                new Player { number = "12", surname = "Surname", name = "Names", inches = 75}
            };

            List<ProtocolLine> p = new List<ProtocolLine>
            {
                new ProtocolLine { number = "10", quarter = 1, start = 0, duration = 4},
                new ProtocolLine { number = "10", quarter = 1, start = 7, duration = 3},
                new ProtocolLine { number = "10", quarter = 2, start = 0, duration = 8},
                new ProtocolLine { number = "23", quarter = 1, start = 0, duration = 7},
                new ProtocolLine { number = "23", quarter = 2, start = 5, duration = 8},
                new ProtocolLine { number = "5", quarter = 1, start = 3, duration = 6},
                new ProtocolLine { number = "5", quarter = 3, start = 0, duration = 5},
                new ProtocolLine { number = "7", quarter = 1, start = 8, duration = 4},
                new ProtocolLine { number = "7", quarter = 4, start = 2, duration = 10},
                new ProtocolLine { number = "12", quarter = 1, start = 10, duration = 5},
                new ProtocolLine { number = "12", quarter = 2, start = 0, duration = 10}
            };

            var result1 = p
                .Where(pl => pl.number == "10")
                .Sum(pl => pl.duration);

            Console.WriteLine(result1);

            var result2 = p
                .Where(pl => pl.quarter == 1)
                .GroupBy(pl => pl.number)
                .Select(group => new { number = group.Key, totalDuration = group.Sum(g => g.duration) })
                .Where(pp => pp.totalDuration >= 5)
                .OrderByDescending(pp => pp.totalDuration)

                .Join(k,
                pp => pp.number,
                kk => kk.number,
                (pp, kk) => { return new { kk.surname, kk.name, pp.number, pp.totalDuration }; }
                );

            foreach (var el in result2)
            {
                Console.WriteLine(el);
            }
        }
    }

    class Player
    {
        public string number { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public int inches { get; set; }
    }

    class ProtocolLine
    {
        public string number { get; set; }
        public int quarter { get; set; }
        public int start { get; set; }
        public int duration { get; set; }
    }
}

