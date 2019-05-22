using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] GameObject playerParticle = null;
    [SerializeField] GameObject concreteParticle = null;
    [SerializeField] GameObject woodParticle = null;
    [SerializeField] GameObject metalParticle = null;
    [SerializeField] GameObject dirtParticle = null;
    void Start()
    {
        Destroy(this.gameObject,5f);
    }
    void FixedUpdate()
    {
        transform.Translate(transform.right * bulletSpeed * Time.deltaTime, Space.World);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject particleObject = null;
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            particleObject = Instantiate(playerParticle, transform.position, Quaternion.identity);
        }
        else if (other.CompareTag("Concrete"))
        {
            Destroy(this.gameObject);
            particleObject = Instantiate(concreteParticle, transform.position, Quaternion.identity);
        }
        else if (other.CompareTag("Wood"))
        {
            Destroy(this.gameObject);
            particleObject = Instantiate(woodParticle, transform.position, Quaternion.identity);
        }
        else if (other.CompareTag("Metal"))
        {
            Destroy(this.gameObject);
            particleObject = Instantiate(metalParticle, transform.position, Quaternion.identity);
        }
        else if (other.CompareTag("Dirt"))
        {
            Destroy(this.gameObject);
            particleObject = Instantiate(dirtParticle, transform.position, Quaternion.identity);
        }

        if (particleObject == null) return;

        particleObject.transform.position = new Vector3(particleObject.transform.position.x, particleObject.transform.position.y, -3f);
        Destroy(particleObject.gameObject, 2f);
    }
}
