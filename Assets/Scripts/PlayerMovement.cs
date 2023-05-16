using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameManager gmScript;
    private Animator anim;
    private SpriteRenderer sprRender;
    private Rigidbody2D rb;

    public Vector3 horizontal;
    public Vector3 vertical;

    public AudioSource audioSrc;

    //Clips
    public AudioClip pickupSound;
    public AudioClip walkingSound;
    public AudioClip depositSound;

    [SerializeField]
    private bool holdingGarbage;
    [SerializeField]
    private int speed;

    //0 is forward, 1 is backward
    private int facing;
    

    //0 is right, 1 is left;
    private int horizontalFacing;

    private float h;
    private float v;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        audioSrc = GetComponent<AudioSource>();
        gmScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        sprRender = GetComponent<SpriteRenderer>();
        facing = 0;
        holdingGarbage=false;
        SetAnimator(facing, holdingGarbage);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        
        Vector2 playerInput = new Vector2(h,v);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            facing = 1;
            SetAnimator(facing, holdingGarbage);
            anim.SetTrigger("Walk");
            playerInput.y = 1;
            Debug.Log(h);
            //rb.MovePosition(transform.position+vertical * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            facing = 0;
            SetAnimator(facing, holdingGarbage);
            anim.SetTrigger("Walk");
            playerInput.y = -1;
            //rb.MovePosition(transform.position-vertical * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            sprRender.flipX=false;
            SetAnimator(facing, holdingGarbage);
            anim.SetTrigger("Walk");
            playerInput.x = 1;
            //rb.MovePosition(transform.position+horizontal * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            sprRender.flipX=true;
            SetAnimator(facing, holdingGarbage);
            anim.SetTrigger("Walk");
            playerInput.x = -1;
            //rb.MovePosition(transform.position-horizontal * Time.deltaTime);
        }
        
        if (!Input.anyKey)
        {
            playerInput = Vector2.zero;
            anim.SetTrigger("Idle");
        }
        
        rb.AddForce(playerInput*Time.deltaTime*speed, ForceMode2D.Impulse);
        

    }

    private void OnCollisionEnter2D(Collision2D other) {
        switch (other.gameObject.tag)
        {
            case "Trash":
                audioSrc.PlayOneShot(pickupSound);
                Destroy(other.gameObject);
                holdingGarbage=true;
                gmScript.amountOfGarbageHeld++;
                break;
            case "Bin":
                if (holdingGarbage)
                {
                    audioSrc.PlayOneShot(pickupSound);
                    gmScript.TrashCollected+=gmScript.amountOfGarbageHeld;
                    holdingGarbage=false;
                    gmScript.amountOfGarbageHeld=0;
                    gmScript.UpdateText();
                }
                break;
            

        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        switch (other.gameObject.tag)
        {
            case "ArcadeMachine":
                gmScript.spacebarPromptText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    other.gameObject.GetComponent<SpriteRenderer>().sprite = gmScript.cleanArcadeMachine;
                } 
                break;
            case "PizzaTable":
                gmScript.spacebarPromptText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                other.gameObject.GetComponent<SpriteRenderer>().sprite = gmScript.cleanPizzaTable;
                }
                break;
            case "PizzaStool":
                gmScript.spacebarPromptText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                other.gameObject.GetComponent<SpriteRenderer>().sprite = gmScript.cleanPizzaStool;
                }
                break;
            case "Bed":
                gmScript.spacebarPromptText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                other.gameObject.GetComponent<SpriteRenderer>().sprite = gmScript.cleanBed;
                }
                break;
            case "Stall":
                gmScript.spacebarPromptText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                other.gameObject.GetComponent<SpriteRenderer>().sprite = gmScript.cleanStall;
                }
                break;
            case "BedsideTable":
                gmScript.spacebarPromptText.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                other.gameObject.GetComponent<SpriteRenderer>().sprite = gmScript.cleanBedside;
                }
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        switch (other.gameObject.tag)
        {
            case "ArcadeMachine":
                gmScript.spacebarPromptText.gameObject.SetActive(false);
                break;
            case "PizzaTable":
                gmScript.spacebarPromptText.gameObject.SetActive(false);
                break;
            case "PizzaStool":
                gmScript.spacebarPromptText.gameObject.SetActive(false);
                break;
            case "Bed":
                gmScript.spacebarPromptText.gameObject.SetActive(false);
                break;
        }
    }

    void SetAnimator(int face, bool hasGarbage){

        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0f);
        }
        if(face == 0 && !hasGarbage)
        {
            anim.SetLayerWeight(0, 1f);
        }
        else if (face == 0 && hasGarbage)
        {
            anim.SetLayerWeight(1, 1f);
        }
        else if(face == 1 && hasGarbage)
        {
            anim.SetLayerWeight(2, 1f);
        }
        else if(face == 1 && !hasGarbage)
        {
            anim.SetLayerWeight(3, 1f);
        }
    }
}
