using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    public new GameObject camera;
    public GameObject player;
    public GameObject playerBody;
    public float speed;
    public float cameraSensitivity;
    public float cameraDistance;
    [SerializeField]
    private float horizontalCameraAngle = 0;
    [SerializeField]
    private float verticalCameraAngle = 0;

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // локальное направление движения
        var localMoveDirection = new Vector3(horizontal, 0, vertical);
        // направление взгляда игрока
        var playerForward = Vector3.ProjectOnPlane(playerBody.transform.position - camera.transform.position, Vector3.up);
        // глобальное направление движения
        var moveDirection = Vector3.Normalize(Quaternion.FromToRotation(Vector3.forward, playerForward) * localMoveDirection);

        player.transform.position = player.transform.position + Time.deltaTime * speed * moveDirection;
    }

    void MoveCamera()
    {
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        // обновление значений угла камеры
        horizontalCameraAngle += horizontal * cameraSensitivity * Time.deltaTime;
        verticalCameraAngle += vertical * cameraSensitivity * Time.deltaTime;

        // ограничение вертикального угла, чтобы камера не переворачивалась над игроком и не заходила под землю
        verticalCameraAngle = Mathf.Clamp(verticalCameraAngle, 0, 60);

        // ограничение значений углов от 0 до 360, чтобы не вызвать переполнения переменных
        if (horizontalCameraAngle > 360f) horizontalCameraAngle -= 360f;
        if (horizontalCameraAngle < 0f) horizontalCameraAngle += 360f;

        // смещение камеры относительно игрока
        Vector3 newCameraPosition = Vector3.back * cameraDistance;

        // вертикальный поворот вектора смещения
        newCameraPosition = Quaternion.Euler(verticalCameraAngle, 0f, 0f) * newCameraPosition;
        // горизонтальный поворот вектора смещения
        newCameraPosition = Quaternion.Euler(0f, horizontalCameraAngle, 0f) * newCameraPosition;

        // применение вектора смещения к камере
        camera.transform.position = playerBody.transform.position + newCameraPosition;
        // поворот камеры в сторону игрока
        camera.transform.LookAt(playerBody.transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
        MoveCamera();
    }
}
