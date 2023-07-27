using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astar
{
    internal class GridBuilder
    {
        int dimension;
        int cellCount;
        string[] cells;
        int numObstacles;
        List<int> obstaclesList = new List<int>();
        List<Node> nodes = new List<Node>();

        public GridBuilder(int val) {
            dimension = val;
            cellCount = val * val;
            cells = new string[cellCount];
            numObstacles = (int)Math.Floor(cellCount * .1);
            GenerateObstacles();
        }

        public int Dimension { get { return dimension; } set { dimension = value; } }
        public int CellCount { get { return cellCount; } set { cellCount = value; } }
        public string[] Cells { get { return cells; } set {  cells = value; } }
        public List<int> ObstaclesList { get {  return obstaclesList; } set {  obstaclesList = value; } }
        public List<Node> Nodes { get {  return nodes; } set { nodes = value; } }

        public void GenerateObstacles()
        {
            Random random = new Random();
            int num;
            for(int i = 0; i<numObstacles; i++)
            {
                num = random.Next(0, cellCount);
                if (cells[num] == " x ")
                {
                    i--;
                }
                else
                {
                    cells[num] = " x " ;
                }
            }
        }

        public void EnumerateGridLocations()
        {
            for (int i = 0; i < cellCount; i++)
            {
                if (cells[i] == " x ")
                {
                    continue;
                }
                else
                {
                    cells[i] = i.ToString();
                }
            }
        }

        public void DrawGrid()
        {

            for (int i = 0; i < cells.Length; i++)
            {
                if (i % dimension == 0 && i != 0)
                {
                    Console.WriteLine();
                    for(int j = 0; j < (dimension*6)+1; j++) 
                    {
                        Console.Write("-");
                    }
                    Console.WriteLine();
                }
                Console.Write(String.Format("{0,4}", cells[i]) + " |");
            }
            Console.WriteLine();
        }

        public void IntialDrawDisplay()
        {
            EnumerateGridLocations();
            DrawGrid();
        }

        public void DeEnumerateGridLocations(int start, int end)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                if(cells[i] == " x ")
                {
                    continue;
                }
                if(i == start)
                {
                    cells[i] = "S";
                }
                else if (i == end)
                {
                    cells[i] = "E";
                }
                else
                {
                    cells[i] = "";
                }
            }
        }

        public void EnumeratePathLocations(int start, int end, List<Node> list)
        {
            int[] posList = new int[list.Count];
            for(int i = 0; i < list.Count; i++)
            {
                posList[i] = list[i].Pos;
            }
            for(int i = 0; i < cells.Length;i++)
            {
                if(posList.Contains(i) && i != start && i != end)
                {
                    cells[i] = " > ";
                }
            }
        }

        public void DrawDisplay(int start, int end)
        {
            DeEnumerateGridLocations(start, end);
            DrawGrid();
        }

        public void DrawFinalDisplay(int start, int end, List<Node> pathList)
        {
            DeEnumerateGridLocations(start, end);
            EnumeratePathLocations(start, end, pathList);
            DrawGrid();
            if(pathList.Count < 1)
            {
                Console.WriteLine("No path was found.");
            }
            else
            {
                Console.WriteLine("Path found with " + pathList.Count + " total moves required!");
            }
        }
    }
}
