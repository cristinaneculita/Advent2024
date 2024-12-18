// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Globalization;
using System.Linq.Expressions;
using System.Net.Sockets;
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
var l = lines.Length;
//for first star

var lat = 71;
char[][] map = new char[lat][];
int[][] cost = new int[lat][];
bool[][] luat = new bool[lat][];
for (int i = 0; i < lat; i++)
{
    map[i] = new char[lat];
    cost[i] = new int[lat];
    luat[i] = new bool[lat];
    for (int j = 0; j < lat; j++)
    {
        map[i][j] = '.';
        cost[i][j] = int.MaxValue;
        luat[i][j] = false;
    }
}

//citire date
var lim = 2900;
var crapat = false;
while (!crapat)
{
    Console.WriteLine(lim);
    for (int i = 0; i < lim; i++)
    {
        var xcit = lines[i].Split(',');
        var x = int.Parse(xcit[0]);
        var y = int.Parse(xcit[1]);
        map[y][x] = '#';
    }
    for (int i = 0; i < lat; i++)
    {
        for (int j = 0; j < lat; j++)
        {
            cost[i][j] = int.MaxValue;
            luat[i][j] = false;
        }
    }

    //prelucrare
    var ic = 0;
    var jc = 0;
    cost[ic][jc] = 0;
    Dijskstra();
    lim++;
}

Console.WriteLine(lines[lim-2]);
void Dijskstra()
{
    int ii = 0, jj = 0;
    while (cost[lat-1][lat-1] == int.MaxValue)
    {
        Point u = minDist();
        if (u.I == -1 || u.J == -1)
        {
            crapat = true;
            break;
        }
        luat[u.I][u.J] = true;

        //calculez noile distante
        //dr
        if (u.J < lat - 1)
        {
            ii = u.I;
            jj = u.J + 1;
            if (!luat[ii][jj]
                && map[ii][jj] != '#'
                && cost[u.I][u.J] != int.MaxValue
                && cost[u.I][u.J] + 1 < cost[ii][jj])
            {
                cost[ii][jj] = cost[u.I][u.J] + 1;
            }
        }
        //jos
        if (u.I < lat - 1)
        {
            ii = u.I + 1;
            jj = u.J;
            if (!luat[ii][jj]
                && map[ii][jj] != '#'
                && cost[u.I][u.J] != int.MaxValue
                && cost[u.I][u.J] + 1 < cost[ii][jj])
            {
                cost[ii][jj] = cost[u.I][u.J] + 1;
            }
        }
        //stg
        if (u.J > 0)
        {
            ii = u.I;
            jj = u.J - 1;
            if (!luat[ii][jj]
                && map[ii][jj] != '#'
                && cost[u.I][u.J] != int.MaxValue
                && cost[u.I][u.J] + 1 < cost[ii][jj])
            {
                cost[ii][jj] = cost[u.I][u.J] + 1;
            }
        }
        //sus
        if (u.I > 0)
        {
            ii = u.I - 1;
            jj = u.J;
            if (!luat[ii][jj]
                && map[ii][jj] != '#'
                && cost[u.I][u.J] != int.MaxValue
                && cost[u.I][u.J] + 1 < cost[ii][jj])
            {
                cost[ii][jj] = cost[u.I][u.J] + 1;
            }
        }
    }
}
Point minDist()
{
    int min = int.MaxValue;
    Point p = new Point(-1, -1);
    for (int i = 0; i < lat; i++)
    {
        for (int j = 0; j < lat; j++)
        {
            if (luat[i][j] == false && cost[i][j] < min)
            {
                min= cost[i][j];
                p.I = i;
                p.J = j;
            }
        }
    }

    return p;
}
public class Point
{
    public int I { get; set; }
    public int J { get; set; }

    public Point(int i, int j)
    {
        I = i;
        J = j;
    }
}


//Console.WriteLine(sum);
