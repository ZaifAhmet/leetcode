public class Solution {
    public int[] PlusOne(int[] digits) {
        int length = digits.Length;
        digits[length - 1] += 1;

        if(digits[length - 1] < 9)  return digits;
        
        for(int i = length - 1; i >= 0; i--){
            if(digits[i] > 9){
                digits[i] = 0;
                if(i - 1 >= 0) digits[i - 1] += 1;
            }
        }

        if(digits[0] > 9 || digits[0] == 0 || length <= 1) {
            Array.Resize(ref digits, length + 1);
            digits[0] = 1;
            digits[length] = 0;
        }
        
        return digits;
    }
}