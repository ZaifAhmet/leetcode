public class Solution {
    public int[] FinalPrices(int[] prices) {
        int length = prices.Length;
            Stack<int> stack = new();
            for (int i = 0; i < length; i++)
            {
                while (stack.Any() && prices[i] <= prices[stack.Peek()])
                {
                    int prevIndex = stack.Pop();
                    prices[prevIndex] = prices[prevIndex] - prices[i];
                }
                stack.Push(i);
            }
            return prices;
    }
}