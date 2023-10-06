using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaEntrance : MonoBehaviour
{
    private string _transitionName;
    private bool shouldUnfrezeeAfterFade;

    public Camera customCamera;

    public string transitionName
    {
        get { return _transitionName; }
        set { _transitionName = value; }
    }

    public float waitToFade = 1f;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController player = PlayerController.instance;

        //Debug.Log("Debug: AreaEntrance");

        if (player != null && player.areaTransitionName != null && _transitionName == player.areaTransitionName)
        {
            player.transform.position = transform.position;
            customCamera.transform.position = new Vector3(transform.position.x, transform.position.y, customCamera.transform.position.z);

            //Debug.Log("Debug: camera position");
            shouldUnfrezeeAfterFade = true;
            UIFade.instance.FadeFromBlack();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (shouldUnfrezeeAfterFade)
        {
            waitToFade -= Time.deltaTime * 4;
            if (waitToFade <= 0)
            {
                shouldUnfrezeeAfterFade = false;
                GameManager.instance.fadingBetweenAreas = false;
            }
        }
    }
}
