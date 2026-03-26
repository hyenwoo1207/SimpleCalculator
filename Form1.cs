using System;
using System.Windows.Forms;

namespace SimpleCalculater
{
    public partial class Form1 : Form
    {
        enum Operators
        {
            None,
            Add,
            Subtract,
            Multiply,
            Divide
        }

        // Designer에서 참조하는 누락된 이벤트 핸들러들
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // 의도적으로 비워둠(디자이너 이벤트 연결용)
        }

        private void btn1_Click(object sender, EventArgs e) => NumberButton_Click(sender, e);
        private void btn2_Click(object sender, EventArgs e) => NumberButton_Click(sender, e);
        private void btn3_Click(object sender, EventArgs e) => NumberButton_Click(sender, e);
        private void btn4_Click(object sender, EventArgs e) => NumberButton_Click(sender, e);
        private void btn5_Click(object sender, EventArgs e) => NumberButton_Click(sender, e);
        private void btn6_Click(object sender, EventArgs e) => NumberButton_Click(sender, e);
        private void btn7_Click(object sender, EventArgs e) => NumberButton_Click(sender, e);
        private void btn8_Click(object sender, EventArgs e) => NumberButton_Click(sender, e);
        private void btn9_Click(object sender, EventArgs e) => NumberButton_Click(sender, e);
        private void button3_Click(object sender, EventArgs e) => NumberButton_Click(sender, e); // btn0

        // CE 버튼: 현재 항목만 지움
        private void button17_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            isNewEntry = true;
        }

        Operators currentOperator = Operators.None;
        double firstOperand = 0;
        bool isNewEntry = true;

        public Form1()
        {
            InitializeComponent();
            txtDisplay.Text = "0";
        }

        // 숫자 버튼 공통 이벤트
        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (txtDisplay.Text == "0" || isNewEntry)
            {
                txtDisplay.Text = btn.Text;
                isNewEntry = false;
            }
            else
            {
                txtDisplay.Text += btn.Text;
            }
        }

        // + 버튼
        private void btnPlus_Click(object sender, EventArgs e)
        {
            firstOperand = Convert.ToDouble(txtDisplay.Text);
            currentOperator = Operators.Add;
            isNewEntry = true;
        }

        // - 버튼
        private void btnMinus_Click(object sender, EventArgs e)
        {
            firstOperand = Convert.ToDouble(txtDisplay.Text);
            currentOperator = Operators.Subtract;
            isNewEntry = true;
        }

        // * 버튼
        private void btnMultiply_Click(object sender, EventArgs e)
        {
            firstOperand = Convert.ToDouble(txtDisplay.Text);
            currentOperator = Operators.Multiply;
            isNewEntry = true;
        }

        // / 버튼
        private void btnDivide_Click(object sender, EventArgs e)
        {
            firstOperand = Convert.ToDouble(txtDisplay.Text);
            currentOperator = Operators.Divide;
            isNewEntry = true;
        }

        // = 버튼
        private void btnEqual_Click(object sender, EventArgs e)
        {
            double secondOperand = Convert.ToDouble(txtDisplay.Text);
            double result = 0;

            switch (currentOperator)
            {
                case Operators.Add:
                    result = firstOperand + secondOperand;
                    break;

                case Operators.Subtract:
                    result = firstOperand - secondOperand;
                    break;

                case Operators.Multiply:
                    result = firstOperand * secondOperand;
                    break;

                case Operators.Divide:
                    if (secondOperand == 0)
                    {
                        MessageBox.Show("0으로 나눌 수 없습니다.");
                        return;
                    }
                    result = firstOperand / secondOperand;
                    break;

                case Operators.None:
                    result = secondOperand;
                    break;
            }

            txtDisplay.Text = result.ToString();
            isNewEntry = true;
            currentOperator = Operators.None;
        }

        // C 버튼
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            firstOperand = 0;
            currentOperator = Operators.None;
            isNewEntry = true;
        }

        // . 버튼
        private void btnDot_Click(object sender, EventArgs e)
        {
            if (isNewEntry)
            {
                txtDisplay.Text = "0.";
                isNewEntry = false;
                return;
            }

            if (!txtDisplay.Text.Contains("."))
            {
                txtDisplay.Text += ".";
            }
        }
    }
}
