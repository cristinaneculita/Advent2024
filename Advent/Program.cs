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
char[][] m = new char[l][];
bool[][] anti = new bool[l][];
var antenas = new List<char>();
long sum = 0;
//citire date
for (int i = 0; i < l; i++)
{
    m[i] = lines[i].ToCharArray();
    anti[i] = new bool[l];
}


//prelucrare date
for (int i = 0; i < l; i++)
{
    for (int j = 0; j < l; j++)
    {
        if (m[i][j] != '.')
        {
            if (!antenas.Contains(m[i][j]))
                antenas.Add(m[i][j]);
        }

        anti[i][j] = false;
    }
}

foreach (var antena in antenas)
{
    List<Point> allOcc = GetAllOcc(antena);



    foreach (var occ1 in allOcc)
    {
        foreach (var occ2 in allOcc)
        {
            if (occ1 != occ2)
            {
                
                FindAllAntinodes(anti, occ1, occ2);


            }
        }
    }

}

for (int i = 0; i < l; i++)
{
    for (int j = 0; j < l; j++)
    {
        if (anti[i][j])
            sum++;
    }
}

Console.WriteLine(sum);

void FindAllAntinodes(bool[][] anti, Point occ1, Point occ2)
{
    var diffx = occ1.X - occ2.X;
    var diffy = occ1.Y - occ2.Y;
    var anti1x = occ1.X + diffx;
    var anti1y = occ1.Y + diffy;
    if (InLimits(anti1x) && InLimits(anti1y))
        anti[anti1x][anti1y] = true;
    var anti2x = occ2.X - diffx;
    var anti2y = occ2.Y - diffy;
    if (InLimits(anti1x) && InLimits(anti1y))
        anti[anti1x][anti1y] = true;

}
bool InLimits(int coord)
{
    if (coord >= 0 && coord < l)
    { return true; }

    return false;
}
List<Point> GetAllOcc(char c)
{
    var result = new List<Point>();
    for (int i = 0; i < l; i++)
    {
        for (int j = 0; j < l; j++)
        {
            if (m[i][j] == c)
                result.Add(new Point(i, j));
        }
    }
    return result;
}
public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals(object obj)
    {
        var item = obj as Point;

        if (item == null)
        {
            return false;
        }

        if (item.X == this.X && item.Y == this.Y)
            return true;
        return false;
    }

    public override int GetHashCode()
    {
        return this.X.GetHashCode();
    }
}