using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    public Team humanTeam;
    public Team opposingTeam;
    public int time;
    public int tickSize;

    public enum GameSpeed { 
        Tryhard, Slow, Normal, Fast, Fastest
    }

    public int GetGameTickIncrement(GameSpeed speed) {
        switch (speed) {
            case GameSpeed.Tryhard:
                return 1 * 6;
            case GameSpeed.Slow:
                return 2 * 6;
            case GameSpeed.Normal:
                return 4 * 6;
            case GameSpeed.Fast:
                return 8 * 6;
            case GameSpeed.Fastest:
                return 16 * 6;
        }
        return 4;
    }

    // Start is called before the first frame update
    void Start() {
        this.humanTeam = new Team();
        this.opposingTeam = new Team();

        this.time = 0;
        this.tickSize = GetGameTickIncrement(GameSpeed.Normal);
    }

    // Update is called once per frame
    void FixedUpdate() {
        // Time ticks along...
        this.time += this.tickSize;
        
        // Check to see if the game is over
        if (this.time >= MaxTime.MAX_TIME) {
            GameOver();
        }
        
        // Update Players
        this.humanTeam.Update(this.tickSize);
        
        // Update AI
        this.opposingTeam.Update(this.tickSize);
        this.opposingTeam.MakeAIMove();
        
    }

    void GameOver() {
        // TODO: maybe implement this :)
    }

    void UpdateSpeed(GameSpeed newSpeed) {
        this.tickSize = GetGameTickIncrement(newSpeed);
    }
    
}
