using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Debug: PlayerLoader");
        if (PlayerController.instance == null)
        {
            //Debug.Log("Debug: Instantiate");
            PlayerController playerInstance = Instantiate(player).GetComponent<PlayerController>();
            playerInstance.transform.position = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
