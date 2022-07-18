using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 員工
{
    public partial class Form_搜尋視窗 : Form
    {
        public string Phone{ get; set; }
        private bool _IsOkClick = false;

        private string _KeyWord;
        public string KeyWord
        {
            get { return _KeyWord; }
            set { _KeyWord = value; }
        }
        public bool IsOkClick
        {
            get { return _IsOkClick; }
        }
        public Form_搜尋視窗()
        {
            InitializeComponent();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            _IsOkClick= true;
            _KeyWord = txtKeyWord.Text;
            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
