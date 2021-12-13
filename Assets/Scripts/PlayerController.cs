using Photon.Pun;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public Joystick moveStick;
    public Joystick camStick;
    public Transform cam;
    private float xrot;

    public PhotonView view;

    // Start is called before the first frame update
    private void Start()
    {
        speed = 8f;
        xrot = 0f;
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
        //control.Move(direction * speed * Time.deltaTime);
        transform.position = transform.position + direction * speed * Time.deltaTime;

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
        cam.transform.position = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z - 14f);
        cam.transform.Rotate(Vector3.up, camStick.Horizontal * speed / 16);
        cam.transform.localEulerAngles = new Vector3(xrot, cam.transform.localEulerAngles.y, 0);
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

        xrot = cam.transform.localEulerAngles.x;
    }
}