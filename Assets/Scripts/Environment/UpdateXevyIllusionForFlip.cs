using UnityEngine;
using System.Collections;

public class UpdateXevyIllusionForFlip : MonoBehaviour
{
    BossOrientation _bossOrientation;

	private void Start ()
    {
        StartCoroutine(UpdateXevyIllusion());
        _bossOrientation = GetComponent<BossOrientation>();
	}

    private IEnumerator UpdateXevyIllusion()
    {
        while (true)
        {
            _bossOrientation.FlipTowardsPlayer();
            yield return null;
        }
    }
}
