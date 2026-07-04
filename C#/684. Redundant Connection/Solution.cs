public class Solution {
    int[] parent;
    int[] rank;

    public int[] FindRedundantConnection(int[][] edges) {
        int length = edges.Length + 1;
        parent = new int[length];
        rank = new int[length];

        for(int i = 1;i < length; i++){
            parent[i] = i;
            rank[i] = 0;
        }

        int[] res = new int[2];

        for(int i = 0; i < edges.Length; i++){
            if(!Union(edges[i][0],edges[i][1])){
                res = edges[i];
            }
        }
        return res;
    }

    public int Find(int i){
        if(parent[i] != i){
            parent[i] = Find(parent[i]);
        }
        return parent[i];
    }

    public bool Union(int i,int j){
        int rootI = Find(i);
        int rootJ = Find(j);

        if(rootI == rootJ) return false;

        if(rank[rootI] < rank[rootJ]){
            parent[rootI] = rootJ;
        }else if(rank[rootI] > rank[rootJ]){
            parent[rootJ] = rootI;
        }else{
            parent[rootJ] = rootI;
            rank[rootI]++;
        }
        return true;
    }
}