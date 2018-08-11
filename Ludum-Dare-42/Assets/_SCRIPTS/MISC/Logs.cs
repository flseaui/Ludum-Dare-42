using UnityEngine;

namespace MISC
{
    public static class Logs
    {
        public static void Print2DArray<T>(T[,] matrix)
        {
            var temp = "";
            for (var i = 0; i < matrix.GetLength(0); ++i)
                for (var j = 0; j < matrix.GetLength(1); ++j)
                   temp += matrix[i, j] + "\t";
                Debug.Log(temp);
        }
    }
}