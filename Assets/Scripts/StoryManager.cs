using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StoryManager : MonoBehaviour
{
    public Text storyText;
    public Text continueText;
    public static StoryManager singleton;
    protected bool canContinue = false;
    protected System.Action onContinue;

    StoryManager()
    {
        singleton = this;
    }

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(2.0f);
        ShowStory("hello, world", () =>
        {
            ShowStory("here is some more text", null);
        });
    }

    public void ShowStory(string text, System.Action onContinue)
    {
        storyText.text = "";
        storyText.gameObject.SetActive(true);
        this.onContinue = onContinue;
        canContinue = false;
        storyText.DOText(text, 2.0f);
        Invoke("CanContinue", 1.0f);
    }

    public void HideStory()
    {
        storyText.gameObject.SetActive(false);
        continueText.gameObject.SetActive(false);
    }

    public void CanContinue()
    {
        canContinue = true;
        continueText.gameObject.SetActive(true);
    }

    public void Update()
    {
        if (canContinue && Input.anyKeyDown && onContinue != null)
        {
            canContinue = false;
            HideStory();
            onContinue.Invoke();
        }
    }

}
