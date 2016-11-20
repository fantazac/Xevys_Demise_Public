using UnityEngine;
using System.Collections;

/*
 * BEN_REVIEW
 * 
 * OK.
 */
public class SpawnToCheckpointAfterDeath : MonoBehaviour
{
    Checkpoints _lastCheckpoint = Checkpoints.HubCheckpoint;

    private void Start()
    {
        GetComponent<PlayDeathAnimation>().OnDyingAnimationFinished += SpawnToCheckpoint;
    }

    public void SaveCheckpoint(Checkpoints _checkpointIdentifier)
    {
        _lastCheckpoint = _checkpointIdentifier;
    }

    private void SpawnToCheckpoint()
    {
        transform.position = GetCheckpointSpawnPoint(_lastCheckpoint);
        GetComponent<Health>().FullHeal();
        GetComponentInChildren<Animator>().SetBool("IsDying", false);
    }

    private Vector3 GetCheckpointSpawnPoint(Checkpoints lastCheckpoint)
    {
        if (lastCheckpoint == Checkpoints.HubCheckpoint)
        {
            /*
             * BEN_REVIEW
             * 
             * Chiffres magiques. Rendre cela configurable dans l'éditeur.
             */
            return new Vector3(-27.4f, -4.12f, -5);
        }
        else if (lastCheckpoint == Checkpoints.EarthCheckpoint)
        {
            /*
             * BEN_REVIEW
             * 
             * Chiffres magiques. Rendre cela configurable dans l'éditeur.
             */
            return new Vector3(27.02f, -4.12f, -5);
        }
        else if (lastCheckpoint == Checkpoints.AirCheckpoint)
        {
            /*
             * BEN_REVIEW
             * 
             * Chiffres magiques. Rendre cela configurable dans l'éditeur.
             */
            return new Vector3(-17.71f, 39.4f, -5);
        }
        else if (lastCheckpoint == Checkpoints.WaterCheckpoint)
        {
            /*
             * BEN_REVIEW
             * 
             * Chiffres magiques. Rendre cela configurable dans l'éditeur.
             */
            return new Vector3(-101.55f, -5.1f, -5);
        }
        else if (lastCheckpoint == Checkpoints.FireCheckpoint)
        {
            /*
             * BEN_REVIEW
             * 
             * Chiffres magiques. Rendre cela configurable dans l'éditeur.
             */
            return new Vector3(3.04f, -33.09f, -5);
        }
        else
        {
            return Vector3.zero;
        }
    }
}
