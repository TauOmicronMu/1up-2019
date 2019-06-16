using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggle_camera : MonoBehaviour
{
    private Camera playerCamera;

    private Camera mainCamera;

    //Make sure to attach these Buttons in the Inspector
    public Button _Button;

    private int count;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        var allCameras = Camera.allCameras;
        mainCamera = allCameras[0];
        playerCamera = allCameras[1];
        Button btn = _Button.GetComponent<Button>();
        btn.onClick.AddListener(delegate { Toggle(); });
    }


    private void OnGUI()
    {
        GUI.Label(new Rect(200, 200, 100, 100), count.ToString());
    }

    void Toggle()
    {
        GUI.Label(new Rect(200, 200, 100, 100), count.ToString());
        count++;
//        playerCamera.enabled = !mainCamera.enabled;
//        mainCamera.enabled = !playerCamera.enabled;
    }
}