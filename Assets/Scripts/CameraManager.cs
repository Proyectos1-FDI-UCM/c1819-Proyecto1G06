using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public float offsetLimit = 5f;
    //public float minDistance = 1f;    //si cambiamos la funcionalidad entrará en juego

    Camera cam;
    Transform player { get { return GameManager.instance.player.transform; } }

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        GetComponentInParent<FollowTarget>().target = player;
    }

    /// <summary>
    /// Aumenta la distancia entre el jugador y la cámara 
    /// según la posición del ratón, con la función x^(1/2).
    /// </summary>
    void LateUpdate()
    {
        Vector3 mousePos = (Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2)) / (Screen.height / 2) * (cam.orthographicSize);
        mousePos = Vector3.ClampMagnitude(mousePos, cam.orthographicSize);
        Vector3 camPos = (mousePos * Mathf.Pow(mousePos.magnitude, 0.5f)) / cam.orthographicSize * offsetLimit;
        transform.localPosition = camPos;
    }
}
