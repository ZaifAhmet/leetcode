public class Solution {
    public int NumberOfSubstrings(string s) {
        int []idxs = new int[] {-1,-1,-1};
        int subCount = 0,minIdx = -1;

        for(int i = 0;i < s.Length;i++){
            idxs[s[i] - 'a'] = i;
            minIdx = Math.Min(idxs[0],Math.Min(idxs[1],idxs[2]));
            if(minIdx > -1) subCount += minIdx + 1; 
        }
        return subCount;
    }
}