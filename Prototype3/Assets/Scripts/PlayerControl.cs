using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    float horizontalAxis;
    float verticalAxis;
    float speed = 1f;
    private Vector2 currentRotation;
    public float sensitivity = 0.0000001f;
    public float maxYAngle = 80f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(horizontalAxis, 0, verticalAxis);

        gameObject.transform.Translate(moveDir*speed*Time.deltaTime);
    
        if(Input.GetKey(KeyCode.Space))
        {
         
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.transform.Translate(new Vector3(0.0f, 5, 0.0f) * speed * Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }

        currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
        currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
        gameObject.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
    }
}
