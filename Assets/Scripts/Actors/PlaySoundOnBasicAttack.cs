using UnityEngine;
using System.Collections;

public class PlaySoundOnBasicAttack : MonoBehaviour
{

    [SerializeField]
    private int _basicAttackSoundIndex = 0;

    private AudioSourcePlayer _audioSourcePlayer;

    private void Start()
    {
        _audioSourcePlayer = GetComponent<AudioSourcePlayer>();

        GetComponent<PlayerBasicAttack>().OnBasicAttack += PlayBasicAttackSound;
    }

    private void PlayBasicAttackSound()
    {
        _audioSourcePlayer.Play(_basicAttackSoundIndex);
    }


}
