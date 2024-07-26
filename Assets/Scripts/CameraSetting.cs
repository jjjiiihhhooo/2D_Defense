using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    public Camera cam;

    private void OnEnable()
    {
        CameraResolution();
    }


    private void OnPreCull() => GL.Clear(true, true, Color.black);

    private void CameraResolution()
    {
        Debug.LogError("noCam");

        if (cam == null) cam = Camera.main;
        Debug.LogError("Cam");
        Rect rect = cam.rect;

        float scaleHeight = ((float)Screen.width / Screen.height) / ((float)16 / 9); //(가로 / 세로)
        float scaleWidth = 1f / scaleHeight;

        if (scaleHeight < 1)
        {
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
        }
        else
        {
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
        }

        cam.rect = rect;
    }

}
