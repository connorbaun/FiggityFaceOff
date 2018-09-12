using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {
    public int p1HP = 100;
    public int p2HP = 100;
    public Slider p1Health;
    public Slider p2Health;

    private HUDManager hud;
    private StateManager state;
    


    // Use this for initialization
    void Start ()
    {
        hud = FindObjectOfType<HUDManager>();
        state = FindObjectOfType<StateManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        p1Health.value = p1HP;
        p2Health.value = p2HP;

        if (p1HP <= 0)
        {
            Debug.Log("Player 2 has won");
            StartCoroutine(hud.VictoryUI(1));
            StartCoroutine(state.PlayerVictory());

        }

        if (p2HP <= 0)
        {
            Debug.Log("Player 1 has won");
            StartCoroutine(hud.VictoryUI(2));
            StartCoroutine(state.PlayerVictory());
        }
	}

}
