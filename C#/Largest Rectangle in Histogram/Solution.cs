
public class Solution
{
    public int LargestRectangleArea(int[] heights) {
    Stack<int> stack = new();
    int length = heights.Length;
    int maxArea = int.MinValue;
    int currentArea = 0;
    
    for(int i= 0;i < heights.Length;i++){
        while(stack.Any() && heights[stack.Peek()] >= heights[i]){
            int Index = stack.Pop();
            int witdh = stack.Any() ? (i - stack.Peek() - 1) : i;
            currentArea = heights[Index] * witdh;
            maxArea = Math.Max(maxArea, currentArea);
        }
        stack.Push(i);
    }

    while(stack.Any()){
        int Index = stack.Pop();
        int witdh = stack.Any() ? (length - stack.Peek() - 1) : length;
        currentArea = heights[Index] * witdh;
        maxArea = Math.Max(maxArea, currentArea);
    }
    return maxArea;
    }
}
