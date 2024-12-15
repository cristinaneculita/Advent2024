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
    if (lines[i].Length > 0 && lines[i][0] == '#')
    {
        map[i] = new char[2 * lines[i].Length];
        for (int j = 0; j < lines[i].Length; j++)
        {
            switch (lines[i][j])
            {
                case '#':
                    map[i][2 * j] = '#';
                    map[i][2 * j + 1] = '#';
                    break;
                case 'O':
                    map[i][2 * j] = '[';
                    map[i][2 * j + 1] = ']';
                    break;
                case '.':
                    map[i][2 * j] = '.';
                    map[i][2 * j + 1] = '.';
                    break;
                case '@':
                    map[i][2 * j] = '@';
                    map[i][2 * j + 1] = '.';
                    break;
            }
        }
    }
    else if (!string.IsNullOrEmpty(lines[i]))
    {
        instr = lines[i].ToCharArray();
    }
}
int iurm = 0;
int jurmfrom = 0;
int jurmto = 0;
var ll = lines[0].Length;
var cc = 2 * ll;
int ri = 0, rj = 0;
var agatate = new List<Box>();
//procesare date
for (int i = 0; i < ll; i++)
{
    for (int j = 0; j < cc; j++)
    {
        if (map[i][j] == '@')
        {
            ri = i;
            rj = j;
        }
    }
}

for (int i = 0; i < ll; i++)
{
    for (int j = 0; j < cc; j++)
    {
        Console.Write(map[i][j]);
    }

    Console.WriteLine();
}

Console.WriteLine(cutii());

int cutii()
{
    var cut = 0;
    for (int i = 0; i < ll; i++)
    {
        for (int j = 0; j < cc; j++)
        {
            if (map[i][j] == '[')
                cut++;
        }
    }
    return cut;
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
    
    if(i<instr.Length-1)
        Console.WriteLine(instr[i + 1]);
}
Print();
int cate = 0;
for (int i = 0; i < ll; i++)
{
    for (int j = 0; j < cc; j++)
    {
        if (map[i][j] == '[')
        {
            cate++;
            sum += 100 * i + j;
        }
    }
}
Print();
Console.WriteLine(ri + " " + rj);
Console.WriteLine(sum);
Console.WriteLine(cate);
void Print()
{
    for (int ii = 0; ii < ll; ii++)
    {
        for (int jj = 0; jj < cc; jj++)
        {
            Console.Write(map[ii][jj]);
        }
        Console.WriteLine();
    }
}



//for (int i = 0; i < ll; i++)
//{
//    for (int j = 0; j < ll; j++)
//    {
//        if (map[i][j] == 'O')
//            sum += 100 * i + j;
//    }
//}


//Console.WriteLine(sum);
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
        case ']':
            agatate = new List<Box>();
            MoveColumnDown(']');
            break;
        case '[':
            agatate = new List<Box>();
            MoveColumnDown('[');
            break;
    }
}
void MoveLeft()
{
    switch (map[ri][rj - 1])
    {
        case '.':
            map[ri][rj] = '.';
            rj = rj - 1;
            map[ri][rj] = '@';
            break;
        case '#':
            break;
        case ']':
            MoveColumnLeft();
            break;
    }
}
void MoveRight()
{
    switch (map[ri][rj + 1])
    {
        case '.':
            map[ri][rj] = '.';
            rj = rj + 1;
            map[ri][rj] = '@';
            break;
        case '#':
            break;
        case '[':
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
        case ']':
            agatate = new List<Box>();
            MoveColumnUp(']');
            break;
        case '[':
            agatate = new List<Box>();
            MoveColumnUp('[');
            break;
    }
}

void MoveColumnUp(char car)
{
    int c = 1;
    if (car == ']')
    {
        agatate.Add(new Box(ri - 1, rj - 1));
    }

    if (car == '[')
    {
        agatate.Add(new Box(ri - 1, rj));
    }

    char f = '-';
    while (true)
    {
        f = BoxInWayUp(ri - c);
        if (f == '#')
            break;
        if (f == '.')
            break;
        c++;
    }

    if (f == '.')
    {
        ShiftAllUp();
        ri = ri - 1;
        map[ri][rj] = '@';
        map[ri + 1][rj] = '.';
    }
}

