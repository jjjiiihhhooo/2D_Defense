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
        if (cam == null) cam = Camera.main;
        Rect rect = cam.rect;

        float scaleHeight = ((float)Screen.width / Screen.height) / ((float)16 / 9); //(���� / ����)
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
