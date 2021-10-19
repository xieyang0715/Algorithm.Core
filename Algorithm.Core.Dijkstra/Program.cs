using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Core.Dijkstra
{
    class Program
    {
        /*
         * 狄克斯特拉算法
         * 
         * 第一步：找出最便宜的节点。
         * 第二步：计算前往该节点的开销。如果开销比以前更低，则更新开销
         * 重复第一二步
         */
        static void Main(string[] args)
        {
            var nodes = InitNodes();
            var costs = InitCosts();
            var parents = InitParent();
            var nextDealKeys = NextDealKeys();

            Handel("start", nodes, costs, parents, nextDealKeys);

            //处理输出
            List<string> path = new List<string>();
            string next = "end";
            while (!path.Contains("start"))
            {
                path.Add(next);
                parents.TryGetValue(next, out next);
            }
            path.Reverse();
            Console.WriteLine("path:{0}", string.Join(',', path));


            Console.ReadKey();
        }

        /// <summary>
        /// 狄克斯特拉算法
        /// </summary>
        public static void Handel(string key, Dictionary<string, Dictionary<string, int>> nodes, Dictionary<string, int> costs, Dictionary<string, string> parents, List<string> nextDealKeys)
        {
            var handelNodesList = HandelNodesList();
            //获取当前key子节点
            nodes.TryGetValue(key, out Dictionary<string, int> childs);
            //找出最小花费节点
            var min = Find_Min_Cost_Node(childs, handelNodesList);
            while (min.Key != "")
            {
                if (IsCycle(key, min, parents)) break;
                //更新最小花费节点最小花费
                UpdateCosts(key, min, costs, parents);
                //添加到已处理节点
                handelNodesList.Add(min.Key);
                //添加到待处理节点(需要处理其子节点)
                nextDealKeys.Add(min.Key);
                //找出下一个最小花费节点
                min = Find_Min_Cost_Node(childs, handelNodesList);
                //当前子节点处理完闭
                if (min.Key == "")
                {
                    var minKey = nextDealKeys[0];
                    //nextDealKeys.RemoveAt(0);
                    nextDealKeys.RemoveAll(p => p == minKey);
                    Handel(minKey, nodes, costs, parents, nextDealKeys);//使用广度优先搜索
                }
            }
        }

        public static bool IsCycle(string parentKey, KeyValuePair<string, int> minNode, Dictionary<string, string> parent)
        {
            var next = parentKey;
            while (next != null && next != "end")
            {
                next = parent[next];
                if (next == minNode.Key) return true;
            }
            return false;
        }

        public static void UpdateCosts(string parentKey, KeyValuePair<string, int> minNode, Dictionary<string, int> costs, Dictionary<string, string> parent)
        {
            if (costs.ContainsKey(minNode.Key) && costs[minNode.Key] > minNode.Value)
            {
                costs[minNode.Key] = minNode.Value;
                parent[minNode.Key] = parentKey;
            }
        }

        //找出最便宜节点
        public static KeyValuePair<string, int> Find_Min_Cost_Node(Dictionary<string, int> nodes, List<string> handelNodesList)
        {
            var min = new KeyValuePair<string, int>("", int.MaxValue);
            if (nodes == null) return min;
            foreach (var item in nodes)
            {
                if (min.Value > item.Value && !handelNodesList.Contains(item.Key)) min = item;
            }
            return min;
        }

        /// <summary>
        /// 已处理节点
        /// </summary>
        /// <returns></returns>
        public static List<string> HandelNodesList() => new List<string>();
        /// <summary>
        /// 添加到待处理节点(需要处理其子节点)
        /// </summary>
        /// <returns></returns>
        public static List<string> NextDealKeys() => new List<string>();
        public static List<string> CurChildsList() => new List<string>();

        #region example1

        ////输出：path:start,b,a,end
        ////初始化图节点
        public static Dictionary<string, Dictionary<string, int>> InitNodes() => new Dictionary<string, Dictionary<string, int>>() {
                { "start", new Dictionary<string, int> { {"a", 6} ,{"b", 2} }},
                { "a", new Dictionary<string, int> { { "end", 1}}},
                { "b", new Dictionary<string, int> { {"a", 3} ,{ "end", 5} }},
                { "end", new Dictionary<string, int> {}},
        };

        public static Dictionary<string, int> InitCosts() => new Dictionary<string, int>() {
                { "start", int.MaxValue},
                { "a", int.MaxValue},
                { "b", int.MaxValue},
                { "end", int.MaxValue},
        };

        public static Dictionary<string, string> InitParent() => new Dictionary<string, string>() {
                { "start", null},
                { "a", null},
                { "b", null},
                { "end", null},
        };

        #endregion

        #region example2
        ////输出：path:start,b,c,end
        ////初始化图节点
        //public static Dictionary<string, Dictionary<string, int>> InitNodes() => new Dictionary<string, Dictionary<string, int>>() {
        //        { "start", new Dictionary<string, int> { {"a", 2} ,{"b", 5} }},
        //        { "a", new Dictionary<string, int> { { "c", 7},{ "b", 8}}},
        //        { "b", new Dictionary<string, int> { { "c", 2 }, { "d", 4}}},
        //        { "c", new Dictionary<string, int> { { "end", 1}} },
        //        { "d", new Dictionary<string, int> { { "c", 6 }, { "end", 1}}},
        //        { "end", new Dictionary<string, int> {}},
        //};

        //public static Dictionary<string, int> InitCosts() => new Dictionary<string, int>() {
        //        { "start", int.MaxValue},
        //        { "a", int.MaxValue},
        //        { "b", int.MaxValue},
        //        { "c", int.MaxValue},
        //        { "d", int.MaxValue},
        //        { "end", int.MaxValue},
        //};

        //public static Dictionary<string, string> InitParent() => new Dictionary<string, string>() {
        //        { "start", null},
        //        { "a", null},
        //        { "b", null},
        //        { "c", null},
        //        { "d", null},
        //        { "end", null},
        //};
        #endregion

        #region example3 循环节点

        //输出：path:start,a,b,end
        //初始化图节点
        //public static Dictionary<string, Dictionary<string, int>> InitNodes() => new Dictionary<string, Dictionary<string, int>>() {
        //        { "start", new Dictionary<string, int> { {"a", 10} }},
        //        { "a", new Dictionary<string, int> { { "b", 20}}},
        //        { "b", new Dictionary<string, int> { {"c", 1} ,{ "end", 30} }},
        //        { "c", new Dictionary<string, int> { { "a", 1}}},
        //        { "end", new Dictionary<string, int> {}},
        //};

        //public static Dictionary<string, int> InitCosts()
        //{
        //    var result = new Dictionary<string, int>();
        //    foreach (var item in InitNodes().Keys)
        //    {
        //        result.Add(item, int.MaxValue);
        //    }
        //    return result;
        //}

        //public static Dictionary<string, string> InitParent()
        //{
        //    var result = new Dictionary<string, string>();
        //    foreach (var item in InitNodes().Keys)
        //    {
        //        result.Add(item, null);
        //    }
        //    return result;
        //}

        #endregion

    }
}
