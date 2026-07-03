class Solution {
    public boolean isArraySpecial(int[] nums) {
        int length = nums.length - 1;

        for (int i = length; i >= 0; i--) {
            if (i - 1 >= 0) {
                if ((nums[i] + nums[i - 1]) % 2 == 0) {
                    return false;
                }
            }
        }

        return true;
    }
}