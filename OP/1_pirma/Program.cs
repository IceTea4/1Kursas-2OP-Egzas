using System;

namespace Pasiruosimas_egzui
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyLinkList list = new MyLinkList();
            list.AddToEnding(1);
            list.AddToEnding(2);
            list.AddToEnding(3);
            list.AddToEnding(4);
            list.AddToEnding(5);
            list.AddToEnding(6);
            list.AddToEnding(7);

            //list.Insert_1png(8);
            //bool rez0 = list.RemoveNegative_2png();
            //Console.WriteLine(rez0);
            //bool rez1 = list.RemoveGiven_3png(1);
            //Console.WriteLine(rez1);
            //bool rez2 = list.RemoveZero_4png();
            //Console.WriteLine(rez2);
            /*
            MyLinkList one = new MyLinkList();
            MyLinkList two = new MyLinkList();
            one.AddToEnding(2);
            one.AddToEnding(3);
            one.AddToEnding(4);
            one.AddToEnding(8);
            two.AddToEnding(1);
            two.AddToEnding(5);
            two.AddToEnding(6);
            two.AddToEnding(7);
            MyLinkList rez3 = Join_5png(one, two);
            rez3.Print();*/
            list.Print();
        }

        public static MyLinkList Join_5png(MyLinkList one, MyLinkList two)
        {
            MyLinkList list = new MyLinkList();

            one.Begin();
            two.Begin();

            while (one.Exist() || two.Exist())
            {
                int min;

                if (!one.Exist())
                {
                    min = two.Get();
                    two.Next();
                }
                else if (!two.Exist())
                {
                    min = one.Get();
                    one.Next();
                }
                else
                {
                    if (one.Get() > two.Get())
                    {
                        min = two.Get();
                        two.Next();
                    }
                    else
                    {
                        min = one.Get();
                        one.Next();
                    }
                }

                list.AddToEnding(min);
            }

            return list;
        }
    }

    public sealed class MyLinkList
    {
        private sealed class Node
        {
            public int Data { get; set; }
            public Node Next { get; set; }

            public Node(int data, Node next)
            {
                Data = data;
                Next = next;
            }
        }

        private Node begin;
        private Node current;
        private Node end;

        public MyLinkList()
        {
            begin = current = end = null;
        }

        public void AddToEnding(int data)
        {
            Node node = new Node(data, null);

            if (begin != null)
            {
                end.Next = node;
                end = node;
            }
            else
            {
                begin = node;
                end = node;
            }
        }

        public void Print()
        {
            for (Node d = begin; d != null; d = d.Next)
            {
                Console.WriteLine(d.Data);
            }
        }

        public void Begin()
        {
            current = begin;
        }

        public void Next()
        {
            current = current.Next;
        }

        public bool Exist()
        {
            return current != null;
        }

        public int Get()
        {
            return current.Data;
        }

        public void Insert_1png(int value)
        {
            if (begin == null)
            {
                begin = new Node(value, null);
                return;
            }

            Node prev = null;

            for (Node d = begin; d != null; d = d.Next)
            {
                if (prev == null && value < d.Data)
                {
                    begin = new Node(value, begin);
                    return;
                }
                else if (value < d.Data)
                {
                    prev.Next = new Node(value, d);
                    return;
                }

                prev = d;
            }

            prev.Next = new Node(value, null);
        }

        public bool RemoveNegative_2png()
        {
            bool removed = false;

            if (begin == null)
            {
                return removed;
            }

            Node prev = null;

            for (Node node = begin; node != null; node = node.Next)
            {
                if (node.Data < 0)
                {
                    if (prev == null)
                    {
                        begin = begin.Next;
                    }
                    else
                    {
                        prev.Next = node.Next;
                    }

                    removed = true;
                }
                else
                {
                    prev = node;
                }
            }

            return removed;
        }

        public bool RemoveGiven_3png(int data)
        {
            bool removed = false;

            if (begin == null)
            {
                return removed;
            }

            Node prev = null;

            for (Node node = begin; node != null; node = node.Next)
            {
                if (data == node.Data)
                {
                    if (prev == null)
                    {
                        begin = begin.Next;
                    }
                    else
                    {
                        prev.Next = node.Next;
                    }

                    removed = true;
                }
                else
                {
                    prev = node;
                }
            }

            return removed;
        }

        public bool RemoveZero_4png()
        {
            bool removed = false;

            if (begin == null)
            {
                return removed;
            }

            Node prev = null;

            for (Node node = begin; node != null; node = node.Next)
            {
                if (node.Data == 0)
                {
                    if (prev == null)
                    {
                        begin = begin.Next;
                    }
                    else
                    {
                        prev.Next = node.Next;
                    }

                    removed = true;
                }
                else
                {
                    prev = node;
                }
            }

            return removed;
        }
    }
}
