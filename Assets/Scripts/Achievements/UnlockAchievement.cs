using UnityEngine;

public class UnlockAchievement : MonoBehaviour
{
    private LockStateController _lockStateController;
    
    private void Start()
    {
        _lockStateController = GameObject.Find("LockStateController").GetComponent<LockStateController>();
        _lockStateController.OnUnlockAchievement += Unlock;

    }

    private void Unlock(int index)
    {
        switch (gameObject.name)
        {
            case "ContainerBehemoth":
                if (index == 0)
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                break;
            case "ContainerPhoenix":
                if (index == 1)
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                break;
            case "ContainerNeptune":
                if (index == 2)
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                break;
            case "ContainerVulcan":
                if (index == 3)
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                break;
            case "ContainerXevy":
                if (index == 4)
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.transform.GetChild(1).gameObject.SetActive(false);
                }
                break;
            case "ContainerSkeltals":
                if (index == 5)
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                break;
            case "ContainerScarabs":
                if (index == 6)
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                break;
            case "ContainerBats":
                if (index == 7)
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                break;
            case "ContainerBoots":
                if (index == 8)
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                break;
            case "ContainerFeather":
                if (index == 9)
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                break;
            case "ContainerBubble":
                if (index == 10)
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                break;
            case "ContainerFireArmor":
                if (index == 11)
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                break;
        }
    }
}
