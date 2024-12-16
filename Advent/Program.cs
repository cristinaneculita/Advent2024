// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Globalization;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

string[] lines = File.ReadAllLines("input.txt");
//Hashtable memo = new Hashtable();
char[] instr = new char[1];
long sum = 0;
var l = lines.Length;
char[][] map = new char[l][];
int[][][] cost = new int[l][][];
bool[][][] luat = new bool[l][][];
bool[][] visited = new bool[l][];
List<Point>[][] prev = new List<Point>[l][];
int orientare = 1;
int starti = 0, startj = 0, starto = 1;
int endi = 1, endj = l-2;
//citire date
for (int i = 0; i < l; i++)
{
    map[i] = lines[i].ToCharArray();
    cost[i] = new int[lines[i].Length][];
    luat[i] = new bool[lines[i].Length][];
    prev[i] = new List<Point>[l];

    for (int j = 0; j < lines[i].Length; j++)
    {
        prev[i][j] = new List<Point>();
        cost[i][j] = new int[4];
        luat[i][j] = new bool[4];
        if (map[i][j] == 'S')
        {
            starti = i;
            startj = j;
          
        }

        visited[i] = new bool[l];
    }
}

int min = Dijkstra(starti, startj, starto, endi, endj).Item1;
Console.WriteLine(min);
(int, int) p1 = (0, 0);
(int, int) p2 = (0, 0);
var count = 0;
//prelucrare date
//for (int i = 0; i < l; i++)
//{
//    for (int j = 0; j < l; j++)
//    {
//        if (map[i][j] != '#')
//        {
//            Console.WriteLine($"try {i} {j}: {count}");
//            p1 = Dijkstra(starti, startj, starto, i, j);

//            if (p1.Item1 <= min)
//            {
//                p2 = Dijkstra(i, j, p1.Item2, endi, endj);
//                if (p1.Item1 + p2.Item1 == min)
//                    count++;
//            }
//        }
//    }
//}

//Console.WriteLine(count);

var ci = endi;
var cj = endj;
DFSRec(ci,cj);
Console.WriteLine(count);
Console.ReadKey();


//Console.WriteLine(sum);
 void DFSRec(int ii, int jj)
{

    // Mark the current vertex as visited
    visited[ii][jj] = true;
    map[ii][jj] = 'O';

    count++;
    foreach (Point p in prev[ii][jj])
    {
        if (!visited[p.I][p.J])
        {
            DFSRec(p.I,p.J);
        }
    }
}

 int cc = 0;
 for (int i = 0; i < l; i++)
 {
     Console.WriteLine();
     for (int j = 0; j < l; j++)
     {
        Console.Write(map[i][j]);
        if (map[i][j] == 'O')
            cc++;
     }
 }

 Console.WriteLine(cc);
