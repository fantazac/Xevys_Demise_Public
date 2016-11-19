using UnityEngine;
using System.Collections;

public class HubCameraSwitchDown : HubCameraSwitch
{
    private void Start ()
	{
	    base.Start();
	    _nbNode1 = 0;
	    _nbNode2 = 38;
	}
}
