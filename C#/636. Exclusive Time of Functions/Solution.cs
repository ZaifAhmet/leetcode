public class Solution {
        public int[] ExclusiveTime(int n, IList<string> logs) {
            Stack<(int id,int start)> stack = new();
            int[] sol = new int[n];

            foreach (var log in logs)
            {
                var entry = log.Split(':');
                int currentId = Int32.Parse(entry[0]);
                int time = Int32.Parse(entry[2]);

                if (entry[1] == "start")
                {
                    if (stack.Count > 0)
                    {
                        sol[stack.Peek().id] += time - stack.Peek().start;
                    }
                    stack.Push((currentId, time));
                }
                else
                {
                    var current = stack.Pop();
                    sol[current.id] += time - current.start + 1;

                    if (stack.Count > 0)
                    {
                        stack.Push((stack.Pop().id,time+1));
                    }
                }


            }

            return sol;
    }
}