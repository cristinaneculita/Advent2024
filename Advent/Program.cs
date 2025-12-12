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
 Dictionary<(long, long), bool> pointInsideCache = new Dictionary<(long, long), bool>();

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
//var intpoints = new List<Point>();
//for (int i = 0; i < points.Count-1; i++)
//{
//    Console.WriteLine("points "+i+" out of "+l);
//    if (points[i].X == points[i + 1].X)
//    {
//         long min = points[i].Y < points[i + 1].Y ? points[i].Y : points[i + 1].Y;
//         long max = points[i].Y > points[i + 1].Y ? points[i].Y : points[i + 1].Y;
//         for (long j = min+1; j < max; j++)
//         {
//             if(!intpoints.Any(p=>p.X==points[i].X && p.Y==j))
//                 intpoints.Add( new Point(){ X= points[i].X, Y=j});
//         }
//    }
//    if (points[i].Y == points[i + 1].Y)
//    {
//        long min = points[i].X < points[i + 1].X ? points[i].X : points[i + 1].X;
//        long max = points[i].X > points[i + 1].X ? points[i].X : points[i + 1].X;
//        for (long j = min + 1; j < max; j++)
//        {
//            if (!intpoints.Any(p => p.X ==j && p.Y == points[i].Y))
//                intpoints.Add(new Point() { X = j, Y = points[i].Y });
//        }
//    }
//}

//if (points[0].X == points[l-1].X)
//{
//    long min = points[0].Y < points[l-1].Y ? points[0].Y : points[l-1].Y;
//    long max = points[0].Y > points[l - 1].Y ? points[0].Y : points[l - 1].Y;
//    for (long j = min + 1; j < max; j++)
//    {
//        if (!intpoints.Any(p => p.X == points[0].X && p.Y == j))
//            intpoints.Add(new Point() { X = points[0].X, Y = j });
//    }
//}
//if (points[0].Y == points[l - 1].Y)
//{
//    long min = points[0].X < points[l - 1].X ? points[0].X : points[l - 1].X;
//    long max = points[0].X > points[l - 1].X ? points[0].X : points[l - 1].X;
//    for (long j = min + 1; j < max; j++)
//    {
//        if (!intpoints.Any(p => p.X == j && p.Y == points[0].Y))
//            intpoints.Add(new Point() { X = j, Y = points[0].Y });
//    }
//}

// All contour points include both original points and interpolated points
var allContourPoints = new HashSet<(long, long)>();
var edges = new HashSet<(long, long)>();
var lns = new HashSet<(long, long)>();
// Add all original points
//foreach (var p in points)
//{
//    allContourPoints.Add((p.X, p.Y));
//    edges.Add((p.X, p.Y));
//}

//// Add all interpolated points
//foreach (var p in intpoints)
//{
//    allContourPoints.Add((p.X, p.Y));
//    lns.Add((p.X, p.Y));
//}



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

    if (!IsPointInsideContour(minx, miny, points) ||
        !IsPointInsideContour(maxx, miny, points) ||
        !IsPointInsideContour(minx, maxy, points) ||
        !IsPointInsideContour(maxx, maxy, points))
        return false;

    // Check edges more sparsely
    long stepSize = Math.Max(1, (maxx - minx) / 10); // Sample every 10th point

    for (long x = minx; x <= maxx; x += stepSize)
    {
        if (!IsPointInsideContour(x, miny, points) ||
            !IsPointInsideContour(x, maxy, points))
            return false;
    }

    for (long y = miny; y <= maxy; y += stepSize)
    {
        if (!IsPointInsideContour(minx, y, points) ||
            !IsPointInsideContour(maxx, y, points))
            return false;
    }

    for (long i = minx; i <= maxx; i++)
    {
        for (long j = miny; j <= maxy; j++)
        {
            if (!IsPointInsideContour(i, j, points))
                return false;
        }
    }

    return true;
}
bool IsPointInsideContour(long x, long y, List<Point> points)
{
    // Check cache first
    var cacheKey = (x, y);
    if (pointInsideCache.TryGetValue(cacheKey, out bool cachedResult))
    {
        return cachedResult;
    }

    // Check if point is exactly on a vertex
    if (points.Any(p => p.X == x && p.Y == y))
    {
        pointInsideCache[cacheKey] = true;
        return true;
    }

    // Check if point is on an edge
    for (int i = 0; i < points.Count; i++)
    {
        var p1 = points[i];
        var p2 = points[(i + 1) % points.Count];

        // Check if point is on this edge
        if (IsPointOnEdge(x, y, p1.X, p1.Y, p2.X, p2.Y))
        {
            pointInsideCache[cacheKey] = true;
            return true;
        }
    }

    // Ray casting algorithm with proper handling of edge cases
    int intersections = 0;

    for (int i = 0; i < points.Count; i++)
    {
        var p1 = points[i];
        var p2 = points[(i + 1) % points.Count];

        // Skip horizontal edges (they don't contribute to crossings)
        if (p1.Y == p2.Y)
            continue;

        // Ensure p1.Y < p2.Y for consistent processing
        if (p1.Y > p2.Y)
        {
            (p1, p2) = (p2, p1); // Swap
        }

        // Check if ray intersects this edge
        // Ray must be strictly between p1.Y and p2.Y (not touching endpoints)
        if (y > p1.Y && y <= p2.Y)
        {
            // Calculate intersection point
            long intersectX = p1.X + ((y - p1.Y) * (p2.X - p1.X)) / (p2.Y - p1.Y);

            // Count intersection if it's to the right of our point
            if (x < intersectX)
            {
                intersections++;
            }
        }
    }

    bool result = (intersections % 2) == 1;
    pointInsideCache[cacheKey] = result;
    return result;
}

// Helper method to check if point is on edge
bool IsPointOnEdge(long px, long py, long x1, long y1, long x2, long y2)
{
    // Check if point is on line segment
    if (x1 == x2) // Vertical line
    {
        return px == x1 && py >= Math.Min(y1, y2) && py <= Math.Max(y1, y2);
    }
    else if (y1 == y2) // Horizontal line  
    {
        return py == y1 && px >= Math.Min(x1, x2) && px <= Math.Max(x1, x2);
    }
    else
    {
        // General line - check if point lies on line segment
        // Using cross product to check collinearity and range check
        long crossProduct = (py - y1) * (x2 - x1) - (px - x1) * (y2 - y1);
        if (crossProduct != 0) return false; // Not collinear

        // Check if point is within segment bounds
        return px >= Math.Min(x1, x2) && px <= Math.Max(x1, x2) &&
               py >= Math.Min(y1, y2) && py <= Math.Max(y1, y2);
    }
}
class Point
{
    public long X { get; set; }
    public long Y { get; set; }

}
