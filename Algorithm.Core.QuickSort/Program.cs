using System;

namespace Algorithm.Core.QuickSort
{
    /*
     * 快速排序
     * 1.选择用作基准值的元素tmp，确定low、high索引
     * 2.先从队尾开始向前扫描且当low < high时,如果a[high] > tmp,则high–,但如果a[high] < tmp,则将high的值赋值给low,即arr[low] = a[high],同时要转换数组扫描的方式,即需要从队首开始向队尾进行扫描了
     * 3.同理,当从队首开始向队尾进行扫描时,如果a[low] < tmp,则low++,但如果a[low] > tmp了,则就需要将low位置的值赋值给high位置,即arr[low] = arr[high],同时将数组扫描方式换为由队尾向队首进行扫描.
     * 4.不断重复①和②,知道low>=high时(其实是low=high),low或high的位置就是该基准数据在数组中的正确索引位置
     * 
     * 参考：https://blog.csdn.net/nrsc272420199/article/details/82587933
     */
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 2, 7, 0, -3, 10, 30, 6, 9, 1 };
            //int[] array = {  7 ,2};
            Console.WriteLine("old array:{0}", string.Join(',', array));
            QuickSort(array, 0, array.Length - 1);
            Console.WriteLine("sort array:{0}", string.Join(',', array));

            Console.WriteLine("Hello World!");
        }
        //找到基准数据正确索引，并整理数据（基准数据左侧数据均小于基准数据，右侧均大于基准数据）
        public static int GetIndex(int[] arr, int low, int high)
        {
            int tmp = arr[low];
            while (low < high)
            {
                while (low < high && arr[high] > tmp)
                {
                    high--;
                }
                arr[low] = arr[high];//将大于基准数据移到基准数据左侧
                while (low < high && arr[low] < tmp)
                {
                    low++;
                }
                arr[high] = arr[low];//将小于基准数据移到基准数据右侧
            }
            arr[low] = tmp;//基准数据回填
            return low;
        }

        public static void QuickSort(int[] arr, int low, int high)
        {
            int index = GetIndex(arr, low, high);//排除该正确索引，查找两侧子列表
            if (index - 1 > low) QuickSort(arr, low, index - 1);//继续查找左侧列表
            if (index + 1 < high) QuickSort(arr, index + 1, high);//继续查找右侧列表
        }
    }
}
