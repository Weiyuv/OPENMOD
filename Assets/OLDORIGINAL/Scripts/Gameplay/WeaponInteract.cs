using UnityEngine;

public class WeaponInteract : MonoBehaviour
{
    public Transform rightHand;
    public Transform leftHand;
    public GameObject itemReference;
    GameObject weaponInstance;
    public MenuGame menuGame;
    public MoveChanPhisical moveChanPhisical;
    public Animator animator;

    [Header("Starting Weapon")]
    public GameObject startingWeaponPrefab;

    void Start()
    {
        menuGame = FindObjectOfType<MenuGame>();
        moveChanPhisical = GetComponent<MoveChanPhisical>();
        if (menuGame == null)
        {
            Debug.LogError("MenuGame script not found in the scene.");
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on this GameObject.");
        }

        // Instanciar arma inicial
        if (startingWeaponPrefab != null)
        {
            itemReference = Instantiate(startingWeaponPrefab);
            EquipWeapon(true);
        }
    }

    void Update()
    {
        if (weaponInstance != null)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (weaponInstance.activeSelf)
                {
                    UnequipWeapon(); // Guarda a arma
                }
                else
                {
                    ReEquipWeapon(); // Reequipa a arma
                }
            }
        }
    }

    public void EquipWeapon(bool fromInventory = false)
    {
        if (itemReference == null)
        {
            Debug.LogError("Weapon prefab is not assigned.");
            return;
        }

        if (!fromInventory)
        {
            menuGame.AddItemToInventory(itemReference.GetComponent<ItemRef>().item, 1);
        }

        if (weaponInstance == null)
        {
            weaponInstance = itemReference;
            weaponInstance.transform.SetParent(rightHand);
            weaponInstance.transform.localPosition = Vector3.zero;
            weaponInstance.transform.localRotation = Quaternion.identity;
            weaponInstance.layer = LayerMask.NameToLayer("Player");

            FixedJoint fixedJoint = weaponInstance.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = rightHand.GetComponent<Rigidbody>();
            fixedJoint.connectedBody.collisionDetectionMode = CollisionDetectionMode.Continuous;

            animator.SetLayerWeight(animator.GetLayerIndex("Sword"), 1);
            animator.SetBool("Weapon", true);
            moveChanPhisical.haveWeapons = true;
        }
    }

    public void ReEquipWeapon()
    {
        if (weaponInstance != null)
        {
            weaponInstance.SetActive(true);
            animator.SetLayerWeight(animator.GetLayerIndex("Sword"), 1);
            animator.SetBool("Weapon", true);
            moveChanPhisical.haveWeapons = true;
        }
    }

    public void UnequipWeapon()
    {
        if (weaponInstance != null)
        {
            weaponInstance.SetActive(false);
            animator.SetLayerWeight(animator.GetLayerIndex("Sword"), 0);
            animator.SetBool("Weapon", false);
            moveChanPhisical.haveWeapons = false;
        }
    }

    public void EquipWeaponFromInventory(Item item)
    {
        if (item == null)
        {
            Debug.LogError("Item is not assigned.");
            return;
        }
        if (weaponInstance == null)
        {
            itemReference = Instantiate(item.prefab);
            EquipWeapon(true);
        }
    }

    public void DropWeapon()
    {
        if (weaponInstance != null)
        {
            weaponInstance.transform.SetParent(null);
            weaponInstance.GetComponent<Rigidbody>().isKinematic = false;
            weaponInstance.GetComponent<Collider>().enabled = true;

            FixedJoint fixedJoint = weaponInstance.GetComponent<FixedJoint>();
            if (fixedJoint != null)
            {
                Destroy(fixedJoint);
            }

            animator.SetLayerWeight(animator.GetLayerIndex("Sword"), 0);
            animator.SetBool("Weapon", false);
            menuGame.RemoveItemFromInventory(weaponInstance.GetComponent<ItemRef>().item, 1);
            moveChanPhisical.haveWeapons = false;
        }
    }

    public void StoreItem(Item item)
    {
        if (item == null)
        {
            Debug.LogError("Item is not assigned.");
            return;
        }
        menuGame.AddItemToInventory(itemReference.GetComponent<ItemRef>().item, 1);
        Destroy(itemReference);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            itemReference = other.gameObject;
            EquipWeapon();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            itemReference = collision.gameObject;
            EquipWeapon();
        }

        if (collision.gameObject.CompareTag("Item"))
        {
            itemReference = collision.gameObject;
            StoreItem(itemReference.GetComponent<ItemRef>().item);
        }
    }
}
