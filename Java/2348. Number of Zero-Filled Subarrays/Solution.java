class Solution {
    public long zeroFilledSubarray(int[] nums) {
        long cnt = 0, stack = 0;
        for(int x:nums){
            if(x == 0) stack++;
            else stack = 0;
            cnt += stack;
        }
        return cnt;
    }
}