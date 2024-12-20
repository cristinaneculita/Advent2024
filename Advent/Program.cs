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

var l = lines.Length;
char[][] map = new char[l][];
int[][] cost = new int[l][];
bool[][] luat= new bool[l][];

//citire date
for (int i = 0; i < l; i++)
{
    map[i] = lines[i].ToCharArray();
    cost[i] = new int[l];
    luat[i] = new bool[l];
    for (int j = 0; j < l; j++)
    {
        cost[i][j] = int.MaxValue;
        luat[i][j] = false;
    }
}

int si = 0, sj = 0, ei = 0,ej = 0;
//prelucrare date
for (int i = 0; i < l; i++)
{
    for (int j = 0; j < l; j++)
    {
        if (map[i][j] == 'S')
        {
            si = i;
            sj = j;
        }

        if (map[i][j] == 'E')
        {
            ei = i;
            ej = j;
        }
    }
}

cost[si][sj] = 0;
Dijsktra();
var pic = cost[ei][ej];
var many = 0;
//scot cate un perete si vad cate salvez
for (int a = 1; a < l-1; a++)
{
    for (int b = 1; b < l-1; b++)
    {
        for (int i = 0; i < l; i++)
        {
            map[i] = lines[i].ToCharArray();
            cost[i] = new int[l];
            luat[i] = new bool[l];
            for (int j = 0; j < l; j++)
            {
                cost[i][j] = int.MaxValue;
                luat[i][j] = false;
            }
        }
        cost[si][sj] = 0;
        if (map[a][b] == '#')
        {
            map[a][b] = '.';
            Dijsktra();
            var picc = cost[ei][ej];
            Console.WriteLine($"{a} {b} :{picc}" );
            if (picc + 100 <= pic)
                many++;
        }

    }
}

Console.WriteLine(many);
Console.ReadKey();
Console.ReadKey();
Console.ReadKey();



void Dijsktra()
{
    int ii = 0, jj = 0;
    while (cost[ei][ej] == int.MaxValue)
    {
        Point u = minDist();
        
        luat[u.I][u.J] = true;
        //dr
        if (u.J < l - 1)
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
        if (u.I < l - 1)
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
    for (int i = 0; i < l; i++)
    {
        for (int j = 0; j < l; j++)
        {
            if (luat[i][j] == false && cost[i][j] < min)
            {
                min = cost[i][j];
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