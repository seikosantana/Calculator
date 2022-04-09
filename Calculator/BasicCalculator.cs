using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class BasicCalculator : IBasicCalculator
    {
        public decimal Operand1 { get; set; }
        public decimal Operand2 { get; set; }

        IBasicCalculator.Operators _operator;
        private bool isLastButtonOperator;
        public void Reset()
        {
            Operand1 = 0;
            Operand2 = 0;
            isLastButtonEquals = false;
            isLastButtonOperator = false;
            Operator = IBasicCalculator.Operators.NotAssigned;
        }

        public IBasicCalculator.Operators Operator
        {
            get => _operator;
            set
            {
                _operator = value;
                isLastButtonOperator = true;
            }
        }

        public bool IsLastButtonAnOperator
        {
            get
            {
                bool ret = isLastButtonOperator;
                isLastButtonOperator = false;
                return ret;
            }
        }

        bool isLastButtonEquals = false;
        public bool IsLastButtonEquals
        {
            get
            {
                bool ret = isLastButtonEquals;
                isLastButtonEquals = false;
                return ret;
            }
        }

        public decimal Calculate()
        {
            isLastButtonOperator = true;
            isLastButtonEquals = true;
            switch (Operator)
            {
                case IBasicCalculator.Operators.NotAssigned:
                    return Operand2;
                case IBasicCalculator.Operators.Plus:
                    return Operand1 + Operand2;
                case IBasicCalculator.Operators.Minus:
                    return Operand1 - Operand2;
                case IBasicCalculator.Operators.Multiply:
                    return Operand1 * Operand2;
                case IBasicCalculator.Operators.Divide:
                    return Operand1 / Operand2;
                default:
                    return 0;
            }
        }
    }
}
