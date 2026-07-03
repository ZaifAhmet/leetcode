class Solution {
    public int maxAscendingSum(int[] nums) {
        int result = nums[0], maxResult = nums[0];
        if (nums.length == 1) {
            return nums[0];
        }

        for (int i = 0; i < nums.length - 1; i++) {
            if (nums[i] < nums[i + 1]) {
                result += nums[i + 1];
            } else if (nums[i] > nums[i + 1]) {
                result = nums[i + 1];
            } else {
                result = nums[i];
            }
            maxResult = Math.max(maxResult, result);
        }
        return maxResult;
    }
}