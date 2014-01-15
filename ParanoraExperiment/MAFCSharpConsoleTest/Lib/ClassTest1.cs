using System;
using System.AddIn;
using System.AddIn.Contract;
using System.AddIn.Hosting;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAFCSharpConsoleTest.Lib
{
    [AddInBase()]
    public abstract class Calculator
    {
        public abstract double Calc(double a, double b);
    }

    [AddIn("Paranora AddIn", Version = "1.0.0.0")]
    public class AddInCalcV1 : Calculator
    {
        public override double Calc(double a, double b)
        {
            return a + b;
        }
    }

    [AddInContract]
    public interface ICalc1Contract : IContract
    {
        double Calc(double a, double b);
    }

    [HostAdapterAttribute()]
    public class CalculatorContractToViewHostSideAdapter : Calculator
    {
        private ICalc1Contract _contract;
        private ContractHandle _handle;

        public CalculatorContractToViewHostSideAdapter(ICalc1Contract contract)
        {
            _contract = contract;
            _handle = new ContractHandle(contract);
        }

        public override double Calc(double a, double b)
        {
            return _contract.Calc(a, b);
        }
    }

    [AddInAdapter()]
    public class CalculatorViewToContractAddInSideAdapter : ContractBase, ICalc1Contract
    {
        private Calculator _view;

        public CalculatorViewToContractAddInSideAdapter(Calculator view)
        {
            _view = view;
        }

        public virtual double Calc(double a, double b)
        {
            return _view.Calc(a, b);
        }
    }

    public class ClassTest1
    {
        public static void Test()
        {
            String addInRoot = Environment.CurrentDirectory;
            AddInStore.Update(addInRoot);

            Collection<AddInToken> tokens = AddInStore.FindAddIns(typeof(Calculator), addInRoot);
            if (tokens.Count == 0)
            {
                Console.WriteLine("No plugins available.");
                return;
            }

            AddInToken calcToken = ChooseCalculator(tokens);

            Calculator calc = calcToken.Activate<Calculator>(AddInSecurityLevel.Internet);

            //Run the add-in.
            RunCalculator(calc);
        }

        private static AddInToken ChooseCalculator(Collection<AddInToken> tokens)
        {
            Console.WriteLine("Available Plugins: ");
            int tokNo = 0;
            foreach (AddInToken tok in tokens)
            {
                Console.WriteLine("\t[{0}]: {1}", tokNo.ToString(), tok.AssemblyName);
                tokNo++;
            }
            Console.WriteLine("Which calculator do you want to use?");
            String line = Console.ReadLine();
            int selection;
            if (Int32.TryParse(line, out selection))
            {
                if (selection < tokens.Count)
                {
                    return tokens[selection];
                }
            }
            return tokens[0];
        }

        private static void RunCalculator(Calculator calc)
        {
            Console.Write("Input two numbers such as 2 3 (Or type <q> to exit). \n%> ");
            String line = Console.ReadLine();
            while (!line.Equals("q"))
            {
                try
                {
                    String[] parts = line.Split(' ');
                    double a = Double.Parse(parts[0]);
                    double b = Double.Parse(parts[1]);
                    Console.Write("{0}\n%> ", calc.Calc(a, b));
                }
                catch
                {
                    Console.Write("Invalid command: {0} \n%> ", line);
                }
                line = Console.ReadLine();
            }
        }
    }
}
