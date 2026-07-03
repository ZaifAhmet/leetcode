class Solution {

    public int numIslands(char[][] grid) {
        int result = 0;
        int m = grid.length, n = grid[0].length;
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                if (grid[i][j] == '1') {
                    rec(grid, i, j);
                    result += 1;
                }
            }
        }
        return result;
    }

    public void rec(char[][] grid, int l, int w) {
        if (grid[l][w] == '0')
            return;
        grid[l][w] = '0';
        if (l + 1 < grid.length)
            rec(grid, l + 1, w);
        if (w + 1 < grid[0].length)
            rec(grid, l, w + 1);
        if (l - 1 >= 0)
            rec(grid, l - 1, w);
        if (w - 1 >= 0)
            rec(grid, l, w - 1);
        //if(l + 1 < grid.length && w + 1 < grid[0].length) rec(grid,l+1,w+1);
    }
}