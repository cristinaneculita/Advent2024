// See https://aka.ms/new-console-template for more information

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
int[][] m = new int[l][];
int sum = 0;
int[][] nines = new int[l][];
//citire date
for (int i = 0; i < l; i++)
{
    var v = lines[i].ToCharArray();
    m[i]=new int[l];
    nines[i] = new int[l];
    for (int j = 0; j < l; j++)
    {
        m[i][j] = v[j] - '0';
        nines[i][j] = 0;
    }

    
}

//procesare date
for (int i = 0; i < l; i++)
{
    for (int j = 0; j < l; j++)
    {
        if (m[i][j] == 0)
        {
            for (int k = 0; k < l; k++)
            {
                for (int n = 0; n < l; n++)
                {
                    nines[k][n] = 0;
                }
            }
            SearchPath(i, j, 0);
        }
    }
}
Console.WriteLine(sum);

void SearchPath(int i, int j, int pas)
{
    if (pas == 9)
    {
     //   if (nines[i][j] == 0)
        {
            sum++;
            nines[i][j] = 1;
        }
        return;
    }
    else
    {
        if(vecinsus(i,j)==pas+1)
            SearchPath(i-1,j,pas+1);
        if (vecindr(i, j) == pas + 1)
            SearchPath(i, j+1, pas + 1);
        if (vecinjos(i, j) == pas + 1)
            SearchPath(i+1, j, pas+1);
        if (vecinstg(i, j) == pas + 1)
            SearchPath(i, j-1, pas + 1);
    }
}

int vecinsus(int i, int j)
{
    if (i > 0)
        return m[i-1][j];
    return -1;
}

int vecindr(int i, int j)
{
    if (j <l-1)
        return m[i][j+1];
    return -1;
}

int vecinjos(int i, int j)
{
    if (i <l-1)
        return m[i+1][j];
    return -1;
}
int vecinstg(int i, int j)
{
    if (j > 0)
        return m[i][j-1];
    return -1;
}