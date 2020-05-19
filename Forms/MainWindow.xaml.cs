using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
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
using System.IO;
using System.Xml.Serialization;
using System.Xml;
namespace Lab8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
   
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
                new Student(){imie = "Stefan", nazwisko = "Belnikowicz", nrIndeksu = "1026", wydzial = "WZ", marks = {
                        new Mark("5", "WF"),
                        new Mark("5", "angielski"),
                        new Mark("5", "statystyka")
                    }
                },
                new Student(){imie = "Andrzej", nazwisko = "Brzesniak", nrIndeksu = "1027", wydzial = "WE", marks = {
                        new Mark("5", "matematyka"),
                        new Mark("5", "obwody"),
                        new Mark("5", "elektryka")
                    }
                },
                new Student(){imie = "Lukasz", nazwisko = "Kaczrakowski", nrIndeksu = "1028", wydzial = "WZ", marks = {
                        new Mark("4", "statystyka"),
                        new Mark("4", "angielski"),
                        new Mark("4", "logistyka")
                    }
                }
        };
        IOHelper iOHelper = new IOHelper();
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

        private void loadfromFileItem_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".txt";
            dialog.FileName = "plik";
            dialog.Filter = "TXT document (.txt) |*.txt";

            if (dialog.ShowDialog() == true)
            {
                if (!iOHelper.ReadAndWriteStudents(dialog.FileName, lista))
                    MessageBox.Show("Plik nie zawiera danych ");


                studentsGrid.Items.Refresh();
            }
        }
               

        private void Savealltofile_Click(object sender, RoutedEventArgs e)
        {
            
            iOHelper.WriteAllStudentsData("plikDomyslny.txt", lista);
        }

        private void saveDynamically_Click(object sender, RoutedEventArgs e)
        {

            iOHelper.SaveDynamically("dynamicznyPlikDomyslny.txt", lista);
        }

        private void loadDynamicallyItem_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".txt";
            dialog.FileName = "plik";
            dialog.Filter = "TXT document (.txt) |*.txt";

            if (dialog.ShowDialog() == true)
            {
                List<Student> tempList = iOHelper.loadDynamically(dialog.FileName);
                if (tempList.Count >= 1)
                {
                    lista.Clear();
                    foreach (var item in tempList)
                    {
                        lista.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("Plik jest pusty!");
                }

               
                studentsGrid.Items.Refresh();
              

            }
            


        }

        private void SavetoXMLItem_Click(object sender, RoutedEventArgs e) // NOWA OPCJA - SERIALIZATION TO XML FILE
        {
            

            FileStream fs = new FileStream("./XMLplikDomyslny.xml", FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));

            serializer.Serialize(fs, lista);

            
            
       
            
            
            fs.Close();
        }

        private void LoadfromXMLItem_Click(object sender, RoutedEventArgs e) // NOWA OPCJA - DESERIALIZATION FROM XML FILE
        {
        

            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".xml";
            dialog.FileName = "plik";
            dialog.Filter = "XML document (.xml) |*.xml";

            if (dialog.ShowDialog() == true)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
                XmlReader reader = XmlReader.Create(dialog.FileName);
                List<Student> tempList = (List<Student>)serializer.Deserialize(reader);

                if (tempList.Count >= 1)
                {
                    lista.Clear();
                    foreach (var item in tempList)
                        lista.Add(item);
                    studentsGrid.Items.Refresh();
                    reader.Close();
                    Debug.WriteLine(dialog.FileName);
                }
                else
                    MessageBox.Show("Plik XML jest pusty!");
            }

        }

        
        
        private void SavetoXMLASItem_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dialog1 = new Microsoft.Win32.SaveFileDialog();

            dialog1.DefaultExt = ".xml";
            dialog1.FileName = "Nowy plik";
            dialog1.Filter = "XML document (.xml) |*.xml";

            if (dialog1.ShowDialog() == true)
            {

                FileStream fs = new FileStream(dialog1.FileName, FileMode.Create);
                XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
                serializer.Serialize(fs, lista);
                fs.Close();
                if (File.Exists(dialog1.FileName))
                    MessageBox.Show("Plik XML zostal zapisany!");
            }
        }

        private void SavealltofileAS_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dialog1 = new Microsoft.Win32.SaveFileDialog();

            dialog1.DefaultExt = ".txt";
            dialog1.FileName = "Nowy plik";
            dialog1.Filter = "TXT document (.txt) |*.txt";

            if (dialog1.ShowDialog() == true)
            {
                iOHelper.WriteAllStudentsData(dialog1.FileName, lista);
                if (File.Exists(dialog1.FileName))
                    MessageBox.Show("Plik TXT zostal zapisany!");
            }
        }

        private void saveDynamicallyAs_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dialog1 = new Microsoft.Win32.SaveFileDialog();

            dialog1.DefaultExt = ".txt";
            dialog1.FileName = "Nowy plik";
            dialog1.Filter = "TXT document (.txt) |*.txt";
            if (dialog1.ShowDialog() == true)
            {
                iOHelper.SaveDynamically(dialog1.FileName, lista);
                if (File.Exists(dialog1.FileName))
                    MessageBox.Show("Plik TXT zostal zapisany!");
            }
        }
    }
}
