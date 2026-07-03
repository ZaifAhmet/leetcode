class Solution {
    static final double TARGET = 24;
    static final double EPSILON = 1e-6; // hata toleransı
    static List<Double> memo = new ArrayList();
    public static boolean solve(List<Double> nums) {
        if (nums.size() == 1) {
            // sonuca ulaştık mı?
            return Math.abs(nums.get(0) - TARGET) < EPSILON;
        }
        for(int i = 0;i < nums.size();i++){
            for(int j = 0;j < nums.size();j++){
                if(i == j) continue;
                
                List<Double> next = new ArrayList<>();
                for(int k = 0;k < nums.size();k++){
                    if(k!=i && k!= j) next.add(nums.get(k));
                }
                double a = nums.get(i);
                double b = nums.get(j);
               for(double res: calculate(a,b)){
                   next.add(res);
                   if(solve(next)) return true;
                   next.remove(next.size() -1 );
               }
            }
        }
        return false;
    }
    private static List<Double> calculate(double a, double b) {
        List<Double> results = new ArrayList<>();
        results.add(a + b);
        results.add(a - b);
        results.add(b - a);
        results.add(a * b);
        if (Math.abs(b) > EPSILON) results.add(a / b);
        if (Math.abs(a) > EPSILON) results.add(b / a);
        return results;
    }
    
    public static boolean judgePoint24(int[] cards) {
        List<Double> nums = new ArrayList();
        for(double x: cards) nums.add(x);
        return solve(nums);
    }
}