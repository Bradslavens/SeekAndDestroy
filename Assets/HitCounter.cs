using UnityEngine;

public class HitCounter : MonoBehaviour
{
    public int hitLimit = 3;  // Number of times the object can be hit before deactivation
    private int hitCount = 0; // Current hit count

    private void OnCollisionEnter(Collision collision)
    {

        // Check if the collision occurred with the object that can hit this game object
        if (collision.gameObject.CompareTag("HittingObject"))
        {
            collision.gameObject.SetActive(false);
            // Increment the hit count
            hitCount++;

            // Check if the hit count exceeds the limit
            if (hitCount >= hitLimit)
            {
                // Deactivate the hit object
                // Deactivate this object
                gameObject.SetActive(false);

            }
        }
    }
}
