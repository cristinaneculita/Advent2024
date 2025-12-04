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
var c = lines[0].Length;

char[][] m = new char[c][];
for (int i = 0; i < c; i++)
{
    m[i] = lines[i].ToCharArray();
}

char[][] marked = new char[c][];
for (int i = 0; i < c; i++)
{
    marked[i] = new char[l];
}


long sum = 0;
int partialsum = 1;
while (partialsum!=0)
{
    partialsum = 0;
    for (int i = 0; i < l; i++)
    {
        for (int j = 0; j < c; j++)
        {
            marked[i][j] = '-';
        }
    }

    for (int i = 0; i < l; i++)
    {
        for (int j = 0; j < c; j++)
        {
            if (m[i][j] == '@' && Valid(m, i, j))
                marked[i][j] = '#';
        }
    }
    for (int i = 0; i < l; i++)
    {
        for (int j = 0; j < c; j++)
        {
            if(marked[i][j] == '#')
            {
                partialsum++;
                m[i][j] = '.';
            }
        }
    }
    sum += partialsum;
}

Console.WriteLine(sum);

bool Valid(char[][] chars, int i, int j)
{
    var s = 0;
    if (i > 0 && j > 0 && chars[i - 1][j - 1] == '@')
        s++;
    if (i > 0 && chars[i - 1][j] == '@')
        s++;
    if (i > 0 && j < chars.Length - 1 && chars[i - 1][j + 1] == '@')
        s++;
    if (j > 0 && chars[i][j - 1] == '@')
        s++;
    if (j < chars.Length - 1 && chars[i][j + 1] == '@')
        s++;
    if (i < chars.Length - 1 && j > 0 && chars[i + 1][j - 1] == '@')
        s++;
    if (i < chars.Length - 1 && chars[i + 1][j] == '@')
        s++;
    if (i < chars.Length - 1 && j < chars.Length - 1 && chars[i + 1][j + 1] == '@')
        s++;
    return s < 4;
}