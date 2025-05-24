using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    KeyActionList currentList;
    List<KeyAction> startKeyActions, currentKeyActions;
    GameObject currentListObj;
    Dictionary<string, GameObject> keyActionObjects;
    public bool stopMovement;
    [Header("KeyActions")]
    [SerializeField] GameObject startListObj;
    [SerializeField] GameObject pianoObj;

    void Awake()
    {
        instance = this;   
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyActionObjects = new()
        {
            ["normal"] = startListObj,
            ["piano"] = pianoObj
        };

        currentListObj = startListObj;
        currentList = currentListObj.GetComponent<KeyActionList>();
        currentKeyActions = currentList.keyActions;
        startKeyActions = currentKeyActions;
    }

    // Update is called once per frame
    void Update()
    {
        bool _invoked = false;
        foreach (var action in currentKeyActions)
        {
            if (!string.IsNullOrEmpty(action.key) && Input.GetKeyDown(action.key.ToLower()))
            {
                action.onKeyPressed.Invoke();
                _invoked = true;
        
                Transform nextListTransform = currentListObj.transform.Find(action.key.ToLower());
                if(nextListTransform != null && nextListTransform != currentListObj.transform)
                {
                    GameObject nextListObject = nextListTransform.gameObject;
                    currentListObj = nextListObject;
                }
                else
                {
                    currentListObj = startListObj;
                }

                currentList = currentListObj.GetComponent<KeyActionList>();
                currentKeyActions = currentList.keyActions;
            }
        } 
        if(startKeyActions != currentKeyActions && (!_invoked))
        {
            foreach (var action in startKeyActions)
            {
                if (!string.IsNullOrEmpty(action.key) && Input.GetKeyDown(action.key.ToLower()))
                {
                    currentListObj = startListObj;
                    currentList = currentListObj.GetComponent<KeyActionList>();
                    currentKeyActions = currentList.keyActions;

                    action.onKeyPressed.Invoke();
            
                    Transform nextListTransform = currentListObj.transform.Find(action.key.ToLower());
                    if(nextListTransform != null && nextListTransform != currentListObj.transform)
                    {
                        GameObject nextListObject = nextListTransform.gameObject;
                        currentListObj = nextListObject;
                    }
                    else
                    {
                        currentListObj = startListObj;
                    }

                    currentList = currentListObj.GetComponent<KeyActionList>();
                    currentKeyActions = currentList.keyActions;
                }
            } 
        }

        if(currentKeyActions.Count == 0)
        {
            currentListObj = startListObj;
            currentList = currentListObj.GetComponent<KeyActionList>();
            currentKeyActions = currentList.keyActions;
        }
    }

    public void SwitchStandardKeyActions(string _keyActions)
    {
        if(currentListObj == keyActionObjects[_keyActions]) return;

        currentListObj = keyActionObjects[_keyActions];
        currentList = currentListObj.GetComponent<KeyActionList>();
        currentKeyActions = currentList.keyActions;
        startKeyActions = currentKeyActions;
        HandleExtraSwitchingLogic(_keyActions);
    }

    void HandleExtraSwitchingLogic(string _keyActions)
    {
        switch(_keyActions)
        {
            case "piano":
                stopMovement = true;
                break;
            case "normal":
                stopMovement = false;
                break;
        }
    }
}
