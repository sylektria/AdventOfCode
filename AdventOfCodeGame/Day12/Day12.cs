﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeGame
{
    public class Day12
    {
        public Day12()
        {
            TextReader txtreader = new TextReader();

            List<string> allData = new List<string>();

            allData = txtreader.ReadText("Day12\\Day12Input.txt");

            Console.WriteLine("Day 12:");

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