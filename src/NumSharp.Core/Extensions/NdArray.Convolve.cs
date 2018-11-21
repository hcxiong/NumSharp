using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using NumSharp;

namespace NumSharp.Core.Extensions
{
    public static partial class NDArrayExtensions
    {
        /// <summary>
        /// Convolution of 2 series  
        /// </summary>
        /// <param name="numSharpArray1"></param>
        /// <param name="numSharpArray2"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static NDArrayGeneric<double> Convolve(this NDArrayGeneric<double> numSharpArray1, NDArrayGeneric<double> numSharpArray2, string mode = "full" )
        {
            int nf = numSharpArray1.Shape.Shapes[0];
            int ng = numSharpArray2.Shape.Shapes[0];

            var numSharpReturn = new NDArrayGeneric<double>();

            switch (mode)
            {
                case "full":
                {
                    int n  = nf + ng - 1;

                    var outArray = new double[n];

                    for (int idx = 0; idx < n; ++idx)
                    {
                        int jmn = (idx >= ng - 1) ? (idx - (ng - 1)) : 0;
                        int jmx = (idx < nf - 1) ? idx : nf - 1;

                        for (int jdx = jmn; jdx <= jmx; ++jdx )
                        {
                            outArray[idx] += ( numSharpArray1[jdx] * numSharpArray2[idx - jdx] );
                        }
                    }
                
                    numSharpReturn.Data = outArray;

                    break;
                }
                case "valid":
                {
                    var min_v = (nf < ng) ? numSharpArray1 : numSharpArray2;
                    var max_v = (nf < ng) ? numSharpArray2 : numSharpArray1;
            
                    int n  = Math.Max(nf, ng) - Math.Min(nf, ng) + 1;
                
                    double[] outArray = new double[n];
  
                    for(int idx = 0; idx < n; ++idx) 
                    {
                        int kdx = idx; 
                    
                        for(int jdx = (min_v.Shape.Shapes[0] - 1); jdx >= 0; --jdx) 
                        {
                            outArray[idx] += min_v[jdx] * max_v[kdx];
                            ++kdx;
                        }
                    }

                    numSharpReturn.Data = outArray;
                    
                    break;
                }
                case "same":
                {
                    // followed the discussion on 
                    // https://stackoverflow.com/questions/38194270/matlab-convolution-same-to-numpy-convolve
                    // implemented numpy convolve because we follow numpy
                    var npad = numSharpArray2.Shape.Shapes[0] - 1;

                    if (npad % 2 == 1)
                    {
                        npad = (int) Math.Floor(((double)npad) / 2.0);
                    
                        numSharpArray1.Data.ToList().AddRange(new double[npad+1]);
                        var puffer = (new double[npad]).ToList();
                        puffer.AddRange(numSharpArray1.Data);
                        numSharpArray1.Data = puffer.ToArray(); 
                        numSharpArray1.Shape = new Shape(numSharpArray1.Data.Length);
                    }
                    else 
                    {
                        npad = npad / 2;
                    
                        var puffer = ((double[]) numSharpArray1.Data).ToList(); 
                        puffer.AddRange(new double[npad]);
                        numSharpArray1.Data = puffer.ToArray();
                        numSharpArray1.Shape = new Shape(numSharpArray1.Data.Length);
                    
                        puffer = (new double[npad]).ToList();
                        puffer.AddRange(numSharpArray1.Data);
                        numSharpArray1.Data = puffer.ToArray();
                        numSharpArray1.Shape = new Shape(numSharpArray1.Data.Length); 
                    }

                    numSharpReturn = numSharpArray1.Convolve(numSharpArray2,"valid");
                    break;
                }
            }
            numSharpReturn.Shape = new Shape(numSharpReturn.Data.Length);
            return numSharpReturn;
        }
    }
  }