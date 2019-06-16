using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace DefaultNamespace
{
    public class Traverse
    {
        private readonly Dictionary<String, HashSet<Task>> dependancies;

        public Traverse()
        {
            dependancies = trav(Task.CreateTaskTree());
        }

        private Dictionary<String, HashSet<Task>> trav(Task tree)
        {
            Dictionary<String, HashSet<Task>> dependancies = new Dictionary<String, HashSet<Task>>();
            return aux(tree, "", dependancies);
        }

        private Dictionary<String, HashSet<Task>> aux(Task task, String label,
            Dictionary<String, HashSet<Task>> dependancies)
        {
            dependancies[label].Add(task);
            foreach (var next in task.nextTasks)
            {
                aux(next, task.label, dependancies);
            }

            return dependancies;
        }
    }
}