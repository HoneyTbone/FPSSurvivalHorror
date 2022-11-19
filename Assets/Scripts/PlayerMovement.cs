using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Base Controller off Unity Tutorial from Brackeys
https://www.youtube.com/watch?v=_QajrabyTJc&ab_channel=Brackeys
- Tyson Toller
*/

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Character Controller")]
    [SerializeField] CharacterController pc;

    [Header("Move Characteristics")]
    [SerializeField] float moveSpeed = 12f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 3f;

    [Header("Ground Check")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;


    // Update is called once per frame
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        pc.Move(move * moveSpeed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        pc.Move(velocity * Time.deltaTime);
    }
}
