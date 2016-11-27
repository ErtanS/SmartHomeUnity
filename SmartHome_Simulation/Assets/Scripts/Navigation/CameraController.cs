using UnityEngine;
using System.Collections;


/// <summary>
/// Steruerung der Kamera die zur Platzierung von Komponenten genutzt wird
/// </summary>
public class CameraController : MonoBehaviour
{
    public float speedForward;
    public float speedRotation;
    private float LockLeft = -8;
    private float LockRight = 32;
    private float LockUp = 22;
    private float LockDown = 3;
    private float lockRotationUp = 10;
    private float lockRotationDown = 89;

    /// <summary>
    /// Verarbeitung der Eingaben des Benutzers und Steuerung der Kamera 
    /// 
    /// W = Nach oben drehen
    /// A = Nach links drehen
    /// S = Nach unten drehen
    /// D = Nach rechts drehen
    /// 
    /// ↑ = Nach vorne gehen
    /// → = Nach rechts gehen
    /// ← = nach Links gehen
    /// ↓ = Nach hinten gehen
    /// 
    /// Bild Up = Kamera auf Y Achse hoch
    /// Bild Down = Kamera auf Y Achse runter
    /// 
    /// </summary>
    void Update()
    {
        float posY = transform.position.y;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(speedForward*Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-speedForward*Time.deltaTime, 0, 0));
        }
        float upDownSpeed = speedForward;
        float rotateSpeedLeftRight = speedRotation;
        Vector3 currentPos = transform.rotation.eulerAngles;
        float tempX = currentPos.x;
        if (tempX >= 180)
        {
            tempX -= 360;
        }
        if (currentPos.x >= 88)
        {
            upDownSpeed *= 10;
            rotateSpeedLeftRight *= 0.06f;
        }
        else if (currentPos.x >= 85)
        {
            upDownSpeed *= 5;
            rotateSpeedLeftRight *= 0.4f;
        }
        else if (currentPos.x >= 80)
        {
            upDownSpeed *= 3;
            rotateSpeedLeftRight *= 0.5f;
        }
        else if (currentPos.x >= 75)
        {
            upDownSpeed *= 2;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, 0, -upDownSpeed*Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward*upDownSpeed*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(-speedRotation*Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(speedRotation*Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -rotateSpeedLeftRight*Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, rotateSpeedLeftRight*Time.deltaTime, 0);
        }
        transform.position = new Vector3(transform.position.x, posY, transform.position.z);

        if (Input.GetKey(KeyCode.PageUp))
        {
            transform.position += new Vector3(0, speedForward*Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.PageDown))
        {
            transform.position += new Vector3(0, -speedForward*Time.deltaTime, 0);
        }
        Vector3 test = transform.rotation.eulerAngles;
        test.z = 0;
        tempX = test.x;
        if (tempX >= 180)
        {
            tempX -= 360;
        }
        if (tempX >= lockRotationDown)
        {
            test.x = lockRotationDown;
        }
        if (tempX <= lockRotationUp)
        {
            test.x = lockRotationUp;
        }
        Quaternion q = Quaternion.Euler(test);
        transform.rotation = q;

        if (transform.position.x <= LockLeft)
        {
            transform.position = new Vector3(LockLeft, transform.position.y, transform.position.z);
        }
        if (transform.position.x >= LockRight)
        {
            transform.position = new Vector3(LockRight, transform.position.y, transform.position.z);
        }
        if (transform.position.y <= LockDown)
        {
            transform.position = new Vector3(transform.position.x, LockDown, transform.position.z);
        }
        if (transform.position.y >= LockUp)
        {
            transform.position = new Vector3(transform.position.x, LockUp, transform.position.z);
        }
        if (transform.position.z <= LockLeft)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, LockLeft);
        }
        if (transform.position.z >= LockRight)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, LockRight);
        }
    }
}