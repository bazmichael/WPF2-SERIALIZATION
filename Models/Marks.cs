using System;
using System.Collections.Generic;
using System.Text;

namespace Lab8
{
    public class Mark
    {
        public string ocena { get; set; }
        public string przedmiot { get; set; }

        public Mark()
        {
            ocena = "0";
            przedmiot = "nieznany";
        }

        public Mark(string ocena_ = " ", string przedmiot_ = " ")
        {
            ocena = ocena_;
            przedmiot = przedmiot_;
           
        }
    }
}
