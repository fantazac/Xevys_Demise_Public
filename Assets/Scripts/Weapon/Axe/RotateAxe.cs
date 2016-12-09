using UnityEngine;
using System.Collections;

public class RotateAxe : MonoBehaviour
{
    [SerializeField]
    private float _rotationPerSecond = 180f;

    private int _orientation = 0;

    public bool CanRotate { get; set; }

    private void Start()
    {
        CanRotate = true;

        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        //Il est nécessaire d'attendre un update pour connaitre l'orientation car elle est toujours > 0 au Start()
        yield return null;

        _orientation = transform.localScale.y > 0 ? -1 : 1;

        while (CanRotate)
        {
            transform.Rotate(new Vector3(0, 0, _orientation * _rotationPerSecond * Time.deltaTime));

            yield return null;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
