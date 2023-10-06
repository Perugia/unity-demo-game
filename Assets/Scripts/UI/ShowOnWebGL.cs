using UnityEngine;

public class ShowOnWebGL : MonoBehaviour
{
    public bool showOnWebGL = true;
    void Start()
    {
        #if UNITY_EDITOR || !UNITY_WEBGL
            gameObject.SetActive(!showOnWebGL);
        #endif
    }
}
