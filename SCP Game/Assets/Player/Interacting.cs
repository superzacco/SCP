using UnityEngine;
using UnityEngine.UI;

public class Interacting : MonoBehaviour
{
    [SerializeField] private int minInteractionDistance;
    [SerializeField] private Image grabIcon;
    [SerializeField] private Inventory inventoryScript;

    [SerializeField] private GameObject item;

    void Update()
    {    
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward) ;
        RaycastHit hit;

        if (Physics.Raycast (ray, out hit, minInteractionDistance, 1 << 6))
        {
            grabIcon.enabled = true;
            item = hit.collider.gameObject;
        }
        else
        {
            grabIcon.enabled = false;
            item = null;
        } 

        if(Input.GetMouseButtonUp(0) && item != null && !inventoryScript.inventoryOpen)
        {
            inventoryScript.FindSlotForItem(item.GetComponent<Item>().itemIcon, item);
        }  
    }
}
