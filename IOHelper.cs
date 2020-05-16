using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Lab8
{
    public class IOHelper
    {
        private FileStream fs;
        private StreamWriter sw;
        private StreamReader sr;
        private string fileName;

        public IOHelper(FileStream fs_ = null, StreamWriter sw_ = null, StreamReader sr_ = null, string fileName_ = null)
        {
            fs = fs_;
            sw = sw_;
            sr = sr_;
            fileName = fileName_;
        }

        public bool WriteAllStudentsData(string fileName_, List<Student> students )
        {
            bool success = true;
            fs = new FileStream(fileName_, FileMode.Create);
            sw = new StreamWriter(fs);

            foreach(Student student in students)
            {
                sw.Write("[[Student]]\n");
                sw.Write("[Imie]\n");
                sw.Write(student.imie + "\n" );
                sw.Write("[Nazwisko]\n");
                sw.Write(student.nazwisko + "\n" );
                sw.Write("[NrIndeksu]\n");
                sw.Write(student.nrIndeksu + "\n");
                sw.Write("[Wydzial]\n");
                sw.Write(student.wydzial + "\n");
                sw.Write("[[]]\n");
            }




            sw.Close();

            return success;
        }

        public bool ReadAndWriteStudents(string fileName, List<Student> list)
        {
            bool success = true;
            string tempImie = null, tempNazw = null, tempIndeks = null, tempWydzial = null;
            List<String> lines = File.ReadLines(fileName).ToList();
            List<Student> tempStudents = new List<Student>();

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i] == "[Imie]")
                    tempImie = lines[i + 1];
                else if (lines[i] == "[Nazwisko]")
                    tempNazw = lines[i + 1];
                else if (lines[i] == "[NrIndeksu]")
                    tempIndeks = lines[i + 1];
                else if (lines[i] == "[Wydzial]")
                    tempWydzial = lines[i + 1];

                else if (lines[i] == "[[]]")
                    tempStudents.Add(new Student(tempImie, tempNazw, tempWydzial, tempIndeks));
            };


            if (lines.Count <= 3)
                success = false;
            else
            {

                list.Clear();
                foreach (Student st in tempStudents)
                    list.Add(st);
                

            }
        



            return success;

        }

        public bool WriteAllStudentsData(List<Student> students)
        {
            fs = new FileStream(fileName, FileMode.Create);
            sw = new StreamWriter(fs);

            foreach (Student student in students)
            {
                sw.Write("[[Student]]\n");
                sw.Write("[Imie]\n");
                sw.Write(student.imie + "\n");
                sw.Write("[Nazwisko]\n");
                sw.Write(student.nazwisko + "\n");
                sw.Write("[NrIndeksu]\n");
                sw.Write(student.nrIndeksu + "\n");
                sw.Write("[Wydzial]\n");
                sw.Write(student.wydzial + "\n");
                sw.Write("[[]]\n");
            }




            sw.Close();


            return true;
        }


    }
}
