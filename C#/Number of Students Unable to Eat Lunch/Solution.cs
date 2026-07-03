
public class Solution {
    public int CountStudents(int[] students, int[] sandwiches) {
        int studentFrontIdx = 0, sandwichTopIdx = 0, studentMinIdx = 0;

        for(int i = 0; i < students.Length * 4; i++){
            if(sandwichTopIdx >= sandwiches.Length) break;
            if(studentFrontIdx >= students.Length) studentFrontIdx = studentMinIdx;
            if(students[studentFrontIdx] == sandwiches[sandwichTopIdx]){
                students[studentFrontIdx] = students[studentMinIdx];
                sandwichTopIdx++;
                studentMinIdx++;
            }
                studentFrontIdx++;
        }
        return students.Length - studentMinIdx;
    }
}