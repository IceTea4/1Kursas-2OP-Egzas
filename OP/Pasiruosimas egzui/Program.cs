//moketi: padaryt linklist klase; node klase (duomenu tipai)
// add to list i prieki ir i gala (lifo/fifo)
// spausdint moket
// rusiavimas bubuliuku arba isrinkimu
// elementu paieska sarase, iterpimas, salinimas, sukeitimas vietomis
// parasyti komentarus metodui ir ju tipams
// moketi ideti fiktyvius elementus viena arba du

using System;

namespace Pasiruosimas_egzui
{
    //pirma uzduotis
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

            list.SplitAndSwitch();
            list.Print();
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

        //fiktyvus:
        //private Node prev;

        public MyLinkList()
        {
            //fiktyvus:
            /*
            end = new Node(int.MaxValue, null);
            begin = new Node(int.MaxValue, end);
            prev = begin;
            */

            begin = current = end = null;
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

        public void AddToBegining(int data)
        {
            begin = new Node(data, begin);
        }

        public void AddToEnding(int data)
        {
            //fiktyvus:
            /*
            Node node = new Node(data, end);

            prev.Next = node;
            prev = node;
            */


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

        public void SortSelection()
        {
            for (Node node = begin; node != null; node = node.Next)
            {
                Node max = node;

                for (Node node2 = node.Next; node2 != null; node2 = node2.Next)
                {
                    if (max.Data.CompareTo(node2.Data) > 0)
                    {
                        max = node2;
                    }
                }

                int item = node.Data;
                node.Data = max.Data;
                max.Data = item;
            }
        }

        public void SortBubble()
        {
            bool flag = true;

            while (flag)
            {
                flag = false;

                for (Node d = begin; d.Next != null; d = d.Next)
                {
                    int one = d.Data;
                    int two = d.Next.Data;

                    if (one.CompareTo(two) > 0)
                    {
                        d.Data = two;
                        d.Next.Data = one;

                        flag = true;
                    }
                }
            }
        }

        private int Count()
        {
            int count = 0;

            for (Node d = begin; d != null; d = d.Next)
            {
                count++;
            }

            return count;
        }

        public void SplitAndSwitch()
        {
            if (begin == null || begin.Next == null)
            {
                return;
            }

            int count = Count();

            //kitas variantas:
            /*
            Node beforeSlow = null;
            Node slow = begin;
            Node fast = begin;

            while (fast != null && fast.Next != null)
            {
                beforeSlow = slow;
                slow = slow.Next;
                fast = fast.Next.Next;
            }
            */

            Node beforeMid = null;
            Node mid = begin;

            for (int i = 0; i < count / 2; i++)
            {
                beforeMid = mid;
                mid = mid.Next;
            }

            Node firstHalf = begin;
            Node secondHalf = mid;

            begin = secondHalf;
            end.Next = firstHalf;
            end = beforeMid;
            beforeMid.Next = null;
        }

        public void Print()
        {
            for (Node d = begin; d != null; d = d.Next)
            {
                Console.WriteLine(d.Data);
            }
        }
    }
}
