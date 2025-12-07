// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.ComponentModel.Design;
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

var l= lines.Length;
var c= lines[0].Length;


char[][] m = new char[l][];
int splits = 0;
bool[][] solved = new bool[l][];
for (int i = 0; i < l; i++)
{
    m[i] = lines[i].ToCharArray();
    solved[i] = new bool[c];
}
int starti = 0;
int startj = 0;
for (int i = 0; i < l; i++)
{
    for (int j = 0; j < c; j++)
    {
        if (m[i][j] == 'S')
        {
            starti = i;
            startj = j;
            break;
        }
    }
}

bool exit = false;
while (true)
{
    for (int i = 0; i < l; i++)
    {
        for (int j = 0; j < c; j++)
        {
            if ((m[i][j] == 'S' || m[i][j] == '|') && (!solved[i][j]))
            {
                exit = Solve(i, j);
                
                solved[i][j] = true;

            }
        }
    }

    if (exit)
        break;
}

Console.WriteLine(splits);
bool Solve(int i, int j)
{
    if (i == l - 1)
    {
        return true;
    }

    if (m[i + 1][j] == '.')
    {
        m[i + 1][j] = '|';
    }

    if (m[i + 1][j] == '^')
    {
        if (j > 0)
            m[i + 1][j - 1] = '|';
        if (j < c - 1)
            m[i + 1][j + 1] = '|';
        splits++;
    }

    return false;

}