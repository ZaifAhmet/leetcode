
public class Solution {
    public int PivotInteger(int n) {
        var temp = (double)((long)n * n + n) / 2;
var x = Math.Sqrt(temp);

// Sayının mod 1'i sıfırsa bu bir tam sayıdır
if (x % 1 == 0) 
{
    int pivot = (int)x;
    return pivot;
}
return -1;
    }
}