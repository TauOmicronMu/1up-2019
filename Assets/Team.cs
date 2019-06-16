using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class Team {
    public CoffeeMachine coffeeMachine;
    public Whiteboard whiteboard;
    public NetworkSwitch networkSwitch;
    public Phone phone;
    public Toilet toilet;

    public List<Player> players;
    public List<Task> completed;

    public Task tasks;

    public List<Task> available;

    public Team() {
        this.coffeeMachine = new CoffeeMachine();
        this.whiteboard = new Whiteboard();
        this.networkSwitch = new NetworkSwitch();
        this.phone = new Phone();
        this.toilet = new Toilet();
        
        this.players = new List<Player>();
        for (int i = 0; i < 4; i++) {
            this.players.Add(new Player(i));
        }

        this.tasks = Task.CreateTaskTree(); // Creates a new tree via the factory method

        this.completed = new List<Task>();
        this.completed.Add(this.tasks);
        
        this.available = GetAvailableTasks();
    }

    public void Update(int tickSize) {
        switch (Random.Range(0, 5)) {
            case 0:
                this.coffeeMachine.Update(tickSize);
                break;
            case 1:
                this.whiteboard.Update(tickSize);
                break;
            case 2:
                this.networkSwitch.Update(tickSize);
                break;
            case 3:
                this.phone.Update(tickSize);
                break;
            case 4:
                this.toilet.Update(tickSize);
                break;
        }

        int hygieneDeficit = 0;
        foreach (var player in this.players) {
            // Work out which players smell awful
            if (player.hygeine < 500000) {
                hygieneDeficit += (500000 - player.hygeine);
            }
        }

        // Make their sanity lowwwww
        int sanityDamage = hygieneDeficit / 1600;
        foreach (var player in this.players) {
            player.sanity -= sanityDamage;

            // Update each player
            player.Update(tickSize);
        }
        
        foreach (var player in this.players) {
            // Check to see if the player just completed a task!
            if (player.current == null && player.justCompleted != null) {
                if(!this.completed.Contains(player.justCompleted) && this.tasks.Contains(player.justCompleted)) {
                    this.completed.Add(player.justCompleted);
                }
                
                // Remove it from all other players so we don't re-apply each of the boosts
                foreach (var p in this.players) {
                    if (p.id == player.id) continue;
                    if (p.current == null) continue;
                    if (p.current.label.Equals(player.justCompleted.label)) {
                        p.boosts = null;
                        p.boostsGlobal = null;
                        p.fixes = Fixes.None;
                        p.current = null;
                        p.justCompleted = null;
                    }
                }
                
                player.justCompleted = null;
                this.available = GetAvailableTasks();
            }
            
            // Apply any boosts from each player completing tasks
            if (player.boosts == null) continue;
            if (player.boostsGlobal == null) continue;
            if (player.fixes != Fixes.None) {
                // If it was a fix task, fix the respective breakable
                switch (player.fixes) {
                    case Fixes.Coffee:
                        this.coffeeMachine.Fix();
                        break;
                    case Fixes.Whiteboard:
                        this.whiteboard.Fix();
                        break;
                    case Fixes.Switch:
                        this.networkSwitch.Fix();
                        break;
                    case Fixes.Phone:
                        this.phone.Fix();
                        break;
                    case Fixes.Toilet:
                        this.toilet.Fix();
                        break;
                }
            }

            for (int i = 0; i < player.boosts.Count; i++) {
                if (!player.boostsGlobal[i]) {
                    // Apply single-player buffs to the one who completed the task.
                    player.hygeine += player.boosts[0];
                    player.sanity += player.boosts[1];
                    player.energy += player.boosts[2];
                    player.foodness += player.boosts[3];
                }
                else {
                    // If it's a global buff, apply it to all players
                    foreach (var boostee in this.players) {
                        boostee.hygeine += player.boosts[0];
                        boostee.sanity += player.boosts[1];
                        boostee.energy += player.boosts[2];
                        boostee.foodness += player.boosts[3];
                    }
                }
                
            }
            
            player.boosts = null;
            player.boostsGlobal = null;
            player.fixes = Fixes.None;
        }
    }

    /*
     * Is a task with a given label completed?
     */
    public bool CheckComplete(string label, Task task) {
        if (task.label.Equals(label)) {
            return task.IsComplete();
        }

        foreach (var t in task.nextTasks) {
            CheckComplete(label, t);
        }

        return false;
    }

    public List<Task> GetAvailableTasks() {
        List<Task> available = new List<Task>();

        // First of all, get the tasks from each breakable
        Task coffeeTask = coffeeMachine.getSpecialTask();
        Task whiteboardTask = whiteboard.getSpecialTask();
        Task switchTask = networkSwitch.getSpecialTask();
        Task phoneTask = phone.getSpecialTask();
        Task toiletTask = toilet.getSpecialTask();

        available.Add(coffeeTask);
        available.Add(whiteboardTask);
        available.Add(phoneTask);
        available.Add(toiletTask);

        if (switchTask != null) {
            available.Add(switchTask);
        }

        // For each Task, if it is completed, look at its children (recursively?),
        // If it isn't completed, then add it to the list iff all of its parents
        // are completed. 

        // Either doubly link the tree
        // Or check all available with their children for references to task
        
        completed.ForEach((t) => {
            if (t.nextTasks != null) {
                available.AddRange(t.nextTasks.FindAll((ta) => !ta.IsComplete()));
            }
        });
        var workingSet = new HashSet<Task>();
        var toRemove = new List<Task>();
        available.ForEach((t) => searchForShit(workingSet, toRemove, t));
        available.RemoveAll((t) => toRemove.Contains(t));
        
        return available;
    }

    public void searchForShit(HashSet<Task> workingSet, List<Task> toRemove, Task toTest) {
        if (workingSet.Contains(toTest)) {
            toRemove.Add(toTest);
        }

        workingSet.Add(toTest);
        if (toTest == null) return;
        if (toTest.nextTasks == null) return;
        foreach (var task in toTest.nextTasks) {
            searchForShit(workingSet, toRemove, task);
        }
    }
    
    public void MakeAIMove() {
        foreach (var player in this.players) {
            List<Task> available = GetAvailableTasks();
            if (player.current == null) {
                player.current = available[Random.Range(0, available.Count)];
            }
        }
    }
}

