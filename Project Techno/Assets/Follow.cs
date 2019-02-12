using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private Vector3 offset = new Vector3(0, 10, 0);
    [SerializeField]
    private float speed = 7.5f;
    

    private Transform tf = null;

    private void Awake()
    {
        tf = GetComponent<Transform>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        tf.position = Vector3.Lerp(tf.position, target.position + target.TransformDirection(offset), speed * Time.deltaTime);
        tf.LookAt(target);
	}
}
