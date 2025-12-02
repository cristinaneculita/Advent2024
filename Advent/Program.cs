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
int s = 50;
int t = 0;
foreach (var line in lines)
{
    var dir = line[0];
    var val = int.Parse(line[1..]);
    Console.Write(line + " ");
    if (dir == 'R')
    {
        
        while (val>0)
        {
            s+=1;
            if (s == 100)
            {
                t++;
                s = 0;
            }

            val--;

        }
    }
    else if (dir == 'L')
    {

        while (val>0) 
        {
            s-=1;
            if (s == 0)
            {
                t++;
            }

            if (s == -1)
                s = 99;
            val--;
            // Console.Write(" added");
        }
    }
    //if (s==0)
    //    t+=1;
    Console.WriteLine(s);
}
Console.WriteLine(t);
  


