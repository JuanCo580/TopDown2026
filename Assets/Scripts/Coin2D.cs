using UnityEngine;
public class Coin2D : MonoBehaviour
{
    public AudioClip collectSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, Camera.main.transform.position);
            }
            Destroy(gameObject);
        }
    }
}