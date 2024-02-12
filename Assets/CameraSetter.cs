using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetter : MonoBehaviour
{
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = gameObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.planeDistance = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(canvas.worldCamera == null)
        {
            canvas.worldCamera = Camera.main;

        }
    }
}
