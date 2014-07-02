using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MinWaitingOrder
{
    class Program
    {
        private static List<Person> inputPersons = new List<Person>()
            {
                new Person() { FullWaterTime = 3},
                new Person() { FullWaterTime = 10},
                new Person() { FullWaterTime = 5},
                new Person() { FullWaterTime = 7},
                new Person() { FullWaterTime = 6},
                new Person() { FullWaterTime = 4}
            };

        private static List<Person> bestOrderPersons = new List<Person>();

        static void Main(string[] args)
        {
            foreach (var inputPerson in inputPersons)
            {
                InsertPersonIntoBestOrder(inputPerson);
            }

            StringBuilder sb = new StringBuilder();
            foreach (var person in bestOrderPersons)
            {
                sb.AppendLine(string.Format("full water time is {0}", person.FullWaterTime));
            }

            Console.WriteLine(sb.ToString());
            Console.WriteLine(GetTotalWaitingTime(bestOrderPersons));
            Console.Read();
        }

        private static void InsertPersonIntoBestOrder(Person inputPerson)
        {
            int minIndex = 0;
            int minWaitingTime = int.MaxValue;
            List<Person> minWaitingOrder = new List<Person>();
            minWaitingOrder.AddRange(bestOrderPersons);

            for (int index = 0; index <= minWaitingOrder.Count; index++)
            {
                minWaitingOrder.Insert(index, inputPerson);
                var currentTotalWaitingTime = GetTotalWaitingTime(minWaitingOrder);

                if (currentTotalWaitingTime < minWaitingTime)
                {
                    minWaitingTime = currentTotalWaitingTime;
                    minIndex = index;
                }

                minWaitingOrder.Remove(inputPerson);
            }

            bestOrderPersons.Insert(minIndex, inputPerson);    
        }

        private static int GetTotalWaitingTime(List<Person> minWaitingOrder)
        {
            int totalWaitingTime = 0;

            for (int position = 0; position < minWaitingOrder.Count; position++)
            {
                totalWaitingTime += minWaitingOrder[position].FullWaterTime * (minWaitingOrder.Count - position);
            }

            return totalWaitingTime;
        }
    }

    public class Person
    {
        public int FullWaterTime { get; set; }
    }
}
