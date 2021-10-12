using System;
using System.Collections.Generic;

namespace All_Nodes_Distance_K_in_Binary_Tree_OR_TTL_Expiry
{
  class Program
  {
    public class TreeNode
    {
      public int val;
      public TreeNode left;
      public TreeNode right;
      public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
      {
        this.val = val;
        this.left = left;
        this.right = right;
      }
    }
    static void Main(string[] args)
    {
      TreeNode root = new TreeNode(3);
      root.left = new TreeNode(5);
      root.left.left = new TreeNode(6);
      root.left.right = new TreeNode(2);
      root.left.right.left = new TreeNode(7);
      root.left.right.right = new TreeNode(4);
      root.right = new TreeNode(1);
      root.right.left = new TreeNode(0);
      root.right.right = new TreeNode(8);
      Program p = new Program();
      var result = p.DistanceK(root, root.left, 2);
      Console.WriteLine(string.Join(",", result));
    }

    Dictionary<TreeNode, TreeNode> graph = new Dictionary<TreeNode, TreeNode>();
    public IList<int> DistanceK(TreeNode root, TreeNode target, int k)
    {
      var response = new List<int>();
      if (root == null) return response;
      // 1. prepare the dictionary for each cur with its parent.
      CreateGraph(root, null);
      // 2. perform BFS from target cur
      Queue<TreeNode> q = new Queue<TreeNode>();
      q.Enqueue(target);
      HashSet<int> visited = new HashSet<int>();
      while(q.Count > 0)
      {
        int size = q.Count;
        for(int i = 0; i < size; i++)
        {
          var cur = q.Dequeue();
          if (cur != null)
          {
            int key = cur.val;
            if (k == 0) response.Add(key);
            if (graph.ContainsKey(cur) && !visited.Contains(key))
            {
              visited.Add(key);
              q.Enqueue(graph[cur]);
            }
            if (cur.left != null && !visited.Contains(cur.left.val))
            {
              visited.Add(cur.left.val);
              q.Enqueue(cur.left);
            }
            if (cur.right != null && !visited.Contains(cur.right.val))
            {
              visited.Add(cur.right.val);
              q.Enqueue(cur.right);
            }
          }
        }
        k--;
        if (k < 0) break;
      }
      return response;
    }

    private void CreateGraph(TreeNode root, TreeNode parent)
    {
      if (root == null) return;
      if (!graph.ContainsKey(root))
        graph.Add(root, parent);
      else
        graph[root] = parent;
      CreateGraph(root.left, root);
      CreateGraph(root.right, root);
    }
  }
}