[System.Serializable]
public abstract class Breakable {
    public bool isBroken = false;
    public string brokenString;
    public string fineAndDandyString;
    public Fixes fixes;

    public List<int> boosts;
    public List<bool> boostsGlobal;
    
    public bool DoesItBreakQuestionMark() {
        return (Random.Range(0, MaxTime.MAX_TIME) <= 16);
    }

    public void Fix() {
        this.isBroken = false;
    }

    public void Update(int tickSize) {
        for (int i = 0; i < tickSize; i++) {
            if (DoesItBreakQuestionMark()) isBroken = true;
        }
    }

    public Task getSpecialTask() {
        if (isBroken) {
            return new Task(this.brokenString, 5000, new List<int>() {0, 50000, -50000, 0},
                new List<bool>() {false, true, false, false}, this.fixes);
        }

        return new Task(this.fineAndDandyString, 5000, this.boosts, this.boostsGlobal, Fixes.None);
    }
}

[System.Serializable]
public class CoffeeMachine : Breakable {
    public CoffeeMachine() {
        this.brokenString = "Fix Coffee Machine";
        this.fineAndDandyString = "Make Coffee";
        this.fixes = Fixes.Coffee;
        this.boosts = new List<int>() {-25000, -50000, 250000, -10000};
        this.boostsGlobal = new List<bool>() {true, true, true, true};
    }
}

[System.Serializable]
public class Whiteboard : Breakable {
    public Whiteboard() {
        this.brokenString = "Clean Filthy Whiteboard";
        this.fineAndDandyString = "Plan Future Endeavours";
        this.fixes = Fixes.Whiteboard;
        this.boosts = new List<int>() {-35000, 25000, -25000, -25000};
        this.boostsGlobal = new List<bool>() {false, true, false, false};
    }
}

[System.Serializable]
public class NetworkSwitch : Breakable {
    public NetworkSwitch() {
        this.brokenString = "Turn Network Switch Off and On Again";
        this.fineAndDandyString = "Stare Aimlessly at Switch";
        this.fixes = Fixes.Switch;
        this.boosts = new List<int>() {0, -25000, 0, 0};
        this.boostsGlobal = new List<bool>() {false, false, false, false};
    }
}

[System.Serializable]
public class Phone : Breakable {
    public Phone() {
        this.brokenString = "Hit Phone With Hammer";
        this.fineAndDandyString = "Order Pizza";
        this.fixes = Fixes.Phone;
        this.boosts = new List<int>() {-50000, 30000, -45000, 300000};
        this.boostsGlobal = new List<bool>() {true, true, true, true};
    }
}

[System.Serializable]
public class Toilet : Breakable {
    public Toilet() {
        this.brokenString = "Unblock Toilet";
        this.fineAndDandyString = "Relieve Oneself";
        this.fixes = Fixes.Toilet;
        this.boosts = new List<int>() {100000, 450000, 25000, -50000};
        this.boostsGlobal = new List<bool>() {false, false, false, false};
    }
}