void ShiftAllUp()
{
    agatate.Reverse();
    foreach (var box in agatate)
    {
        map[box.i - 1][box.jfrom] = map[box.i][box.jfrom];
        map[box.i - 1][box.jfrom + 1] = map[box.i][box.jfrom + 1];
        map[box.i][box.jfrom] = '.';
        map[box.i][box.jfrom + 1] = '.';
        //Console.WriteLine("am mutat " + agatate.IndexOf(box));
        //Print();
    }
}
char BoxInWayUp(int i)
{
    var agatateLinie = agatate.Where(b => b.i == i).ToList();
    var found = false;
    foreach (var box in agatateLinie)
    {
        if (map[box.i - 1][box.jfrom] == ']')
        {
            if (!agatate.Any(b => b.i == box.i - 1 && b.jfrom == box.jfrom - 1))
                agatate.Add(new Box(box.i - 1, box.jfrom - 1));
            found = true;
        }
        if (map[box.i - 1][box.jfrom] == '[')
        {
            if (!agatate.Any(a => a.i == box.i - 1 && a.jfrom == box.jfrom))
                agatate.Add(new Box(box.i - 1, box.jfrom));
            found = true;
        }
        if (map[box.i - 1][box.jfrom] == '#')
        {
            return '#';
        }

        if (map[box.i - 1][box.jfrom + 1] == ']')
        {
            if (!agatate.Any(a => a.i == box.i - 1 && a.jfrom == box.jfrom))
                agatate.Add(new Box(box.i - 1, box.jfrom));
            found = true;
        }
        if (map[box.i - 1][box.jfrom + 1] == '[')
        {
            if (!agatate.Any(a => a.i == box.i - 1 && a.jfrom == box.jfrom + 1))
                agatate.Add(new Box(box.i - 1, box.jfrom + 1));
            found = true;
        }
        if (map[box.i - 1][box.jfrom + 1] == '#')
        {
            return '#';
        }
    }
    //for (int j = jurmfromc; j <= jurmtoc; j++)
    //{
    //    if (map[i][j] == ']')
    //    {
    //        if (map[i - 1][j] == '[')
    //        {
    //            jurmto++;
    //            found = true;
    //        }
    //        else if (map[i - 1][j] == '#')
    //            return '#';
    //        else if (map[i - 1][j] == ']')
    //            found = true;
    //    }
    //    if (map[i][j] == '[')
    //    {
    //        if (map[i - 1][j] == ']')
    //        {
    //            jurmfrom--;
    //            found = true;
    //        }
    //        else if (map[i - 1][j] == '#')
    //            return '#';
    //        else if (map[i - 1][j] == '[')
    //            found = true;
    //    }

    //}

    if (found)
        return 'O';
    else return '.';
}
void MoveColumnDown(char car)
{
    int c = 1;
    if (car == ']')
    {
        agatate.Add(new Box(ri + 1, rj - 1));
    }

    if (car == '[')
    {
        agatate.Add(new Box(ri + 1, rj));
    }

    char f = '-';
    while (true)
    {
        f = BoxInWayDown(ri + c);

        if (f == '#')
            break;
        if (f == '.')
            break;
        c++;
    }

    if (f == '.')
    {
        ShiftAllDown();
        ri = ri + 1;
        map[ri][rj] = '@';
        map[ri - 1][rj] = '.';
    }
}
void ShiftAllDown()
{
    agatate.Reverse();
    foreach (var box in agatate)
    {
        map[box.i + 1][box.jfrom] = map[box.i][box.jfrom];
        map[box.i + 1][box.jfrom + 1] = map[box.i][box.jfrom + 1];
        map[box.i][box.jfrom] = '.';
        map[box.i][box.jfrom + 1] = '.';
        //Console.WriteLine("am mutat " + agatate.IndexOf(box));
        // Print();
    }
}
char BoxInWayDown(int i)
{
    var agatateLinie = agatate.Where(b => b.i == i).ToList();
    var found = false;
    foreach (var box in agatateLinie)
    {
        if (map[box.i + 1][box.jfrom] == ']')
        {
            if (!agatate.Any(a => a.i == box.i + 1 && a.jfrom == box.jfrom - 1))
                agatate.Add(new Box(box.i + 1, box.jfrom - 1));
            found = true;
        }
        if (map[box.i + 1][box.jfrom] == '[')
        {
            if (!agatate.Any(a => a.i == box.i + 1 && a.jfrom == box.jfrom))
                agatate.Add(new Box(box.i + 1, box.jfrom));
            found = true;
        }
        if (map[box.i + 1][box.jfrom] == '#')
        {
            return '#';
        }

        if (map[box.i + 1][box.jfrom + 1] == ']')
        {
            if (!agatate.Any(a => a.i == box.i + 1 && a.jfrom == box.jfrom))
                agatate.Add(new Box(box.i + 1, box.jfrom));
            found = true;
        }
        if (map[box.i + 1][box.jfrom + 1] == '[')
        {
            if (!agatate.Any(a => a.i == box.i + 1 && a.jfrom == box.jfrom+1))
                agatate.Add(new Box(box.i + 1, box.jfrom + 1));
            found = true;
        }
        if (map[box.i + 1][box.jfrom + 1] == '#')
        {
            return '#';
        }
    }
    //for (int j = jurmfromc; j <= jurmtoc; j++)
    //{
    //    if (map[i][j] == ']')
    //    {
    //        if (map[i - 1][j] == '[')
    //        {
    //            jurmto++;
    //            found = true;
    //        }
    //        else if (map[i - 1][j] == '#')
    //            return '#';
    //        else if (map[i - 1][j] == ']')
    //            found = true;
    //    }
    //    if (map[i][j] == '[')
    //    {
    //        if (map[i - 1][j] == ']')
    //        {
    //            jurmfrom--;
    //            found = true;
    //        }
    //        else if (map[i - 1][j] == '#')
    //            return '#';
    //        else if (map[i - 1][j] == '[')
    //            found = true;
    //    }

    //}

    if (found)
        return 'O';
    else return '.';
}
void MoveColumnLeft()
{
    int c = 1;
    while (map[ri][rj - c] == ']' || map[ri][rj - c] == '[')
        c++;
    if (map[ri][rj - c] == '.')
    {
        do
        {
            map[ri][rj - c] = '[';
            map[ri][rj - c + 1] = ']';
            c -= 2;
        } while (map[ri][rj - c + 1] != '@');

        map[ri][rj] = '.';
        rj = rj - 1;
        map[ri][rj] = '@';
    }
}

void MoveColumnRight()
{
    int c = 1;
    while (map[ri][rj + c] == ']' || map[ri][rj + c] == '[')
        c++;
    if (map[ri][rj + c] == '.')
    {
        do
        {
            map[ri][rj + c] = ']';
            map[ri][rj + c - 1] = '[';
            c -= 2;
        } while (map[ri][rj + c - 1] != '@');

        map[ri][rj] = '.';
        rj = rj + 1;
        map[ri][rj] = '@';
    }
}

//Console.WriteLine(sum);



class Box
{
    public int jfrom { get; set; }
    public int i { get; set; }

    public Box(int i, int jfrom)
    {
        this.jfrom = jfrom;
        this.i = i;
    }
}