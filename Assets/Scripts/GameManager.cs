using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //Counters
    public int totalTrashInLevel;
    public int trashCollected;
    private float _timer;
    private float Timer{
        get{
            return _timer;
        }
        set{
            _timer = value;
            if (_timer <= 0 && trashCollected != totalTrashInLevel)
            {
                GameLose();
            }
        }
    }

    //Vectors
    



    private void Awake() {
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
        
    }

    
}
