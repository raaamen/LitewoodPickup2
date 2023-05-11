using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

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

    //Vectors
    
    //Text
    public TMP_Text trashRemainingText;


    private void Awake() {
        UpdateText();
        totalTrashInLevel = GameObject.FindGameObjectsWithTag("Trash").Length;
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    
}
