using UnityEngine;
using System.Collections;

public class HubCameraSwitch : MonoBehaviour
{
    private CameraManager _cameraManager;
    private WaitForSeconds _resetingCameraNodeDelay;

    private void Start ()
	{
	    _cameraManager = GameObject.Find("Main Camera").GetComponent<CameraManager>();
        _resetingCameraNodeDelay = new WaitForSeconds(0.2f);
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (_cameraManager.CurrentArea == 38)
            {
                GameObject backupNode = _cameraManager.getListNodes()[38];
                _cameraManager.getListNodes()[38] = _cameraManager.getListNodes()[0];
                _cameraManager.setCurrentArea(0);
                StartCoroutine("ResetingCameraNodeCoroutine", backupNode);
            }
        }
    }

    private IEnumerator ResetingCameraNodeCoroutine(GameObject backupNode)
    {
        yield return _resetingCameraNodeDelay;

        _cameraManager.getListNodes()[38] = backupNode;
    }
}
