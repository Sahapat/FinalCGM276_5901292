using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotater : MonoBehaviour
{
    [System.Serializable]
    private struct RotaterClamp
    {
        public float MaxAngle;
        public float MinAngle;

        public static RotaterClamp Defualt
        {
            get
            {
                RotaterClamp temp = new RotaterClamp();
                temp.MaxAngle = 0;
                temp.MinAngle = 0;
                return temp;
            }
        }
    }
    [SerializeField] float currentAngle = 0f;
    [SerializeField] RotaterClamp rotaterClamp = RotaterClamp.Defualt;
    [SerializeField] float speed = 5f;
    [SerializeField] int direction = 1;
    [SerializeField] bool isRotate = false;
    [SerializeField] float time = 2f;
    [SerializeField] LineRenderer laserShowing = null;

    float counterTime = 0;
    void FixedUpdate()
    {
        if (isRotate)
        {
            /* transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + speed * Time.deltaTime * direction);
            if (direction > 0)
            {
                if (transform.eulerAngles.z < 300 && transform.eulerAngles.z > rotaterClamp.MinAngle)
                {
                    direction = -1;
                }
            }
            else
            {
                if (transform.eulerAngles.z > 300 && transform.eulerAngles.z < rotaterClamp.MaxAngle)
                {
                    direction = 1;
                }
            }
            currentAngle = transform.eulerAngles.z;
            laserShowing.enabled = true; */
            if(counterTime >= time)
            {
                direction *= (-1);
                counterTime = 0;
            }
            /* transform.localRotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + speed * Time.deltaTime * direction); */
            transform.Rotate(new Vector3(0,0,direction*speed*Time.deltaTime));
            currentAngle= transform.eulerAngles.z;
            counterTime += Time.deltaTime;
            laserShowing.enabled = true;
        }
        else
        {
            laserShowing.enabled = false;
        }
    }
    public void SetRotation(float z)
    {
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, z, transform.rotation.w);
    }
    public void StartRotate()
    {
        transform.rotation = Quaternion.identity;
        isRotate = true;
    }
    public void StopRotate()
    {
        isRotate = false;
    }
    public void SetFlip()
    {
        if(this.transform.root.gameObject.name == "Player1")
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        counterTime = 0;
    }
}
