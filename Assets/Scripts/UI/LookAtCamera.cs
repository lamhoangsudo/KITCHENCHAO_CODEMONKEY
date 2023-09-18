using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private enum Mode
    {
        LookAt,
        LookAtInvert,
        LookAtForward,
        LookAtForwardInvert,
    }
    [SerializeField] private Mode mode;
    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInvert:
                Vector3 dir = transform.position - Camera.main.transform.forward;
                transform.LookAt(dir + transform.position);
                break;
            case Mode.LookAtForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.LookAtForwardInvert:
                transform.forward = -Camera.main.transform.forward;
                break;
        }


    }
}
