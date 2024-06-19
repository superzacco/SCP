using UnityEngine.UI;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inv. Opening & Closing")]
    [SerializeField] public bool inventoryOpen;
    [SerializeField] GameObject inventoryScreen;
    [SerializeField] Looking playerLookingScript;

    [Header("Item Management")]
    [SerializeField] private GameObject inventorySlots;
    [SerializeField] private GameObject slotItemImages;
    [SerializeField] private GameObject itemInHand;
    [SerializeField] private RectTransform itemImageInHand;
    [SerializeField] private GameObject[] inventoryItemsArray;
    [SerializeField] private GameObject[] inventorySlotsArray;
    [SerializeField] private Image[] inventoryImageSlotsArray;
    [SerializeField] private int hoveredSlot;
    [SerializeField] private int itemOriginSlot;

    [Header("Item Management (Debug)")]
    [SerializeField] private bool hoveringOverSlotDropArea;

    void Awake()
    {
        inventoryItemsArray = new GameObject[8];
        inventorySlotsArray = new GameObject[8];
        inventoryImageSlotsArray = new Image[8];

        for (int s = 0; s < inventorySlots.transform.childCount; s++)
        {
            inventorySlotsArray[s] = inventorySlots.transform.GetChild(s).gameObject;
        }

        for (int s = 0; s < slotItemImages.transform.childCount; s++)
        {
            inventoryImageSlotsArray[s] = slotItemImages.transform.GetChild(s).GetComponent<Image>();
        }
        
        inventoryScreen.SetActive(false);
    }

    void Update()
    {
        #region // INVENTORY SCRIPT
        if (inventoryScreen.activeSelf)
            inventoryOpen = true;
        else
            inventoryOpen = false;

        if (Input.GetKeyDown(KeyCode.Tab) && !inventoryOpen) 
        {
            inventoryScreen.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            playerLookingScript.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Tab) && inventoryOpen)
        {
            ReturnToSameSlot(); 
            hoveringOverSlotDropArea = false;
            hoveredSlot = itemOriginSlot;

            inventoryScreen.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;   

            playerLookingScript.enabled = true;
        }
        #endregion

        #region // OLD SLOT SCRIPT (DROPPING ITEMS)
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast (ray, out hit, 100, 1 << 5) && inventoryOpen) // UI layer only
        {
            if (hit.transform.CompareTag("InvDropArea"))
            {
                hoveringOverSlotDropArea = true;
            }
            else
            {
                hoveringOverSlotDropArea = false;
            }

            if (System.Array.IndexOf(inventorySlotsArray, hit.collider.gameObject) != -1)
            {
                hoveredSlot = System.Array.IndexOf(inventorySlotsArray, hit.collider.gameObject);
            }
        }

        if (Input.GetMouseButtonDown(0) && inventoryOpen)
        {
            itemInHand = inventoryItemsArray[hoveredSlot];
            itemOriginSlot = hoveredSlot;
        }

        if (Input.GetMouseButton(0) && inventoryOpen)
        {
            Vector2 anchoredPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(slotItemImages.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out anchoredPos);
            itemImageInHand = slotItemImages.transform.GetChild(itemOriginSlot).GetComponent<RectTransform>();

            Vector2 reAdjust = itemImageInHand.sizeDelta / 2.0f;
            itemImageInHand.anchoredPosition = anchoredPos - reAdjust * new Vector2(-1, 1);
        }

        if (Input.GetMouseButtonUp(0) && itemInHand != null && inventoryOpen)
        {
            if (hoveringOverSlotDropArea)
            {
                DropItem();
            }

            if (!hoveringOverSlotDropArea)
            {
                if (itemOriginSlot != hoveredSlot)
                {
                    SwapItems();
                }

                if (itemOriginSlot == hoveredSlot)
                {
                    ReturnToSameSlot();
                }
            }

            itemInHand = null;
        }
        #endregion
    }

    public void FindSlotForItem(Sprite itemIcon, GameObject item)
    {
        for (int i = 0; i < inventorySlots.transform.childCount; i++) // loop over slots until empty one
        {
            if (inventorySlots.transform.GetChild(i).childCount == 0) // if empty, it's suitable -- then:
            {
                Transform suitableSlot = inventorySlots.transform.GetChild(i); // set suitable slot

                item.transform.parent = suitableSlot;                 
                item.transform.position = suitableSlot.transform.position;
                item.SetActive(false); // put it there

                inventoryItemsArray[i] = item; // assign picked up to inventory item array

                // get same numbered image slot as the item slot above
                Image imageSlotToSet = slotItemImages.transform.GetChild(i).GetComponent<Image>();

                imageSlotToSet.sprite = itemIcon;   // put the right image on the right slot
                imageSlotToSet.color = new Color(1,1,1,1);
                break;
            }
        }
    }

    void DropItem()
    {
        itemInHand.SetActive(true);
        itemInHand.transform.parent = null;
        inventoryItemsArray[itemOriginSlot] = null;

        itemInHand.transform.position = this.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));

        ResetItemIcon();
    }

    void SwapItems()
    {
        if (inventoryItemsArray[hoveredSlot] == null)
        {
            itemInHand.transform.parent = inventorySlotsArray[hoveredSlot].transform;
            inventoryImageSlotsArray[hoveredSlot].sprite = inventoryItemsArray[itemOriginSlot].GetComponent<Item>().itemIcon;
            inventoryImageSlotsArray[hoveredSlot].color = new Color(1,1,1,1);
            inventoryItemsArray[itemOriginSlot] = null;
            inventoryItemsArray[hoveredSlot] = itemInHand;

            ResetItemIcon();
        }
        else
        {
            GameObject TEMPORARY_ITEM;
            Sprite TEMPORARY_IMAGE;

            TEMPORARY_ITEM = inventoryItemsArray[itemOriginSlot];
            TEMPORARY_IMAGE = inventoryImageSlotsArray[itemOriginSlot].sprite;

            inventoryItemsArray[itemOriginSlot] = inventoryItemsArray[hoveredSlot];
            inventoryItemsArray[hoveredSlot] = TEMPORARY_ITEM;

            inventoryImageSlotsArray[itemOriginSlot].sprite = inventoryImageSlotsArray[hoveredSlot].sprite;
            inventoryImageSlotsArray[hoveredSlot].sprite = TEMPORARY_IMAGE;

            inventoryImageSlotsArray[itemOriginSlot].transform.position = inventorySlotsArray[itemOriginSlot].transform.position;
        }
    }
    
    void ReturnToSameSlot()
    {
        itemImageInHand.transform.position = inventorySlotsArray[itemOriginSlot].transform.position;
    }

    void ResetItemIcon()
    {
        Image imageSlotToReset = slotItemImages.transform.GetChild(itemOriginSlot).GetComponent<Image>();

        imageSlotToReset.sprite = null;
        imageSlotToReset.color = new Color(1,1,1,0);
    }
}
