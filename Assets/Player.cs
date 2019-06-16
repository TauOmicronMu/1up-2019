using System.Collections.Generic;
using UnityEngine.PlayerLoop;

[System.Serializable]
public class Player {
    public int id;
    
    public int hygeine;
    public int sanity;
    public int energy;
    public int foodness;

    public Task current;

    public Task justCompleted;
    public List<int> boosts;
    public List<bool> boostsGlobal;
    public Fixes fixes;
    
    public Player(int id) {
        this.id = id;
        
        this.hygeine = 1000000;
        this.sanity = 1000000;
        this.energy = 1000000;
        this.foodness = 1000000;
        
        this.current = null;

        this.boosts = null;
        this.boostsGlobal = null;

        this.justCompleted = null;
        this.fixes = Fixes.None;
    }

    public double PerformanceMultiplier() {
        return (sanity + energy + foodness) / 3000000.0;
    }

    public void Update(int tickSize) {
        this.sanity -= tickSize;
        this.hygeine -= tickSize * 4;
        this.energy -= tickSize * 8;
        this.foodness -= tickSize * 16;

        if (hygeine < 0) hygeine = 0;
        if (energy < 0) energy = 0;
        if (foodness < 0) foodness = 0;
        if (sanity < 0) sanity = 0;
        
        if (hygeine > 1000000) hygeine = 1000000;
        if (energy > 1000000) energy = 1000000;
        if (foodness > 1000000) foodness = 1000000;
        if (sanity > 1000000) sanity = 1000000;
        
        if (this.current == null) return;

        // Player works slower based on their values.
        this.current.Update((int) (tickSize * PerformanceMultiplier()) );
        if (!this.current.IsComplete()) {
            return;
        }

        if (this.current.special) {
            this.boosts = this.current.boosts;
            this.boostsGlobal = this.current.boostsGlobal;
            this.fixes = this.current.fixes;
        }

        this.justCompleted = current;
        this.current = null;

        // TODO: should we notify the players with some SFX or something?
    }
}
