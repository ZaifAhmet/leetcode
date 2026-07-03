public class Solution {
    public int[] GetConcatenation(int[] nums) {
        int Length = nums.Length;
        int[] nums2 = new int[Length * 2];
        nums2 = nums.Concat(nums).ToArray();
        return nums2;
    }
}