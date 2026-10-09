using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ShipMovement))]
[RequireComponent(typeof(ShipShooting))]
[RequireComponent(typeof(ShipMessages))]
public class ShipInventory : MonoBehaviour
{
    [SerializeField] private Image itemImageHolder;

    private ShipMovement movement;
    private ShipShooting shooting;
    private ShipMessages messages;

    private List<Color> items = new List<Color>();
    private int activeItemIndex = -1;

    void Awake()
    {
        movement = GetComponent<ShipMovement>();
        shooting = GetComponent<ShipShooting>();
        messages = GetComponent<ShipMessages>();
    }

    void Update()
    {
        CycleItems();
        UseItem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            PickUpItem(other.gameObject);
        }
    }

    void PickUpItem(GameObject item)
    {
        Color color = item.GetComponent<Renderer>().material.color;

        Destroy(item);

        items.Add(color);
        activeItemIndex = items.Count - 1;

        itemImageHolder.color = items[activeItemIndex];
        itemImageHolder.enabled = true;
    }

    void CycleItems()
    {
        if (!Input.GetKeyDown(KeyCode.LeftControl)) return;

        if (items.Count > 0)
        {
            if (activeItemIndex < items.Count - 1)
            {
                activeItemIndex++;
            }
            else
            {
                activeItemIndex = 0;
            }
            itemImageHolder.color = items[activeItemIndex];
        }
        else
        {
            ClearItemHolder();
        }
    }

    void UseItem()
    {
        if (!Input.GetKeyDown(KeyCode.E) || items.Count == 0 || activeItemIndex == -1) return;

        ApplyEffect(items[activeItemIndex]);
        items.RemoveAt(activeItemIndex);

        if (items.Count == 0)
        {
            ClearItemHolder();
            return;
        }

        if (activeItemIndex > 0)
        {
            activeItemIndex--;
        }
        itemImageHolder.color = items[activeItemIndex];
    }

    void ApplyEffect(Color color)
    {
        if (color == Color.blue)
        {
            messages.ShowMessage(" +  Move Speed");
            movement.AddMoveSpeed(5f);
        }
        else if (color == Color.red)
        {
            messages.ShowMessage(" + Fire Rate");
            shooting.ReduceCooldown(0.1f);
        }
        else if (color == Color.green)
        {
            messages.ShowMessage(" + Rotation Speed");
            movement.AddRotationSpeed(10f);
        }
    }

    void ClearItemHolder()
    {
        itemImageHolder.color = Color.white;
        activeItemIndex = -1;
        itemImageHolder.enabled = false;
    }
}