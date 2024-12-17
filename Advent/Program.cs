// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Diagnostics;
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
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

string[] lines = File.ReadAllLines("input.txt");
//Hashtable memo = new Hashtable();
var l = lines.Length;
long A = 0;
long B = 0; long C=0;
List<int> instr = new List<int>();
//citire date
for (int i = 0; i < l; i++)
{
    if (lines[i].Contains("Register A"))
    {
        var x = lines[i].Remove(0, 12);
        A = long.Parse(x);
    }
    if (lines[i].Contains("Register B"))
    {
        var x = lines[i].Remove(0, 12);
        B = long.Parse(x);
    }
    if (lines[i].Contains("Register C"))
    {
        var x = lines[i].Remove(0, 12);
        C = long.Parse(x);
    }
    if (lines[i].Contains("Program"))
    {
        var x = lines[i].Remove(0,9 );
        var y = x.Split(',').ToList();
        foreach (var yy in y)
        {
            instr.Add(int.Parse(yy));
        }
    }
}

long initialA = A;
var output = new List<int>();
long count = 0;
while (true)
{
    output = new List<int>();
    A = initialA + count;
    B = 0;
    C = 0;
    Console.WriteLine(A);

    Output();
    
    count+=1;
}

Console.WriteLine(A);
 


void Output()
{
    long ind = 0;
    List<int> output = new List<int>();
    //prelucrare date
    while (true)
    {
        var ins = instr[(int)ind];
        long jumps = Solve(ins, instr[(int)ind + 1]);

        if (jumps == -1)
            ind += 2;
        else
        {
            ind = jumps;
        }
        if (ind >= instr.Count)
            break;
    }
}

long Solve(int instr, long op)
{
    switch (instr)
    {
        case 0:
            A = A / (int)Math.Pow(2, Combo(op));
            break;
        case 1:
            B = B ^ op;
            break;
        case 2:
            B = Combo(op) % 8;
            break;
        case 3:
            if (A != 0)
                return op;
            break;
        case 4:
            B = B ^ C;
            break;
        case 5:
            output.Add((int)(Combo(op)%8));
            break;
        case 6:
            B = A / (int)Math.Pow(2, Combo(op));
            break;
        case 7:
            C = A / (int)Math.Pow(2, Combo(op));

           

            break;
    }

    return -1;
}

foreach (var o in output)
{
   Console.Write(o+",");
}
Console.ReadKey();

long Combo(long op)
{
    if (op >= 0 && op <= 3)
        return op;
    if (op == 4)
        return A;
    if(op==5)
        return B;
    if (op == 6)
        return C;
    return 0;
}
//Console.WriteLine(sum);
