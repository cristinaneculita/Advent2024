// See https://aka.ms/new-console-template for more information

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Globalization;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

string[] lines = File.ReadAllLines("input.txt");
var l = lines.Length;
var c = lines[0].Length;
var sum = 1;
var sum2 = 0;
Guard g = new Guard();
int[][] m = new int[l][];
int[][] v = new int[l][];
//citire date
for (int i = 0; i < l; i++)
{
    m[i] = new int[c];
    v[i] = new int[c];
    var ca = lines[i].ToCharArray();
    for (int j = 0; j < ca.Length; j++)
    {
        v[i][j] = 0;
        if (ca[j] == '.')
            m[i][j] = 0;
        if (ca[j]=='#')
            m[i][j]= 1;
        if (ca[j] == '^')
        {
            g.x = i;
            g.y = j;
            g.dir = 1;
            v[i][j] = 1;
        }
    }
}


//prelucrare date
var possible = true;
var exit = false;
while (true)
{
    possible = true;
    switch (g.dir)
    {
        case 1:
            while (possible)
            {
                if (g.x == 0)
                {
                    possible = false;
                    exit = true;
                }
                else if (m[g.x - 1][g.y]==0)
                {
                    g.x--;
                    sum++;
                    v[g.x][g.y] = 1;
                }
                else
                {
                    possible = false;
                    g.dir = 2;
                }
            }
            break;
        case 2:
            while (possible)
            {
                if (g.y == c-1)
                {
                    possible = false;
                    exit = true;
                }
                else if (m[g.x][g.y+1] == 0)
                {
                    g.y++;
                    sum++;
                    v[g.x][g.y] = 1;
                }
                else
                {
                    possible = false;
                    g.dir = 3;
                }
            }
            break;
        case 3:
            while (possible)
            {
                if (g.x == l-1)
                {
                    possible = false;
                    exit = true;
                }
                else if (m[g.x + 1][g.y] == 0)
                {
                    g.x++;
                    sum++;
                    v[g.x][g.y] = 1;
                }
                else
                {
                    possible = false;
                    g.dir = 4;
                }
            }
            break;
        case 4:
            while (possible)
            {
                if (g.y == 0)
                {
                    possible = false;
                    exit = true;
                }
                else if (m[g.x][g.y-1] == 0)
                {
                    g.y--;
                    sum++;
                    v[g.x][g.y] = 1;
                }
                else
                {
                    possible = false;
                    g.dir = 1;
                }
            }
            break;

    }

    if (exit)
        break;

}

var sum3 = 0;
for (int i = 0; i < l; i++)
{
    for (int j = 0; j < c; j++)
    {
        if (v[i][j] == 1)
            sum3++;
    }
}



Console.WriteLine(sum3);

public class Guard
{
    public int x { get; set; }
    public int y { get; set; }
    public int dir { get; set; }

    public Guard()
    {
        
    }

    public Guard(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}