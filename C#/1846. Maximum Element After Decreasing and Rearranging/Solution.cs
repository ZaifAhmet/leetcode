public class Solution {
    public int MaximumElementAfterDecrementingAndRearranging(int[] arr) {
        int length = arr.Length;
        int[] counts = new int[length + 1];
        int ans = 0;
        foreach(int num in arr){
            counts[Math.Min(num,length)] += 1;
        }
        
        for(int i= 0;i<= length;i++){
            ans = Math.Min(ans + counts[i],i);
        }
        return ans;
    }
}