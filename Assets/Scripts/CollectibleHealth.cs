using UnityEngine;

public class CollectibleHealth : MonoBehaviour
{
    public AudioClip collectedClip;
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.PlaySound(collectedClip);
            controller.ChangeHealth(1);
            Destroy(gameObject);
        }
    }
}
