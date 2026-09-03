using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private int requiredItems = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerPickup playerPickup = other.GetComponent<PlayerPickup>();

        if (playerPickup == null)
            return;

        if (playerPickup.ItemsCollected >= requiredItems)
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.Log(
                $"You need {requiredItems} items. " +
                $"You currently have {playerPickup.ItemsCollected}."
            );
        }
    }
}