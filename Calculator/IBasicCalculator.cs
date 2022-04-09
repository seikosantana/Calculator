using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    interface IBasicCalculator
    {
        enum Operators
        {
            NotAssigned,
            Plus,
            Minus,
            Multiply,
            Divide
        }
        bool IsLastButtonAnOperator { get; }
        bool IsLastButtonEquals { get; }
        decimal Operand1 { get; set; }
        decimal Operand2 { get; set; }
        Operators Operator { get; set; }
        void Reset();
        static decimal GetSqrt(decimal number)
        {
            return (decimal)Math.Sqrt((double)number);
        }
        decimal Calculate();
    }
}
