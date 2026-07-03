import java.util.HashSet;
import java.util.Set;
import java.util.TreeSet;
import java.util.stream.Collectors;

class Solution {
    public int maxSum(int[] nums) {
        Set<Integer> hs = Arrays.stream(nums).boxed().collect(Collectors.toSet());
        TreeSet<Integer> sortedSet = new TreeSet<>(hs);
        int sum = sortedSet.pollLast();
        if(sortedSet.size() <= 0 || sortedSet.last() <= 0)
            return sum;
        while (!sortedSet.isEmpty()) {
            if (sortedSet.last() <= 0) break;
            sum += sortedSet.pollLast();
        }
        return sum;   
    }
}