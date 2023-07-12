using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheildSpawn : MonoBehaviour
{
    public GameObject sheild;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SheidSpawn()
    {
        Vector3 shieldTransform = Camera.main.transform.position + Camera.main.transform.forward * 0.3f;
        GameObject shelid = Instantiate(sheild, shieldTransform, Camera.main.transform.rotation);
        Destroy(shelid ,2f);
    }
}
