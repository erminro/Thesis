using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject objectToSpawn;

    public GameObject objectToSpawnOn;
    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(-3.75f, 3.25f);
        float z = Random.Range(-2f, 18f);
        float y = Random.Range(0f, 5f);

        GameObject gameObject = Instantiate(objectToSpawnOn, this.transform.position + new Vector3(x, y / 2, z), Quaternion.identity);
        var localScale = gameObject.transform.localScale;
        localScale = localScale + new Vector3(0, y - localScale.y, 0);
        gameObject.transform.localScale = localScale;
        Instantiate(objectToSpawn, this.transform.position + new Vector3(x, 0.5f + y, z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
