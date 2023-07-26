using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Astar
{
    internal class Node
    {
        int pos, row, col, f, g, h, type;
        Node parent;
        List<string> bounds = new List<string>();

        public int Pos { get { return pos; } set { pos = value; } }
        public int Row { get { return row; } set { row = value; } }
        public int Col { get { return col; } set { col = value; } }
        public Node Parent { get { return parent; } set { parent = value; } }
        public int F { get { return f; } set { F = value; } }
        public int G { get { return g; } set { g = value; } }
        public int H { get { return h; } set { h = value; } }
        public int Type { get { return type; } set { type = value; } }
        public List<string> Bounds { get { return bounds; } }

        //constructor for obstacle nodes
        public Node(int pos, int dimension, bool traversable)
        {
            this.pos = pos;
            this.row = pos / dimension;
            this.col = pos % dimension;
            if(traversable == true)
            {
                this.type = 0;
            }
            else
            {
                this.type = 1;
            }
            bounds = DetermineBounds(dimension);
        }

        public Node(int pos, int dimension, int h, int g)
        {
            this.pos = pos;
            this.row = pos / dimension;
            this.col = pos % dimension;
            this.h = h;
            this.g = g;
            type = 0;
            setF();
        }

        public void setF()
        {
            f = g + h;
        }

        public void setG(int val)
        {
            g = val;
        }

        public void setH(int val)
        {
            h =val;
        }

        public void setParent(Node node)
        {
            parent = node;
        }

        public List<string> DetermineBounds(int dimension)
        {
            if(row == 0)
            {
                bounds.Add("left");
            }
            if(row == dimension-1)
            {
                bounds.Add("right");
            }
            if(col == 0)
            {
                bounds.Add("top");
            }
            if (col == dimension-1)
            {
                bounds.Add("bottom");
            }
            if(bounds.Count == 0)
            {
                bounds.Add("none");
            }

            return bounds;
        }

        public bool CompareTo(Node node)
        {
            if(this.f < node.f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
