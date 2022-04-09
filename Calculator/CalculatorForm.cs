using System.Runtime.InteropServices;

namespace Calculator
{
    public partial class CalculatorForm : Form
    {

        #region darktitlebar
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e)
        {
            if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }

        #endregion

        IBasicCalculator calculator;

        public CalculatorForm()
        {
            InitializeComponent();
            InitEvents();

            calculator = new BasicCalculator();

            this.KeyDown += HandleKeyDown;
        }

        void InitEvents()
        {
            // Number buttons
            btn0.Click += AppendNumber;
            btn1.Click += AppendNumber;
            btn2.Click += AppendNumber;
            btn3.Click += AppendNumber;
            btn4.Click += AppendNumber;
            btn5.Click += AppendNumber;
            btn6.Click += AppendNumber;
            btn7.Click += AppendNumber;
            btn8.Click += AppendNumber;
            btn9.Click += AppendNumber;

            // Clearing buttons
            btnBackspace.Click += PerformBackspace;
            btnClear.Click += ClearEntry;

            // Operator buttons
            btnPlus.Click += delegate
            {
                calculator.Operand1 = CurrentEntry;
                calculator.Operator = IBasicCalculator.Operators.Plus;
            };
            btnMin.Click += delegate
            {
                calculator.Operand1 = CurrentEntry;
                calculator.Operator = IBasicCalculator.Operators.Minus;
            };
            btnMultiply.Click += delegate
            {
                calculator.Operand1 = CurrentEntry;
                calculator.Operator = IBasicCalculator.Operators.Multiply;
            };
            btnDivide.Click += delegate
            {
                calculator.Operand1 = CurrentEntry;
                calculator.Operator = IBasicCalculator.Operators.Divide;
            };

            // Square Root operation
            btnSqrt.Click += delegate
            {
                calculator.Operand1 = CurrentEntry;
                CurrentEntry = IBasicCalculator.GetSqrt(CurrentEntry);
            };

            // Equals button
            btnEquals.Click += delegate
            {
                if (calculator.IsLastButtonEquals)
                {
                    decimal lastOperand1 = calculator.Operand1;
                    decimal lastOperand2 = calculator.Operand2;
                    calculator.Operand2 = CurrentEntry;
                    calculator.Operand1 = lastOperand2;
                    CurrentEntry = calculator.Calculate();
                }
                else
                {
                    calculator.Operand2 = CurrentEntry;
                }
            };

            lblEntry.TextChanged += VerifyText;
        }

        private void ClearEntry(object? sender, EventArgs e)
        {
            CurrentEntry = 0;
            calculator.Reset();
        }

        private void VerifyText(object? sender, EventArgs e)
        {
            if (lblEntry.Text.Trim() == String.Empty)
            {
                CurrentEntry = 0;
            }
        }

        private void PerformBackspace(object? sender, EventArgs e)
        {
            if (lblEntry.Text.Length == 1)
            {
                lblEntry.Text = "0";
            }
            else
            {
                lblEntry.Text = lblEntry.Text.Substring(0, lblEntry.Text.Length - 1);
            }
        }

        decimal CurrentEntry
        {
            get
            {
                return decimal.Parse(lblEntry.Text);
            }
            set
            {
                lblEntry.Text = value.ToString();
            }
        }

        private void AppendNumber(object? sender, EventArgs e)
        {
            if (sender is null)
                return;

            string textToInsert = (sender as Button).Text;
            if (CurrentEntry == 0 || calculator.IsLastButtonAnOperator)
            {
                lblEntry.Text = textToInsert;
            }
            else
            {
                lblEntry.Text += textToInsert;
            }
        }

        private void HandleKeyDown(object? sender, KeyEventArgs e)
        {

        }
    }
}