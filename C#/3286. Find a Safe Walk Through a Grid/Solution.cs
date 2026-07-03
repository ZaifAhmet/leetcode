using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Solution {
    public bool FindSafeWalk(IList<IList<int>> grid, int health) {
        int rowCount = grid.Count;
        int colCount = grid[0].Count;

        var directions = new (int dRow,int dCol)[]{
            (-1,0),
            (1,0),
            (0,-1),
            (0,1)
        };

        int[,] minDamage = new int[rowCount,colCount];

        for (int i = 0; i < rowCount; i++){
            for (int j = 0; j < colCount;j++){
                minDamage[i,j] = int.MaxValue;
            }
        }

        LinkedList<(int row,int col,int currentDamage)> deque = new LinkedList<(int row,int col,int currentDamage)>();
        deque.AddFirst((0,0,grid[0][0]));
        minDamage[0,0] = grid[0][0];

        while(deque.Count > 0){
            var (currRow, currCol, currDamage) = deque.First.Value;
            deque.RemoveFirst();

            if(currRow == rowCount - 1 && currCol == colCount - 1){
                health -= minDamage[rowCount-1,colCount-1];
                break;
            }

            foreach(var dir in directions){

                int newRow = currRow + dir.dRow;
                int newCol = currCol + dir.dCol;

                if(newRow >= 0 && newRow < rowCount && newCol >= 0 && newCol < colCount){
                    
                    int weight = grid[newRow][newCol] + currDamage;

                    if(minDamage[newRow,newCol] > weight){
                        minDamage[newRow,newCol] = weight;

                        if(grid[newRow][newCol] == 0) deque.AddFirst((newRow,newCol,weight));
                        else deque.AddLast((newRow,newCol,weight));
                    }
                }
            }

        }

        if(health > 0) return true;
        return false;
    }
}