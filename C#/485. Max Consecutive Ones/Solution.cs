public class Solution {
    public int FindMaxConsecutiveOnes(int[] nums) {
        int maxCons = 0, currentCount = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            
            if (nums[i] == 1)
            {
                currentCount++;
            }else
            {
                currentCount = 0;
            }
            maxCons = Math.Max(maxCons, currentCount);
        }
        return maxCons;
    }
}