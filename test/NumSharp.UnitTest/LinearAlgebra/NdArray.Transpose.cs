using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;
using NumSharp.Core.Extensions;
using System.Linq;
using NumSharp.Core;

namespace NumSharp.UnitTest.LinearAlgebra
{
    [TestClass]
    public class TransposeTest
    {
        [TestMethod]
        public void TwoxThree()
        {
            NDArrayGeneric<double> np1 = new NumPyGeneric<double>().arange(6).reshape(3,2);
            
            var np1Transposed = np1.transpose();

            Assert.IsTrue(np1Transposed[0,0] == 0);
            Assert.IsTrue(np1Transposed[0,1] == 2);
            Assert.IsTrue(np1Transposed[0,2] == 4);
            Assert.IsTrue(np1Transposed[1,0] == 1);
            Assert.IsTrue(np1Transposed[1,1] == 3);
            Assert.IsTrue(np1Transposed[1,2] == 5);

        }
    }
}
