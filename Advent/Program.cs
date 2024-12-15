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

string[] lines = File.ReadAllLines("input.txt");
var l = lines.Length;
//Hashtable memo = new Hashtable();
char[] instr = new char[1];
long sum = 0;
char[][] map = new char[lines[0].Length][];
//citire date
for (int i = 0; i < l; i++)
{
    if (lines[i].Length>0 && lines[i][0] == '#')
    {
        map[i] = lines[i].ToCharArray();
    }
    else if (!string.IsNullOrEmpty(lines[i]))
    {
        instr = lines[i].ToCharArray();
    }
}

var ll = lines[0].Length;
int ri=0, rj=0;
//procesare date
for (int i = 0; i < ll; i++)
{
    for (int j = 0; j < ll; j++)
    {
        if (map[i][j] == '@')
        {
            ri = i;
            rj = j;
        }
    }
}

for (int i = 0; i < instr.Length; i++)
{
    switch (instr[i])
    {
        case '^':
            MoveUp();
            break;
        case '>':
            MoveRight();
            break;
        case '<':
            MoveLeft();
            break;
        case 'v':
            MoveDown();
            break;
    }

    //Console.WriteLine(instr[i]);
    //for (int ii = 0; ii < ll; ii++)
    //{
    //    for (int jj = 0; jj < ll; jj++)
    //    {
    //        Console.Write(map[ii][jj]);
    //    }
    //    Console.WriteLine();
    //}
}




for (int i = 0; i < ll; i++)
{
    for (int j = 0; j < ll; j++)
    {
        if (map[i][j] == 'O')
            sum += 100 * i + j;
    }
}


Console.WriteLine(sum);
void MoveDown()
{
    switch (map[ri + 1][rj])
    {
        case '.':
            map[ri][rj] = '.';
            ri = ri + 1;
            map[ri][rj] = '@';
            break;
        case '#':
            break;
        case 'O':
            MoveColumnDown();   
            break;
    }
}
void MoveLeft()
{
    switch (map[ri][rj-1])
    {
        case '.':
            map[ri][rj] = '.';
            rj = rj - 1;
            map[ri][rj] = '@';
            break;
        case '#':
            break;
        case 'O':
            MoveColumnLeft();
            break;
    }
}
void MoveRight()
{
    switch (map[ri][rj+1])
    {
        case '.':
            map[ri][rj] = '.';
            rj = rj + 1;
            map[ri][rj] = '@';
            break;
        case '#':
            break;
        case 'O':
            MoveColumnRight();
            break;
    }
}
void MoveUp()
{
    switch (map[ri - 1][rj])
    {
        case '.':
            map[ri][rj] = '.';
            ri = ri - 1;
            map[ri][rj] = '@';
            break;
        case '#':
            break;
        case 'O':
            MoveColumnUp();
            break;
    }
}

void MoveColumnUp()
{
    int c = 1;
    while (map[ri - c][rj] == 'O')
        c++;
    if (map[ri - c][rj] == '.')
    {
        map[ri - c][rj] = 'O';
        map[ri][rj] = '.';
        ri = ri - 1;
        map[ri][rj] = '@';
    }
}

void MoveColumnDown()
{
    int c = 1;
    while (map[ri + c][rj] == 'O')
        c++;
    if (map[ri + c][rj] == '.')
    {
        map[ri + c][rj] = 'O';
        map[ri][rj] = '.';
        ri = ri + 1;
        map[ri][rj] = '@';
    }
}

void MoveColumnLeft()
{
    int c = 1;
    while (map[ri][rj-c] == 'O')
        c++;
    if (map[ri][rj-c] == '.')
    {
        map[ri][rj-c] = 'O';
        map[ri][rj] = '.';
        rj = rj - 1;
        map[ri][rj] = '@';
    }
}

void MoveColumnRight()
{
    int c = 1;
    while (map[ri][rj + c] == 'O')
        c++;
    if (map[ri][rj + c] == '.')
    {
        map[ri][rj + c] = 'O';
        map[ri][rj] = '.';
        rj = rj + 1;
        map[ri][rj] = '@';
    }
}
Console.WriteLine(sum);
