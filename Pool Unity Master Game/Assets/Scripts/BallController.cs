using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{

    private Rigidbody _rigidbody;

	// Use this for initialization
	void Start ()
	{
	    _rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate()
    {
        if (_rigidbody.velocity.magnitude < 0.3 && _rigidbody.velocity.magnitude>0)
        {
            if (_rigidbody.velocity.magnitude > 0.03)
            {
                _rigidbody.velocity *= (float)0.9;
            }
            else
            {
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
            }
        }
    }
}
