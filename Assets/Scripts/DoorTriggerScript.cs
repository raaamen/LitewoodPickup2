using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;
    public bool canTravel;
    public GameObject spawnPoint;
    public GameObject camspawnPoint;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canTravel && Input.GetKeyDown(KeyCode.Space))
        {
            player.transform.position = spawnPoint.transform.position;
            cam.transform.position = camspawnPoint.transform.position; 
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Player Hit Trigger");
            canTravel = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Player Left Trigger");
        canTravel = false;
    }
}
