using UnityEngine;
using System.Collections;

public class CorpsePickup : MonoBehaviour
{
    public float thisMass = 0f;
    public bool thrownByPlayer = false;

    // Use this for initialization
    void Start()
    {
        thisMass = 1f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !thrownByPlayer)
        {
            other.gameObject.GetComponent<Inventory>().changeMass(thisMass);
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && thrownByPlayer)
        {
            thrownByPlayer = false;
        }
    }
}