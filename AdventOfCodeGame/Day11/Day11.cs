﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeGame
{
    public class Day11
    {
        public Day11()
        {
            TextReader txtreader = new TextReader();

            List<string> allData = new List<string>();

            allData = txtreader.ReadText("Day11\\Day11Input.txt");

            Console.WriteLine("Day 11:");

            Part1(allData);

            Part2(allData);

            Console.WriteLine();
        }

        private void Part1(List<string> allData)
        {

        }

        private void Part2(List<string> allData)
        {

        }
    }
}