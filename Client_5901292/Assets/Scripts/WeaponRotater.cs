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

    [SerializeField] LineRenderer laserShowing = null;

    void FixedUpdate()
    {
        if (isRotate)
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + speed * Time.deltaTime * direction);
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
            laserShowing.enabled = true;
        }
        else
        {
            laserShowing.enabled = false;
        }
    }
    public void StartRotate()
    {
        isRotate = true;
    }
    public void StartRotate(float rotationZ)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,rotationZ);
        isRotate = true;
    }
    public void StopRotate()
    {
        isRotate = false;
    }
}