(int,int) Dijkstra(int starti, int startj, int starto, int endi, int endj)
{

    int ii = 0, jj = 0, oo = 0;
    InitData(starti, startj, starto);

   
    while (NotLuatEnd(endi, endj).Item1 == int.MaxValue)
    {
        Point u = minDist();
        luat[u.I][u.J][u.O] = true;

        //calculez noile distante
        //dr 
        ii = u.I;
        jj = u.J + 1;
        oo = u.O;
        if (!luat[ii][jj][oo]
            && map[ii][jj] != '#'
            && cost[u.I][u.J][u.O] != int.MaxValue
            && oo == 1
            && cost[u.I][u.J][u.O] + 1 <= cost[ii][jj][oo])
        {
            cost[ii][jj][oo] = cost[u.I][u.J][u.O] + 1;
            if (!prev[ii][jj].Any(p=>p.I==u.I && p.J==u.J))
                prev[ii][jj].Add(new Point(u.I, u.J));
        }
        // jos
        ii = u.I + 1;
        jj = u.J;
        oo = u.O;
        if (!luat[ii][jj][oo]
            && map[ii][jj] != '#'
            && cost[u.I][u.J][u.O] != int.MaxValue
            && oo == 2
            && cost[u.I][u.J][u.O] + 1 <= cost[ii][jj][oo])
        {
            cost[ii][jj][oo] = cost[u.I][u.J][u.O] + 1;
            if(!prev[ii][jj].Any(p => p.I == u.I && p.J == u.J))
                prev[ii][jj].Add(new Point(u.I, u.J));
        }
        // stg
        ii = u.I;
        jj = u.J - 1;
        oo = u.O;
        if (!luat[ii][jj][oo]
            && map[ii][jj] != '#'
            && cost[u.I][u.J][u.O] != int.MaxValue
            && oo == 3
            && cost[u.I][u.J][u.O] + 1 <= cost[ii][jj][oo])
        {
            cost[ii][jj][oo] = cost[u.I][u.J][u.O] + 1;
            if(!prev[ii][jj].Any(p => p.I == u.I && p.J == u.J))
                prev[ii][jj].Add(new Point(u.I, u.J));
        }
        // sus
        ii = u.I - 1;
        jj = u.J;
        oo = u.O;
        if (!luat[ii][jj][oo]
            && map[ii][jj] != '#'
            && cost[u.I][u.J][u.O] != int.MaxValue
            && oo == 0
            && cost[u.I][u.J][u.O] + 1 <= cost[ii][jj][oo])
        {
            cost[ii][jj][oo] = cost[u.I][u.J][u.O] + 1;
            if(!prev[ii][jj].Any(p => p.I == u.I && p.J == u.J))
                prev[ii][jj].Add(new Point(u.I, u.J));
        }
        //90gr c
        ii = u.I;
        jj = u.J;
        oo = (u.O + 1) % 4;
        if (!luat[ii][jj][oo]
            && map[ii][jj] != '#'
            && cost[u.I][u.J][u.O] != int.MaxValue
            && cost[u.I][u.J][u.O] + 1000 <= cost[ii][jj][oo])
        {
            cost[ii][jj][oo] = cost[u.I][u.J][u.O] + 1000;

        }
        //90gr antic
        ii = u.I;
        jj = u.J;
        oo = (u.O - 1);
        if (oo == -1)
            oo = 3;
        if (!luat[ii][jj][oo]
            && map[ii][jj] != '#'
            && cost[u.I][u.J][u.O] != int.MaxValue
            && cost[u.I][u.J][u.O] + 1000 <= cost[ii][jj][oo])
        {
            cost[ii][jj][oo] = cost[u.I][u.J][u.O] + 1000;

        }
    }

    return (NotLuatEnd(endi, endj));
}

void InitData(int starti, int startj, int starto)
{
    for (int i = 0; i < l; i++)
    {
        map[i] = lines[i].ToCharArray();
        cost[i] = new int[lines[i].Length][];
        luat[i] = new bool[lines[i].Length][];
       // prev[i] = new List<Point>[l];

        for (int j = 0; j < lines[i].Length; j++)
        {
           // prev[i][j] = new List<Point>();
            cost[i][j] = new int[4];
            luat[i][j] = new bool[4];
            if (i==starti && j==startj)
            {
                for (int k = 0; k < 4; k++)
                {
                    luat[i][j][k] = false;
                    if (k == starto)
                        cost[i][j][k] = 0;
                    else
                    {
                        cost[i][j][k] = int.MaxValue;
                    }
                }
            }
            else
            {
                for (int k = 0; k < 4; k++)
                {
                    cost[i][j][k] = int.MaxValue;
                    luat[i][j][k] = false;
                }
            }

        }

    }
}

(int,int) NotLuatEnd(int endi, int endj)
{
    if (cost[endi][endj][0] != int.MaxValue)
        return (cost[endi][endj][0],0);
    if (cost[endi][endj][1] != int.MaxValue)
        return (cost[endi][endj][1],1);
    if (cost[endi][endj][2] != int.MaxValue)
        return (cost[endi][endj][2],2);
    return (cost[endi][endj][3],3);
}
Point minDist()
{
    int min = Int32.MaxValue;
    Point p = new Point(-1, -1, -1);
    for (int i = 0; i < l; i++)
    {
        for (int j = 0; j < l; j++)
        {
            for (int k = 0; k < 4; k++)
            {
                if (luat[i][j][k] == false && cost[i][j][k] < min)
                {
                    min = cost[i][j][k];
                    p.I = i;
                    p.J = j;
                    p.O = k;
                }
            }
        }
    }

    return p;
}
public class Point
{
    public int I { get; set; }
    public int J { get; set; }
    public int O { get; set; }

    public Point(int i, int j)
    {
        I = i;
        J = j;
    }

    public Point(int i, int j, int o)
    {
        I = i;
        J = j;
        O = o;
    }
}