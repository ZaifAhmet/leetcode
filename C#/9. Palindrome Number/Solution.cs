public class Solution {
    public bool IsPalindrome(int x) {

        if (x < 0 || (x % 10 == 0 && x != 0)) return false;

        int originalX = x; 
        int tersSayi = 0;

        while (x > 0)
        {
            int sonBasamak = x % 10;       
            tersSayi = (tersSayi * 10) + sonBasamak; 
            x /= 10;                       
        }

        return originalX == tersSayi;
    }
}