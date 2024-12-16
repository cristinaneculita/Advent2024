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
int orientare = 1;
//citire date
for (int i = 0; i < l; i++)
{
    map[i] = lines[i].ToCharArray();
    cost[i] = new int[lines[i].Length][];
    luat[i] = new bool[lines[i].Length][];
    for (int j = 0; j < lines[i].Length; j++)
    {
        cost[i][j] = new int[4];
        luat[i][j] = new bool[4];
        if (map[i][j] == 'S')
        {
            cost[i][j][1] = 0;
            luat[i][j][1] = false;
            cost[i][j][0] = int.MaxValue;
            cost[i][j][2] = int.MaxValue;
            cost[i][j][3] = int.MaxValue;
            luat[i][j][0] = false;
            luat[i][j][2] = false;
            luat[i][j][3] = false;
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

//prelucrare date
Dijkstra();



//Console.WriteLine(sum);

void Dijkstra()
{
    int ii = 0, jj = 0, oo = 0;
    while (NotLuatEnd()==int.MaxValue)
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
            && cost[u.I][u.J][u.O] + 1 < cost[ii][jj][oo])
        {
            cost[ii][jj][oo] = cost[u.I][u.J][u.O] + 1;
        }
        // jos
        ii = u.I + 1;
        jj = u.J;
        oo = u.O;
        if (!luat[ii][jj][oo]
            && map[ii][jj] != '#'
            && cost[u.I][u.J][u.O] != int.MaxValue
            && oo == 2
            && cost[u.I][u.J][u.O] + 1 < cost[ii][jj][oo])
        {
            cost[ii][jj][oo] = cost[u.I][u.J][u.O] + 1;
        }
        // stg
        ii = u.I;
        jj = u.J - 1;
        oo = u.O;
        if (!luat[ii][jj][oo]
            && map[ii][jj] != '#'
            && cost[u.I][u.J][u.O] != int.MaxValue
            && oo == 3
            && cost[u.I][u.J][u.O] + 1 < cost[ii][jj][oo])
        {
            cost[ii][jj][oo] = cost[u.I][u.J][u.O] + 1;
        }
        // sus
        ii = u.I - 1;
        jj = u.J;
        oo = u.O;
        if (!luat[ii][jj][oo]
            && map[ii][jj] != '#'
            && cost[u.I][u.J][u.O] != int.MaxValue
            && oo == 0
            && cost[u.I][u.J][u.O] + 1 < cost[ii][jj][oo])
        {
            cost[ii][jj][oo] = cost[u.I][u.J][u.O] + 1;
        }
        //90gr c
        ii = u.I;
        jj = u.J;
        oo = (u.O+1)%4;
        if (!luat[ii][jj][oo]
            && map[ii][jj] != '#'
            && cost[u.I][u.J][u.O] != int.MaxValue
            && cost[u.I][u.J][u.O] + 1000 < cost[ii][jj][oo])
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
            && cost[u.I][u.J][u.O] + 1000 < cost[ii][jj][oo])
        {
            cost[ii][jj][oo] = cost[u.I][u.J][u.O] + 1000;
        }
    }

    Console.WriteLine(NotLuatEnd());
}

int NotLuatEnd()
{
    if (cost[1][l - 2][0] != int.MaxValue)
        return cost[1][l - 2][0];
    if (cost[1][l - 2][1] != int.MaxValue)
        return cost[1][l - 2][1];
    if (cost[1][l - 2][2] != int.MaxValue)
        return cost[1][l - 2][2];
    return cost[1][l - 2][3];
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
                    min= cost[i][j][k];
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