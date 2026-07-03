public class Solution {
    public IList<int> FindDisappearedNumbers(int[] nums) {
        int n = nums.Length;
            int[] sol = new int[n + 1];
            List<int> solList = new();

            for (int i = 1; i <= n; i++)
            {
                sol[i] = i;
            }
            for (int i = 0; i < n; i++)
            {
                if (sol[nums[i]] == nums[i])
                {
                    sol[nums[i]] = 0;
                }
            }
            for (int i = 1; i <= n; i++)
            {
                if (sol[i] != 0)
                {
                    solList.Add(sol[i]);
                }
            }
            return solList;
    }
}