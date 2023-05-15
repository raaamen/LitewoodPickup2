using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum Location{
        Street,
        Arcade,
        Pizza,
        House
    }

    public bool arcadeUnlocked;
    public bool pizzaUnlocked;

    public Location playerLocation;

    public List<AudioClip> music;

    //Counters
    public float totalTrashInLevel;
    private float _trashCollected;
    public float TrashCollected{
        get{
            return _trashCollected;
        }
        set{
            _trashCollected = value;
            UpdateText();
            if (_trashCollected == totalTrashInLevel)
            {
                GameWin();
            }
        }
    }
    private float _timer;
    private float Timer{
        get{
            return _timer;
        }
        set{
            _timer = value;
            if (_timer <= 0 && TrashCollected != totalTrashInLevel)
            {
                GameLose();
            }
        }
    }

    [SerializeField]
    private int amountToUnlockPizzaPlace;
    [SerializeField]
    private int amountToUnlockArcade;

    [SerializeField]
    private float garbageMeterAdder;

    [SerializeField]
    public float amountOfGarbageHeld;

    //Vectors
    
    //Text
    public TMP_Text trashRemainingText;
    public TMP_Text amtUnlockPizzaText;
    public TMP_Text amtUnlockArcadeText;

    public AudioSource audioSrc;

    public Slider garbageSlider;


    //arcade and pizza
    public Sprite cleanArcadeMachine;
    public Sprite cleanPizzaTable;
    public Sprite cleanPizzaStool;
    public Sprite cleanBed;


    public Sprite openDoorArcade;
    public Sprite openDoorPizza;

    public RawImage arcadeStar;
    public RawImage pizzaStar;

    private void Awake() {
        audioSrc = GetComponent<AudioSource>();
        UpdateText();
        totalTrashInLevel = GameObject.FindGameObjectsWithTag("Trash").Length;
        garbageMeterAdder = 1/totalTrashInLevel;
        playerLocation = Location.House;
    }
    // Start is called before the first frame update
    void Start()
    {
        amtUnlockArcadeText.text = ""+amountToUnlockArcade;
        amtUnlockPizzaText.text = ""+amountToUnlockPizzaPlace;
    }


    void GameWin(){

    }
    void GameLose(){

    }
    public void UpdateText(){
        trashRemainingText.text = ""+TrashCollected;
        Debug.Log("Trash collected "+TrashCollected);
        garbageSlider.value+=(amountOfGarbageHeld*garbageMeterAdder);

        if (TrashCollected >= amountToUnlockArcade)
        {
            arcadeStar.color = Color.yellow;
            UnlockArcade();
        }
        if (TrashCollected >= amountToUnlockPizzaPlace)
        {
            pizzaStar.color = Color.yellow;
            UnlockPizzaPlace();
        }

    }

    void UnlockArcade(){
        arcadeUnlocked = true;
    }
    void UnlockPizzaPlace(){
        pizzaUnlocked = true;
    }

    void ChangeMusic(){
        switch (playerLocation)
        {
            case Location.Street:
                audioSrc.clip = music[0];
                break;
            case Location.Arcade:
                audioSrc.clip = music[1];
                break;
            case Location.Pizza:
                audioSrc.clip = music[2];
                break;
            case Location.House:
                audioSrc.clip = music[3];
                break;
        }
    }

}
