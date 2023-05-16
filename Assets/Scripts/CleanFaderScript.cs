using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CleanFaderScript : MonoBehaviour
{

    public Sprite cleanVersion;

    public UnityEvent PostClean;

    public void CleanVersion(){
        this.GetComponent<SpriteRenderer>().sprite = cleanVersion;
    }

    
}
