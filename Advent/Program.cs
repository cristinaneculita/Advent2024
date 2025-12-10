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
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

string[] lines = File.ReadAllLines("input.txt");

var l= lines.Length;
var points = new List<Point>();
for (int i = 0; i < l; i++)
{
    var x = lines[i].Split(',');
    points.Add(new Point()
    {
        X= int.Parse(x[0]),
        Y = int.Parse(x[1])
    });
}
var intpoints = new List<Point>();
for (int i = 0; i < points.Count-1; i++)
{
    Console.WriteLine("points "+i+" out of "+l);
    if (points[i].X == points[i + 1].X)
    {
         long min = points[i].Y < points[i + 1].Y ? points[i].Y : points[i + 1].Y;
         long max = points[i].Y > points[i + 1].Y ? points[i].Y : points[i + 1].Y;
         for (long j = min+1; j < max; j++)
         {
             if(!intpoints.Any(p=>p.X==points[i].X && p.Y==j))
                 intpoints.Add( new Point(){ X= points[i].X, Y=j});
         }
    }
    if (points[i].Y == points[i + 1].Y)
    {
        long min = points[i].X < points[i + 1].X ? points[i].X : points[i + 1].X;
        long max = points[i].X > points[i + 1].X ? points[i].X : points[i + 1].X;
        for (long j = min + 1; j < max; j++)
        {
            if (!intpoints.Any(p => p.X ==j && p.Y == points[i].Y))
                intpoints.Add(new Point() { X = j, Y = points[i].Y });
        }
    }
}

if (points[0].X == points[l-1].X)
{
    long min = points[0].Y < points[l-1].Y ? points[0].Y : points[l-1].Y;
    long max = points[0].Y > points[l - 1].Y ? points[0].Y : points[l - 1].Y;
    for (long j = min + 1; j < max; j++)
    {
        if (!intpoints.Any(p => p.X == points[0].X && p.Y == j))
            intpoints.Add(new Point() { X = points[0].X, Y = j });
    }
}
if (points[0].Y == points[l - 1].Y)
{
    long min = points[0].X < points[l - 1].X ? points[0].X : points[l - 1].X;
    long max = points[0].X > points[l - 1].X ? points[0].X : points[l - 1].X;
    for (long j = min + 1; j < max; j++)
    {
        if (!intpoints.Any(p => p.X == j && p.Y == points[0].Y))
            intpoints.Add(new Point() { X = j, Y = points[0].X });
    }
}

// All contour points include both original points and interpolated points
var allContourPoints = new HashSet<(long, long)>();
var edges = new HashSet<(long, long)>();
var lns = new HashSet<(long, long)>();
// Add all original points
foreach (var p in points)
{
    allContourPoints.Add((p.X, p.Y));
    edges.Add((p.X, p.Y));
}

// Add all interpolated points
foreach (var p in intpoints)
{
    allContourPoints.Add((p.X, p.Y));
    lns.Add((p.X, p.Y));
}



long areamax  = 0;
for (int i = 0; i < points.Count; i++)
{
    Console.WriteLine("i "+i);
    for (int j = i+1; j < points.Count; j++)
    {
        Console.WriteLine("j "+j);
        var area = (Math.Abs(points[i].X - points[j].X) + 1) * (Math.Abs(points[i].Y - points[j].Y)+1);
        if (area > areamax && AllPointsInContour(points[i].X, points[i].Y, points[j].X, points[j].Y))
        {
            areamax = area;
            Console.WriteLine("temp "+areamax);
        }

      
    }
}

