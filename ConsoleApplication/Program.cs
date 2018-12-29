using System;
using System.Collections.ObjectModel;
using System.Linq;
using ConsoleApplication.Converters;
using ConsoleApplication.View;
using ViewModel.Windows;
using ViewModel.TreeViewItems;

namespace ConsoleApplication
{
    class Program
    {
        public static MainWindowVM ViewModel { get; set; } = new MainWindowVM();

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
            Console.WriteLine("Type 'o' or 'O' to open file. Type 's' or 'S' to save. Type 'e' or 'E' to exit.");
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
                case "s":
                case "S":
                    {
                        Console.Clear();
                        Console.WriteLine("Type the path where you want to save file and press enter.");
                        ViewModel.ClickSave.Execute(null);
                        if (ViewModel.PathForSerialization == null)
                            Menu("Wrong path!\n");
                        else
                        {
                            Menu("Serialization success!\n");
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
            Console.WriteLine("Type 'b' or 'B' to go back to the menu. Type 's' or 'S' to save. Type 'e' or 'E' to exit.");
            string input = Console.ReadLine();
            switch (input)
            {
                case "b":
                case "B":
                    {
                        Menu("");
                        break;
                    }
                case "s":
                case "S":
                    {
                        Console.Clear();
                        Console.WriteLine("Type the path where you want to save file and press enter.");
                        ViewModel.ClickSave.Execute(null);
                        if (ViewModel.PathForSerialization == null)
                            Menu("Wrong path!\n");
                        else
                        {
                            Menu("Serialization success!\n");
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
                value[1] = itemConsole.IsExpanded ? "[-] " : "[+] ";
                value[2] = (string)TreeViewItemToStringConverter.Instance.Convert(itemConsole.TreeItem,null,null,null);
                value[3] = itemConsole.TreeItem.Name;
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