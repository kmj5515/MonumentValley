using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Image fadeImg;
    public List<Text> fadeText = new List<Text>();

    private bool isReady;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        isReady = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isReady)
        {
            StartCoroutine(FadeIn(fadeImg, 0f, 0.2f));            
        }
    }

    IEnumerator FadeIn(Image target, float endAlpha, float speed)
    {
        Color fadeColor = target.color;
        Color originColor = target.color;

        float timing = 0;

        while (target.color.a > endAlpha)
        {
            //이미지a
            fadeColor.a = Mathf.Lerp(originColor.a, endAlpha, timing);

            //텍스트a
            for (int i = 0; i < fadeText.Count; i++)
            {
                fadeText[i].color = new Color(1.0f, 1.0f, 1.0f, fadeColor.a);
            }       

            target.color = fadeColor;

            timing += Time.deltaTime * speed;

            yield return null;
        }

        fadeColor.a = endAlpha;
        target.color = fadeColor;
        isReady = true;

        yield return null;
    }

    public bool Ready
    {
        get { return isReady; }
        set { isReady = value; }
    }
}
