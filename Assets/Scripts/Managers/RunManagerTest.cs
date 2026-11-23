using UnityEngine;



public class RunManagerTest : MonoBehaviour
{
    private void Start()
    {
        RunManager.Instance.AddMoney(100);

        RunManager.Instance.AddBurnout(25);

        Debug.Log("Current day: " +
            RunManager.Instance.CurrentDay);

        Debug.Log("Money: " +
            RunManager.Instance.Money);

        Debug.Log("Burnout: " +
            RunManager.Instance.Burnout);
    }
}

