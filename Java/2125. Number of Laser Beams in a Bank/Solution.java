class Solution {
    public int numberOfBeams(String[] bank) {
       int result = 0, i = 0;
        Integer num[] = new Integer[bank.length];
        for (String string : bank) {

            num[i] = 0;
            for (char s : string.toCharArray()) {
                if (s == '1') {
                    num[i]++;
                }
            }
            i++;
        }

        for (int j = 0; j < num.length; j++) {
            int temp1 = 0;
            int temp2 = 0;
            int temp = 0;
            if (num[j] > 0 && j != num.length-1) {
                temp1 = num[j];
                for (int k = j + 1; k < num.length; k++) {
                    if (num[k] > 0) {
                        temp2 = num[k];
                        break;
                    }
                }
                temp = temp1*temp2;
            }

            result += temp;
        }
        return result;
    }
}