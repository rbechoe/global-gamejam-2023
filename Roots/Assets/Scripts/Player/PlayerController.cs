using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;

    public Vector3 newDir;

    public float gravity = -9.81f;

    private Vector3 velocity;
    private bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    List<Collider> interactables = new List<Collider>();

    public GameObject interUI;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        newDir = transform.right * x + transform.forward * z;
        controller.Move(newDir * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (interactables.Count > 0)
        {
            interUI.SetActive(true);
        }
        else
        {
            interUI.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 2);

        interactables.Clear();
        foreach(Collider col in cols)
        {
            if (col.CompareTag("puzzle"))
            {
                interactables.Add(col);
            }
        }
    }
}
