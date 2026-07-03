class Solution {
    public record Condition(int x, int y) {
    }

    private static double[][] memo = new double[4800 + 1][4800 + 1];
    private static boolean[][] computed = new boolean[4800 + 1][4800 + 1];

    public static final Condition[] Conditions = {
            new Condition(100, 0),
            new Condition(75, 25),
            new Condition(50, 50),
            new Condition(25, 75)
    };

    public double soupServings(int n) {
        if (n >= 4800)
            return 1;
       
        double prob = recursiveServing(n, n);
        return prob;
    }

    public static double recursiveServing(int a, int b) {
        a = Math.max(0, a);
        b = Math.max(0, b);

        if (computed[a][b])
            return memo[a][b];

        if (a <= 0 && b > 0)
            return 1;
        if (a <= 0 && b <= 0)
            return 0.5;
        if (a > 0 && b <= 0)
            return 0;

        double toplam = 0;
        for (Condition c : Conditions) {
            toplam += 0.25 * recursiveServing(a - c.x, b - c.y);
        }

        computed[a][b] = true;
        memo[a][b] = toplam;
        return toplam;
    }
}