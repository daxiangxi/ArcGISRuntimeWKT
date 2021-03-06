﻿using System;
using System.IO;
using System.Reflection;
using ArcGISRuntimeWKT;
using Esri.ArcGISRuntime.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArcGISRuntimeWKTTests
{
    [TestClass]
    public class WktFileTests : EsriBaseTest
    {
        [TestMethod]
        public void TestWkTfiles()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var root = "ArcGISRuntimeWKTTests.wktsForTesting.";
            string[] paths =
            {
                root + @"DataDoors187_aoi_wktOutputFile.txt",
                root + @"Huge_Point_Poly_test_wktOutputFile.txt",
                root + @"NYS_wktOutputFile.txt",
                root + @"zz_wktOutputFile.txt"
            };

            foreach (var path in paths)
            {
                Console.WriteLine("Current WKT file: ", path);
                var resource = assembly.GetManifestResourceStream(path);

                using (var reader = new StreamReader(resource))
                {
                    var line = reader.ReadToEnd();

                    Console.WriteLine("\tWKT:\n{0}", line);
                    var geom = Parser.GeometryFromWkt(line);
                    var poly = geom as Polygon;
                    var newWkt = Parser.GeometryToWkt(geom);
                    Console.WriteLine(
                        "\tA polygon was created from the above WKT. Then WKT was extracted from that object. THe new WKT is:\n{0}",
                        newWkt);
                    var geom2 = Parser.GeometryFromWkt(newWkt);
                    var poly2 = geom2 as Polygon;

                    PolygonComparer.Compare(poly, poly2);
                    PolygonComparer.Compare(geom, geom2);
                    var sameWkt = PolygonComparer.Compare(line, newWkt);
                    if (sameWkt)
                        Console.WriteLine("The WKT's are the same!!!!!");
                    else
                        Console.WriteLine("The WKT's are NOT the same. Sad day.");
                }
            }
        }
    }
}