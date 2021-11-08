using System;

namespace fraction2
{
    public class Fraction
    {
        public int IntegerPart;
        public int Numerator;
        public int Denominator;

        public Fraction(string str)
        {
            if(str.Contains(' '))
            {
                var arr = str.Split(" ");
                IntegerPart = Convert.ToInt32(arr[0]);
                var arr2 = arr[1].Split('/');
                Numerator = Convert.ToInt32(arr2[0]);
                Denominator = Convert.ToInt32(arr2[1]);
            }
            else
            {
                var arr = str.Split('/');
                Numerator = Convert.ToInt32(arr[0]);
                Denominator = Convert.ToInt32(arr[1]);
            }
        }
        public Fraction(int integer, int numer, int denomer)
        {
            IntegerPart = integer;
            Numerator = numer;
            Denominator = denomer;
        }
        public Fraction(int numer, int denomer)
        {
            Numerator = numer;
            Denominator = denomer;
        }

        public Fraction AsIrregular()
        {
            return new Fraction(IntegerPart * Denominator + Numerator, Denominator);
        }

        public Fraction AsRegular()
        {
            int num = Numerator / Denominator;
            return new Fraction(num, Numerator - (num * Denominator), Denominator);
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            a = a.AsIrregular();
            b = b.AsIrregular();
            return new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator).AsRegular();
        }

        public static Fraction operator /(Fraction a, Fraction b)
        {
            a = a.AsIrregular();
            b = b.AsIrregular();
            return new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
        }

        public static Fraction operator +(Fraction a, Fraction b)
        {
            a = a.AsIrregular();
            b = b.AsIrregular();
            var first = new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Denominator);
            var second = new Fraction(b.Numerator * a.Denominator, a.Denominator * b.Denominator);

            return new Fraction(first.Numerator + second.Numerator, first.Denominator).AsRegular();
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            a = a.AsIrregular();
            b = b.AsIrregular();
            var first = new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Denominator);
            var second = new Fraction(b.Numerator * a.Denominator, a.Denominator * b.Denominator);

            return new Fraction(first.Numerator - second.Numerator, first.Denominator).AsRegular();
        }

        public override string ToString()
        {
            var normalized = AsRegular();
            return $"{normalized.IntegerPart} {normalized.Numerator}/{normalized.Denominator}";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var str = "2 3/4 + 1/2";
            var op = FindOperation(str);
            var arr = str.Split($" {op} ");
            var a = new Fraction(arr[0]);
            var b = new Fraction(arr[1]);
            Fraction answer = DoingOperation(a, b, op);
            Console.WriteLine(answer.ToString());
        }
        public static Fraction DoingOperation(Fraction a, Fraction b, string op )
        {
            switch (op)
            {
                case "+":
                    return a + b;
                case "-":
                    return a - b;
                case "*":
                    return a * b;
                case "//":
                    return a / b;
                default:
                    throw new Exception("error");
            }
        }
        public static string FindOperation(string str)
        {
            str = str.Replace("//", "@");
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '+' || str[i] == '-' || str[i] == '*')
                {
                    return str[i].ToString();
                }
                else if (str[i] == '@')
                {
                    return "//";
                }
            }
            throw new Exception("no operation");
        }
    }


    /*public class Fraction
    {
        public double FractionalNumber;
        public static string Operation;
        readonly char[] Symbols = new char[] { '+', '-', '*' };
        public static List<double> Numbers = new List<double>();

        public Fraction(string fraction)
        {
            var array = fraction.Split(" ");
            for (int i = 0; i < array.Length; i++)
            {
                var e = array[i];
                if (e.Length == 1)
                {
                    if (Symbols.Contains(e)) //??
                        Operation = e;
                    else
                    {
                        FractionalNumber = Convert.ToInt32(e);
                        if (array[i + 1].Length == 3)
                            FractionalNumber += FractionalPart(array[i + 1]);
                    }
                }
                else if (e.Length == 2)
                {
                    Operation = e;
                }
                else
                {
                    FractionalNumber = FractionalPart(e);
                }
                Numbers.Add(FractionalNumber);
            }
        }
        public double FractionalPart(string str)
        {
            var array = str.Split();
            return Convert.ToInt32(array[0]) / Convert.ToInt32(array[2]);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var str = "2 3/4 + 1/2";
            new Fraction(str);
            Console.WriteLine(DoingOperation());
        }

        public static double DoingOperation()
        {
            var num1 = Fraction.Numbers[0];
            var num2 = Fraction.Numbers[1];
            switch (Fraction.Operation)
            {
                case "+":
                    return num1 + num2;
                case "-":
                    return num1 - num2;
                case "*":
                    return num1 * num2;
                case "//":
                    {
                        if (num2 != 0.0)
                            return num1 / num2;
                        else
                            throw new Exception("error");
                    }
                default:
                    throw new Exception("error");
            }
        }
    }*/
}
