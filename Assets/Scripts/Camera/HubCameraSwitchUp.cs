using UnityEngine;
using System.Collections;

public class HubCameraSwitchUp : HubCameraSwitch
{
    private void Start ()
	{
        base.Start();
        _nbNode1 = 38;
        _nbNode2 = 0;
    }
}
