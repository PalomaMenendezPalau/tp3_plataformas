using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP2
{
    public partial class Form1 : Form
    {
        string[] sessionValues = new string[2]; 

        public Form1(string[] sessionVals)
        {   
            
            InitializeComponent();
            sessionValues = sessionVals;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public void setSessionValues(string[] values) 
        {
            sessionValues = values;
        }

        public string[] getSessionValues() 
        {
            return sessionValues;
        }
    }
}
