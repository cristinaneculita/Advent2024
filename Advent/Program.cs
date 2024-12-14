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
using System.Xml.Schema;

    string[] lines = File.ReadAllLines("input.txt");
var l = lines.Length;
//Hashtable memo = new Hashtable();
var xmax = 101;
var ymax = 103;
//var xmax = 11;
//var ymax = 7;
long sum = 0;
var robots = new List<Robot>();
bool[][] imagine = new bool[xmax][];
for (int i = 0; i < xmax; i++)
{
    imagine[i] = new bool[ymax];
    for (int j = 0; j < ymax; j++)
    {
        imagine[i][j] = false;
    }

}
//citire date
for (int i = 0; i < l; i++)
{
    var x = lines[i].Split(',');
    var t1 = int.Parse(x[0].Substring(2));
    var y = x[1].Split(" ");
    var t2 = int.Parse(y[0]);
    var t3 = int.Parse(y[1].Substring(2));
    var t4 = int.Parse(x[2]);
    Robot r = new Robot(t1, t2, t3, t4);
    robots.Add(r);
}
//procesare date

for (int i = 0; i < 10000; i++)
{
    Console.WriteLine(i);
    foreach (var robot in robots)
    {
        Step(robot);
    }

    if (((i - 62) % 103 == 0) || ((i - 81) % 101 == 0))
    {
        Write(robots);
        Console.ReadKey();
    }
}
   


var sfert1 = 0;
var sfert2 = 0;
var sfert3 = 0;
var sfert4 = 0;
var medx = xmax / 2;
var medy = ymax / 2;

foreach (var robot in robots)
{
    if (robot.Posx < medx && robot.Posy < medy)
        sfert1++;
    if (robot.Posx > medx && robot.Posy < medy)
        sfert2++;
    if (robot.Posx < medx && robot.Posy > medy)
        sfert3++;
    if (robot.Posx > medx && robot.Posy > medy)
        sfert4++;
}

Console.WriteLine(sfert1 * sfert2 * sfert3 * sfert4);
void Step(Robot robot)
{
    robot.Posx = (robot.Posx + robot.Velx) % xmax;
    if (robot.Posx < 0)
        robot.Posx = xmax + robot.Posx;
    robot.Posy = (robot.Posy + robot.Vely) % ymax;
    if (robot.Posy < 0)
        robot.Posy = ymax + robot.Posy;
}
void Write(List<Robot> list)
{
    for (int i = 0; i < ymax; i++)
    {
        for (int j = 0; j < xmax; j++)
        {
            imagine[j][i] = false;
        }
    }

    foreach (var robot in list)
    {
        imagine[robot.Posx][robot.Posy] = true;
    }

    for (int i = 0; i < ymax; i++)
    {
       // Console.Write(i);
        for (int j = 0; j < xmax; j++)
        {
            if (imagine[j][i])
                Console.Write('#');
            else
                Console.Write('.');
        }

        Console.WriteLine();
    }
}
public class Robot
{
    public int Posx { get; set; }
    public int Posy { get; set; }
    public int Velx { get; set; }
    public int Vely { get; set; }

    public Robot()
    {

    }

    public Robot(int posx, int posy, int velx, int vely)
    {
        Posx = posx;
        Posy = posy;
        Velx = velx;
        Vely = vely;
    }
}
