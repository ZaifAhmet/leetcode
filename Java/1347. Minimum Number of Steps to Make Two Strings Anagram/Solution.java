class Solution {
    public int minSteps(String s, String t) {
        Map<Character, Integer> mapS = new HashMap<>();
        Map<Character, Integer> mapT = new HashMap<>();
        int Count = t.length() - 1;
        int result = 0;
        
        while(Count >= 0) {
            mapT.put(t.charAt(Count), mapT.getOrDefault(t.charAt(Count), 0) + 1);
            mapS.put(s.charAt(Count), mapS.getOrDefault(s.charAt(Count), 0) + 1);
            Count--;
        }
        
        for(Map.Entry<Character,Integer> eT : mapT.entrySet()){
           if(mapS.containsKey(eT.getKey()) && eT.getValue() > mapS.get(eT.getKey()))
            result += eT.getValue() - mapS.get(eT.getKey());
           if(!mapS.containsKey(eT.getKey()))
            result += eT.getValue();
        }
        
        return result;
    }
}