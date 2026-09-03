using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] private float pickupRange = 5f;

    [Header("UI")]
    [SerializeField] private TMP_Text pickupPrompt;
    [SerializeField] private TMP_Text itemCounter;

    [Header("Required Items")]
    [SerializeField] private int requiredItems = 3;

    private GameObject nearbyObject;

    private HashSet<GameObject> collectedObjects = new HashSet<GameObject>();

    public int ItemsCollected => collectedObjects.Count;

    private void Start()
    {
        UpdateItemCounter();
    }

    private void Update()
    {
        FindNearbyObject();

        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (nearbyObject != null)
            {
                PickUpObject(nearbyObject);
            }
        }
    }

    private void FindNearbyObject()
    {
        nearbyObject = null;

        GameObject[] pickableObjects =
            GameObject.FindGameObjectsWithTag("Pickable");

        float closestDistance = pickupRange;

        foreach (GameObject obj in pickableObjects)
        {
            float distance = Vector3.Distance(
                transform.position,
                obj.transform.position
            );

            if (distance <= closestDistance)
            {
                closestDistance = distance;
                nearbyObject = obj;
            }
        }

        if (pickupPrompt != null)
        {
            pickupPrompt.gameObject.SetActive(nearbyObject != null);
        }
    }

    private void PickUpObject(GameObject objectToPickUp)
    {
        // Prevent the same object from being counted twice.
        if (collectedObjects.Contains(objectToPickUp))
            return;

        collectedObjects.Add(objectToPickUp);

        // Make the item disappear.
        Destroy(objectToPickUp);

        UpdateItemCounter();
    }

    private void UpdateItemCounter()
    {
        if (itemCounter != null)
        {
            itemCounter.text = $"Items: {ItemsCollected}/{requiredItems}";
        }
    }
}