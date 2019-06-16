using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour {
    public Dropdown player1Dropdown;
    public Dropdown player2Dropdown;
    public Dropdown player3Dropdown;
    public Dropdown player4Dropdown;

    public Team humans;
    public Player player1;
    public Player player2;
    public Player player3;
    public Player player4;

    public List<Task> available;

    public List<string> options;

    public bool first = true;
    
    // Start is called before the first frame update
    void Start() {
        options = new List<string>();
    }

    // Update is called once per frame
    void Update() {
        if (first) {
            humans = GameObject.Find("GameState").GetComponent<GameState>().humanTeam;

            player1 = humans.players[0];
            player2 = humans.players[1];
            player3 = humans.players[2];
            player4 = humans.players[3];

            available = humans.available;
            
            first = false;
        }
        
        player1Dropdown.ClearOptions();
        player2Dropdown.ClearOptions();
        player3Dropdown.ClearOptions();
        player4Dropdown.ClearOptions();
        
        options.Clear();
        
        foreach (var task in available) {
            options.Add(task.label);
        }
        
        player1Dropdown.AddOptions(options);
        player2Dropdown.AddOptions(options);
        player3Dropdown.AddOptions(options);
        player4Dropdown.AddOptions(options);

    }

    void Player1Update() {
        
    }

    void PLayer2Update() {
        
    }

    void Player3Update() {
        
    }

    void Player4Update() {
        
    }
}
