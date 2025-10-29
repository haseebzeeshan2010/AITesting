using UnityEngine;

public class CityBuilder : MonoBehaviour
{
    [SerializeField] private GameObject cityPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the city area.");
            cityPrefab.SetActive(true);
            // Additional logic for when the player enters the city area can be added here
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has exited the city area.");
            cityPrefab.SetActive(false);
            // Additional logic for when the player exits the city area can be added here
        }
    }
}
