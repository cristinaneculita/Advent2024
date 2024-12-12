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
Hashtable memo= new Hashtable();

long sum = 0;
char[][] m = new char[l][];
bool[][] luat = new bool[l][];
//citire date
for (int i = 0; i < l; i++)
{
    m[i] = lines[i].ToCharArray();
    luat[i] = new bool[l];
    for (int j = 0; j < l; j++)
    {
        luat[i][j] = false;
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
                CalculatePret(i, j, t,pret);
                sum += pret.Perimeter * pret.Area;

            }
        }
    }
}

Console.WriteLine(sum);
void CalculatePret(int i, int j, char t,Pret pret)
{
    //pun area
    pret.Area++;
    var x = CountVeciniExt(i, j, t);
    pret.Perimeter += x;
    luat[i][j] = true;

    //dr
    if (mat(i, j + 1) == t)
    {
        CalculatePret(i, j + 1, t, pret);
    }
    //jos
    if (mat(i + 1, j) == t)
    {
        CalculatePret(i + 1, j, t, pret);
    }
    //stg
    if (mat(i, j - 1) == t)
    {
        CalculatePret(i, j - 1, t, pret);
    }
    //sus
    if (mat(i - 1, j) == t)
    {
        CalculatePret(i - 1, j, t, pret);
    }
}
char mat(int i, int j)
{
    if (i >= 0 && j >= 0 && i <= l - 1 && j <= l - 1 && luat[i][j] == false)
        return m[i][j];
    return '-';
}
int CountVeciniExt(int i, int j, char t)
{
    var vecini = 0;
    if (i == 0)
        vecini++;
    if (j == 0)
        vecini++;
    if(i==l-1)
        vecini++;
    if(j==l-1)
        vecini++;
    if (j<l-1 && m[i][j + 1] != t)
        vecini++;
    if (i<l-1&& m[i + 1][j]!=t)
        vecini++;
    if(j>0 && m[i][j-1]!=t)
        vecini++;
    if(i>0 && m[i - 1][j]!=t)
        vecini++;
    return vecini;
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

