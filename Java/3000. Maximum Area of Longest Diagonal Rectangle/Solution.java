class Solution {
    public int areaOfMaxDiagonal(int[][] dimensions) {
        double maxDiagonal = 0;
        int result = 0;
        int len = 0,wid = 0;
        double diagonal = 0;
        for(int i = 0;i < dimensions.length;i++){
            len = dimensions[i][0];
            wid = dimensions[i][1];
            diagonal = Math.sqrt(len*len + wid*wid);
            if(maxDiagonal < diagonal){
                maxDiagonal = diagonal;
                result = len * wid;
            }
            else if(maxDiagonal == diagonal){
                result = Math.max(result,len*wid);
            }
        }
        return result;
    }
}