using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UpdatePlayerStats : MonoBehaviour
{
    public readonly int MAX = 1000000;

    public int playerNo;
    public Player Player;
    public SimpleHealthBar hygieneBar;
    public SimpleHealthBar sanityBar;
    public SimpleHealthBar energyBar;
    public SimpleHealthBar foodnessBar;
    private bool first = true;

    // Start is called before the first frame update
    void Start()
    {
        hygieneBar.UpdateColor(Color.red);
        sanityBar.UpdateColor(Color.magenta);
        energyBar.UpdateColor(Color.yellow);
        foodnessBar.UpdateColor(Color.gray);
    }

    // Update is called once per frame
    void Update()
    {
        if (first)
        {
            List<Player> humans = GameObject.Find("GameState").GetComponent<GameState>().humanTeam.players;
            Player = humans[playerNo - 1];
            first = false;
        }

        hygieneBar.UpdateBar(Player.hygeine, MAX);
        sanityBar.UpdateBar(Player.sanity, MAX);
        energyBar.UpdateBar(Player.energy, MAX);
        foodnessBar.UpdateBar(Player.foodness, MAX);
    }
}