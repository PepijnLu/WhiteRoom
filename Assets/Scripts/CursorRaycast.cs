using UnityEngine.UI;
using UnityEngine;

public class CursorRaycast : MonoBehaviour
{
    [SerializeField] Camera playerCam;
    [SerializeField] Image playerCursor;
    [SerializeField] Sprite normalCursorSprite, interactableCursorSprite;
    [SerializeField] float interactableSizeIncrease;
    Vector3 originalCursorSize;
    [Header("LayerMasks")]
    [SerializeField] LayerMask interactablesMask;

    void Start()
    {
        playerCursor.sprite = normalCursorSprite;
        originalCursorSize = playerCursor.transform.localScale;
    }
    void Update()
    {
        InitiateRaycast();   
    }
    void InitiateRaycast()
    {
        Ray ray = playerCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            Debug.Log("Hit: " + hit.collider.name);
            if(CheckColliderLayerMask(hit.collider, interactablesMask)) 
            {
                HandleRaycast(hit);
                if(playerCursor.sprite != interactableCursorSprite) 
                {
                    playerCursor.sprite = interactableCursorSprite;
                    playerCursor.transform.localScale = new Vector3(originalCursorSize.x * interactableSizeIncrease, originalCursorSize.y * interactableSizeIncrease, originalCursorSize.z * interactableSizeIncrease);
                }
            }
            else
            {
                if(playerCursor.sprite != normalCursorSprite) 
                {
                    playerCursor.sprite = normalCursorSprite;
                    playerCursor.transform.localScale = originalCursorSize;
                    InputManager.instance.SwitchStandardKeyActions("normal");
                }
            }
        }
        else
        {
            if(playerCursor.sprite != normalCursorSprite) 
            {
                playerCursor.sprite = normalCursorSprite;
                playerCursor.transform.localScale = originalCursorSize;
                InputManager.instance.SwitchStandardKeyActions("normal");
            }
        }
    }

    void HandleRaycast(RaycastHit _hit)
    {
        if(_hit.collider.gameObject.CompareTag("Piano")) 
        {
            InputManager.instance.SwitchStandardKeyActions("piano");
        }
    }

    bool CheckColliderLayerMask(Collider col, LayerMask layerMask)
    {
        if(col == null) return false;

        if (((1 << col.gameObject.layer) & layerMask) != 0)
        {
            //Debug.Log($"Raycast: {col.gameObject.name} is on {layerMask}");
            return true;
        }

        //Debug.Log($"Raycast: {col.gameObject.name} is not on {layerMask}");
        return false;
    }
}
