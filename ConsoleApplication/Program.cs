using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            PathFinder = new CmdPathFinder()
        };

        public static TreeViewCmd CmdView { get; set; }

        static void Main(string[] args)
        {
            Menu(string.Empty);
        }

        private static void Menu (string message)
        {
            Console.Clear();
            Console.Write(message);
            Console.WriteLine("e - exit, o - open");
            string choose = Console.ReadLine();
            switch (choose)
            {
                case "O":
                case "o":
                case "Open":
                    {
                        Console.Clear();
                        Console.WriteLine("Type absolute Path of file you want to open");
                        ViewModel.HierarchicalAreas = new ObservableCollection<TreeViewItem>();
                        ViewModel.ClickOpen.Execute(null);
                        if (ViewModel.PathVariable == null)
                            Menu("Wrong Path\n");
                        else
                        {
                            CmdView = new TreeViewCmd(new ObservableCollection<TreeViewItemCmd>(ViewModel.HierarchicalAreas.Select(n => new TreeViewItemCmd(n, 0))));
                            TreeView(String.Empty);
                        }
                        break;
                    }
                case "E":
                case "e":
                case "Exit":
                    {
                        Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        Menu("Wrong Option!\n");
                        break;
                    }
            }
        }

        private static void TreeView(string message)
        {
            Console.Clear();
            Console.Write(message);
            Console.WriteLine("Path:" + ViewModel.PathVariable);
            Print();
            Console.WriteLine("Type id that you want to expand/shrink");
            Console.WriteLine("Type 'Go back', 'b', 'B' to go back to Menu");
            string temp = Console.ReadLine();
            switch (temp)
            {
                case "Go back":
                case "B":
                case "b":
                    {
                        Menu(String.Empty);
                        break;
                    }
                default:
                    {
                        int parsedTemp;
                        if (!Int32.TryParse(temp, out parsedTemp) || parsedTemp < 0 || parsedTemp > CmdView.Data.Count - 1)
                        {
                            TreeView("Incorrect format, try again\n");
                            return;
                        }
                        Expand(parsedTemp);
                        TreeView(String.Empty);
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
                value[0] = "id:" + index;
                //aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                value[1] = (string)TreeViewItemToStringConverter.Instance.Convert(itemConsole.TreeItem,null,null,null);
                value[2] = itemConsole.IsExpanded ? "[-] " : "[+] ";
                value[3] = itemConsole.TreeItem.Name;
                //aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                ConsoleColor color = (ConsoleColor)TreeViewItemToConsoleColorConverter.Instance.Convert(itemConsole.TreeItem, null, null, null);
                PrintWithIndent(value, itemConsole.Id, color);
                index++;
            }
        }

        private static void PrintWithIndent(string[] value, int indent, ConsoleColor color)
        {
            Console.Write(new string(' ', indent * 3));
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(value[0]);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(value[1]);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(value[2]);
            Console.ResetColor();
            Console.ForegroundColor = color;
            Console.WriteLine(value[3]);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
