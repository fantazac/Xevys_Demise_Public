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
    private WaitForSeconds _timeBeforeDisappear;

	private void Start ()
	{
	    GetComponent<Image>().enabled = false;

	    _animator = GetComponent<Animator>();
	    _playerOxygen = StaticObjects.GetPlayer().GetComponent<PlayerOxygen>();
        _playerFloating = StaticObjects.GetPlayer().GetComponentInChildren<PlayerFloatingInteraction>();
        _timeBeforeDisappear = new WaitForSeconds(0.2f);
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
            StartCoroutine(TimerBeforeDisappearCoroutine());
        }
    }

    private IEnumerator TimerBeforeDisappearCoroutine()
    {
        yield return _timeBeforeDisappear;
        GetComponent<Image>().enabled = false;
    }
}
