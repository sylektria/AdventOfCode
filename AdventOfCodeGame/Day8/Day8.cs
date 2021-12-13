using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeGame
{
    public class Day8
    {
        public Day8()
        {
            TextReader txtreader = new TextReader();

            List<string> allData = new List<string>();

            allData = txtreader.ReadText("Day8\\Day8Input.txt");

            List<LineOfData> data = new List<LineOfData>();

            foreach (var line in allData)
            {
                List<string> lineparts = line.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                data.Add(new LineOfData(lineparts[0].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList(),
                                        lineparts[1].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList()));

            }

            Console.WriteLine("Day 8:");

            Part1(data);

            Part2(data);

            Console.WriteLine();
        }

        private void Part1(List<LineOfData> data)
        {
            int [] digits = new int [10];

            foreach (var classInstance in data)
            {
                foreach (var part in classInstance.SecondPart)
                {
                    switch (part.Length)
                    {
                        /* Number 1 */
                        case 2:
                            digits[1]++;
                            break;
                        /* Number 4 */
                        case 4:
                            digits[4]++;
                            break;
                        /* Number 7 */
                        case 3:
                            digits[7]++;
                            break;
                        /* Number 8 */
                        case 7:
                            digits[8]++;
                            break;
                        default:
                            break;
                    }
                }
            }

            int counter = 0;

            foreach (var digit in digits)
            {
                counter += digit;
            }
            
            Console.WriteLine("Part 1 Solution: " + counter);
        }


        private void Part2(List<LineOfData> data)
        {
            string [] numberWithLetters = new string [10];
            List<int> allNumbers = new List<int>();

            string number = string.Empty;

            foreach (var classInstance in data)
            {
                numberWithLetters = DetermineAndSetNumbers(classInstance.FirstPart);

                foreach (var part in classInstance.SecondPart)
                {
                    string orderByABC = String.Concat(part.OrderBy(c => c));

                    for (int i = 0; i < 10; i++)
                    {
                        if (orderByABC == numberWithLetters[i])
                        {
                            number += i;
                        }
                    }
                }

                allNumbers.Add(Int32.Parse(number));

                number = string.Empty;
            }

            Console.WriteLine("Part 2 Solution: " + allNumbers.Sum());
        }

        private string[] DetermineAndSetNumbers(List<string> firstPart)
        {
            string [] numberWithLetters = new string[10];

            foreach (var item in firstPart.OrderBy(l=>l.Length))
            {
                string wordsInOrder = String.Concat(item.OrderBy(c => c));
                string[] words = { wordsInOrder };

                switch (item.Length)
                {
                    /* Number 1 */
                    case 2:
                        numberWithLetters[1] = wordsInOrder;
                        break;

                    /* Number 4 */
                    case 4:
                        numberWithLetters[4] = wordsInOrder;
                        break;

                    /* Number 7 */
                    case 3:
                        numberWithLetters[7] = wordsInOrder;
                        break;

                    /* Number 8 */
                    case 7:
                        numberWithLetters[8] = wordsInOrder;
                        break;

                    /* Number 0,6,9 */
                    case 6:                       

                        /* If the word contains the 4 sequence the number is 9 */
                        if (words.Where(w => numberWithLetters[4].All(w.Contains)).Count() != 0)
                        {
                            numberWithLetters[9] = wordsInOrder;
                        }
                        /* The word contains the 1 sequence, then the number is 0 */
                        else if ((words.Where(w => numberWithLetters[1].All(w.Contains))).Count() != 0)
                        {
                            numberWithLetters[0] = wordsInOrder;
                        }
                        /* Only the 6 left */
                        else
                        {
                            numberWithLetters[6] = wordsInOrder;
                        }
                        break;

                    /* Number 2,3,5 */
                    case 5:
                        string diff4_1 = numberWithLetters[4].Replace(numberWithLetters[1][0].ToString(), "").Replace(numberWithLetters[1][1].ToString(), "");

                        /* The word contains the 1 sequence, then the number is 3 */
                        if (words.Where(w => numberWithLetters[1].All(w.Contains)).Count() != 0)
                        {
                            numberWithLetters[3] = wordsInOrder;
                        }
                        /* If the word contains one of the part of the 1 sequence the number is 5 */
                        else if (words.Where(w => diff4_1.All(w.Contains)).Count() != 0)
                        {
                            numberWithLetters[5] = wordsInOrder;
                        }
                        /* Only the 2 left */
                        else
                        {
                            numberWithLetters[2] = wordsInOrder;
                        }
                        break;

                    default:
                        break;
                }
            }

            return numberWithLetters;
        }
    }
}
