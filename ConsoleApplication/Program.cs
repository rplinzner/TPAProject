using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Logger;
using BusinessLogic.Logger.Enum;
using BusinessLogic.Logger.Interface;
using BusinessLogic.ViewModel;
using BusinessLogic.ViewModel.TreeViewItems;
using ConsoleApplication.CmdHelper;
using ConsoleApplication.Converters;
using ConsoleApplication.View;

namespace ConsoleApplication
{
    class Program
    {
        public static MainWindowVM ViewModel { get; set; } = new MainWindowVM()
        {
            PathFinder = new CmdPathFinder(),
            LogFactory = new BaseLogFactory(new List<ILogger>
            {
                new FileLogger("CMDlog.txt")
            },LogOutputLevelEnum.Debug)
        };

        public static TreeViewCmd CmdView { get; set; }

        static void Main(string[] args)
        {
            Menu(string.Empty);
        }

        private static void Menu (string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Type 'o' or 'O' to open file. Type 'e' or 'E' to exit.");
            string choise = Console.ReadLine();
            switch (choise)
            {
                case "o":
                case "O":
                    {
                        Console.Clear();
                        Console.WriteLine("Type the path of file you want to open and press enter.");
                        ViewModel.HierarchicalAreas = new ObservableCollection<TreeViewItem>();
                        ViewModel.ClickOpen.Execute(null);
                        if (ViewModel.PathVariable == null)
                            Menu("Wrong path!\n");
                        else
                        {
                            CmdView = new TreeViewCmd(new ObservableCollection<TreeViewItemCmd>(ViewModel.HierarchicalAreas.Select(n => new TreeViewItemCmd(n, 0))));
                            TreeView("");
                        }
                        break;
                    }
                case "e":
                case "E":
                    {
                        Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        Menu("Wrong option! Choose: 'e' / 'E' / 'o' / 'O'.\n");
                        break;
                    }
            }
        }

        private static void TreeView(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Path: " + ViewModel.PathVariable);
            Print();
            Console.WriteLine("Type number of item that you want to expand or shrink");
            Console.WriteLine("Type 'b' or 'B' to go back to the menu. Type 'e' or 'E' to exit.");
            string input = Console.ReadLine();
            switch (input)
            {
                case "b":
                case "B":
                    {
                        Menu("");
                        break;
                    }

                case "e":
                case "E":
                    {
                        Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        int parsedInput;
                        if (!Int32.TryParse(input, out parsedInput) || parsedInput < 0 || parsedInput > CmdView.Data.Count - 1)
                        {
                            TreeView("Incorrect format, please try again\n");
                            return;
                        }
                        Expand(parsedInput);
                        TreeView("");
                        break;
                    }
            }
        }

        public static void Expand(int index)
        {
            CmdView.Expand(index);
            Console.Clear();
            Print();
        }
        public static void Print()
        {
            int index = 0;
            foreach (TreeViewItemCmd itemConsole in CmdView.Data)
            {
                string[] value = new string[4];
                value[0] = "[" + index + "]: ";
                //aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                value[1] = itemConsole.IsExpanded ? "[-] " : "[+] ";
                value[2] = (string)TreeViewItemToStringConverter.Instance.Convert(itemConsole.TreeItem,null,null,null);
                value[3] = itemConsole.TreeItem.Name;
                //aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                ConsoleColor color = (ConsoleColor)TreeViewItemToConsoleColorConverter.Instance.Convert(itemConsole.TreeItem, null, null, null);
                PrintWithTabAndColor(value, itemConsole.Id, color);
                index++;
            }
        }

        private static void PrintWithTabAndColor(string[] value, int indent, ConsoleColor color)
        {
            Console.Write(new string(' ', indent * 3));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(value[0]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(value[1]);
            Console.Write(value[2]);
            Console.ForegroundColor = color;
            Console.WriteLine(value[3]);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
