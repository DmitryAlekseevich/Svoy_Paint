using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Конструктор_1
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine();
            ResearchTeam team = new ResearchTeam();
            Paper[] papers = new Paper[2];
            papers[0] = new Paper("Космический нинздя", new DateTime(2022, 6, 1));
            papers[1] = new Paper("ООО Колобок", new DateTime(2021, 8, 21));
            team.AddPapers(papers);

            foreach (Paper paper in team.list)
            {
                Console.WriteLine(paper.Publication);
                Console.WriteLine(paper.Time.ToShortDateString());
            }


            Console.WriteLine(team.ToString());

            Console.ReadKey();
        }
    }



    class Paper
    {


        string NameP { get; set; }   // свойство типа string , в котором хранится название публикации
        string Author { get; set; }   // свойство типа Person для автора публикации
        DateTime Data { get; set; }   // свойство типа DateTime c датой публикации


        //конструктор c параметрами типа string, Person, DateTime для инициализации всех свойств класса;
        public Paper(string name, string author, DateTime data)
        {
            NameP = name;
            Author = author;
            Data = data;
        }

        // конструктор без параметров, инициализирующий все свойства класса некоторыми значениями по умолчанию
        public Paper() : this("WarandWorld", "Lolstoy", new DateTime(1889, 7, 1)) { }



        public Paper(string publication, DateTime time)
        {
            Publication = publication;
            Time = time;
        }
        public string Publication;

        public DateTime Time;
    }

    //Определить тип TimeFrame  перечисление(enum) со значениями Year, TwoYears, Long.
    enum TimeFrame { Year, TwoYears, Long }


    class ResearchTeam
    {


        private string Theme;        // закрытое поле типа string c названием темы исследований
        private string NameOfOrg;    // закрытое поле типа string с названием организации
        private int NumberOfRed;     // закрытое поле типа int - регистрационный номер
        private TimeFrame Last;      // закрытое поле типа TimeFrame для информации о продолжительности ис-следований
        // закрытое поле типа Paper*+, в котором хранится список публикаций

        private List<Paper> Papers = new List<Paper>() { new Paper("pub_1", new DateTime(1881, 6, 2)), new Paper("pub_2", new DateTime(1883, 6, 2)), new Paper("pub_3", new DateTime(1885, 6, 2)) };  //,



        //  В классе ResearchTeam определить конструкторы  :
        //  Конструктор c параметрами типа string, string, int, TimeFrame для инициализации соответствующих полей класса


        public ResearchTeam(string theme, string nameoforg, int numberofred)
        {
            Theme = theme;
            NameOfOrg = nameoforg;
            NumberOfRed = numberofred;

        }


        //Конструктор без параметров, инициализирующий поля класса значениями по умолчанию         
        public ResearchTeam() : this("Ecology", "Colos", 1924714927) { }


        //В классе ResearchTeam определить свойства c методами get и set
        //Cвойство типа string для доступа к полю с названием темы исследова-ний
        public string theme
        {
            get
            {
                return Theme;
            }
        }

        //Cвойство типа string для доступа к полю с названием организации
        public string nameoforg
        {
            get
            {
                return NameOfOrg;
            }
        }

        //Cвойство типа int для доступа к полю с номером регистрации
        public int numberofred
        {
            get
            {
                return NumberOfRed;
            }
        }

        //Cвойство типа TimeFrame для доступа к полю с продолжительностью ис-следований
        public TimeFrame last
        {
            get
            {
                return Last;
            }
        }

        //Cвойство типа Paper[] для доступа к полю со списком публикаций по те-ме исследований    

        public List<Paper> list
        {
            get { return Papers; }

            set { Papers = value; }
        }


        public void AddPapers(Paper[] papers)
        {
            Papers.AddRange(papers);
        }


        public override string ToString()
        {
            return string.Join(",", Papers.Select(p => p.Publication));
        }


    }


}