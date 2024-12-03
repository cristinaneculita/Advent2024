// See https://aka.ms/new-console-template for more information

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;

string[] lines = File.ReadAllLines("input.txt");
var l = lines.Length;
long sum = 0;

for (int i = 0; i < lines.Length; i++)
{
    var index = 0;
    while (index < lines[i].Length)
    {
        index = lines[i].IndexOf("mul", index);
        if (index == -1)
        {
            break;
        }

        if (lines[i][index + 3] != '(')
        {
            index += 3;
            continue;
        }

        if (!isNum(lines[i][index + 4]))
        {
            index += 3;
            continue;
        }

        int c = 4;
        var nr1 = "";
        while (isNum(lines[i][index + c]))
        {
            nr1 += lines[i][index + c];
            c++;

        }


        if (nr1.Length >3)
        {
            index += 6;
            continue;
        }

        if (lines[i][index + c] != ',')
        {
            index += c;
            continue;
        }

        c++;
        if (!isNum(lines[i][index + c]))
        {
            index += c;
            continue;
        }

        var nr2 = "";
        while (isNum(lines[i][index + c]))
        {
            nr2 += lines[i][index + c];
            c++;

        }

        if (nr2.Length>3)
        {
            index += c;
            continue;
        }

        if (lines[i][index + c] != ')')
        {
            index+= c;
            continue;
        }

        sum += int.Parse(nr1) * int.Parse(nr2);
        index += c;
    }

    
}
Console.WriteLine(sum);
bool isNum(char c)
{
    return (c >= '0' && c<='9');
}






