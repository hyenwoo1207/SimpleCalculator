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


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

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

        // CE 버튼: 현재 항목(마지막 숫자)만 지움
        private void button17_Click(object sender, EventArgs e)
        {
            // 표현식에서 마지막 숫자 부분을 제거하고 0으로 대체
            if (string.IsNullOrEmpty(txtDisplay.Text) || txtDisplay.Text == "0")
            {
                txtDisplay.Text = "0";
                currentEntry = string.Empty;
                isNewEntry = true;
                return;
            }

            int idx = txtDisplay.Text.LastIndexOfAny(new char[] { '+', '-', 'X', '*', '/', '÷' });
            if (idx >= 0)
            {
                // 남아있는 표현식(연산자 포함)에 0을 붙여 현재 항목을 0으로 만듦
                txtDisplay.Text = txtDisplay.Text.Substring(0, idx + 1) + "0";
            }
            else
            {
                // 표현식에 연산자가 없으면 전체를 0으로
                txtDisplay.Text = "0";
            }

            currentEntry = string.Empty;
            isNewEntry = true;
        }

        Operators currentOperator = Operators.None;
        double firstOperand = 0;
        bool isNewEntry = true;
        // 현재 입력 중인 숫자(표현식의 마지막 숫자)
        string currentEntry = string.Empty;

        public Form1()
        {
            InitializeComponent();
            txtDisplay.Text = "0";
        }

        // 숫자 버튼 공통 이벤트
        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            // 숫자 입력을 currentEntry와 표현식(txtDisplay)에 반영
            if (isNewEntry)
            {
                currentEntry = btn.Text;
                if (txtDisplay.Text == "0")
                    txtDisplay.Text = btn.Text;
                else
                    txtDisplay.Text += btn.Text;
                isNewEntry = false;
            }
            else
            {
                currentEntry += btn.Text;
                if (txtDisplay.Text == "0")
                    txtDisplay.Text = btn.Text;
                else
                    txtDisplay.Text += btn.Text;
            }
        }

        // + 버튼
        private void btnPlus_Click(object sender, EventArgs e)
        {
            // 현재 입력 숫자를 파싱
            double value = 0;
            if (!double.TryParse(currentEntry, out value))
            {
                // 표현식에서 마지막 숫자 추출
                string last = GetLastNumberFromExpression(txtDisplay.Text);
                double.TryParse(last, out value);
            }

            if (currentOperator == Operators.None)
            {
                firstOperand = value;
            }
            else
            {
                // 연속 연산 지원(이전 연산 적용)
                switch (currentOperator)
                {
                    case Operators.Add:
                        firstOperand = firstOperand + value;
                        break;
                    case Operators.Subtract:
                        firstOperand = firstOperand - value;
                        break;
                    case Operators.Multiply:
                        firstOperand = firstOperand * value;
                        break;
                    case Operators.Divide:
                        if (value == 0)
                        {
                            MessageBox.Show("0으로 나눌 수 없습니다.");
                            return;
                        }
                        firstOperand = firstOperand / value;
                        break;
                }
            }

            // 표현식에 연산자 추가(중복 연산자 방지)
            if (!string.IsNullOrEmpty(txtDisplay.Text) && !EndsWithOperator(txtDisplay.Text))
                txtDisplay.Text += "+";
            else if (EndsWithOperator(txtDisplay.Text))
            {
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1) + "+";
            }

            currentOperator = Operators.Add;
            currentEntry = string.Empty;
            isNewEntry = true;
        }

        // - 버튼
        private void btnMinus_Click(object sender, EventArgs e)
        {
            // 현재 입력 숫자를 파싱
            double value = 0;
            if (!double.TryParse(currentEntry, out value))
            {
                string last = GetLastNumberFromExpression(txtDisplay.Text);
                double.TryParse(last, out value);
            }

            if (currentOperator == Operators.None)
            {
                firstOperand = value;
            }
            else
            {
                switch (currentOperator)
                {
                    case Operators.Add:
                        firstOperand = firstOperand + value;
                        break;
                    case Operators.Subtract:
                        firstOperand = firstOperand - value;
                        break;
                    case Operators.Multiply:
                        firstOperand = firstOperand * value;
                        break;
                    case Operators.Divide:
                        if (value == 0)
                        {
                            MessageBox.Show("0으로 나눌 수 없습니다.");
                            return;
                        }
                        firstOperand = firstOperand / value;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(txtDisplay.Text) && !EndsWithOperator(txtDisplay.Text))
                txtDisplay.Text += "-";
            else if (EndsWithOperator(txtDisplay.Text))
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1) + "-";

            currentOperator = Operators.Subtract;
            currentEntry = string.Empty;
            isNewEntry = true;
        }

        // * 버튼
        private void btnMultiply_Click(object sender, EventArgs e)
        {
            // 현재 입력 숫자를 파싱
            double value = 0;
            if (!double.TryParse(currentEntry, out value))
            {
                string last = GetLastNumberFromExpression(txtDisplay.Text);
                double.TryParse(last, out value);
            }

            if (currentOperator == Operators.None)
            {
                firstOperand = value;
            }
            else
            {
                switch (currentOperator)
                {
                    case Operators.Add:
                        firstOperand = firstOperand + value;
                        break;
                    case Operators.Subtract:
                        firstOperand = firstOperand - value;
                        break;
                    case Operators.Multiply:
                        firstOperand = firstOperand * value;
                        break;
                    case Operators.Divide:
                        if (value == 0)
                        {
                            MessageBox.Show("0으로 나눌 수 없습니다.");
                            return;
                        }
                        firstOperand = firstOperand / value;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(txtDisplay.Text) && !EndsWithOperator(txtDisplay.Text))
                txtDisplay.Text += "X";
            else if (EndsWithOperator(txtDisplay.Text))
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1) + "X";

            currentOperator = Operators.Multiply;
            currentEntry = string.Empty;
            isNewEntry = true;
        }

        // / 버튼
        private void btnDivide_Click(object sender, EventArgs e)
        {
            // 현재 입력 숫자를 파싱
            double value = 0;
            if (!double.TryParse(currentEntry, out value))
            {
                string last = GetLastNumberFromExpression(txtDisplay.Text);
                double.TryParse(last, out value);
            }

            if (currentOperator == Operators.None)
            {
                firstOperand = value;
            }
            else
            {
                switch (currentOperator)
                {
                    case Operators.Add:
                        firstOperand = firstOperand + value;
                        break;
                    case Operators.Subtract:
                        firstOperand = firstOperand - value;
                        break;
                    case Operators.Multiply:
                        firstOperand = firstOperand * value;
                        break;
                    case Operators.Divide:
                        if (value == 0)
                        {
                            MessageBox.Show("0으로 나눌 수 없습니다.");
                            return;
                        }
                        firstOperand = firstOperand / value;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(txtDisplay.Text) && !EndsWithOperator(txtDisplay.Text))
                txtDisplay.Text += "÷";
            else if (EndsWithOperator(txtDisplay.Text))
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1) + "÷";

            currentOperator = Operators.Divide;
            currentEntry = string.Empty;
            isNewEntry = true;
        }

        // = 버튼
        private void btnEqual_Click(object sender, EventArgs e)
        {
            // 현재 입력 숫자 또는 표현식의 마지막 숫자 가져오기
            double secondOperand = 0;
            if (!double.TryParse(currentEntry, out secondOperand))
            {
                string last = GetLastNumberFromExpression(txtDisplay.Text);
                double.TryParse(last, out secondOperand);
            }

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

            // 계산 결과는 textBox3에 표시
            textBox3.Text = result.ToString();

            // 상태 초기화(계산 결과를 다음 입력의 시작값으로 사용 가능)
            isNewEntry = true;
            currentOperator = Operators.None;
            currentEntry = result.ToString();
        }

        // C 버튼
        private void btnClear_Click(object sender, EventArgs e)
        {
            // 전체 초기화
            txtDisplay.Text = "0";
            textBox3.Text = string.Empty;
            firstOperand = 0;
            currentOperator = Operators.None;
            currentEntry = string.Empty;
            isNewEntry = true;
        }

        // DEL 버튼: 마지막 입력 문자(현재 항목의 마지막 문자 또는 연산자)를 삭제
        private void button19_Click(object sender, EventArgs e)
        {
            // 우선 현재 입력 항목이 있으면 그 항목에서 한 글자 삭제
            if (!string.IsNullOrEmpty(currentEntry))
            {
                if (currentEntry.Length > 1)
                {
                    currentEntry = currentEntry.Substring(0, currentEntry.Length - 1);
                    txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
                }
                else
                {
                    // currentEntry 한 글자만 남아있으면 삭제하면 0으로 대체
                    currentEntry = string.Empty;
                    if (!string.IsNullOrEmpty(txtDisplay.Text))
                    {
                        txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
                        if (string.IsNullOrEmpty(txtDisplay.Text) || EndsWithOperator(txtDisplay.Text) == false && txtDisplay.Text.Length == 0)
                            txtDisplay.Text = "0";
                    }
                    else
                    {
                        txtDisplay.Text = "0";
                    }
                }

                isNewEntry = string.IsNullOrEmpty(currentEntry);
                return;
            }

            // currentEntry가 비어있으면 표현식 끝의 연산자 또는 숫자 삭제
            if (!string.IsNullOrEmpty(txtDisplay.Text))
            {
                // 마지막이 연산자면 연산자만 제거
                if (EndsWithOperator(txtDisplay.Text))
                {
                    txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
                    currentOperator = Operators.None;
                }
                else
                {
                    // 마지막 숫자를 하나 제거
                    if (txtDisplay.Text.Length > 1)
                        txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
                    else
                        txtDisplay.Text = "0";
                }
            }

            isNewEntry = txtDisplay.Text == "0";
        }

        // . 버튼
        private void btnDot_Click(object sender, EventArgs e)
        {
            if (isNewEntry)
            {
                currentEntry = "0.";
                if (txtDisplay.Text == "0")
                    txtDisplay.Text = currentEntry;
                else
                    txtDisplay.Text += currentEntry;
                isNewEntry = false;
                return;
            }

            if (!currentEntry.Contains("."))
            {
                currentEntry += ".";
                txtDisplay.Text += ".";
            }
        }

        // 표현식의 마지막 숫자를 추출하는 유틸
        private string GetLastNumberFromExpression(string expr)
        {
            if (string.IsNullOrEmpty(expr))
                return string.Empty;
            int idx = expr.LastIndexOfAny(new char[] { '+', '-', 'X', '*', '/', '÷' });
            if (idx >= 0 && idx < expr.Length - 1)
                return expr.Substring(idx + 1);
            return expr;
        }

        private bool EndsWithOperator(string expr)
        {
            if (string.IsNullOrEmpty(expr))
                return false;
            char last = expr[expr.Length - 1];
            return last == '+' || last == '-' || last == 'X' || last == '*' || last == '/' || last == '÷';
        }

        private void btnMultiply_Click_1(object sender, EventArgs e)
        {
            // Designer wired to this method; forward to main handler
            btnMultiply_Click(sender, e);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDivide_Click_1(object sender, EventArgs e)
        {
            // Designer wired to this method; forward to main handler
            btnDivide_Click(sender, e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
