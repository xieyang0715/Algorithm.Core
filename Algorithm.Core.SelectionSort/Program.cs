using System;

namespace Algorithm.Core.SelectionSort
{
    //选择排序
    class Program
    {
        /*
         * 首先在未排序序列中找到最小（大）元素，存放到排序序列的起始位置。
         * 再从剩余未排序元素中继续寻找最小（大）元素，然后放到已排序序列的末尾。
         * 重复第二步，直到所有元素均排序完毕。
         */
        static void Main(string[] args)
        {
            int[] array = { 2, 7, 0, -3, 10, 30, 6, 9, 1 };
            //int[] array = {  7 ,2};
            Console.WriteLine("old array:{0}", string.Join(',', array));
            SelectionSort(array);
            Console.WriteLine("sort array:{0}", string.Join(',', array));


            Console.WriteLine("Hello World!");
        }
        public static void SelectionSort(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int minDataIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if(array[minDataIndex]  > array[j])
                    {
                        minDataIndex = j;
                    }
                }
                int tmpValue = array[i];
                array[i] = array[minDataIndex];
                array[minDataIndex] = tmpValue;
            }
        }
    }
}
