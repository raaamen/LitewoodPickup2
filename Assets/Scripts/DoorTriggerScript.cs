using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerScript : MonoBehaviour
{
    public enum DoorType{
        Arcade,
        Pizza,
        Bedroom,
        Outside
    }
    public DoorType doorType;
    public bool arcadeDoor;
    public bool pizzaDoor;
    public GameObject player;
    public GameObject cam;
    public bool canTravel;
    public GameObject spawnPoint;
    public GameObject camspawnPoint;

    public SceneFade fadeManager;
    public GameManager gmScript;

    

    // Start is called before the first frame update
    void Start()
    {
        fadeManager = GameObject.Find("GameManager").GetComponent<SceneFade>();
        gmScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canTravel && Input.GetKeyDown(KeyCode.Space))
        {
            gmScript.sfxSrc.PlayOneShot(gmScript.sfx[2]);
            fadeManager.CallSceneSwitch(spawnPoint.transform, camspawnPoint.transform);
            switch (doorType)
            {
                case DoorType.Arcade:
                    gmScript.playerLocation = GameManager.Location.Arcade;
                    break;
                case DoorType.Pizza:
                    gmScript.playerLocation = GameManager.Location.Pizza;
                    break;
                case DoorType.Bedroom:
                    gmScript.playerLocation = GameManager.Location.House;
                    break;
                case DoorType.Outside:
                    gmScript.playerLocation = GameManager.Location.Street;
                    break;
            }
            gmScript.ChangeMusic();
            //player.transform.position = spawnPoint.transform.position;
            //cam.transform.position = camspawnPoint.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch (doorType)
            {
                case DoorType.Arcade:
                    if (gmScript.arcadeUnlocked)
                    {
                        canTravel = true;
                    }
                    break;
                case DoorType.Pizza:
                    if (gmScript.pizzaUnlocked)
                    {
                        canTravel = true;
                    }
                    break;
                case DoorType.Bedroom:
                    canTravel = true;
                    break;
                case DoorType.Outside:
                    canTravel = true;
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Player Left Trigger");
        canTravel = false;
    }
}
