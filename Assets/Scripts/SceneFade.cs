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

        //var black = Instantiate(blackScreen, Vector2.zero, Quaternion.identity);
        //black.transform.parent = GameObject.Find("Canvas").transform;
        //black.gameObject.transform.position = Vector2.zero;
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

    public void TitleScreenFadeOut(Image titleScreen){
        titleScreen.CrossFadeAlpha(0f, fadeSpeed*3, false);
    }

    public void FadeOutImage(Image image){
        image.CrossFadeAlpha(0f, fadeSpeed, false);
    }

    public IEnumerator FadeOutImage(SpriteRenderer sprite){
        Color transparent = new Color(0,0,0,0);
        float alpha = sprite.color.a;
        var temp = sprite.color;

        while (sprite.color.a > 0)
        {
            alpha-=0.01f;
            temp.a = alpha;
            sprite.color = temp;
            yield return new WaitForSeconds(fadeSpeed);
        }
    }
    public void CallFadeOutImage(SpriteRenderer sprite){
        StartCoroutine(FadeOutImage(sprite));
    }

    public IEnumerator FadeInImage(SpriteRenderer sprite){
        Color transparent = new Color(0,0,0,0);
        float alpha = sprite.color.a;
        var temp = sprite.color;

        while (sprite.color.a < 1)
        {
            alpha+=0.01f;
            temp.a = alpha;
            sprite.color = temp;
            yield return new WaitForSeconds(fadeSpeed);
        }
    }

    public void CallFadeInImage(SpriteRenderer sprite){
        StartCoroutine(FadeInImage(sprite));
    }

    public void FadeInImage(Image image){
        Color fixedColor = image.color;
        fixedColor.a = 1;
        image.color = fixedColor;
        image.CrossFadeAlpha(0f, 0f, true);

        image.CrossFadeAlpha(1, fadeSpeed, false);
    }

    public IEnumerator SceneSwitch(Transform newPos, Transform cameraPos){
        Debug.Log("Fading out");
        StartCoroutine("FadeOut");
        yield return new WaitForSeconds(fadeSpeed*2);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.gameObject.transform.position = newPos.position;
        cam.transform.position = cameraPos.transform.position;
        Debug.Log("Fading in");
        StartCoroutine("FadeIn");
        yield return new WaitForSeconds(fadeSpeed);
    }

    public void CallSceneSwitch(Transform newpos, Transform cameraPos){
        StartCoroutine(SceneSwitch(newpos, cameraPos));
    }
}
