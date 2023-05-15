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

    public Location playerLocation;

    public List<AudioClip> music;

    //Counters
    public int totalTrashInLevel;
    private int _trashCollected;
    public int TrashCollected{
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
    private float garbageMeterAdder;

    //Vectors
    
    //Text
    public TMP_Text trashRemainingText;
    public TMP_Text amtUnlockPizzaText;
    public TMP_Text amtUnlockArcadeText;

    public AudioSource audioSrc;

    public Slider garbageSlider;

    private void Awake() {
        audioSrc = GetComponent<AudioSource>();
        UpdateText();
        totalTrashInLevel = GameObject.FindGameObjectsWithTag("Trash").Length;
        playerLocation = Location.House;
    }
    // Start is called before the first frame update
    void Start()
    {
        garbageMeterAdder = 1/totalTrashInLevel;
        amtUnlockArcadeText.text = ""+amountToUnlockArcade;
        amtUnlockPizzaText.text = ""+amountToUnlockPizzaPlace;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameWin(){

    }
    void GameLose(){

    }
    void UpdateText(){
        trashRemainingText.text = "Trash remaining: "+(totalTrashInLevel-TrashCollected);
        garbageSlider.value = 1-(TrashCollected*garbageMeterAdder);

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
