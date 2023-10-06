using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    public string areaToLoad;
    public string areaTransitionName;

    public AreaEntrance theEntrance;

    public float waitToLoad = 1f;
    private bool shouldLoadAfterFade;

    void Start()
    {
        theEntrance.transitionName = areaTransitionName;
    }

    void Update()
    {
        if (shouldLoadAfterFade)
        {
            waitToLoad -= Time.deltaTime;
            if (waitToLoad <= 0)
            {
                shouldLoadAfterFade = false;
                PlayerController.instance.areaTransitionName = areaTransitionName;
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            shouldLoadAfterFade = true;
            GameManager.instance.fadingBetweenAreas = true;
            UIFade.instance.FadeToBlack();
        }
    }
}
