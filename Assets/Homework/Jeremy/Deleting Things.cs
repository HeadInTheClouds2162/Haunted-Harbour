using UnityEngine;

public class MYNAME_InteractionController : MonoBehaviour
{
    [Header("Settings")]
    public float interactRange = 5f;
    public float throwForce = 10f;
    public Transform holdParent; // Create an empty GameObject child of the Camera for this

    private GameObject carriedItem;

    void Update()
    {
        // Check for "E" key press
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (carriedItem == null)
            {
                TryInteract();
            }
            else
            {
                ThrowItem();
            }
        }
    }

    void TryInteract()
    {
        // Create a ray from the center of the viewport
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange))
        {
            // --- OBJECTIVE: Delete the object ---
            // Destroy(hit.collider.gameObject); 

            // --- BONUS: Pick up the object ---
            PickUpItem(hit.collider.gameObject);
        }
    }

    void PickUpItem(GameObject obj)
    {
        carriedItem = obj;
        
        // Disable physics so it doesn't jitter while we hold it
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Parent it to the player/camera so it moves with us
        obj.transform.SetParent(holdParent);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
    }

    void ThrowItem()
    {
        // Unparent the item
        carriedItem.transform.SetParent(null);
        
        Rigidbody rb = carriedItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            
            // BONUS BONUS: Throw toward mouse position/Center of Screen
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            rb.AddForce(ray.direction * throwForce, ForceMode.Impulse);
        }

        carriedItem = null;
    }
}