using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace ConsoleApplication1
{
    class Weapon
    {
        static List<Bullet> stock = new List<Bullet>();
        static Bullet tmpB, tmpB2;
        static int max;
        int cont;
        Stack<Bullet> holder;
        public Weapon()
        {
            max = 7;
            cont = 0;
            holder = new Stack<Bullet>();
            tmpB = new Bullet();
        }
        static public void PrintStock()
        {
            int CntStkc = GetCountStock();
            for (int i = 0; i < CntStkc; i++)
            {
                Console.Write(i + 1 + "   ");
            }
            Console.WriteLine();
            for (int i = 0; i < CntStkc; i++)
            {
                Console.Write(stock[i].GetPower() + "   ");
            }
        }
        static public void AddBullet(int p)
        {
            tmpB = new Bullet(p);
            stock.Add(tmpB);
        }
        static public void ConcatBullet(int t1, int t2)
        {
            tmpB = stock.ElementAt(t1-1);
            tmpB2 = stock.ElementAt(t2-1);
            tmpB += tmpB2;
            stock.RemoveAt(t1-1);
            stock.Insert(t1-1, tmpB);
            stock.RemoveAt(t2-1);
        }
        static public int GetCountStock()
        {
            return stock.Count();
        }
        public static Weapon operator ++(Weapon op)
        {
            if (op.cont < Weapon.max)
            {
                if (Weapon.stock.Count() > 0)
                {
                    op.holder.Push(Weapon.stock.ElementAt<Bullet>(0));
                    Weapon.stock.RemoveAt(0);
                    op.cont++;
                }
                else
                {
                    Console.WriteLine("Склад пуст");
                }
            }
            else
            {
                Console.WriteLine("Обойма переполнена");
            }
            return op;
        }
        public static Weapon operator --(Weapon op)
        {
            if (op)
            {
                tmpB.SetPower((op.holder.Pop()).GetPower());
                Console.WriteLine("Выстрел мощостью " + tmpB.GetPower() + " dem");
                op.cont--;
            }
            else
            {
                Console.WriteLine("Обойма пуста");
            }
            return op;
        }
        public static Weapon operator +(Weapon op1, int op2)
        {
            for (int i = 0; i < op2; i++)
            {
                op1++;
            }
            return op1;
        }
        public static Weapon operator -(Weapon op1, int op2)
        {
            for (int i = 0; i < op2; i++)
            {
                op1--;
            }
            return op1;
        }
        public static bool operator true(Weapon op)
        {
            if (op.cont > 0)
                return true;
            else
                return false;
        }
        public static bool operator false(Weapon op)
        {
            if (op.cont < 1)
                return true;
            else
                return false;
        }
    }

    class Bullet
    {
        int power;
        public Bullet()
        {
            power = 1;
        }
        public Bullet(int p)
        {
            power = p;
        }
        public void SetPower(int p)
        {
            power = p;
        }
        public int GetPower()
        {
            return power;
        }
        public static Bullet operator +(Bullet op1, Bullet op2)
        {
            op1.SetPower(op1.GetPower() + op2.GetPower());
            return op1;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo key_info;
            Weapon gun = new Weapon();
            int t1, t2;
            int p;
            Bullet tmpB = new Bullet();
            string tmp;
            Regex rxNums = new Regex(@"^\d+$");
            Console.WriteLine("Нажмите s для добавления пули по-умолчанию\n\tp для добавления пули с параметром\n\th для выстрела\n\tq для заряда\n\tk для сложения пуль\n\tw для заряда нескольких\n\ti для выстрела нескольких");
            do
            {
                key_info = Console.ReadKey(true);

                if (key_info.Key != ConsoleKey.S && key_info.Key != ConsoleKey.P && key_info.Key != ConsoleKey.Q && key_info.Key != ConsoleKey.H && key_info.Key != ConsoleKey.Escape && key_info.Key != ConsoleKey.F && key_info.Key != ConsoleKey.K && key_info.Key != ConsoleKey.W && key_info.Key != ConsoleKey.I)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                }
                if (key_info.Key == ConsoleKey.S)
                {
                    //tmpB.SetPower(1);
                    Weapon.AddBullet(1);
                }
                if (key_info.Key == ConsoleKey.P)
                {
                    do
                    {
                        Console.WriteLine("Введите мощьность от 1 до 5:\t");
                        tmp = Console.ReadLine();
                        if (rxNums.IsMatch(tmp))
                        {
                            p = int.Parse(tmp);
                            if(p > 0 && p < 6){
                                //tmpB.SetPower(p);
                                Weapon.AddBullet(p);
                                break;
                            }
                        }
                        else
                        {
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.WriteLine("                               ");
                            Console.SetCursorPosition(0, Console.CursorTop - 2);
                        }
                    } while (true);
                }
                if (key_info.Key == ConsoleKey.H)
                {
                    gun--;
                }
                if (key_info.Key == ConsoleKey.Q)
                {
                    gun++;
                }
                if (key_info.Key == ConsoleKey.K)
                {
                    Weapon.PrintStock();
                    Console.WriteLine("Выбирете два номера");
                    #region do
                    do
                    {
                        tmp = Console.ReadLine();
                        if (rxNums.IsMatch(tmp))
                        {
                            t1 = int.Parse(tmp);
                            if (t1 > 0 && t1 <= Weapon.GetCountStock())
                            {
                                do
                                {
                                    tmp = Console.ReadLine();
                                    if (rxNums.IsMatch(tmp))
                                    {
                                        t2 = int.Parse(tmp);
                                        if (t2 > 0 && t2 <= Weapon.GetCountStock() && t2 != t1)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                                            Console.WriteLine("                               ");
                                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                                        }
                                    }
                                    else
                                    {
                                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                                        Console.WriteLine("                               ");
                                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                                    }
                                } while (true);
                                break;
                            }
                            else
                            {
                                Console.SetCursorPosition(0, Console.CursorTop - 1);
                                Console.WriteLine("                               ");
                                Console.SetCursorPosition(0, Console.CursorTop - 1);
                            }
                        }
                        else
                        {
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.WriteLine("                               ");
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                        }
                    } while (true);
                    #endregion
                    Weapon.ConcatBullet(t1, t2);
                }
                if (key_info.Key == ConsoleKey.W)
                {
                    Console.WriteLine("Введите кол-во зарядов:\t");
                    tmp = Console.ReadLine();
                    if (rxNums.IsMatch(tmp))
                    {
                        p = int.Parse(tmp);
                        gun += p;
                    }
                }
                if (key_info.Key == ConsoleKey.I)
                {
                    do
                    {
                        Console.WriteLine("Введите кол-во выстрелов от 1 до 7:\t");
                        tmp = Console.ReadLine();
                        if (rxNums.IsMatch(tmp))
                        {
                            p = int.Parse(tmp);
                            if (p > 0 && p < 8)
                            {
                                gun -= p;
                                break;
                            }
                        }
                        else
                        {
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.WriteLine("                               ");
                            Console.SetCursorPosition(0, Console.CursorTop - 2);
                        }
                    } while (true);
                }
                Console.WriteLine("Дальше");
            } while (key_info.Key != ConsoleKey.Escape);
            
        }
    }
}