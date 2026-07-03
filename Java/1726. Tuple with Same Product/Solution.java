class Solution {
    public int tupleSameProduct(int[] nums) {
        HashMap<Integer, Integer> maps = new HashMap<>();
        int result = 0;

        for (int i = 0; i < nums.length; i++) {
            for (int j = 0; j < nums.length; j++) {
                if (i == j) {
                    continue;
                }
                int res = nums[i] * nums[j];
                int count = 1 + maps.getOrDefault(res, 0);
                maps.put(res, count);
                
            }
        }

        for (Map.Entry<Integer, Integer> entry : maps.entrySet()) {

            int value = entry.getValue() / 2;
            result += (2 * value) * (2 * (value - 1));

        }

        return result;

    }
}