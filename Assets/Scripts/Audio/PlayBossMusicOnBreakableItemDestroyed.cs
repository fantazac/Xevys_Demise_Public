using UnityEngine;
using System.Collections;

public class PlayBossMusicOnBreakableItemDestroyed : MonoBehaviour
{
    [SerializeField]
    private GameObject _bossBattleMusicZone;

    [SerializeField]
    private bool _destroyMusicZoneOnBossDefeated = true;

    private SpawnBossOnBreakableItemDestroyed _spawnBoss;
    private AudioSourcePlayer _bossBattleMusic;
    private Health _health;

    private void Start()
    {
        _spawnBoss = GetComponent<SpawnBossOnBreakableItemDestroyed>();
        _spawnBoss.OnBossFightEnabled += EnableMusic;
        _spawnBoss.OnBossFightDisabled += DisableMusic;
        if (_destroyMusicZoneOnBossDefeated)
        {
            _spawnBoss.OnBossFightFinished += DestroyMusicZone;
        }
        else
        {
            _spawnBoss.OnBossFightFinished += DisableMusic;
        }

        _bossBattleMusic = _bossBattleMusicZone.GetComponent<AudioSourcePlayer>();
    }

    private void EnableMusic()
    {
        _bossBattleMusic.Play();
    }

    private void DisableMusic()
    {
        _bossBattleMusic.Stop();
    }

    private void DestroyMusicZone()
    {
        DisableMusic();
        Destroy(_bossBattleMusicZone);
    }
}
