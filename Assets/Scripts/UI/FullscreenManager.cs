using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class FullscreenManager : MonoBehaviour
{
    #if UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void RequestFullscreen();

    #endif

    public void RequestToFullscreen()
    {
        AudioManager.instance.PlaySFX(4);
        #if !UNITY_EDITOR && UNITY_WEBGL
            RequestFullscreen();
        #endif
    }
}