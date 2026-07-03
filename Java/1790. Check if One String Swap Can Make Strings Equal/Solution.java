class Solution {
    public boolean areAlmostEqual(String s1, String s2) {
        if (s1.equals(s2))
            return true;

        List<int[]> diffPairs = new ArrayList<>();

        for (int i = 0; i < s1.length(); i++) {
            if (s1.charAt(i) != s2.charAt(i)) {
                diffPairs.add(new int[] { s1.charAt(i), s2.charAt(i) });
                if (diffPairs.size() > 2)
                    return false;
            }
        }

        return diffPairs.size() == 2 &&
                diffPairs.get(0)[0] == diffPairs.get(1)[1] &&
                diffPairs.get(0)[1] == diffPairs.get(1)[0];
    }
}