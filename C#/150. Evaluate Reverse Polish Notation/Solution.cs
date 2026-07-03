public class Solution {
    public int EvalRPN(string[] tokens) {
         Stack<int> stack = new();
            double result = 0;

            for (int i = 0; i < tokens.Length; i++)
            {
                if (int.TryParse(tokens[i],out int number))
                {
                    stack.Push(number);
                }else
                {
                    int firstNum = stack.Pop();
                    int secondNum = stack.Pop();
                    switch (tokens[i])
                    {
                        case "+":
                            result = firstNum + secondNum;
                            break;
                        case "-":
                            result = secondNum - firstNum;
                            break;
                        case "*":
                            result = firstNum * secondNum;
                            break;
                        case "/":
                            result = secondNum / firstNum;
                            break;
                    }
                    stack.Push((int)result);
                }
            }

            return stack.Pop();
    }
}