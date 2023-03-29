using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 90 degrees per second on y axis
        transform.Rotate(0, 90 * Time.deltaTime, 0); 
    }

    private void OnTriggerEnter(Collider other) {
        GameManager.instance.AddPoints(50);
        Destroy(gameObject);
    }
}
