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
long sum = 0;
int[] val = new int[lines[0].Length];
//citire date
for (int i = 0; i < l; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        val[j] = int.Parse(lines[i][j].ToString());
    }
}

long indmic = 0;
long indmare = 0;
long indmicdr = val.Length-1;
long indmaredr = val.Length / 2;
long pusiDinDr = 0;
long many = HowManyInBigArray();

long HowManyInBigArray()
{
    long s = 0;
    for (int i = 0; i < val.Length; i+=2)
    {
        s += val[i];
    }
    return s;
}

//prelucrare date
for (int i = 0; i < val.Length; i++)
{
    if (i % 2 == 0)
    {
        for (int j = 0; j < val[i]; j++)
        {
            sum += indmare * indmic;
            indmare++;
            if (indmare == many)
                break;
        }
        indmic++;
    }
    else
    {
        for (int j = 0; j < val[i]; j++)
        {
            //var rm = FindRightmost();
            sum += indmaredr * indmare;
            pusiDinDr++;
            if (pusiDinDr == val[indmicdr])
            {
                pusiDinDr = 0;
                indmicdr -= 2;
                indmaredr -= 1;
            }

            indmare++;
          
        }
    }

    if (indmare == many)
        break;
}

Console.WriteLine(sum);
long FindRightmost()
{
    return indmaredr;

}