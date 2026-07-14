/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     int val;
 *     ListNode next;
 *     ListNode() {}
 *     ListNode(int val) { this.val = val; }
 *     ListNode(int val, ListNode next) { this.val = val; this.next = next; }
 * }
 */
class Solution {
    public ListNode addTwoNumbers(ListNode l1, ListNode l2) {
        ListNode node1 = l1;
        ListNode node2 = l2;
        ListNode node3 = new ListNode();
        ListNode l3 = node3;
        int sum = 0;
        int x1 = 0, x2 = 0;
        while (node1 != null || node2 != null) {

            if (node1 != null) {
                x1 = node1.val;
                node1 = node1.next;
            }
            if (node2 != null) {
                x2 = node2.val;
                node2 = node2.next;
            }

            if ((x1 + x2) >= 10) {
                node3.val = ((x1 + x2) % 10) + sum;
                sum = 1;
            } else {
                if (x1 + x2 + sum == 10) {
                    node3.val = 0;
                    sum = 1;
                } else {
                    node3.val = x1 + x2 + sum;
                    sum = 0;
                }
            }
            if (node1 != null || node2 != null) {
                node3.next = new ListNode();
                node3 = node3.next;
            }
            x1 = 0;
            x2 = 0;
        }
        if (sum == 1) {
            node3.next = new ListNode(1);
        }
        return l3;
    }
}