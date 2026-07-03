public class Solution {
    public int RobotSim(int[] commands, int[][] obstacles) {
        int x = 0, y = 0;
        int maxDistance = 0;
         
        int[][] directions = new int[][] {
            new int[] {0, 1},  
            new int[] {1, 0},  
            new int[] {0, -1}, 
            new int[] {-1, 0}  
        };
        int directionIdx = 0;

        var xToY = new Dictionary<int, List<int>>();
        var yToX = new Dictionary<int, List<int>>();

        foreach (var obstacle in obstacles) {
            int oX = obstacle[0], oY = obstacle[1];
            if (!xToY.ContainsKey(oX)) xToY[oX] = new List<int>();
            xToY[oX].Add(oY);
            if (!yToX.ContainsKey(oY)) yToX[oY] = new List<int>();
            yToX[oY].Add(oX);
        }

        foreach (var list in xToY.Values) list.Sort();
        foreach (var list in yToX.Values) list.Sort();

        foreach (int command in commands) {

            if(command == -1){
                 
                directionIdx = (directionIdx + 1) % 4;

            } else if(command == -2) { 
                
                directionIdx = (directionIdx + 3) % 4;
            } else{
                
                int dx = directions[directionIdx][0];
                int dy = directions[directionIdx][1];

                if(dx == 0) { 
                    
                    xToY.TryGetValue(x, out List<int> yList);
                    y = CalculateNextPos(yList, y, command, dy);
                }else { 
                    
                    yToX.TryGetValue(y, out List<int> xList);
                    x = CalculateNextPos(xList, x, command, dx);
                }
                
                maxDistance = Math.Max(maxDistance, x * x + y * y);
            }
        }
        return maxDistance;
    }

    private int CalculateNextPos(List<int> sortedList, int currentPos, int step, int delta) {

        if(sortedList == null) return currentPos + (step * delta);

        int target = currentPos + (step * delta);
        int idx = sortedList.BinarySearch(currentPos);
        if (idx < 0) idx = ~idx;

        if(delta > 0){ 
           
            int nextIdx = sortedList.BinarySearch(currentPos);
            if(nextIdx >= 0) nextIdx++; 
            else nextIdx = ~nextIdx;

            if(nextIdx < sortedList.Count && sortedList[nextIdx] <= target)
                return sortedList[nextIdx] - 1; 
            
        }else { 
            
            int prevIdx = sortedList.BinarySearch(currentPos);
            if (prevIdx >= 0) prevIdx--; 
            else prevIdx = (~prevIdx) - 1;

            if (prevIdx >= 0 && sortedList[prevIdx] >= target) 
                return sortedList[prevIdx] + 1;
            
        }

        return target; 
    }
}