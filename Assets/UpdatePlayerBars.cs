using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayerBars : MonoBehaviour {
    public readonly int MAX = 1000000;
    
    public Player player1;
    public Player player2;
    public Player player3;
    public Player player4;
    
    public SimpleHealthBar p1HygieneBar;
    public SimpleHealthBar p1SanityBar;
    public SimpleHealthBar p1EnergyBar;
    public SimpleHealthBar p1FoodnessBar;
    
    public SimpleHealthBar p2HygieneBar;
    public SimpleHealthBar p2SanityBar;
    public SimpleHealthBar p2EnergyBar;
    public SimpleHealthBar p2FoodnessBar;
    
    public SimpleHealthBar p3HygieneBar;
    public SimpleHealthBar p3SanityBar;
    public SimpleHealthBar p3EnergyBar;
    public SimpleHealthBar p3FoodnessBar;
    
    public SimpleHealthBar p4HygieneBar;
    public SimpleHealthBar p4SanityBar;
    public SimpleHealthBar p4EnergyBar;
    public SimpleHealthBar p4FoodnessBar;
    
    public bool first = true;
    
    // Start is called before the first frame update
    void Start() {
        p1HygieneBar.UpdateColor(Color.red);
        p2HygieneBar.UpdateColor(Color.red);
        p3HygieneBar.UpdateColor(Color.red);
        p4HygieneBar.UpdateColor(Color.red);
        
        p1SanityBar.UpdateColor(Color.magenta);
        p2SanityBar.UpdateColor(Color.magenta);
        p3SanityBar.UpdateColor(Color.magenta);
        p4SanityBar.UpdateColor(Color.magenta);

        p1EnergyBar.UpdateColor(Color.yellow);
        p2EnergyBar.UpdateColor(Color.yellow);
        p3EnergyBar.UpdateColor(Color.yellow);
        p4EnergyBar.UpdateColor(Color.yellow);
        
        p1FoodnessBar.UpdateColor(Color.grey);
        p2FoodnessBar.UpdateColor(Color.grey);
        p3FoodnessBar.UpdateColor(Color.grey);
        p4FoodnessBar.UpdateColor(Color.grey);
    }

    // Update is called once per frame
    void Update() {
        if (first) {
            List<Player> humans = GameObject.Find("GameState").GetComponent<GameState>().humanTeam.players;
            
            this.player1 = humans[0];
            this.player2 = humans[1];
            this.player3 = humans[2];
            this.player4 = humans[3];

            this.first = false;
        }
        
        p1HygieneBar.UpdateBar(player1.hygeine, MAX);
        p2HygieneBar.UpdateBar(player2.hygeine, MAX);
        p3HygieneBar.UpdateBar(player3.hygeine, MAX);
        p4HygieneBar.UpdateBar(player4.hygeine, MAX);
        
        p1SanityBar.UpdateBar(player1.sanity, MAX);
        p2SanityBar.UpdateBar(player2.sanity, MAX);
        p3SanityBar.UpdateBar(player3.sanity, MAX);
        p4SanityBar.UpdateBar(player4.sanity, MAX);

        p1EnergyBar.UpdateBar(player1.energy, MAX);
        p2EnergyBar.UpdateBar(player2.energy, MAX);
        p3EnergyBar.UpdateBar(player3.energy, MAX);
        p4EnergyBar.UpdateBar(player4.energy, MAX);
        
        p1FoodnessBar.UpdateBar(player1.foodness, MAX);
        p2FoodnessBar.UpdateBar(player2.foodness, MAX);
        p3FoodnessBar.UpdateBar(player3.foodness, MAX);
        p4FoodnessBar.UpdateBar(player4.foodness, MAX);
        
    }
}