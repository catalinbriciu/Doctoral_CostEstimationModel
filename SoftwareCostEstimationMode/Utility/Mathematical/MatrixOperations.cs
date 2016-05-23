using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareCostEstimationMode.Utility.Mathematical
{
    static class MatrixOperations
    {
       // link for the LUP code : http://www.rkinteractive.com/blogs/SoftwareDevelopment/post/2013/05/07/Algorithms-In-C-LUP-Decomposition.aspx
        /*
* Perform LUP decomposition on a matrix A.
* Return L and U as a single matrix(double[][]) and P as an array of ints.
* We implement the code to compute LU "in place" in the matrix A.
* In order to make some of the calculations more straight forward and to 
* match Cormen's et al. pseudocode the matrix A should have its first row and first columns
* to be all 0.
* */
public static Tuple<double[][], int[]> LUPDecomposition(double[][] A)
{
    int n = A.Length-1;
    /*
    * pi represents the permutation matrix.  We implement it as an array
    * whose value indicates which column the 1 would appear.  We use it to avoid 
    * dividing by zero or small numbers.
    * */
    int[] pi = new int[n+1];
    double p = 0;
    int kp = 0;
    int pik = 0;
    int pikp = 0;
    double aki = 0;
    double akpi = 0;
            
    //Initialize the permutation matrix, will be the identity matrix
    for (int j = 0; j <= n; j++)
    {
        pi[j] = j;
    }

    for (int k = 0; k <= n; k++)
    {
        /*
        * In finding the permutation matrix p that avoids dividing by zero
        * we take a slightly different approach.  For numerical stability
        * We find the element with the largest 
        * absolute value of those in the current first column (column k).  If all elements in
        * the current first column are zero then the matrix is singluar and throw an
        * error.
        * */
        p = 0;
        for (int i = k; i <= n; i++)
        {
            if (Math.Abs(A[i][k]) > p)
            {
                p = Math.Abs(A[i][k]);
                kp = i;
            }
        }
        if (p == 0)
        {
            throw new Exception("singular matrix");
        }
        /*
        * These lines update the pivot array (which represents the pivot matrix)
        * by exchanging pi[k] and pi[kp].
        * */
        pik = pi[k];
        pikp = pi[kp];
        pi[k] = pikp;
        pi[kp] = pik;
                
        /*
        * Exchange rows k and kpi as determined by the pivot
        * */
        for (int i = 0; i <= n; i++)
        {
            aki = A[k][i];
            akpi = A[kp][i];
            A[k][i] = akpi;
            A[kp][i] = aki;
        }

        /*
            * Compute the Schur complement
            * */
        for (int i = k+1; i <= n; i++)
        {
            A[i][k] = A[i][k] / A[k][k];
            for (int j = k+1; j <= n; j++)
            {
                A[i][j] = A[i][j] - (A[i][k] * A[k][j]); 
            }
        }
    }
    return Tuple.Create(A,pi);
}
        public static double[] LUPSolve(double[][] LU, int[] pi, double[] b)
{
    int n = LU.Length-1;
    double[] x = new double[n+1];
    double[] y = new double[n+1];
    double suml = 0;
    double sumu = 0;
    double lij = 0;

    /*
    * Solve for y using formward substitution
    * */
    for (int i = 0; i <= n; i++)
    {
        suml = 0;
        for (int j = 0; j <= i - 1; j++)
        {
            /*
            * Since we've taken L and U as a singular matrix as an input
            * the value for L at index i and j will be 1 when i equals j, not LU[i][j], since
            * the diagonal values are all 1 for L.
            * */
            if (i == j)
            {
                lij = 1;
            }
            else
            {
                lij = LU[i][j];
            }
            suml = suml + (lij * y[j]);
        }
        y[i] = b[pi[i]] - suml;
    }
    //Solve for x by using back substitution
    for (int i = n; i >= 0; i--)
    {
        sumu = 0;
        for (int j = i + 1; j <= n; j++)
        {
            sumu = sumu + (LU[i][j] * x[j]);
        }
        x[i] = (y[i] - sumu) / LU[i][i];
    }
    return x;
}
        public static double[][] GetMatrixInverse(double[][] InputMatrix)
        {
            int n = InputMatrix.Length;
    //e will represent each column in the identity matrix
    double[] e;
    //x will hold the inverse matrix to be returned
    double[][] x = new double[n][];
    for (int i = 0; i < n; i++)
    {
        x[i] = new double[InputMatrix[i].Length];
    }
    /*
    * solve will contain the vector solution for the LUP decomposition as we solve
    * for each vector of x.  We will combine the solutions into the double[][] array x.
    * */
    double[] solve;

    //Get the LU matrix and P matrix (as an array)
    Tuple<double[][], int[]> results = LUPDecomposition(InputMatrix);

    double[][] LU = results.Item1;
    int[] P = results.Item2;

    /*
    * Solve AX = e for each column ei of the identity matrix using LUP decomposition
    * */
    for (int i = 0; i < n; i++)
    {
        e = new double[InputMatrix[i].Length];
        e[i] = 1;
        solve = LUPSolve(LU, P, e);
        for (int j = 0; j < solve.Length; j++)
        {
            x[j][i] = solve[j];
        }
    }
    return x;
}
        public static double[][] GetMatrixTranspose(double[][] InputMatrix)
        {
            if (InputMatrix == null) throw new ArgumentNullException("InputMatrix");
            if (InputMatrix.GetLength(0) != InputMatrix.GetLength(1)) throw new ArgumentOutOfRangeException("InputMatrix", "matrix is not square");
            /*copy the input matrix into a temp one*/
            double[][] temp_Matrix = new double[InputMatrix.GetLength(0)][];
            for (int count_line = 0; count_line < InputMatrix.GetLength(0); count_line++)
            {
                temp_Matrix[count_line] = new double[InputMatrix.GetLength(0)];
                for (int count_column = 0; count_column < InputMatrix.GetLength(0); count_column++)
                {
                    temp_Matrix[count_line][count_column] = InputMatrix[count_line][count_column];
                }
            }
            int size = InputMatrix.GetLength(0);

            for (int n = 0; n < (size - 1); ++n)
            {
                for (int m = n + 1; m < size; ++m)
                {
                    double temp = temp_Matrix[n][m];
                    temp_Matrix[n][m] = temp_Matrix[m][n];
                    temp_Matrix[m][n] = temp;
                }
            }
            return temp_Matrix;
        }
        public static double[][] GetMatrixProduct(double[][] InputMatrix_1, double[][] InputMatrix_2)
        {
            
            int m, n, p, q;
            m = InputMatrix_1.GetLength(0);
            n = InputMatrix_1.GetLength(1);
            p = InputMatrix_2.GetLength(0);
            q = InputMatrix_2.GetLength(1);
            double[][] ProductMatrix = new double[m][];
            if (n != p) throw new ArgumentOutOfRangeException("InputMatrix", "matrixes cannot be multiplied");
            else
            {
                double sum = 0;
                int count, col, k;
                for( count = 0; count < m; count++)
                {
                    ProductMatrix[count] = new double[q];
                   for( col = 0; col < q; col++)
                   {
                       for(k = 0; k < p; k++)
                       {
                           
                           sum += InputMatrix_1[count][k] * InputMatrix_2[k][col];
                       }

                       ProductMatrix[count][col] = sum;
                       sum = 0;
                   }
                }
            }
            return ProductMatrix;
        }
        public static double[][] GetMatrixDifference(double[][] InputMatrix_1, double[][] InputMatrix_2)
        {
            
            int n,m,p,q;
            n = InputMatrix_1.GetLength(0);
            m = InputMatrix_1.GetLength(1);
            p = InputMatrix_2.GetLength(0);
            q = InputMatrix_2.GetLength(1);
            double[][] Difference = new double[n][];
            if ((n != p) && (m != q))
            {
                throw new ArgumentOutOfRangeException("InputMatrix", "matrixes cannot be multiplied");
            }
            else
            {
                
                for (int count = 0; count < n; count++)
                {
                    Difference[count]= new double[m];
                    for (int col = 0; col < m; col++)
                    {
                        Difference[count][col] = InputMatrix_1[count][col] - InputMatrix_2[count][col];
                    }
                }
            }
            return Difference;
        }
        public static double[][] GetMatrixSum(double[][] InputMatrix_1, double[][] InputMatrix_2)
        {
            
            int n, m, p, q;
            n = InputMatrix_1.GetLength(0);
            m = InputMatrix_1.GetLength(1);
            p = InputMatrix_2.GetLength(0);
            q = InputMatrix_2.GetLength(1);
            double[][] SumMatrix = new double[n][]; 
            if ((n != p) && (m != q))
            {
                throw new ArgumentOutOfRangeException("InputMatrix", "matrixes cannot be multiplied");
            }
            else
            {
                for (int count = 0; count < n; count++)
                {
                    SumMatrix[count] = new double[m];
                    for (int col = 0; col < m; col++)
                    {
                        SumMatrix[count][col] = InputMatrix_1[count][col] + InputMatrix_2[count][col];
                    }
                }
            }
            return SumMatrix;
        }
    }
}
