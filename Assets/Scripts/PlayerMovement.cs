using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameManager gmScript;

    public Animator anim;
    public SpriteRenderer sprRender;

    public Vector3 horizontal;
    public Vector3 vertical;

    public bool holdingGarbage;

    //0 is forward, 1 is backward
    private int facing;

    //0 is right, 1 is left;
    private int horizontalFacing;

    private void Awake() {
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
        Debug.Log(anim.GetLayerIndex("Base Layer"));
        Debug.Log(anim.GetLayerIndex("GarbageBack"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            SetAnimator(facing, holdingGarbage);
            anim.SetTrigger("Walk");
            transform.position+=vertical;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            SetAnimator(facing, holdingGarbage);
            anim.SetTrigger("Walk");
            transform.position-=vertical;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            sprRender.flipX=false;
            SetAnimator(facing, holdingGarbage);
            anim.SetTrigger("Walk");
            transform.position+=horizontal;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            sprRender.flipX=true;
            SetAnimator(facing, holdingGarbage);
            anim.SetTrigger("Walk");
            transform.position-=horizontal;
        }
        else anim.SetTrigger("Idle");

        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        switch (other.gameObject.tag)
        {
            case "Trash":
                Destroy(other.gameObject);
                holdingGarbage=true;
                break;
            case "Bin":
                if (holdingGarbage)
                {
                   gmScript.trashCollected++;
                   //sound effect
                   holdingGarbage=false;
                }
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
