using System;
using System.Collections.Generic;
using System.Text;

namespace Lab8
{
    public class Student
    {
        public List<Mark> marks = new List<Mark>();

        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string wydzial { get; set; }
        public string nrIndeksu { get; set; }
      
        public Student(string imie_ = "", string nazwisko_ = " ", string wydzial_ = " " , string nrIndeksu_ = "0")
        {
            imie = imie_;
            nazwisko = nazwisko_;
            wydzial = wydzial_;
            nrIndeksu = nrIndeksu_;
        }

        public void addMark(Mark mark)
        {
            marks.Add(mark);
        }

        public bool FindIndeks(string indeks)
        {
            bool success = false;
            if (nrIndeksu == indeks)
                success = true;



            return success;
        }

    }
}
