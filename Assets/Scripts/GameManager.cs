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
        House,
        Title
    }

    public bool arcadeUnlocked;
    public bool pizzaUnlocked;

    public bool gameStarted;

    public Location playerLocation;

    public List<AudioClip> music;
    public List<AudioClip> sfx;

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

    public AudioSource musicSrc;
    public AudioSource sfxSrc;

    public Slider garbageSlider;


    //arcade and pizza
    public Sprite cleanArcadeMachine;
    public Sprite cleanPizzaTable;
    public Sprite cleanPizzaStool;
    public Sprite cleanBed;
    public Sprite cleanStall;
    public Sprite cleanBedside;


    public GameObject arcadeClosedSign;
    public GameObject pizzaClosedSign;

    public RawImage arcadeStar;
    public RawImage pizzaStar;

    public TMP_Text spacebarPromptText;

    private void Awake() {
        
        UpdateText();
        totalTrashInLevel = GameObject.FindGameObjectsWithTag("Trash").Length;
        garbageMeterAdder = 1/totalTrashInLevel;
        playerLocation = Location.Title;
        ChangeMusic();
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
            sfxSrc.PlayOneShot(sfx[1]);
            arcadeStar.color = Color.yellow;
            UnlockArcade();
        }
        if (TrashCollected >= amountToUnlockPizzaPlace)
        {
            sfxSrc.PlayOneShot(sfx[1]);
            pizzaStar.color = Color.yellow;
            UnlockPizzaPlace();
        }

    }

    void UnlockArcade(){
        arcadeUnlocked = true;
        Destroy(arcadeClosedSign);
    }
    void UnlockPizzaPlace(){
        pizzaUnlocked = true;
        Destroy(pizzaClosedSign);
    }

    public void ChangeMusic(){
        switch (playerLocation)
        {
            
            case Location.Street:
                musicSrc.clip = music[0];
                break;
            case Location.Arcade:
                musicSrc.clip = music[1];
                break;
            case Location.Pizza:
                musicSrc.clip = music[2];
                break;
            case Location.House:
                musicSrc.clip = music[3];
                break;
            case Location.Title:
                musicSrc.clip = music[4];
                break;
        }
        musicSrc.Play();
    }

    public void TitleScreenMusicSwitch(){
        playerLocation = Location.House;
        ChangeMusic();
    }

}
