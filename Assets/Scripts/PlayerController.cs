using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public int playerNumber = 0; //which player are we? one or two?
    public int speed; //how fast should we move when we push left or right?
    public int fighterIndex = 0; //player 1's current selected fighter
    public int fighterIndex2 = 0; //player 2's current selected fighter

    public bool isSelecting = true; //are we currently at the start menu?
    public bool canInput = false; //can we use the controller at this time?

    public List<string> fighters = new List<string>(); //a ref to the character roster in the game

    public List<Fighter> theFighters = new List<Fighter>();
    public Fighter conB = new Fighter("conB", 5, 18, 10, .2f, .2f, 1f, .5f, .2f, .35f, 0, 0, 200, .2f, .25f);
    public Fighter conO = new Fighter("conO", 8, 28, 7, .2f, .2f, 1.25f, .25f, .2f, .35f, 0, .5f, 200, .2f, 1f);
    public Fighter kev = new Fighter("kevin", 10, 20, 5, .2f, .2f, 1f, .25f, .3f, .5f, .3f, .75f, 200, .25f, .5f);
    public Fighter bern = new Fighter("bern", 3, 15, 15, .05f, .2f, .5f, .25f, .2f, .2f, .2f, .2f, 200, .25f, .1f);
    public Fighter christian = new Fighter("christian", 6, 20, 10, .2f, .2f, 1.5f, .5f, .15f, .5f, 0, 0, 200, .25f, .75f);

    public Fighter myFighter;
    
    public string _fighterName = null; //a ref to the character's fighter name

    [SerializeField]
    private PlayerMotor motor; //ref to the motor, where we will send our inputs to be applied

    [SerializeField]
    private PlayerAnimator myAnimator; //a ref to the animation script, which will tell our sprite how to animate

    private HUDManager hud; // a ref to the UIManager

    private StateManager state; //a ref to the gamestate manager

    private PlayerAttack attack;

    private HealthManager health;

    private bool isPaused = false;


	// Use this for initialization
	void Start ()
    {
        health = FindObjectOfType<HealthManager>();
        motor = GetComponent<PlayerMotor>(); //tell unity what we mean when we say motor

        myAnimator = GetComponent<PlayerAnimator>(); //tell untiy what we mean when we say anim
        myAnimator.SetStartingDirection(playerNumber); //send the playernum over to anim
        attack = GetComponent<PlayerAttack>();

        hud = FindObjectOfType<HUDManager>(); //find the HUDMANAGER obj

        state = FindObjectOfType<StateManager>();

        fighters.Add("conB");
        fighters.Add("conO");
        fighters.Add("bern");
        fighters.Add("christian");
        fighters.Add("kevin");

        theFighters.Add(conB);
        theFighters.Add(conO);
        theFighters.Add(kev);
        theFighters.Add(bern);
        theFighters.Add(christian);
    }

    // Update is called once per frame
    void Update ()
    {
       if (isSelecting == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (fighterIndex >= theFighters.Count - 1)
                {
                    fighterIndex = 0;
                }
                else
                {
                    fighterIndex++;
                }
                //find some way to scroll through the list of fighters.
            }


            if (Input.GetButtonDown("Fire2"))
            {
                if (fighterIndex2 >= theFighters.Count - 1)
                {
                    fighterIndex2 = 0;
                }
                else
                {
                    fighterIndex2++;
                }
            }

            if (Input.GetButtonDown("Pause"))
            {
                isSelecting = false;
                state.RoundCountdown();
            }
        }

        if (playerNumber == 1)
        {
            _fighterName = theFighters[fighterIndex].name;
        }

        if (playerNumber == 2)
        {
            _fighterName = theFighters[fighterIndex2].name;

        }

        hud.CollectNames(playerNumber, _fighterName); //collect player names for nameplates and send it over to the HUDmanager so it displays the proper name for each player

        if (playerNumber == 1)
        {
            speed = theFighters[fighterIndex].moveSpeed;
            myFighter = theFighters[fighterIndex];
            
        }

        if (playerNumber == 2)
        {
            speed = theFighters[fighterIndex2].moveSpeed;
            myFighter = theFighters[fighterIndex2];

        }




        if (canInput) //if player should be allowed to move during this frame...
        {
            //collect inputs in the form of floats
            float hInput = Input.GetAxisRaw(playerNumber + "Horizontal"); //if we push arrow key left/right, we will get -1, 0, or 1

            //Debug.Log(hInput); //make sure it's working, lol.

            //have those inputs affect our direction (transform.right)
            Vector3 _hMov = (transform.right * hInput); //so our horizontal dir will either be 0, left (-1) or right (1)

            //have those inputs be stored in a final "velocity" var which includes speed and time.deltatime
            Vector3 _velocity = (_hMov * speed); //we have a direction and a speed at which to go there

            //send that _velocity vec3 over to motor.
            motor.ReceiveVelocity(_velocity); //since this is in Update, the motor will constantly receive our current _velocity, and move our player based on that value


            if (Input.GetButtonDown("Submit")) //let players restart the round by pressing ps button
            {
                //state.RoundCountdown(); //repos players etc
                hud.SelectionUI(); //bring up the main menu text once again
                state.CharacterSelect();
            }

            if (hInput != 0)
            {
                myAnimator.RunAnim(_fighterName);
            }
            else if (hInput == 0)
            {
                myAnimator.IdleAnim(_fighterName);
            }

            myAnimator.FlipSprite(hInput); //send our current hInput over to SpriteAnimator

            if (Input.GetButtonDown(playerNumber + "Fire1"))
            {
                myAnimator.PunchAnim(_fighterName);
            }

            if (Input.GetButtonDown(playerNumber + "Fire2"))
            {
                //motor.PerformDash();
            }

            if (Input.GetButtonDown(playerNumber + "Fire3"))
            {
                myAnimator.SpecialAnim(_fighterName);
            }
        } else //otherwise just put him in the idle state NOTE This will prob cause problems when death state becomes implemented
        {
            myAnimator.IdleAnim(_fighterName);
        } 
       

        /*if (Input.GetButtonDown(playerNumber + "Fire3"))
        {
            motor.PerformJump();
        } */
	} 
   

    public IEnumerator UnlockController(float time)
    {
        canInput = false; //make it so we cannot use the controller

        yield return new WaitForSeconds(time);

        canInput = true;
    }

    public void FreezeController()
    {
        canInput = false;
    }

    public void UnfreezeController()
    {
        canInput = true;
    }

    public void FreezeMotor()
    {
        motor.ReceiveVelocity(Vector3.zero);
    }

    public void OnTriggerEnter2D(Collider2D collision) //if a TRIGGER ENTERS YOUR PLAYER...
    {
        if (collision.gameObject.name == "punch hitbox") //if a player is smacked by a punch collider...
        {
            //Debug.Log("PLAYER " + playerNumber + " was SOCKED"); //tell us who got socked.

            if (playerNumber == 1)
            {
                if (collision.GetComponent<Collider2D>().tag == "special")
                {
                    health.p1HP -= collision.gameObject.GetComponentInParent<PlayerController>().theFighters[fighterIndex2].specialDamage; //find p1's hp and decrease
                    motor.PerformDash();
                }
                else if (collision.GetComponent<Collider2D>().tag == "hitbox")
                {
                    health.p1HP -= collision.gameObject.GetComponentInParent<PlayerController>().theFighters[fighterIndex2].punchDamage; //find p1's hp and decrease
                    motor.PerformDash();
                }

            }
            else if (playerNumber == 2)
            {
                if (collision.GetComponent<BoxCollider2D>().tag == "special")
                {
                    health.p2HP -= collision.gameObject.GetComponentInParent<PlayerController>().theFighters[fighterIndex].specialDamage; //find p2's hp and decrease 
                    motor.PerformDash();
                }
                else if (collision.GetComponent<Collider2D>().tag == "hitbox")
                {
                    health.p2HP -= collision.gameObject.GetComponentInParent<PlayerController>().theFighters[fighterIndex].punchDamage; //find p2's hp and decrease 
                    motor.PerformDash();
                }

            }
            
        }
    }
}
