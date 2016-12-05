using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BurstBubble : MonoBehaviour
{
    [SerializeField]
    private int _bubbleIndex;

    private Animator _animator;
    private PlayerOxygen _playerOxygen;
    private PlayerFloatingInteraction _playerFloating;
    private WaitForSeconds _timeBeforeDissapear;

	private void Start ()
	{
	    GetComponent<Image>().enabled = false;

	    _animator = GetComponent<Animator>();
	    _playerOxygen = StaticObjects.GetPlayer().GetComponent<PlayerOxygen>();
        _playerFloating = StaticObjects.GetPlayer().GetComponentInChildren<PlayerFloatingInteraction>();
        _timeBeforeDissapear = new WaitForSeconds(0.5f);
	    _playerOxygen.OnOxygenCount += OnOxygenCount;

	    _playerFloating.OnPlayerOutOfWater += OnPlayerOutOfWater;
        _playerFloating.OnPlayerUnderWater += OnPlayerUnderWater;
    }

    private void OnPlayerUnderWater()
    {
        _animator.SetBool("IsBurst", false);
        GetComponent<Image>().enabled = true;
    }

    private void OnPlayerOutOfWater()
    {
        GetComponent<Image>().enabled = false;
    }

    private void OnOxygenCount(int index)
    {
        if (_bubbleIndex == index)
        {
            _animator.SetBool("IsBurst", true);
            StartCoroutine(TimerBeforeDissapearCoroutine());
        }
    }

    private IEnumerator TimerBeforeDissapearCoroutine()
    {
        yield return _timeBeforeDissapear;
        GetComponent<Image>().enabled = false;
    }
}
