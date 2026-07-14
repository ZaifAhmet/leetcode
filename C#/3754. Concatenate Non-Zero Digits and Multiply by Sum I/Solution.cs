public class Solution {
    public long SumAndMultiply(int n) {
        long sum = 0, x = 0;
        int i = 1, tmp;

        while(n % 10 != n){
            tmp = (n % 10);
            x += tmp * i;
            sum += tmp;
            
            if(tmp == 0) n /= 10;
            else {
                n -= tmp;
                i *= 10;
            }
        }
        tmp = (n % 10);
        x += tmp * i;
        sum += tmp;
        n -= tmp;
        return x * sum;
    }
}