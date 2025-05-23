using UnityEngine;

public class PressEventManager : MonoBehaviour
{
    public static PressEventManager instance;

    void Awake()
    {
        instance = this;   
    }

    public void DebugLogSomething(string _debug)
    {
        Debug.Log(_debug);
    }
}
