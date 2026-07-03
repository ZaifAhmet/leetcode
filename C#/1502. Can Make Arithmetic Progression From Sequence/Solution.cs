public class Solution {
    public bool CanMakeArithmeticProgression(int[] arr) {
        Array.Sort(arr);
        int diff = arr[1] - arr[0];

        for(int i = 1; i < arr.Length -1; i++){
            
            if(diff != arr[i+1] - arr[i]){
                return false;
            }
        }
        return true;
    }
}