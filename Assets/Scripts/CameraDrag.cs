using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    public float dragSpeed = .1f;
    public float zoomSpeed = 1.5f;

    private Vector3 dragOrigin;

    void LateUpdate(){
        // Handle camera zooming
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(Vector3.up * -scroll * zoomSpeed, Space.World);
		
        // Handle camera dragging
        if (Input.GetMouseButtonDown(1)){
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(1)) return;

        Vector3 delta = Input.mousePosition - dragOrigin;
        dragOrigin = Input.mousePosition;

        Vector3 move = new Vector3(-delta.x * dragSpeed, 0, -delta.y * dragSpeed);
        transform.Translate(move, Space.World);
    }
}