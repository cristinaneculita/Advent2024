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
bool ende = false;
var l = lines.Length;
var lineop = 5;
char[][] m= new char[lineop][];
long sum=0;
for (int i = 0; i < lineop; i++)
{
    m[i] = lines[i].ToCharArray();
}

var cop = 0;
int start = 0;
int end = 0;
List<char> aux = new List<char>();
while (true)
{
    while (m[lineop-1][cop] == ' ')
        cop++;
    start = cop;
    end = cop;
    while (!AllEmpty(end))
    {
        end++;
    }

   
    //from start to end form numbers
   
    var nr = new List<int>();
    for (int i = start; i < end; i++)
    {
        aux = new List<char>();
        if (m[0][i]!= ' ')
            aux.Add(m[0][i]);
        if (m[1][i]!=' ')
            aux.Add(m[1][i]);
        if (m[2][i]!=' ')
            aux.Add(m[2][i]);
        if (m[3][i]!=' ')
           aux.Add(m[3][i]);
        var cs = aux.ToArray();
        var s = new string(cs);
        nr.Add(int.Parse(s));
    }

    long sc = 0;
    switch (m[lineop-1][cop])
    {
        case '+':
            sc = 0;
            for (int j = 0; j < nr.Count; j++)
            {
                sc += nr[j];
            }

            break;
        case '*':
            sc = 1;
            for (int j = 0; j < nr.Count; j++)
            {
                sc *= nr[j];
            }

            break;
    }

    sum += sc;
    cop++;
    if (cop >= m[lineop - 1].Length)
        break;
    if (ende)
        break;
}

Console.WriteLine(sum);


bool AllEmpty(int end)
{

    if (end >= m[lineop-1].Length)
    {
        ende = true;
        return true;
    }
    
    if (m[0][end] == ' ' && m[1][end] == ' ' && m[2][end] == ' ' && m[3][end] == ' ')
        return true;
    return false;
}