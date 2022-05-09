using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    //Position sin clampear del mouse
    public Vector3 GetScreenPosition()
    {
        Vector3 posMouseScreen = Input.mousePosition;
        return posMouseScreen;

    }
    
    //Position clampeada del mouse en la pantalla
    public Vector3 GetBoundedScreenPosition()
    {
        Vector3 raw = GetScreenPosition();

        raw.x = Mathf.Clamp(raw.x, 0f, Screen.width - 1f);
        raw.y = Mathf.Clamp(raw.y, 0f, Screen.height - 1f);
        raw.z = 0f;
        return raw;
    }
    
    //Position del mouse para distintos tamaños de la pantalla. (0,0) a (1,1)
    public Vector3 GetViewerPortPosition()
    {
        var screenPos = GetScreenPosition();

        screenPos.x = screenPos.x / Screen.width;
        screenPos.y = screenPos.y / Screen.height;

        return screenPos;
    }
    
    //Position en caso de querer una cámara en concreto que no sea la main.
    public Vector3 GetViewerPortPosition(Camera camera)
    {
        var screenPos = GetScreenPosition();
        var viewerPortPos = camera.ScreenToViewportPoint(screenPos);

        return viewerPortPos;
    }

    public Vector3 GetWorldPosition(Camera camera)
    {
        return GetWorldPosition(camera, camera.nearClipPlane);
    }
    
    public Vector3 GetWorldPosition(Camera camera, float worldDepth)
    {
        var screenPos = GetBoundedScreenPosition();
        var screenPosWithDepth = new Vector3(screenPos.x, screenPos.y, worldDepth);
        return camera.ScreenToWorldPoint(screenPosWithDepth);
    }
    
    
    private void Update()
    {
        //Debug.Log(GetScreenPosition());
        
        //Debug.Log(GetBoundedScreenPosition());
        
        //ebug.Log(GetViewerPortPosition());
        
        Debug.Log(GetWorldPosition(Camera.main, 1f));
    }
}
