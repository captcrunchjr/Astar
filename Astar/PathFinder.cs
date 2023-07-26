﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Astar
{
    internal class PathFinder
    {
        int dimension;
        int start;
        int end;

        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();
        List<Node> pathList = new List<Node>();

        public PathFinder(int start, int end, int dimension) {
            this.start = start;
            this.end = end;
            this.dimension = dimension;
            openList.Add(NodeBuilder(start, dimension));
        }

        public Node NodeBuilder(int pos, int dimension)
        {
            Node node = new Node(pos, dimension, CalculateHCost(pos, end), CalculateGCost(pos, start));
            return node;
        }
        public int CalculateHCost(int currentPos, int end)
        {
            return CalculateHorizontalPathCost(currentPos, end) + CalculateVerticalPathCost(currentPos, end);
        }

        public int CalculateGCost(int currentPos, int start)
        {
            return CalculateHorizontalPathCost(currentPos, start) + CalculateVerticalPathCost(currentPos, start);
        }

        public int CalculateHorizontalPathCost(int currentPos, int destination)
        {
            int horizontalMoveCount = ((destination % dimension) - (currentPos % dimension)) * 10;
            if(horizontalMoveCount < 0)
            {
                horizontalMoveCount *= -1;
            }
            Console.WriteLine("The hoizontal moves needed is: " + horizontalMoveCount);

            return horizontalMoveCount;
        }

        public int CalculateVerticalPathCost(int currentPos, int destination)
        {
            int verticalMoveCount = ((destination / dimension) - (currentPos / dimension)) * 10;
            if(verticalMoveCount < 0)
            {
                verticalMoveCount *= -1;
            }
            Console.WriteLine("The vertical moves needed is: " + verticalMoveCount);
            return verticalMoveCount;
        }

        public void GenerateOpenList(Node node, string[] cells)
        {
            //if it's an obstacle, generate the node and add it to the closed list
            if (cells[node.Pos] == " x ")
            {
                closedList.Add(GenerateObstacleNode(node.Pos));
                return;
            }

            //validate this isnt the left bound, if not, generate left node and add to open list
            if (!node.Bounds.Contains("left"))
            {
                Node newNode = NodeBuilder(node.Pos-1, dimension);
                if (!closedList.Contains(newNode))
                {
                    openList.Add(newNode);
                }
            }
            //validate this isnt the right bound, if not, generate right node and add to open list
            if (!node.Bounds.Contains("right"))
            {
                Node newNode = NodeBuilder(node.Pos+1, dimension);
                if(!closedList.Contains(newNode))
                {
                    openList.Add(newNode);
                }
            }
            //validate this isnt the top bound, if not, generate upper node and add to open list
            if (!node.Bounds.Contains("up"))
            {
                Node newNode = NodeBuilder(node.Pos - dimension, dimension);
                if(!closedList.Contains(newNode))
                {
                    openList.Add(newNode);
                }
            }
            //validate this isnt the bottom bound, if not, generate lower node and add to open list
            if (!node.Bounds.Contains("down"))
            {
                Node newNode = NodeBuilder(node.Pos + dimension, dimension);
                if(!closedList.Contains(newNode))
                {
                    openList.Add(newNode);
                }
            }
        }

        public Node GenerateObstacleNode(int pos)
        {
            Node newNode = new Node(pos, dimension, false);
            return newNode;
        }

        void SortOpenList()
        {
            openList.Sort();
        }

        /*void SortOpenList()
        {
            for(int i = 0; i < openList.Count; i++)
            {
                Node temp = openList[i];
                for(int j = 1;  j < openList.Count; j++)
                {
                    if (!temp.CompareTo(openList[j]))
                    {
                        openList[i] = openList[j];
                        openList[j] = temp;
                    }
                }
            }
        }*/
    }
}


/*public string DetermineDirectionToMove() //////////Replaced by evaluating hCost and gCost separately
{
    string vector;
    if (horizontalCost < verticalCost)
    {
        //horizontal
        if ((start % dimension) - (end % dimension) < 0)
        {
            vector = "left";
        }
        else
        {
            vector = "right";
        }
    }
    else
    {
        //vertical
        if((start / dimension) - (end / dimension) < 0)
        {
            vector = "down";
        }
        else
        {
            vector = "up";
        }
    }

    return vector;

}

         public void FindPath(GridBuilder Grid, string[] grid) /////////beefy thing i need to do
        {
            do
            {
                FindNextPosition(grid);
                Grid.DrawFinalDisplay(start, end, pathList, simPosition);
            } while (simPosition != end || openList.Count > 0);
        }

        public void FindNextPosition(string[] grid)
        {
            //find possible moves as long as they arent in the closed list
            GenerateOpenList();
            if(openList.Count == 1 && pathList.Contains(openList.ElementAt(0))) 
            {
                closedList.Add(simPosition);
                simPosition = pathList.Last();
            }
            
            //looking for new lower hMove option
            int newPos = SimulateMove(grid);
            //suggested move is the same as current location, add current location to closedList, except for start
            if(newPos == simPosition && simPosition != start)
            {
                closedList.Add(simPosition);
            }
            //if suggested is different, add old position to path list, change current position to new position
            else
            {
                if (!pathList.Contains(simPosition))
                {
                    pathList.Add(simPosition);
                }
                simPosition = newPos;
            }
        }

        public int SimulateMove(string[] grid)
        {
            int newH = hCost;
            int temp;
            int bestMove = openList.ElementAt(0);
            for(int i = 0; i < openList.Count; i++)
            {
                if (CheckForObstacle(openList.ElementAt(i), grid))
                {
                    closedList.Add(openList.ElementAt(i));
                    openList.RemoveAt(i);
                    i--;
                    continue;
                }
                else
                {
                    temp = PathCost(openList.ElementAt(i));
                    if(temp == 0)
                    {
                        Console.WriteLine("Win Found");
                        break;
                    }
                    else if (temp < newH)
                    {
                        newH = temp;
                        bestMove = openList.ElementAt(i);
                    }
                }
            }

            hCost = newH;
            return bestMove;
        }

        public bool ValidateInBounds(string direction, Node node)
        {
            if(node.Type == 1 )
            {
                return false;
            }
            if (node.Bounds.Contains("none"))
            {
                return true;
            }
            bool valid = true;
            //left bound
            if (node.Bounds.Contains("left"))
            {
                valid = false;
            }
            //right bound
            else if (direction == "right")
            {
                if (node.Pos % dimension == dimension - 1)
                {
                    valid = false;
                }
            }
            //up bound
            else if(direction == "up")
            {
                if(-dimension < dimension)
                {
                    valid = false;
                }
            }
            //down bound
            else
            {
                if(pos+dimension > dimension * dimension)
                {
                    valid = false;
                }
            }

            return valid;

        public List<Node> GenerateNodes()
        {
            List<Node> nodes = new List<Node>();
            for (int i = 0; i < cellCount; i++)
            {
                if (obstaclesList.Contains(i))
                {
                    nodes.Add(GenerateObstacleNodes(i));
                }
                else
                {
                    nodes.Add(GenerateTraversableNodes(i));
                }
            }

            return nodes;
        }

        public Node GenerateObstacleNodes(int pos)
        {
            Node newNode = new Node(pos, dimension, false);
            return newNode;
        }

        public Node GenerateTraversableNodes(int pos)
        {
            Node newNode = new Node(pos, dimension, true);
            return newNode;
        }
        }*/