using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

      public Vector2 inputVector { get; private set; }
      public Vector3 mousePosition { get; private set; }
      public bool zoomedIn { get; private set; }

    public ZoomSwitch zs;

      // Update is called once per frame
      void Update()
      {
         // Movement
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");
            inputVector = new Vector2(h, v);

            mousePosition = Input.mousePosition;

        // Camera Zoom
            if (zoomedIn && Input.GetKeyDown(KeyCode.Z))
                zoomedIn = false;
            if (!zoomedIn && Input.GetKeyDown(KeyCode.Z))
                zoomedIn = true;            
      }

    private void Start()
    {
        zoomedIn = true;
    }
}
