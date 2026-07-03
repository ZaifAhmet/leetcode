public class Solution {
    public int[] Shuffle(int[] nums, int n) {
        int[] sol = new int[nums.Length];
        int j = 0;
        for (int i = 0; i < n; i ++)
        {
            sol[j] = nums[i];
            sol[j + 1] = nums[i + n];
            j += 2;
        }
        return sol;
    }
}