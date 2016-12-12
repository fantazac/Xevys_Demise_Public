using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Prime31.TransitionKit;

public class EnterPortal : MonoBehaviour
{
    [SerializeField]
    private GameObject _otherPortal;

    [SerializeField]
    private int _cameraDimensionsId = 0;

    [SerializeField]
    private Texture2D maskTexture;

    private GameObject _player;
    private bool _playerIsOnPortal = false;
    private CameraManager _mainCameraManager;
    private CameraManager _waterCameraManager;

    private void Start()
    {
        _mainCameraManager = StaticObjects.GetMainCamera().GetComponent<CameraManager>();
        _waterCameraManager = StaticObjects.GetWaterCamera().GetComponent<CameraManager>();
        _player = StaticObjects.GetPlayer();
        _player.GetComponentInChildren<InputManager>().OnEnterPortal += GoToOtherDimension;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == StaticObjects.GetObjectTags().Player)
        {
            _playerIsOnPortal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == StaticObjects.GetObjectTags().Player)
        {
            _playerIsOnPortal = false;
        }
    }

    private void GoToOtherDimension()
    {
        if (_playerIsOnPortal)
        {
            var mask = new ImageMaskTransition()
            {
                maskTexture = maskTexture,
                backgroundColor = Color.black
            };
            TransitionKit.instance.transitionWithDelegate(mask);
            StartCoroutine(WaitOneSecond());
        }
    }

    private IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(1f);

        _player.transform.position = _otherPortal.transform.position + (Vector3.forward * _player.transform.position.z);
        _mainCameraManager.SetCameraOnPlayer(_otherPortal.transform.position + (Vector3.back * 10), _cameraDimensionsId);
        _waterCameraManager.SetCameraOnPlayer(_otherPortal.transform.position + (Vector3.back * 10), _cameraDimensionsId);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
