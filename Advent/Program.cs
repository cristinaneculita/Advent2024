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
var sum = 0;var sum2 = 0;
Guard g = new Guard();
int[][] m = new int[l][];
Vispoint[][] v = new Vispoint[l][];
int gi = 0, gj = 0;
//citire date
for (int i = 0; i < l; i++)
{
    m[i] = new int[c];
    v[i] = new Vispoint[c];
    var ca = lines[i].ToCharArray();
    for (int j = 0; j < ca.Length; j++)
    {
        v[i][j] = new Vispoint();
        v[i][j].Viz = false;
        if (ca[j] == '.')
            m[i][j] = 0;
        if (ca[j]=='#')
            m[i][j]= 1;
        if (ca[j] == '^')
        {
            g.x = i;
            g.y = j;
            g.dir = 1;
            v[i][j].Viz = true;
            v[i][j].Dir = 1;
            gi = i;
            gj = j;
        }
    }
}


//prelucrare date
for (int i = 0; i < l; i++)
{
    for (int j = 0; j < c; j++)
    {
        if (m[i][j] == 0 && !(i == gi && j == gj))
        {
            Console.WriteLine($" {i} {j}");
            m[i][j] = 1;
            v = new Vispoint[l][];
            for (int k = 0; k < c; k++)
            {
                v[k] = new Vispoint[c];
                for (int n = 0; n < c; n++)
                {
                    v[k][n] = new Vispoint();
                }
            }

            v[gi][gj].Viz = true;
            v[gi][gj].Dir = 1;
            g.x = gi;
            g.y = gj;
            g.dir = 1;
            var loop = Way(g, m, v, c, l);
            if (loop)
                sum++;
            m[i][j] = 0;
        }
    }
}





Console.WriteLine(sum);

bool Way(Guard guard, int[][] ints, Vispoint[][] v, int c, int l)
{
    var possible = true;
    var exit = false;
    while (true)
    {
        possible = true;
        switch (guard.dir)
        {
            case 1:
                while (possible)
                {
                    if (guard.x == 0)
                    {
                        possible = false;
                        exit = true;
                    }
                    else if (ints[guard.x - 1][guard.y]==0)
                    {
                       

                        guard.x--;
                        if (v[guard.x][guard.y].Viz && v[guard.x][guard.y].Dir == 1)
                        {
                            return true;
                        }
                        v[guard.x][guard.y].Viz = true;
                        v[guard.x][guard.y].Dir = 1;

                    }
                    else
                    {
                        possible = false;
                        guard.dir = 2;
                    }
                }
                break;
            case 2:
                while (possible)
                {
                    if (guard.y == c-1)
                    {
                        possible = false;
                        exit = true;
                    }
                    else if (ints[guard.x][guard.y+1] == 0)
                    {
                        guard.y++;
                        if (v[guard.x][guard.y].Viz == true && v[guard.x][guard.y].Dir == 2)
                            return true;
                        v[guard.x][guard.y].Viz = true;
                        v[guard.x][guard.y].Dir = 2;
                        
                    }
                    else
                    {
                        possible = false;
                        guard.dir = 3;
                    }
                }
                break;
            case 3:
                while (possible)
                {
                    if (guard.x == l-1)
                    {
                        possible = false;
                        exit = true;
                    }
                    else if (ints[guard.x + 1][guard.y] == 0)
                    {
                        guard.x++;
                        if (v[guard.x][guard.y].Viz && v[guard.x][guard.y].Dir == 3)
                            return true;
                        v[guard.x][guard.y].Viz = true;
                        v[guard.x][guard.y].Dir = 3;
                    }
                    else
                    {
                        possible = false;
                        guard.dir = 4;
                    }
                }
                break;
            case 4:
                while (possible)
                {
                    if (guard.y == 0)
                    {
                        possible = false;
                        exit = true;
                    }
                    else if (ints[guard.x][guard.y-1] == 0)
                    {
                        guard.y--;
                        if (v[guard.x][guard.y].Viz && v[guard.x][guard.y].Dir == 4)
                            return true;
                        v[guard.x][guard.y].Viz = true;
                        v[guard.x][guard.y].Dir = 4;
                    }
                    else
                    {
                        possible = false;
                        guard.dir = 1;
                    }
                }
                break;

        }

        if (exit)
            break;

    }

    return false;
}

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
public class Vispoint{
    public bool Viz { get; set; }
    public int Dir { get; set; }

    public Vispoint()
    {
        
    }
    public Vispoint(bool viz, int dir)
    {
        Viz = viz;
        Dir = dir;
    }
}