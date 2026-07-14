public class Solution {
    public bool[] PathExistenceQueries(int n, int[] nums, int maxDiff, int[][] queries) {
        bool[] result = new bool[queries.Length];
        int[] graph = new int[n];
        int diff = 0;
        graph[0] = 0;

        for(int i = 0;i < n - 1; i++){
            diff = Math.Abs(nums[i] - nums[i + 1]);
            if(diff <= maxDiff){
                graph[i + 1] = graph[i];
            }else{
                graph[i + 1] = graph[i] + 1;
            }
        }
        int Fque,Sque;

        for(int i = 0; i < queries.Length; i++){
            Fque = queries[i][0];
            Sque = queries[i][1];

            if(graph[Fque] == graph[Sque])
                result[i] = true;
            else
                result[i] = false;
        }
        return result;
    }
}