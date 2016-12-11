using UnityEngine;
using System.Collections;

public class CheckpointAnimationManager : MonoBehaviour
{
    [SerializeField] 
    private CheckpointSave[] _checkpoints;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        foreach (CheckpointSave checkpoint in _checkpoints)
        {
            checkpoint.OnCheckpointReached += ShowCheckpointTooltip;
        }
    }

    private void ShowCheckpointTooltip()
    {
        _animator.SetTrigger("FadeIn");
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1.5f);

        _animator.SetTrigger("FadeOut");
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
