using UnityEngine;

public class LocationVistedTrigger : MonoBehaviour
{
    [SerializeField] private LocationSO locationVisited;
    [SerializeField] private bool destroyOnTouch = true;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"Triggered location: {locationVisited.displayName}");
            GameManager.Instance.LocationHistoryTracker.RecordLocation(locationVisited);

            if(destroyOnTouch)
            {
                Destroy(gameObject);
            }
           
        }
    }
}
