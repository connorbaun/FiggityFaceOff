using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public GameObject punchHitbox;
    private PlayerAnimator myAnim;
   

	// Use this for initialization
	void Start ()
    {
        punchHitbox.SetActive(false);
        myAnim = GetComponent<PlayerAnimator>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(GetComponent<BoxCollider2D>().offset);
	}


    public IEnumerator Punch(float xOffset, float yOffset, float xSize, float ySize)
    {
        punchHitbox.SetActive(true);
        punchHitbox.GetComponent<BoxCollider2D>().offset = new Vector2(.2f, .1f);
        punchHitbox.GetComponent<BoxCollider2D>().size = new Vector2(.2f, .5f);
        punchHitbox.GetComponent<BoxCollider2D>().tag = "hitbox";


        yield return new WaitForSeconds(.25f);

        punchHitbox.SetActive(false);
    }

    public IEnumerator Special(float xOffset, float yOffset, float xSize, float ySize)
    {
        punchHitbox.SetActive(true);
        //punchHitbox.GetComponent<BoxCollider2D>().offset = new Vector2(.5f, .1f);
        punchHitbox.GetComponent<BoxCollider2D>().size = new Vector2(1.5f, 1f);
        punchHitbox.GetComponent<BoxCollider2D>().tag = "special";

        yield return new WaitForSeconds(.25f);

        punchHitbox.SetActive(false);
    }
}
