using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Student> lista = new List<Student>()
        {
                new Student(){imie = "Jan", nazwisko = "Budalski", nrIndeksu = "1013", wydzial = "WIMiI", marks = {
                    new Mark("3", "matematyka"),
                        new Mark("3", "angielski"),
                        new Mark("3", "fizyka")
                    } },
                new Student(){imie = "Michal", nazwisko = "Nowak", nrIndeksu = "1021", wydzial = "WIMiI", marks = {
                        new Mark("4", "matematyka"),
                        new Mark("4", "angielski"),
                        new Mark("4", "fizyka")
                    } },
                new Student(){imie = "Tomasz", nazwisko = "Makieta", nrIndeksu = "1025", wydzial = "WIMiI", marks = { 
                        new Mark("5", "matematyka"),
                        new Mark("5", "angielski"),
                        new Mark("5", "fizyka")
                    } 
                },
                new Student(){imie = "Stefan", nazwisko = "Makieta", nrIndeksu = "1026", wydzial = "WZ", marks = {
                        new Mark("5", "WF"),
                        new Mark("5", "angielski"),
                        new Mark("5", "statystyka")
                    }
                },
                new Student(){imie = "Andrzej", nazwisko = "Makieta", nrIndeksu = "1027", wydzial = "WE", marks = {
                        new Mark("5", "matematyka"),
                        new Mark("5", "obwody"),
                        new Mark("5", "elektryka")
                    }
                },
                new Student(){imie = "Lukasz", nazwisko = "Makieta", nrIndeksu = "1028", wydzial = "WZ", marks = {
                        new Mark("4", "statystyka"),
                        new Mark("4", "angielski"),
                        new Mark("4", "logistyka")
                    }
                }
        };
        public MainWindow()
        {
          
            InitializeComponent();


            studentsGrid.ItemsSource = lista;
            studentsGrid.AutoGenerateColumns = false;
                 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(studentsGrid.SelectedItem is Student)
            {
                lista.Remove((Student)studentsGrid.SelectedItem);
                studentsGrid.Items.Refresh();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StudentWindow(lista);
            if (dialog.ShowDialog() == true)
            {
                lista.Add(dialog.student);
                studentsGrid.Items.Refresh();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (studentsGrid.SelectedItem is Student)
            {
                var dialog2 = new MarksWindow((Student)studentsGrid.SelectedItem);
                dialog2.ShowDialog();
            }
            else
                MessageBox.Show("Musisz wybrac studenta!");
                    
                    
             
            
        }
    }
}
