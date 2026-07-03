class Solution {
    public int findMaxFish(int[][] grid) {
        int m = grid.length;
        int n = grid[0].length;
        // İki boyutlu visited dizisi: Ziyaret edilen hücreler true
        boolean[][] visited = new boolean[m][n];
        int maxFish = 0;

        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                // 0'dan büyük bir hücreyi henüz ziyaret etmemişsek BFS yap
                if (grid[i][j] > 0 && !visited[i][j]) {
                    int fishCount = bfs(grid, i, j, visited);
                    if (fishCount > maxFish) {
                        maxFish = fishCount;
                    }
                }
            }
        }
        return maxFish;
    }

    private int bfs(int[][] grid, int startX, int startY, boolean[][] visited) {
        int sum = 0;
        int[][] directions = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
        Queue<int[]> queue = new LinkedList<>();

        // Başlangıç hücresini kuyruğa at ve ziyaret olarak işaretle
        queue.add(new int[] { startX, startY });
        visited[startX][startY] = true;

        while (!queue.isEmpty()) {
            int[] current = queue.poll();
            int x = current[0];
            int y = current[1];

            sum += grid[x][y];

            // Komşuları kontrol et
            for (int[] dir : directions) {
                int newX = x + dir[0];
                int newY = y + dir[1];

                // Sınırlar içinde ve değer > 0 ise
                if (newX >= 0 && newX < grid.length && newY >= 0 && newY < grid[0].length) {
                    if (grid[newX][newY] > 0 && !visited[newX][newY]) {
                        visited[newX][newY] = true;
                        queue.add(new int[] { newX, newY });
                    }
                }
            }
        }
        return sum;
    }
}