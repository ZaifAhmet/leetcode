public class Solution {
    public int CountMajoritySubarrays(int[] nums, int target) {
        int result = 0;
        int length = nums.Length;
        
        for(int i = 0;i < length;i++){
            int scor = 0;
            for(int j = i;j < length;j++){
                if(nums[j] == target){
                    scor++;
                }else scor--;

                if(scor > 0) result++;
            }    
        }
        return result;
    }
}