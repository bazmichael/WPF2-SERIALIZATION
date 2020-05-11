using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddMarkWindow.xaml
    /// </summary>
    public partial class AddMarkWindow : Window
    {
        public Mark mark = new Mark();
        public AddMarkWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(
                !Regex.IsMatch(tbPrzedmiot.Text, @"^[a-zA-Z]+$") ||
                !Regex.IsMatch(tbOcena.Text, @"^[2-5]$")
               )
            {
                MessageBox.Show("Niepoprawne dane oceny!");
                return;
            }

            mark.ocena = tbOcena.Text;
            mark.przedmiot = tbPrzedmiot.Text;
            /*
            mark = new Marks(tbOcena.Text, tbPrzedmiot.Text);*/
            this.DialogResult = true;

        }
    }
}
