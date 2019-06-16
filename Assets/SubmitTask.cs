using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SubmitTask : MonoBehaviour
{
    public int playerNo;
    private List<Task> _tasks;
    private Player Player;
    [FormerlySerializedAs("Dropdown")] public Dropdown dropdown;

    [FormerlySerializedAs("Text")] public Text text;

    [FormerlySerializedAs("Submit")] public Button submit;

    // Start is called before the first frame update
    void Start()
    {
        _tasks = GameObject.Find("GameState").GetComponent<GameState>().humanTeam.available;
        Player = GameObject.Find("GameState").GetComponent<GameState>().humanTeam.players[playerNo - 1];
        submit.onClick.AddListener(delegate { Submit(); });
    }


    private void Submit()
    {
        Player.current = _tasks.Where(item => item.label == dropdown.options[dropdown.value].text).First();
    }
}