using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    [SerializeField]
    Transform[] wheels;
    [SerializeField]
    float enginePower = 150f;
    float power = 0f;
    float brake = 0f;
    float steer = 0f;
    [SerializeField]
    float maxSteer = 25f;

    Rigidbody rb;
    Transform tf;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -.5f, .3f);
        tf = GetComponent<Transform>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        power = Input.GetAxis("Vertical") * enginePower * Time.deltaTime;
        steer = Input.GetAxis("Horizontal") * maxSteer;
        brake = Input.GetKey(KeyCode.LeftShift) ? rb.mass * .1f : 0f;


        SetWheelAngle();

        tf.position += tf.forward * power;
        if(Mathf.Abs(power) > 0.01f)
            tf.Rotate(tf.up, steer * Time.deltaTime * ((power > 0) ? 1f : -1f));
	}

    void SetWheelAngle()
    {
        for (int i = 0; i < 2; i++)
        {
            Vector3 angles = wheels[i].localEulerAngles;
            angles.y = steer;
            wheels[i].localEulerAngles = angles;
        }
        
    }
}
