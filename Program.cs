// Написать программу-калькулятор, вычисляющую выражения вида
// a + b, a - b, a / b, a * b, введенные из командной строки,
// и выводящую результат выполнения на экран.

namespace HomeWork_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] arithmeticOperators = { "+", "-", "*", "/" };
            double argument_1 = 0;
            string operationSign = "";
            double argument_2 = 0;

            if (correctNumberOfArguments(args))
            {
                if (isDigit(args[0], out double argument1))
                    argument_1 = argument1;
                else
                {
                    Console.WriteLine($"Ошибка: первый аргумент - \"{args[0]}\" должен быть числом!");
                    Environment.Exit(0);
                }

                if (isArithmeticOperator(args[1], arithmeticOperators))
                    operationSign = args[1];
                else
                {
                    Console.WriteLine($"Ошибка: вы указали знак операции \"{args[1]}\".\n" +
                        $"К сожалению, я умею выполнять только операции: \"+\", \"-\", \"*\", \"/\"");
                    Environment.Exit(0);
                }

                if (isDigit(args[2], out double argument2))
                    argument_2 = argument2;
                else
                {
                    Console.WriteLine($"Ошибка: второй аргумент - \"{args[2]}\" должен быть числом!");
                    Environment.Exit(0);
                }

                try
                {
                    double result = (ArithmeticOperation(argument_1, operationSign, argument_2));
                    Console.WriteLine($"{argument_1} {operationSign} {argument_2} = {result}");
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Неизвестная ошибка: {ex.Message}");
                }
            }
        }

        private static bool correctNumberOfArguments(string[] args)
        {
            if (args.Length == 3)
                return true;
            else
            {
                Console.WriteLine($"Ошибка: количество входных аргументов должно быть равно 3, а не \"{args.Length}\"\n" +
                    "Например: \"2 + 124\".");
                return false;
            }

        }
        private static bool isDigit(string item, out double resultNumber)
        {
            if (double.TryParse(item, out double number))
            {
                resultNumber = number;
                return true;
            }
            else
            {
                resultNumber = 0;
                return false;
            }
        }

        private static bool isArithmeticOperator(string item, string[] arithmeticOperators)
        {
            return arithmeticOperators.Contains(item);
        }

        private static double ArithmeticOperation(double arg1, string operationSign, double arg2)
        {
            switch (operationSign)
            {
                case "+":
                    return arg1 + arg2;
                case "-":
                    return arg1 - arg2;
                case "*":
                    return arg1 * arg2;
                case "/":
                    if (arg2 != 0)
                        return arg1 / arg2;
                    else
                        throw new DivideByZeroException("деление на 0 невозможно!");
                default:
                    throw new InvalidOperationException($"неизвестный арифметический оператор: {operationSign}");
            }
        }

    }
}
