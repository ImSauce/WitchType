using UnityEngine;

public class LocationVistedTrigger : MonoBehaviour
{
    [SerializeField] private LocationSO locationVisited;
    [SerializeField] private bool destroyOnTouch = true;



    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.LocationHistoryTracker.RecordLocation(locationVisited);

            if(destroyOnTouch)
            {
                Destroy(gameObject);
            }
           
        }
    }
}
