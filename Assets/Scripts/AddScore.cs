using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddScore : MonoBehaviour
{
    private TextMeshProUGUI score;
    private RectTransform rectTranform;

    private void Awake()
    {
        score = GetComponent<TextMeshProUGUI>();
        rectTranform = GetComponent<RectTransform>();
    }

    public void SetScore(int score)
    {        
        this.score.text = "+" + score.ToString();
    }

    public void HideFadeScore()
    {

        rectTranform.anchoredPosition = new Vector2(0, 0);

        StartCoroutine(FadeScore());
    }

   /* private void OnEnable()
    {
        rectTranform.anchoredPosition = new Vector2(0,10);
        
        StartCoroutine(FadeScore());
    }*/
   /* private void Start()
    {
        this.transform.position = GetComponentInParent<Transform>().position;
        StartCoroutine(FadeScore());
    }*/

    private IEnumerator FadeScore()
    {
        float elapsed = 0f;
        float duration =0.3f;

        float to = this.rectTranform.anchoredPosition.y + 30;

        while (elapsed<duration)
        {
            float y = Mathf.Lerp(this.rectTranform.anchoredPosition.y,to,elapsed/duration);
            this.rectTranform.anchoredPosition = new Vector2(this.rectTranform.anchoredPosition.x, y);
            elapsed += Time.deltaTime;
            yield return null;
        }
        this.rectTranform.anchoredPosition = new Vector2(this.rectTranform.anchoredPosition.x,to);
        this.gameObject.SetActive(false);
    }

}
