using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
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
    /// Interaction logic for MarksWindow.xaml
    /// </summary>
    public partial class MarksWindow : Window
    {
        Student student;

        public MarksWindow()
        {
            InitializeComponent();

            marksGrid.ItemsSource = student.marks;
            marksGrid.AutoGenerateColumns = false;

        }
        public MarksWindow(Student student_)
        {
            InitializeComponent();
            student = student_;
            marksGrid.ItemsSource = student.marks;
            marksGrid.AutoGenerateColumns = false;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddMarkWindow();

            if(dialog.ShowDialog() == true)
            {
                student.marks.Add(dialog.mark);
                marksGrid.Items.Refresh();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(marksGrid.SelectedItem is Mark)
            {
                student.marks.Remove((Mark)marksGrid.SelectedItem);
                marksGrid.Items.Refresh();
            }
        }
    }
}
