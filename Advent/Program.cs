// See https://aka.ms/new-console-template for more information

using System.Collections;
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

string[] lines = File.ReadAllLines("input.txt");
var l = lines.Length;
Hashtable memo = new Hashtable();

long sum = 0;
char[][] m = new char[l][];
bool[][] luat = new bool[l][];
List<int>[][] vm = new List<int>[l][];
//citire date
for (int i = 0; i < l; i++)
{
    m[i] = lines[i].ToCharArray();
    luat[i] = new bool[l];
    vm[i] = new List<int>[l];
    for (int j = 0; j < l; j++)
    {
        luat[i][j] = false;
        vm[i][j] = new List<int>();
    }
}

//procesare date

Console.WriteLine();
for (char t = 'A'; t <= 'Z'; t++)
{
    for (int i = 0; i < l; i++)
    {
        for (int j = 0; j < l; j++)
        {
            if (m[i][j] == t && luat[i][j] == false)
            {
                Pret pret = new Pret();
                var v = new List<int>();
                CalculatePret(i, j, t, pret, v);
                var si = pret.Perimeter * pret.Area;
                sum += si;

            }
        }
    }
}

Console.WriteLine(sum);
void CalculatePret(int i, int j, char t, Pret pret, List<int> v)
{
    //pun area
    pret.Area++;
    List<int> vc = CountVeciniExt(i, j, t);


    luat[i][j] = true;
    vm[i][j] = vc;

    pret.Perimeter += DifVec(vc, i, j, t);

    int DifVec(List<int> vecc, int i, int j, char t)
    {
        int d = vecc.Count;
        foreach (var v in vecc)
        {
            //sus
            if (i > 0 && m[i - 1][j] == t && vm[i - 1][j].Contains(v) && luat[i - 1][j])
                d--;
            //dr
            if (j < l - 1 && m[i][j + 1] == t && vm[i][j + 1].Contains(v) && luat[i][j+1])
            { d--; }
            //jos
            if (i < l - 1 && m[i + 1][j] == t && vm[i + 1][j].Contains(v) && luat[i + 1][j])
            { d--; }
            //stg
            if (j > 0 && m[i][j - 1] == t && vm[i][j - 1].Contains(v) && luat[i][j - 1])
            { d--; }

        }

        return d;
    }
    //dr
    if (mat(i, j + 1) == t)
    {
        CalculatePret(i, j + 1, t, pret, vc);
    }
    //jos
    if (mat(i + 1, j) == t)
    {
        CalculatePret(i + 1, j, t, pret, vc);
    }
    //stg
    if (mat(i, j - 1) == t)
    {
        CalculatePret(i, j - 1, t, pret, vc);
    }
    //sus
    if (mat(i - 1, j) == t)
    {
        CalculatePret(i - 1, j, t, pret, vc);
    }
}
char mat(int i, int j)
{
    if (i >= 0 && j >= 0 && i <= l - 1 && j <= l - 1 && luat[i][j] == false)
        return m[i][j];
    return '-';
}
List<int> CountVeciniExt(int i, int j, char t)
{
    var vc = new List<int>();
    var vecini = 0;
    //sus
    if (i == 0)
    {
        vecini++;
        vc.Add(4);
    }
    //stg
    if (j == 0)
    {
        vecini++;
        vc.Add(3);
    }
    //jos
    if (i == l - 1)
    {
        vecini++;
        vc.Add(2);
    }
    //dr
    if (j == l - 1)
    {
        vecini++;
        vc.Add(1);
    }
    //dr
    if (j < l - 1 && m[i][j + 1] != t)
    {
        vecini++;
        vc.Add(1);
    }
    //jos
    if (i < l - 1 && m[i + 1][j] != t)
    {
        vecini++;
        vc.Add(2);
    }
    //stg
    if (j > 0 && m[i][j - 1] != t)
    {
        vecini++;
        vc.Add(3);
    }
    //sus
    if (i > 0 && m[i - 1][j] != t)
    {
        vecini++;
        vc.Add(4);
    }



    return vc;
}
public class Pret
{
    public int Area { get; set; }
    public int Perimeter { get; set; }
    public Pret()
    {

    }


    public Pret(int area, int perimeter)
    {
        Area = area;
        Perimeter = perimeter;
    }
}

