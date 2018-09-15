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
        // yield return new WaitForSeconds(punchWindUpTime);
            //have the player wait until specified time to spawn hit box

        punchHitbox.SetActive(true);
        punchHitbox.GetComponent<BoxCollider2D>().offset = new Vector2(xOffset, yOffset);
        punchHitbox.GetComponent<BoxCollider2D>().size = new Vector2(xSize, ySize);
        punchHitbox.GetComponent<BoxCollider2D>().tag = "hitbox";


        yield return new WaitForSeconds(.25f);

        punchHitbox.SetActive(false);
    }

    public IEnumerator Special(float xOffset, float yOffset, float xSize, float ySize)
    {
        // yield return new WaitForSeconds(specialWindupTime);
            //have the player wait until specified time to spawn hitbox

        punchHitbox.SetActive(true);
        punchHitbox.GetComponent<BoxCollider2D>().offset = new Vector2(xOffset, yOffset);
        punchHitbox.GetComponent<BoxCollider2D>().size = new Vector2(xSize, ySize);
        punchHitbox.GetComponent<BoxCollider2D>().tag = "special";

        yield return new WaitForSeconds(.25f);

        punchHitbox.SetActive(false);
    }
}
