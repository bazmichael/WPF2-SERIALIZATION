using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab8
{
    /// <summary>
    /// Interaction logic for StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        public Student student;
        private List<Student> students;
        public StudentWindow()
        {
            InitializeComponent();


        }

        public StudentWindow(List<Student> students_)
        {
            InitializeComponent();
            students = new List<Student>(students_);
            
        }



        public StudentWindow(Student student = null)
        {
            InitializeComponent();

            if(student != null)
            {
                tbImie.Text = student.imie;
                tbNazwisko.Text = student.nazwisko;
                tbWydzial.Text = student.wydzial;
                tbNrIndeksu.Text = student.nrIndeksu.ToString();

            }

            this.student = student ?? new Student();
        }

     

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(tbImie.Text, @"^[a-zA-Z]+$") ||
                !Regex.IsMatch(tbNazwisko.Text, @"^[a-zA-Z]+$") ||
                !Regex.IsMatch(tbWydzial.Text, @"^[a-zA-Z]+$") ||
                Regex.IsMatch(tbNrIndeksu.Text, @"^[a-zA-Z]+$")
                )
            {
                MessageBox.Show("Niepoprawne dane!");
                return;
            }

            if (students.Any(x => x.FindIndeks(tbNrIndeksu.Text)))
            {
                MessageBox.Show("Numer indeksu musi byc unikatowy!");
                return;
            }
            
            

            student = new Student(tbImie.Text, tbNazwisko.Text, tbWydzial.Text, tbNrIndeksu.Text);
            this.DialogResult = true;
        }
    }
}
