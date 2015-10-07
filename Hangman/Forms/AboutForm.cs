using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hangman
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            TextAreaWriter();
        }

        private void TextAreaWriter()
        {
            textBox1.Text = "Правила: На първия ред се показва скритата дума, замаскирана със звездички, като броя на звездичките съответства на броя на буквите в думата. В празното поле въведете буква, която предполагате, че се съдържа. Ако буквата се съдържа в думата, тя ще се появи на всички позиции, на които я има. В противен случай, сте с една крачка по-близо до смъртта. Имате право на 6 грешни опити, преди да бъдете обедени. Победили или не можете да продължите отново и отново. Enjoy it :)";
        }
    }
}
