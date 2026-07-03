public class Solution {
    public int MaximumLength(int[] nums) {
        Dictionary<int,int> freq = new Dictionary<int,int>();
        int maxLength = 1;

        foreach(int num in nums){
            if(freq.ContainsKey(num))
                freq[num]++;
            else
                freq[num] = 1;
        }

        int value,tempLength;
        long key;

        foreach(var temp in freq){
            key = temp.Key;
            value = temp.Value;
            tempLength = 0;

            if(key == 1){
                value = value % 2 == 0 ? value - 1 : value; 
                maxLength = Math.Max(value,maxLength);
            }

            else{
                while(freq.ContainsKey((int)key) && freq[(int)key] >= 2){
                    tempLength += 2;
                    key = key * key;
                    if(key > int.MaxValue) break;
            }
            if(key <= int.MaxValue && freq.ContainsKey((int)key) && freq[(int)key] >= 1){
                tempLength += 1;
            }else{
                tempLength -= 1;
            }
            maxLength = Math.Max(maxLength, tempLength);
        }

        
    }
    return maxLength;
}
}