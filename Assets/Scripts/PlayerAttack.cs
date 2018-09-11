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


    public IEnumerator Punch()
    {
        punchHitbox.SetActive(true);
        punchHitbox.GetComponent<BoxCollider2D>().offset = new Vector2(.2f, .1f);

        yield return new WaitForSeconds(.25f);

        punchHitbox.SetActive(false);
    }
}
