using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameManager gmScript;

    public Animator anim;

    public Vector3 horizontal;
    public Vector3 vertical;

    public bool holdingGarbage;

    //0 is forward, 1 is backward
    private int facing;

    private void Awake() {
        gmScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        facing = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position+=vertical;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position-=vertical;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position+=horizontal;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position-=horizontal;
        }
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

}
