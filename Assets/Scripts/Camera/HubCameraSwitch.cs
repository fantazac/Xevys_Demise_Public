﻿using UnityEngine;
using System.Collections;

public class HubCameraSwitch : MonoBehaviour {

    protected CameraManager _cameraManager;
    protected float _resetingNodeTime = 0.5f;
    protected WaitForSeconds _resetingCameraNodeDelay;
    protected int _nbNode1;
    protected int _nbNode2;

    protected virtual void Start()
    {
        _cameraManager = GameObject.Find(StaticObjects.GetMainObjects().MainCamera).GetComponent<CameraManager>();
        _resetingCameraNodeDelay = new WaitForSeconds(_resetingNodeTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == StaticObjects.GetObjectTags().Player)
        {
            if (_cameraManager.CurrentArea == _nbNode1)
            {
                GameObject backupNode = _cameraManager.getListNodes()[_nbNode1];
                _cameraManager.getListNodes()[_nbNode1] = _cameraManager.getListNodes()[_nbNode2];
                _cameraManager.setCurrentArea(_nbNode2);
                StartCoroutine(ResetingCameraNodeCoroutine(backupNode));
            }
        }
    }

    private IEnumerator ResetingCameraNodeCoroutine(GameObject backupNode)
    {
        yield return _resetingCameraNodeDelay;

        _cameraManager.getListNodes()[_nbNode1] = backupNode;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
