using System;

namespace Pasiruosimas_egzui_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Player> team = new List<Player>
            {
                new Player { number = "10", surname = "Surn", name = "Name", inches = 75},
                new Player { number = "23", surname = "Sur", name = "Nam", inches = 70},
                new Player { number = "5", surname = "Su", name = "Na", inches = 72},
                new Player { number = "7", surname = "S", name = "N", inches = 73},
                new Player { number = "12", surname = "Surname", name = "Names", inches = 75}
            };

            List<ProtocolLine> protocol = new List<ProtocolLine>
            {
                new ProtocolLine { number = "10", quarter = 3, start = 0, duration = 4, points = 5},
                new ProtocolLine { number = "10", quarter = 3, start = 7, duration = 3, points = 0},
                new ProtocolLine { number = "10", quarter = 3, start = 0, duration = 8, points = 0},
                new ProtocolLine { number = "23", quarter = 1, start = 0, duration = 7, points = 5},
                new ProtocolLine { number = "23", quarter = 2, start = 5, duration = 8, points = 5},
                new ProtocolLine { number = "5", quarter = 1, start = 3, duration = 6, points = 5},
                new ProtocolLine { number = "5", quarter = 1, start = 0, duration = 5, points = 5},
                new ProtocolLine { number = "7", quarter = 1, start = 8, duration = 4, points = 5},
                new ProtocolLine { number = "7", quarter = 4, start = 2, duration = 10, points = 5},
                new ProtocolLine { number = "12", quarter = 1, start = 10, duration = 5, points = 5},
                new ProtocolLine { number = "12", quarter = 3, start = 0, duration = 10, points = 5}
            };

            //1
            int query1 = protocol
                .Where(pl => pl.number == "7")
                .Sum(pl => pl.duration);

            //2
            int query21 = protocol
                .Where(game => game.quarter < 3 && game.number == "10")
                .Sum(pont => pont.points);

            //3
            int query31 = protocol
                .Where(pl => pl.number == "5" && pl.quarter == 1)
                .Count();

            //4
            int query41 = protocol
                .Where(pl => pl.number == "00" && pl.quarter == 2)
                .Sum(pl => pl.points);

            //5
            List<Student> attendees = new List<Student>();
            List<Grade> marks = new List<Grade>();

            int query51 = marks
                .Where(m => m.SubjectCode == "P175B123" && m.StudentID == "B13799")
                .Count();

            //6
            int query61 = marks
                .Where(m => m.Value >= 9 && m.SubjectCode == "P175B122")
                .Count();

            //7
            int query71 = marks
                .Where(m => m.StudentID == "B13774")
                .Count(m => m.Value == 10);

            //Console.WriteLine(query21);

            //1
            var query2 = protocol
                .Where(pl => pl.quarter == 3)
                .GroupBy(pl => pl.number)
                .Select(group => new {number = group.Key, count = group.Count(), totalTime = group.Sum(grp => grp.duration)})
                .Where(grp => grp.count <= 3 && grp.totalTime >= 10)

                .Join(
                team,
                group => group.number,
                player => player.number,
                (group, player) => { return new { player.surname, player.name, player.number, group.totalTime, group.count}; }
                );

            //2
            var query22 = protocol
                .Where(game => game.quarter < 3)
                .GroupBy(pl => pl.number)
                .Select(group => new {number = group.Key, count = group.Count(), totalPoints = group.Sum(player => player.points) })
                .Where(group => group.count == 1 && group.totalPoints >= 1)

                .Join(team,
                group => group.number,
                player => player.number,
                (group, player) => new { player.surname, player.name, player.inches, group.totalPoints }
                );

            //3
            IEnumerable<Player> query32 = protocol
                .Where(pl => pl.quarter == 1 && pl.start < 6 && pl.start + pl.duration >= 6)
                .GroupBy(pl => pl.number)

                .Join(team, group => group.Key, player => player.number, (group, player) => player)
                .OrderByDescending(player => player.inches)
                .ThenBy(player => player.surname);

            //4
            var query42 = protocol
                .Where(pl => pl.quarter == 3)
                .GroupBy(pl => pl.number)
                .Where(group => group.Count() <= 3 && group.All(p => p.points > 0))

                .Join(team, group => group.Key, player => player.number, (group, player) => player);

            //5
            var query52 = marks
                .GroupBy(m => m.StudentID)
                .Where(group => group
                    .GroupBy(sub => sub.SubjectCode)
                    .All(gr => gr.Any(sub => sub.Value == 10)))

                .Join(attendees, gr => gr.Key, st => st.ID, (gr, st) => new { st.ID, st.Surname, st.Name, modulCount = gr.Count(), tenCount = gr.Count(mark => mark.Value == 10) });

            //6
            var query62 = marks
                .GroupBy(m => m.StudentID)
                .Where(gr => gr
                    .GroupBy(sub => sub.SubjectCode)
                    .Select(gr => gr.Average(m => m.Value))
                    .All(avr => avr >= 5 && avr <= 9))

                .Join(attendees, gr => gr.Key, student => student.ID, (gr, student) => new
                {
                    student.ID,
                    student.Surname,
                    student.Name,
                    biggestAvr = gr.GroupBy(sub => sub.SubjectCode).Select(gr => gr.Average(m => m.Value)).Max(),
                    lowesAvr = gr.GroupBy(sub => sub.SubjectCode).Select(gr => gr.Average(m => m.Value)).Min()
                });

            //7
            var query72 = marks
                .GroupBy(m => m.StudentID)
                .Where(gr => gr
                    .GroupBy(sub => sub.SubjectCode)
                    .Select(m => m.Count())
                    .Distinct()
                    .Count() == 1)

                .Join(attendees, gr => gr.Key, st => st.ID, (gr, st) => new
                {
                    st.ID,
                    st.Surname,
                    st.Name,
                    modules = gr.GroupBy(sub => sub.SubjectCode).Count(),
                    oneModuleMarks = gr.GroupBy(sub => sub.SubjectCode).First().Count()
                });

            foreach (var el in query42)
            {
                Console.WriteLine(el.number);
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
        public int points { get; set; }
    }

    class Student
    {
        public string ID {get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string ProgrammeID { get; set; }
    }

    class Grade
    {
        public string StudentID { get; set; }
        public string SubjectCode { get; set; }
        public DateTime Date { get; set; }
        public int Value { get; set; }
    }
}