Console.WriteLine(areamax);
bool AllPointsInContour(long x1, long y1, long x2, long y2)
{
    var minx = x1<x2? x1:x2;
    var maxx = x1 > x2 ? x1 : x2;
    var miny = y1 < y2 ? y1 : y2;
    var maxy = y1 > y2 ? y1 : y2;
    for (long i = minx; i <= maxx; i++)
    {
        for (long j = miny; j <= maxy; j++)
        {
            if (!IsPointInsideContour(i, j, points, intpoints))
                return false;
        }
    }

    return true;
}
 bool IsPointInsideContour(long x, long y, List<Point> points, List<Point> intpoints)
{

    // If point is exactly on the contour, it's inside
    if (allContourPoints.Contains((x, y)))
        return true;

    // Count contour points to the left, but handle edge cases
    var leftCount = 0;

    // Count original points (edges) to the left on the same Y line
    var edgesOnLineToLeft = points.Where(p => p.Y == y && p.X < x).OrderBy(p => p.X).ToList();

    // Process each edge point to determine if we should count it
    foreach (var edgePoint in edgesOnLineToLeft)
    {
        // Find the index of this edge point in the original points list
        var edgeIndex = points.FindIndex(p => p.X == edgePoint.X && p.Y == edgePoint.Y);

        if (edgeIndex != -1)
        {
            // Get previous and next points to determine line directions
            var prevPoint = points[(edgeIndex - 1 + points.Count) % points.Count];
            var nextPoint = points[(edgeIndex + 1) % points.Count];

            // Determine directions of lines coming from this edge
            var prevDirection = prevPoint.Y > y ? 1 : (prevPoint.Y < y ? -1 : 0);
            var nextDirection = nextPoint.Y > y ? 1 : (nextPoint.Y < y ? -1 : 0);

            // If both directions are the same (both up or both down), skip this edge
            if (prevDirection == nextDirection && prevDirection != 0)
            {
                // Same direction - don't count this edge
                continue;
            }
            // If directions are opposite or one is horizontal, count as 1
            else
            {
                leftCount += 1;
            }
        }
    }

    // Count interpolated points to the left (these are always counted normally)
    var interpolatedToLeft = intpoints.Where(p => p.Y == y && p.X < x);

    // Group consecutive interpolated points and handle them as line segments
    var groupedInterpolated = new List<List<Point>>();
    var currentGroup = new List<Point>();

    foreach (var point in interpolatedToLeft.OrderBy(p => p.X))
    {
        if (currentGroup.Count == 0 || point.X == currentGroup.Last().X + 1)
        {
            currentGroup.Add(point);
        }
        else
        {
            groupedInterpolated.Add(currentGroup);
            currentGroup = new List<Point> { point };
        }
    }
    if (currentGroup.Count > 0)
        groupedInterpolated.Add(currentGroup);

    // For each group of consecutive interpolated points, check edge directions
    foreach (var group in groupedInterpolated)
    {
        var startX = group.First().X;
        var endX = group.Last().X;

        // Find the edges that bound this line segment
        var leftEdge = points.Where(p => p.X < startX && p.Y == y).OrderByDescending(p => p.X).FirstOrDefault();
        var rightEdge = points.Where(p => p.X > endX && p.Y == y).OrderBy(p => p.X).FirstOrDefault();

        if (leftEdge != null && rightEdge != null)
        {
            // Get directions from both edges
            var leftEdgeIndex = points.FindIndex(p => p.X == leftEdge.X && p.Y == leftEdge.Y);
            var rightEdgeIndex = points.FindIndex(p => p.X == rightEdge.X && p.Y == rightEdge.Y);

            if (leftEdgeIndex != -1 && rightEdgeIndex != -1)
            {
                var leftPrev = points[(leftEdgeIndex - 1 + points.Count) % points.Count];
                var leftNext = points[(leftEdgeIndex + 1) % points.Count];
                var rightPrev = points[(rightEdgeIndex - 1 + points.Count) % points.Count];
                var rightNext = points[(rightEdgeIndex + 1) % points.Count];

                var leftOutDirection = leftNext.Y > y ? 1 : (leftNext.Y < y ? -1 : 0);
                var rightOutDirection = rightPrev.Y > y ? 1 : (rightPrev.Y < y ? -1 : 0);

                if (leftOutDirection == rightOutDirection && leftOutDirection != 0)
                {
                    // Same direction - don't count any points in this line segment
                    continue;
                }
                else
                {
                    // Opposite directions - count as 1 regardless of how many points
                    leftCount += 1;
                }
            }
        }
        else
        {
            // No bounding edges found, count all points normally
            leftCount += group.Count;
        }
    }

    // If odd number of effective crossings, point is inside
    return leftCount % 2 == 1;
}

class Point
{
    public long X { get; set; }
    public long Y { get; set; }

}
