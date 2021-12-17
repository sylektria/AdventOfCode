// C# program to print all
// paths from a source to
// destination.
using System;
using System.Collections.Generic;
using System.Linq;

// A directed graph using
// adjacency list representation
public class Graph
{

    /* No. of vertices in graph */
    private int NbOfVertices;

    /* adjacency list */
    private List<int>[] adjList;

    private Dictionary<string, int> GraphPoints;

    public List<List<int>> PossiblePaths { get; set; }

    private List<int> smallCaveIdxs;

    public int NbOfUniquePath = 0;


    public Graph(int vertices, Dictionary<string, int> graphPoints, List<int> smallcaveidx)
    {

        /* initialise vertex count */
        NbOfVertices = vertices;

        GraphPoints = graphPoints;

        PossiblePaths = new List<List<int>>();

        smallCaveIdxs = smallcaveidx;

        /* initialise adjacency list */
        InitAdjList();
    }

    /* utility method to initialise adjacency list */
    private void InitAdjList()
    {
        adjList = new List<int>[NbOfVertices];

        for (int i = 0; i < NbOfVertices; i++)
        {
            adjList[i] = new List<int>();
        }
    }

    /// <summary>
    /// add edge from u to v
    /// </summary>
    /// <param name="u"></param>
    /// <param name="v"></param>
    public void AddEdge(int u, int v)
    {
        /* Add v to u's list. */
        adjList[u].Add(v);
    }

    /// <summary>
    /// Prints all paths from 's' to 'd'
    /// </summary>
    /// <param name="s"></param>
    /// <param name="d"></param>
    public void PrintAllPaths(int s, int d, bool part1Call)
    {
        bool[] isVisited = new bool[NbOfVertices];
        List<int> pathList = new List<int>();

        /* add source to path[]  */
        pathList.Add(s);

        /* Call recursive utility */
        PrintAllPathsUtil(s, d, isVisited, pathList, part1Call);
    }

    // isVisited[] keeps track of
    // vertices in current path.
    // localPathList<> stores actual
    // vertices in the current path
    /// <summary>
    /// A recursive function to print all paths from 'u' to 'd'.
    /// </summary>
    /// <param name="u"></param>
    /// <param name="d"></param>
    /// <param name="isVisited"></param>
    /// <param name="localPathList"></param>
    private void PrintAllPathsUtil(int u, int d,
                                bool[] isVisited,
                                List<int> localPathList, bool part1Call)
    {

        if (u.Equals(d))
        {
            //Console.WriteLine(string.Join(" ", localPathList));
            /* if match found then no need to traverse more till depth */

            PossiblePaths.Add(localPathList);

            NbOfUniquePath++;

            return;
        }

        /* Check if the current node is uppercase or not */
        foreach (var pair in GraphPoints)
        {
            if (pair.Value == u)
            {
                if (IsAllUpper(pair.Key))
                {
                    /* Mark the current node as false. If the node is Upper Case */
                    isVisited[u] = false;
                }
                else if (pair.Key.Equals("start") || pair.Key.Equals("end"))
                {
                    isVisited[u] = true;
                }
                else
                {
                    isVisited[u] = true;
                }
            }
        }


        /* Recur for all the vertices adjacent to current vertex */
        foreach (int i in adjList[u])
        {
            if (!isVisited[i])
            {
                /* store current node in path[] */
                localPathList.Add(i);
                PrintAllPathsUtil(i, d, isVisited,
                                localPathList, part1Call);

                /* remove current node in path[] */
                localPathList.Remove(i);
            }
        }

        /* Mark the current node */
        isVisited[u] = false;
    }

    /// <summary>
    /// Check if the string is all upper or not
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    bool IsAllUpper(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            if (!Char.IsUpper(input[i]))
                return false;
        }

        return true;
    }
}

