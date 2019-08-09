using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotSpeed = 450f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8;
    [SerializeField] private Gun gun;

    private CharacterController controller;
    private float shootDistance;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Movement();

        if (Input.GetButtonDown("Shoot"))
        {
            gun.Shoot();
        }
        else if (Input.GetButton("Shoot"))
        {
            gun.ShootContinuous();
        }
    }

    void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        GetMouseRot();

        Vector3 motion = input;
        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? 0.7f : 1;
        motion *= (Input.GetButton("Walk")) ? walkSpeed : runSpeed;
        motion += Vector3.up * -8;

        controller.Move(motion * Time.deltaTime);
    }

    void GetMouseRot()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(camRay, out rayLength))
        {
            Vector3 pointToLook = camRay.GetPoint(rayLength);

            shootDistance = Vector3.Distance(gun.GunTip.position, pointToLook);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

}
