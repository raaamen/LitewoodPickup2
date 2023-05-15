using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneFade : MonoBehaviour
{
    public Image blackScreen;

    public bool fading;

    public float fadeSpeed;

    private PlayerMovement player;
    private GameObject cam;

    private void Awake(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public IEnumerator FadeIn(){
        /*
        Color fixedColor = blackScreen.color;
        fixedColor.a = 0;
        blackScreen.color = fixedColor;
        blackScreen.CrossFadeAlpha(1f, 0f, true);
        */

        blackScreen.CrossFadeAlpha(0, fadeSpeed, false);

        yield return null;
    }

    public IEnumerator FadeOut(){

        Color fixedColor = blackScreen.color;
        fixedColor.a = 1;
        blackScreen.color = fixedColor;
        blackScreen.CrossFadeAlpha(0f, 0f, true);

        blackScreen.CrossFadeAlpha(1, fadeSpeed, false);

        yield return null;
    }

    public IEnumerator SceneSwitch(Transform newPos, Transform cameraPos){
        StartCoroutine("FadeOut");
        yield return new WaitForSeconds(fadeSpeed*2);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.gameObject.transform.position = newPos.position;
        cam.transform.position = cameraPos.transform.position;
        StartCoroutine("FadeIn");
        yield return new WaitForSeconds(fadeSpeed);
    }

    public void CallSceneSwitch(Transform newpos, Transform cameraPos){
        StartCoroutine(SceneSwitch(newpos, cameraPos));
    }
}
