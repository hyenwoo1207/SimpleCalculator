public partial class Form1 : Form
{
    enum Operators
    {
        None,
        Add,
        Subtract,
        Multiply,
        Divide,
        Result
    }

    Operators currentOperator = Operators.None;
    Boolean operatorChangeFlag = false;
    int firstOperand = 0;
    int secondOperand = 0;
}



namespace SimpleCalculater
{
    public partial class Form1 : Form
    {
        bool operatorChangeFlag = false;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (operatorChangeFlag == true)
            {
                txtDisplay.Text = "";
                operatorChangeFlag = false;
            }

            string strNumber = txtDisplay.Text += "1";
            int intNumber = Int32.Parse(strNumber);
            txtDisplay.Text = intNumber.ToString();
        }

        private void button17_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "9";
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "+";
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            txtDisplay.Text += "=";
        }
    }
}
