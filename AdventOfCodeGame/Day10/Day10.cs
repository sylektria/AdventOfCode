using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCodeGame
{
    public class Day10
    {
        public Day10()
        {
            TextReader txtreader = new TextReader();

            List<string> allData = new List<string>();

            allData = txtreader.ReadText("Day10\\Day10Input.txt");

            Console.WriteLine("Day 10:");

            List<Tuple<string, List<char>>> incompleteLines = new List<Tuple<string, List<char>>>();

            Part1(allData, ref incompleteLines);

            Part2(incompleteLines);

            Console.WriteLine();
        }

        private void Part1(List<string> allData, ref List<Tuple<string, List<char>>> incompleteLines)
        {
            
            long errorSum = 0;

            foreach (var item in allData)
            {
                DetermineCharacters(item, ref incompleteLines, ref errorSum);           
            }

            Console.WriteLine("Part 1 Solution: " + errorSum);
        }

        private void DetermineCharacters(string line, ref List<Tuple<string, List<char>>> incompleteLines, ref long errorSum)
        {
            List<char> characters = new List<char>();

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i].Equals('{') || line[i].Equals('<') ||
                    line[i].Equals('[') || line[i].Equals('('))
                {
                    characters.Add(line[i]);
                }
                else
                {
                    switch (line[i])
                    {
                        case '>':
                            if (characters.Last() == '<')
                            {
                                characters.RemoveAt(characters.Count - 1);
                            }
                            else
                            {
                                errorSum += 25137;
                                return;
                            }
                            break;
                        case '}':
                            if (characters.Last() == '{')
                            {
                                characters.RemoveAt(characters.Count - 1);
                            }
                            else
                            {
                                errorSum += 1197;
                                return;
                            }
                            break;
                        case ']':
                            if (characters.Last() == '[')
                            {
                                characters.RemoveAt(characters.Count - 1);
                            }
                            else
                            {
                                errorSum += 57;
                                return;
                            }
                            break;
                        case ')':
                            if (characters.Last() == '(')
                            {
                                characters.RemoveAt(characters.Count - 1);
                            }
                            else
                            {
                                errorSum += 3;
                                return;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            /* There is no return in the method, so it was a match for every character/not enough closing parenthesis */
            incompleteLines.Add(new Tuple<string, List<char>>(line, characters));

        }

        private void Part2(List<Tuple<string, List<char>>> linesAndChars)
        {
            List<long> sum = new List<long>();

            long tmpsum = 0;

            foreach (var line in linesAndChars)
            {
                for (int i = line.Item2.Count() - 1; i >= 0; i--)
                {                    
                    tmpsum *= 5;

                    switch (line.Item2[i])
                    {
                        case '(':
                            tmpsum += 1;
                            break;
                        case '[':
                            tmpsum += 2;
                            break;
                        case '{':
                            tmpsum += 3;
                            break;
                        case '<':
                            tmpsum += 4;
                            break;
                        default:
                            break;
                    }
                }

                sum.Add(tmpsum);
                tmpsum = 0;
            }

            sum.Sort();

            Console.WriteLine("Part 2 Solution: " + sum[sum.Count / 2]);
        }
    }
}
