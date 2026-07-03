public class Solution {
    public IList<string> BuildArray(int[] target, int n) {
         List<string> solutionList = [];
            int[] stream = new int[n];

            for (int i = 0; i < n; i++)
            {
                stream[i] = i + 1;
            }

            int targetIndex = 0;
            int stramIndex = 0;
            while (targetIndex < target.Length && stramIndex < n)
            {
                if (target[targetIndex] == stream[stramIndex])
                {
                    solutionList.Add("Push");
                    targetIndex++;
                    stramIndex++;
                }
                else if (target[targetIndex] > stream[stramIndex])
                {
                    solutionList.Add("Push");
                    solutionList.Add("Pop");
                    stramIndex++;
                }
                else{
                    targetIndex++;
                }
            }

            return solutionList;
    }
}