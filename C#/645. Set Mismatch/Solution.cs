public class Solution {
    public int[] FindErrorNums(int[] nums) {
        int n = nums.Length;
        int[] dupNums = new int[n + 1];
        int actualSum = 0, duplicatedNumber = 0;

        for (int i = 0; i < n; i++)
        {
            dupNums[nums[i]]++;
            if(dupNums[nums[i]] > 1){
                duplicatedNumber = nums[i];
            }
            actualSum += nums[i];
        }
        int expectedSum = n * (n + 1) / 2;
        int misNumber = expectedSum - (actualSum - duplicatedNumber);
        return [duplicatedNumber, misNumber];
    }
}