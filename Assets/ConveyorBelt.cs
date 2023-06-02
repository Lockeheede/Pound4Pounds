using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public List<Transform> WheelTransforms;
    public float RotationRate = 1.0f;

    float curAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
       /* for (int i = 0; i < 7; i++)
        {
            string wheelName = "Wheel " + i.ToString();
            Debug.Log("WheelName = " + wheelName);

            Transform wheelTransform = gameObject.transform.Find(wheelName);
            WheelTransforms.Add(wheelTransform);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        curAngle += RotationRate * Time.deltaTime;
        for (int i = 0; i < WheelTransforms.Count; i++)
        {
            Quaternion rotationQuat = Quaternion.Euler(new Vector3(-curAngle, 0, 0));
                WheelTransforms[i].localRotation = rotationQuat;
        }
    }
}
