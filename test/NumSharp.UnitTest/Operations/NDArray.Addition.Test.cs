using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NumSharp.Core;

namespace NumSharp.UnitTest.Operations
{
    [TestClass]
    public class NDArrayAdditionTest
    {

        [TestMethod]
        public void DoubleTwo1D_NDArrayAddition()
        {
            var np1 = new NDArrayGeneric<double>().arange(4, 1);
            var np2 = new NDArrayGeneric<double>().arange(5, 2);

            var np3 = np1 + np2;

            Assert.IsTrue(Enumerable.SequenceEqual(new double[] { 3, 5, 7 }, np3.Data));
        }

        [TestMethod]
        public void ComplexTwo1D_NDArrayAddition()
        {
            var data1 = new Complex[] { new Complex(1, 2), new Complex(3, 4) };
            var data2 = new Complex[] { new Complex(5, 6), new Complex(7, 8) };

            var np1 = new NDArrayGeneric<Complex>();
            np1.Shape = new Shape(new int[] { 2 });
            np1.Data = data1;

            var np2 = new NDArrayGeneric<Complex>();
            np2.Shape = new Shape(new int[] { 2 });
            np2.Data = data2;

            var np3 = np1 + np2;

            Assert.IsTrue(Enumerable.SequenceEqual(new Complex[] { new Complex(6, 8), new Complex(10, 12) }, np3.Data));

        }

        [TestMethod]
        public void Double1DPlusOffset_NDArrayAddition()
        {
            var np1 = new NDArrayGeneric<double>().arange(4, 1);

            var np3 = np1 + 2;

            Assert.IsTrue(Enumerable.SequenceEqual(new double[] { 3, 4, 5 }, np3.Data));
        }

        [TestMethod]
        public void Complex1DPlusOffset_NDArrayAddition()
        {
            var np1 = new NDArrayGeneric<Complex>();
            np1.Data = new Complex[] { new Complex(1, 2), new Complex(3, 4) };
            np1.Shape = new Shape(new int[] { 2 });

            var np2 = np1 + new Complex(1, 2);

            Assert.IsTrue(Enumerable.SequenceEqual(new Complex[] { new Complex(2, 4), new Complex(4, 6) }, np2.Data));
        }

        [TestMethod]
        public void Complex2DArray_NDArrayAddition()
        {
            var np1 = new NDArrayGeneric<Complex>();
            np1.Data = new Complex[] { new Complex(4, 1), new Complex(3, 5), new Complex(0, -2), new Complex(-3, 2) };
            np1.Shape = new Shape(new int[] { 2, 2 });

            var np2 = new NDArrayGeneric<Complex>();
            np2.Data = new Complex[] { new Complex(1, 2), new Complex(3, 4), new Complex(1, 2), new Complex(3, 4) };
            np2.Shape = new Shape(new int[] { 2, 2 });

            var np3 = np1 + np2;

            // expected
            var np4 = new Complex[] { new Complex(5, 3), new Complex(6, 9), new Complex(1, 0), new Complex(0, 6) };

            Assert.IsTrue(Enumerable.SequenceEqual(np3.Data, np4));
        }

        [TestMethod]
        public void Double2DArray_NDArrayAddition()
        {
            var np1 = new NDArrayGeneric<double>();
            np1.Data = new double[] { 1, 2, 3, 4, 5, 6 };
            np1.Shape = new Shape(new int[] { 2, 3 });

            var np2 = new NDArrayGeneric<double>();
            np2.Data = new double[] { 9, 8, 7, 6, 5, 4 };
            np2.Shape = new Shape(new int[] { 2, 3 });

            var np3 = np1 + np2;

            // expected
            var np4 = new double[] { 10, 10, 10, 10, 10, 10 };

            Assert.IsTrue(Enumerable.SequenceEqual(np3.Data, np4));
        }
    }
}
