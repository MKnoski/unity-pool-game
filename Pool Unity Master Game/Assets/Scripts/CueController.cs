using UnityEngine;
using System.Collections;

public class CueController : MonoBehaviour {

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update () {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Debug.Log("Space key was pressed.");
        //}
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.AddForce(new Vector3(moveHorizontal, 0, moveVertical)*10);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log( " collected: " + collision.collider.name);
        rb.velocity = Vector3.zero;
    }

    public void OnGUI()
    {
        if (Event.current.keyCode == KeyCode.Space && Event.current.type == EventType.KeyDown)
        {
            rb.AddForce(new Vector3(0, 0, 500));


        }
    }

}
