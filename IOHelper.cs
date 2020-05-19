using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
namespace Lab8
{
    public class IOHelper
    {
        private FileStream fs;
        private StreamWriter sw;
        private int counter = 0;

        public IOHelper(FileStream fs_ = null, StreamWriter sw_ = null, StreamReader sr_ = null, string fileName_ = null)
        {
            fs = fs_;
            sw = sw_;
        }

        public bool WriteAllStudentsData(string fileName_, List<Student> students )
        {
            bool success = true;
            fs = new FileStream(fileName_, FileMode.Create);
            sw = new StreamWriter(fs);

            foreach(Student student in students)
            {
                sw.Write("[[Lab8.Student]]\n");
                sw.Write("[imie]\n");
                sw.Write(student.imie + "\n" );
                sw.Write("[nazwisko]\n");
                sw.Write(student.nazwisko + "\n" );
                sw.Write("[nrIndeksu]\n");
                sw.Write(student.nrIndeksu + "\n");
                sw.Write("[wydzial]\n");
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
                if (lines[i] == "[imie]")
                    tempImie = lines[i + 1];
                else if (lines[i] == "[nazwisko]")
                    tempNazw = lines[i + 1];
                else if (lines[i] == "[nrIndeksu]")
                    tempIndeks = lines[i + 1];
                else if (lines[i] == "[wydzial]")
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

 
        public bool SaveDynamically(string fileName, List<Student> list)
        {
            bool success = false;
            FileStream fs = new FileStream(fileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            foreach (Student student in list)
                Saver(student, sw);
        

            sw.Close();

            return success;
        }



        private void Saver<T>(T obj, StreamWriter sw)
        {
            Type t = obj.GetType();
            sw.WriteLine($"[[{t.FullName}]]");
            foreach(var p in t.GetProperties())
            {
                sw.WriteLine($"[{p.Name}]");
                sw.WriteLine(p.GetValue(obj));

            }
            sw.WriteLine("[[]]");
            
        }

        private int studentCounter(string fileName)
        {
            List<String> lines = File.ReadLines(fileName).ToList();
            int amount = 0;
            foreach (var item in lines)
                if (item == "[[]]") amount++;
            


            return amount;
        }

        public List<Student> loadDynamically(string fileName)
        {
            Debug.WriteLine(studentCounter(fileName));
            var amountOfStudents = studentCounter(fileName);
            List<Student> students = new List<Student>();
            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            for (counter = 0; counter < amountOfStudents; counter++) 
            {
                Student student = Loader<Student>(sr);
                students.Add(student);
            }

            sr.Close();
           
            return students;
            
        }

        public T Loader<T>(StreamReader sr) where T : new()
        {
            T ob = default(T);
            Type tob = null;
            PropertyInfo property = null;
            


            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();

                if (line == "[[]]")
                    return ob;

                else if (line.StartsWith("[["))
                {

                    tob = Type.GetType(line.Trim('[', ']'));

                    if (typeof(T).IsAssignableFrom(tob))
                        ob = (T)Activator.CreateInstance(tob);
                }

                else if (line.StartsWith("[") && ob != null)
                    property = tob.GetProperty(line.Trim('[', ']'));

                else if (ob != null && property != null)
                    property.SetValue(ob, Convert.ChangeType(line, property.PropertyType));
            
            }

            return default;
        }


       











    }
}
