class Solution {
    public static boolean isPathCrossing(String path) {
        int x = 0,y = 0;
        
        int points[][] = new int[path.toCharArray().length + 1][2];
        int k = 1;
        points[0][0] = 0;
        points[0][1] = 0;
        for(int i = 0;i < path.toCharArray().length;i++){
            
            switch (path.charAt(i)) {
                case 'N' -> y+=1;
                case 'E' -> x+=1;
                case 'S' -> y-=1;
                case 'W' -> x-=1;
                default -> {
                }
            }
           
            points[k][0] = x;
            points[k][1] = y;
            k++;
    
       }
    for(int i = 0;i < points.length;i++){
        for(int j = i+1;j < points.length;j++){
            if(points[i][0] == points[j][0] && points[i][1] == points[j][1]){
                return true;
            }
        }
    }
    return false;
}
}