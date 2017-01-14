using System;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public int MovingBalls { get { return movingBalls; } }
    private static int movingBalls = 0;
    private Rigidbody _rigidbody;
    private AudioSource audioSource;
    private bool isMoving = false;

    // Use this for initialization
    private void Start()
    {
        this._rigidbody = GetComponent<Rigidbody>();
        this.audioSource = GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.transform.parent.name == "Balls")
        {
            Debug.Log("Object:  " + this._rigidbody.velocity.magnitude + "\t" + "Colider  " + col.rigidbody.velocity.magnitude);
            if (this._rigidbody.velocity.magnitude > 1.0)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            
        }
    }

    private void LateUpdate()
    {
        if ((this._rigidbody.velocity.magnitude < 0.3) && (this._rigidbody.velocity.magnitude > 0))
        {
            if (this._rigidbody.velocity.magnitude > 0.03)
            {
                this._rigidbody.velocity *= (float)0.9;
            }
            else
            {
                this._rigidbody.velocity = Vector3.zero;
                this._rigidbody.angularVelocity = Vector3.zero;
                if (this.isMoving)
                {
                    this.isMoving = false;
                    movingBalls--;
                }
            }
        }

        if (!this.isMoving && this._rigidbody.velocity.magnitude > 0.03)
        {
            this.isMoving = true;
            movingBalls++;
        }
    }
}