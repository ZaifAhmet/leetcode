using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Solution
{
    public int MaximumSafenessFactor(IList<IList<int>> grid) {
        int rowCount = grid.Count;
        int colCount = grid[0].Count;

        var directions = new (int dRow, int dCol)[]{
            (-1, 0), // yukarı
            (1, 0),  // aşağı
            (0, -1), // sol
            (0, 1)   // sağ
        };

        Queue<(int row, int col)> queue = new Queue<(int row, int col)>();
        
        int[][] safeGrid = new int[rowCount][];
        for (int i = 0; i < rowCount; i++)
        {
            safeGrid[i] = new int[colCount];
        }

        for (int row = 0; row < rowCount; row++){
            for (int col = 0; col < colCount; col++){
                if (grid[row][col] == 1){
                    queue.Enqueue((row, col)); 
                    safeGrid[row][col] = 0;
                } else {
                    safeGrid[row][col] = -1;
                }
            }
        }
        
        while (queue.Count > 0){
            var (currRow, currCol) = queue.Dequeue();
            foreach (var dir in directions){
                int newRow = currRow + dir.dRow;
                int newCol = currCol + dir.dCol;
                if (newRow >= 0 && newRow < rowCount && newCol >= 0 && newCol < colCount && 
                    safeGrid[newRow][newCol] == -1){
                    
                    queue.Enqueue((newRow, newCol));
                    safeGrid[newRow][newCol] = safeGrid[currRow][currCol] + 1;
                }
            }
        }
        
        int low = 0;
        int high = Math.Min(safeGrid[0][0], safeGrid[rowCount - 1][colCount - 1]);
        int mid;
        int maxScor = 0;

        while (low <= high){
            mid = (low + high) / 2;

            if (canReach(safeGrid, mid, directions)){ 
                maxScor = mid;
                low = mid + 1;
            } else {
                high = mid - 1;
            }
        }
        return maxScor;
    }

    private bool canReach(int[][] grid, int limit, (int dRow, int dCol)[] directions){
        if (grid[0][0] < limit) return false;

        int n = grid.Length;
        Queue<(int row, int col)> queue = new Queue<(int row, int col)>();
        bool[,] visited = new bool[n, n];
        queue.Enqueue((0, 0));
        visited[0, 0] = true;

        while (queue.Count > 0){
            var (currRow, currCol) = queue.Dequeue();

            if (currRow == n - 1 && currCol == n - 1) return true;

            foreach (var dir in directions){
                int newRow = currRow + dir.dRow;
                int newCol = currCol + dir.dCol;

                if (newRow >= 0 && newRow < n && newCol >= 0 && newCol < n){
                    if (grid[newRow][newCol] >= limit && !visited[newRow, newCol]){
                        visited[newRow, newCol] = true;
                        queue.Enqueue((newRow, newCol));
                    }
                }
            }
        }
        return false;
    }
}