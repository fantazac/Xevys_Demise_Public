using UnityEngine;
using System.Collections;

public class RetractDoor : MonoBehaviour
{
    private const float RETRACT_AMOUNT = 4f;
    private const float RETRACT_SPEED = 0.12f;
    private const int MAIN_CAMERA_BASE_AXE_ITEM_AREA = 17;
    private const int BASE_AXE_AMOUNT_ON_PICKUP = 10;

    private bool _retract = false;
    private float _retractCount = 0;

    public bool Retract { set { _retract = value; } }

    private void Update()
    {
        if (_retract)
        {
            if (_retractCount < RETRACT_AMOUNT)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + RETRACT_SPEED, transform.position.z);
                _retractCount += RETRACT_SPEED;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else if (GameObject.Find("Main Camera").GetComponent<CameraManager>().CurrentArea == MAIN_CAMERA_BASE_AXE_ITEM_AREA && GameObject.Find("Character").GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition < 1 && !GameObject.Find("BaseAxeItem"))
        {
            GameObject.Find("Character").GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition += BASE_AXE_AMOUNT_ON_PICKUP;
        }
        Debug.Log(GameObject.Find("Main Camera").GetComponent<CameraManager>().CurrentArea == MAIN_CAMERA_BASE_AXE_ITEM_AREA && GameObject.Find("Character").GetComponent<PlayerThrowingWeaponsMunitions>().AxeMunition < 1 && !GameObject.Find("BaseAxeItem"));
    }
}
