using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeGame
{
    public class Day14
    {
        public Day14()
        {
            TextReader txtreader = new TextReader();

            List<string> allData = new List<string>();

            allData = txtreader.ReadText("Day14\\Day14Input.txt");

            string firstPolymer = allData[0];

            List<Tuple<string, string>> polymers = new List<Tuple<string, string>>();

            foreach (string line in allData.Skip(2))
            {
                List<string> tmpLine = line.Split(new string[] { " -> " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                polymers.Add(new Tuple<string, string>(tmpLine[0], tmpLine[1]));
            }

            Console.WriteLine("Day 14:");

            Part1(firstPolymer, polymers);

            Part2(firstPolymer, polymers);

            Console.WriteLine();
        }

        /// <summary>
        /// Brute force solution
        /// </summary>
        /// <param name="firstPolymer"></param>
        /// <param name="polymers"></param>
        private void Part1(string firstPolymer, List<Tuple<string, string>> polymers)
        {
            string polymerTemplate = firstPolymer;
            string newString = string.Empty;

            int cntr = 0;
            do
            {
                for (int i = 0; i < polymerTemplate.Length - 1; i++)
                {
                    foreach (var item in polymers)
                    {
                        if (item.Item1 == polymerTemplate.Substring(i, 2))
                        {
                            int length = newString.Length;
                            if (newString != string.Empty)
                            {
                                newString = newString.Remove(length - 1, 1) + polymerTemplate[i] + item.Item2 + polymerTemplate[i + 1];
                            }
                            else
                            {
                                newString = polymerTemplate[i] + item.Item2 + polymerTemplate[i + 1];
                            }
                        }
                    }
                }

                polymerTemplate = newString;
                newString = string.Empty;
                cntr++;

            } while (cntr != 10);

            var charOccurrence = polymerTemplate.GroupBy(c => c).Select(c => new { Char = c.Key, Count = c.Count() }).OrderByDescending(x => x.Count);

            int result = charOccurrence.First().Count - charOccurrence.Last().Count;

            Console.WriteLine("Part 1 Solution: " + result);
        }

        private void Part2(string firstPolymer, List<Tuple<string, string>> polymers)
        {
            string polymerTemplate = firstPolymer;
            string newString = string.Empty;

            Dictionary<string, long> polymerPairs = new Dictionary<string, long>();
            Dictionary<char, long> elementCount = new Dictionary<char, long>();

            /* Go through the first polymer (first row in the input) */
            for (int i = 0; i < firstPolymer.Length - 1; i++)
            {
                foreach (var item in polymers)
                {
                    string firstPart = item.Item1.Substring(0, 1) + item.Item2;
                    string secondPart = item.Item2 + item.Item1.Substring(1, 1);

                    /* Store each "letter" polymer into an element dictonary. */
                    if (!elementCount.ContainsKey(char.Parse(item.Item1.Substring(0, 1))))
                    {
                        elementCount[char.Parse(item.Item1.Substring(0, 1))] = 0;
                    }
                    if (!elementCount.ContainsKey(char.Parse(item.Item1.Substring(1, 1))))
                    {
                        elementCount[char.Parse(item.Item1.Substring(1, 1))] = 0;
                    }

                    /* Add the polymer pair into a dictionary. for example.:
                     * NN will result -> NC CN 
                     * Pair storage works better then whole string looping */
                    if (item.Item1 == polymerTemplate.Substring(i, 2))
                    {
                        if (polymerPairs.ContainsKey(firstPart))
                        {
                            polymerPairs[firstPart]++;
                        }
                        else
                        {
                            polymerPairs.Add(firstPart, 1);
                        }

                        if (polymerPairs.ContainsKey(secondPart))
                        {
                            polymerPairs[secondPart]++;
                        }
                        else
                        {
                            polymerPairs.Add(secondPart, 1);
                        }

                        elementCount[char.Parse(item.Item2)]++;
                    }
                }

                /* Add the current 'letter' into the elementcount dictionary */
                elementCount[char.Parse(firstPolymer.Substring(i, 1))]++;
            }

            /* add last character from the starting list to the character dictonary */
            elementCount[char.Parse(firstPolymer.Substring(firstPolymer.Length - 1, 1))]++;

            int cntr = 1;
            do
            {
                Dictionary<string, long> tmpPolymerPairs = new Dictionary<string, long>();

                foreach (var dictionaryItem in polymerPairs)
                {
                    foreach (var item in polymers)
                    {
                        /* if current dictionary elements match the polymer table element */
                        if (dictionaryItem.Key == item.Item1)
                        {
                            string firstPart = item.Item1.Substring(0, 1) + item.Item2;
                            string secondPart = item.Item2 + item.Item1.Substring(1, 1);

                            elementCount[char.Parse(item.Item2)] += dictionaryItem.Value;

                            /* add new polymer into a dictionary, or increase the existing dictonary value by the
                             * occurrence of the pair */
                            for (int i = 0; i < 1; i++)
                            {
                                if (tmpPolymerPairs.ContainsKey(firstPart))
                                {
                                    tmpPolymerPairs[firstPart]++;
                                }
                                else
                                {
                                    tmpPolymerPairs.Add(firstPart, 1);
                                }

                                if (tmpPolymerPairs.ContainsKey(secondPart))
                                {
                                    tmpPolymerPairs[secondPart]++;
                                }
                                else
                                {
                                    tmpPolymerPairs.Add(secondPart, 1);
                                }
                            }

                            tmpPolymerPairs[firstPart] += dictionaryItem.Value - 1;
                            tmpPolymerPairs[secondPart] += dictionaryItem.Value - 1;
                        }
                    }
                }

                polymerPairs = tmpPolymerPairs;
                cntr++;

            } while (cntr != 40);

            var firstOcc = elementCount.OrderByDescending(x => x.Value).First().Value;
            var lastOcc = elementCount.OrderByDescending(x => x.Value).Last().Value;
            
            Console.WriteLine("Part 2 Solution: " + (firstOcc - lastOcc));
        }
    }
}
