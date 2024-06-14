using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private bool hoveringOverSlot;
    [SerializeField] private bool hoveringOverSlotDropArea;
    [SerializeField] public bool slotFull;

    [SerializeField] GameObject itemInSlot;
    GameObject player;

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (this.transform.childCount > 0)
        {
            itemInSlot = this.transform.GetChild(0).gameObject;
            slotFull = true;
        }
        else
        {
            slotFull = false;
        }  

        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButton(0))
        {
            // Debug.Log(Input.mousePosition);

            if (Physics.Raycast (ray, out hit, 100, 1 << 5) && hit.transform.tag == "InvDropArea") //UI Layer only
            {
                Debug.Log(hit.transform.name);
                hoveringOverSlotDropArea = true;
            }
            else
            {
                Debug.Log("Not over drop area!");
                hoveringOverSlotDropArea = false;
            } 
        }

        if (Input.GetMouseButtonUp(0) && slotFull && hoveringOverSlotDropArea)
        {
            Debug.Log("trying to drop");

            itemInSlot.SetActive(true);
            itemInSlot.transform.parent = null;

            itemInSlot.transform.position = player.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));

            //itemInSlot.GetComponent<Item>().ResetItemIcon();
            itemInSlot = null;
        }
    }

    void OnMouseOver()
    {
        hoveringOverSlot = true;
    }

    void OnMouseExit()
    {
        hoveringOverSlot = false;
    }
}
