using Photon.Pun;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public CharacterController control;

    public Joystick moveStick;
    public Joystick camStick;
    public Transform cam;
    // private float xrot;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public PhotonView view;

    private float cameraAngle;
    private float cameraAngleSpeed = 0.5f;

    //private RaycastHit ray;
    //public float clipOffset = 0.1f;
    //public Vector3 clipCheckOffset = new Vector3(0, 1, 0);

    private Vector3 offset;
    private Vector3 offsetY;

    // Start is called before the first frame update
    private void Start()
    {
        speed = 8f;

        offset = new Vector3(transform.position.x - .5f, transform.position.y + 2.5f, transform.position.z - 2f);
        //offsetX = new Vector3(0, transform.position.y + 2.5f, transform.position.z - 2f);
        // offsetY = new Vector3(0, 0, transform.position.z - 2f);
        //  cam.GetComponent<>
    }

    // Update is called once per frame
    private void Update()
    {
        if (view.IsMine)
        {
            PlayerMove();
            CameraMove();
        }
    }

    public void PlayerMove()
    {
        // Debug.Log("!! MINE");
        Vector3 direction = Vector3.forward * moveStick.Vertical + Vector3.right * moveStick.Horizontal;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        control.Move(direction * speed * Time.deltaTime);
        // transform.position = transform.position + direction * speed * Time.deltaTime;

        //Vector3 direction1 = Vector3.forward * move.Vertical + Vector3.right * move.Horizontal;
        ////control.Move(direction1 * speed * Time.deltaTime);
        //transform.position = transform.position + direction1 * speed * Time.deltaTime;
        //// Debug.Log(direction);
        //// rb.velocity = direction * speed;
    }

    public void CameraMove()
    {
        //Sets camera position to players position at an offset (distance) so that it follows
        //Distance subtracted from z position so camera is positioned on the players side while they move along the x axis
        // cam.transform.position = new Vector3(transform.position.x - .5f, transform.position.y + 2.5f, transform.position.z - 2f);
        //cam.transform.Rotate(Vector3.up, camStick.Horizontal * speed / 14);
        //cam.transform.localEulerAngles = new Vector3(xrot, cam.transform.localEulerAngles.y, 0);

        cameraAngle += camStick.Horizontal * cameraAngleSpeed;
        cam.position = transform.position + Quaternion.AngleAxis(cameraAngle, Vector3.up) * new Vector3(0, 3.5f, -3.4f);
        cam.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - cam.transform.position, Vector3.up);

        //cam.transform.position = new Vector3(transform.position.x - .5f, transform.position.y + 2.5f, transform.position.z - 2f);

        // offsetX = Quaternion.AngleAxis(cameraAngle, Vector3.up) * offsetX;
        // offsetY = Quaternion.AngleAxis(cameraAngle, Vector3.right) * offsetY;
        //offset = Quaternion.AngleAxis(camStick.Horizontal * cameraAngleSpeed, Vector3.up) * offset;
        //cam.transform.position = transform.position + offset + new Vector3(0, 3.5f, -3.4f);
        //cam.transform.LookAt(transform.position);

        //cam.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - cam.transform.position, Vector3.up);

        //Debug.DrawRay(cam.transform.position, ((transform.position + clipCheckOffset) - cam.transform.position), Color.green);
        //if (Physics.Raycast(cam.transform.position, (transform.position + clipCheckOffset) - cam.transform.position, out ray, -cam.transform.localPosition.z - 0.5f))
        //{
        //    cam.transform.position = ray.point + ((transform.position + clipCheckOffset) - cam.transform.position).normalized * clipOffset;
        //    Debug.Log("hit something");
        //}
    }

    public void SetJoysticks(GameObject camera) //*
    {
        Joystick[] tempJoystickList = camera.GetComponentsInChildren<Joystick>();
        foreach (Joystick temp in tempJoystickList)
        {
            if (temp.tag == "Joystick Movement")
                moveStick = temp;
            else if (temp.tag == "Joystick Camera")
                camStick = temp;
        }

        cam = camera.transform;

        // xrot = cam.transform.localEulerAngles.x;
    }
}