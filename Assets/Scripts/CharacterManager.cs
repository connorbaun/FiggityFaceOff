using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public class Fighter
{
    public string name = "missing";

    public int punchDamage = 0;
    public int specialDamage = 0;

    public int moveSpeed = 0;

    public float punchBoxX = 0;
    public float punchBoxY = 0;

    public float specialBoxX = 0;
    public float specialBoxY = 0;

    public float punchOffsetX = 0;
    public float punchOffsetY = 0;

    public float specialOffsetX = 0;
    public float specialOffsetY = 0;

    public float punchForce = 0;

    public Fighter() { } //default (fallback) constructor

    public Fighter(string nm, int punch, int special, int speed, float punX, float punY, float specX, float specY, float punOffX, float punOffY, float specOffX, float specOffY, float pForce) //the real constructor that lets us make new Fighters
    {
        name = nm;
        punchDamage = punch;
        specialDamage = special;
        moveSpeed = speed;
        punchBoxX = punX;
        punchBoxY = punY;
        specialBoxX = specX;
        specialBoxY = specY;
        punchOffsetX = punOffX;
        punchOffsetY = punOffY;
        specialOffsetX = specOffX;
        specialOffsetY = specOffY;
        punchForce = pForce;
    }
}
