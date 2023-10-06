using UnityEngine;

public class ShowOnMobile : MonoBehaviour
{
    public bool showOnMobile = true;
    void Start()
    {
        gameObject.SetActive(showOnMobile == Application.isMobilePlatform);
    }
}
