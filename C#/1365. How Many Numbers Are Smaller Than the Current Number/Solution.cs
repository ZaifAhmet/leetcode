public class Solution {
    public int[] SmallerNumbersThanCurrent(int[] nums) {
        int length = nums.Length;
            int[] solutionArr = new int[length];
            Dictionary<int, int> dic = new();

            for (int i = 0; i < length; i++)
            {
                // Eğer bu sayı için daha önce hesap yaptıysak direkt al
                if (dic.TryGetValue(nums[i], out int oncekiSonuc))
                {
                    solutionArr[i] = oncekiSonuc;
                    continue;
                }

                int count = 0;
                for (int j = 0; j < length; j++)
                {
                    if (i != j && nums[i] > nums[j])
                    {
                        count++;
                    }
                }

                solutionArr[i] = count;
                dic[nums[i]] = count;
            }

            return solutionArr;
    }
}