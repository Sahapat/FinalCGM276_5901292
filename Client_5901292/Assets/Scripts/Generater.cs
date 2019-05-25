using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generater : MonoBehaviour
{
    [SerializeField]GameObject PlatformPrefab = null;
    [SerializeField]float currentPlatformPosY = 4.32f;
    [SerializeField]float increasterY = 4.32f;
    [SerializeField]float currentPlatformPosZ = 0.5f;
    [SerializeField]float increasterZ = 0.5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Generate"))
        {
            Instantiate(PlatformPrefab,new Vector3(0,currentPlatformPosY+increasterY,currentPlatformPosZ+increasterZ),Quaternion.identity);
            currentPlatformPosY += increasterY;
            currentPlatformPosZ += increasterZ;
            other.tag = "Untagged";
        }
    }
}
