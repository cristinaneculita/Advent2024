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
//Hashtable memo = new Hashtable();
var l = lines.Length;
long A = 0;
long B = 0; long C = 0;
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
        var x = lines[i].Remove(0, 9);
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
long dif = 0;
long antbun = 0;
while (true)
{
    output = new List<int>();
    A = initialA + count;
    B = 0;
    C = 0;
    //  if(A%101 == 0)
    //    Console.ReadKey();
    
    Output();
    if (output.Count >= 16)
    {
        string binary = Convert.ToString((initialA + count), 8);
        Console.WriteLine(output.Count + " " + (initialA + count)+ " "+(initialA+count-antbun) +" -->"+Ss(output)+ "B: "+ binary);

        string Ss(List<int> ints)
        {
            var s = "";
            foreach (var i in ints)
            {
                s= s+ (i + ",");
            }

            return s;
        }

        antbun = initialA + count;
    }

    if (EqualOI())
    {
        Console.WriteLine(initialA + count);
        break;
    }



    count += 2;
}

Console.WriteLine(A);
bool EqualOI()
{
    if (output.Count != instr.Count)
        return false;
    for (int i = 0; i < instr.Count; i++)
    {
        if (instr[i] != output[i])
            return false;
    }
    return true;
}


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
        else if (jumps == long.MaxValue)
        {
            break;
        }
        else
        {
            ind = jumps;
        }
        if (ind >= instr.Count)
            break;
    }
}

long Solve(int inst, long op)
{
    switch (inst)
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
            var o = (int)(Combo(op) % 8);
            output.Add(o);
            if (instr[output.Count-1] != o)
            {
                return long.MaxValue;
            }
            else
            {
                
            }

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
    Console.Write(o + ",");
}
Console.ReadKey();

long Combo(long op)
{
    if (op >= 0 && op <= 3)
        return op;
    if (op == 4)
        return A;
    if (op == 5)
        return B;
    if (op == 6)
        return C;
    return 0;
}
//Console.WriteLine(sum);
