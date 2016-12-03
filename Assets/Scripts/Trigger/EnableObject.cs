using UnityEngine;
using System.Collections;

/* BEN_CORRECTION
 * 
 * EnableObject on what ?
 * 
 * Proposition : EnableOtherGameObjectAfterCompletingMovement.
 * 
 * PS : Ce serait tellement plus simple tout cela si Unity avait quelque chose comme ceci : 
 * https://docs.unrealengine.com/latest/images/Engine/Blueprints/Editor/Modes/GraphPanel/k2_graphview.jpg
 * 
 * Il existe des extensions, mais elles sont cher. Un jour, je vais peut-être m'y mettre.
 */
public class EnableObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectToEnable;

    private void Start()
    {
        GetComponent<MoveObjectOnTrigger>().OnFinishedMoving += Enable;
    }

    private void Enable()
    {
        _objectToEnable.SetActive(true);
    }
}
