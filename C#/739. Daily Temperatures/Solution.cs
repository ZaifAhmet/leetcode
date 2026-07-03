public class Solution {
    public int[] DailyTemperatures(int[] temperatures) {
        int[] stack = new int[temperatures.Length];
        int top = -1;
        int[] ret = new int[temperatures.Length];
        for (int i = 0; i < temperatures.Length; i++) {
            while (top > -1 && temperatures[i] > temperatures[stack[top]]) {
                int idx = stack[top--];
                ret[idx] = i - idx;
            }
            stack[++top] = i;
        }
        return ret;
    }
}