class Solution {
    public int longestMonotonicSubarray(int[] nums) {
        if (nums.length == 0)
            return 0;
        if (nums.length == 1)
            return 1;

        int maxLength = 1;
        int currentLength = 1;
        int flag = nums[0] < nums[1] ? 1 : 0; // Artan: 1, Azalan veya eşit: 0

        for (int i = 0; i < nums.length - 1; i++) {
            if (flag == 1) {
                if (nums[i] < nums[i + 1]) {
                    currentLength++;
                } else if (nums[i] > nums[i + 1]) {
                    // Değişim: Artan -> Azalan
                    flag = 0;
                    currentLength = 2;
                } else {
                    // Eşit durum
                    currentLength = 1;
                }
            } else {
                if (nums[i] > nums[i + 1]) {
                    currentLength++;
                } else if (nums[i] < nums[i + 1]) {
                    // Değişim: Azalan -> Artan
                    flag = 1;
                    currentLength = 2;
                } else {
                    // Eşit durum
                    currentLength = 1;
                }
            }
            maxLength = Math.max(maxLength, currentLength);
        }
        return maxLength;
    }
}