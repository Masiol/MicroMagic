using UnityEngine;
using UnityEngine.EventSystems;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float distanceToTarget = 10;
    [SerializeField] private float maxYRotation = 80f; // Max rotation around Y axis
    [SerializeField] private float minYRotation = -80f; // Min rotation around Y axis

    private Vector3 previousPosition;
    private bool mouseOverUI = false;
    private bool savedMouse = false; // Flaga informuj¹ca, czy kursor zosta³ zatrzymany na obiekcie UI podczas ostatniego klikniêcia

    private void Update()
    {
        // SprawdŸ, czy kursor znajduje siê nad obiektem UI
        mouseOverUI = EventSystem.current.IsPointerOverGameObject();

        if (Input.GetMouseButtonDown(0))
        {
            // SprawdŸ, czy kursor znajduje siê nad obiektem UI w momencie klikniêcia
            if (mouseOverUI)
            {
                savedMouse = true; // Ustaw flagê, jeœli klikniêcie nast¹pi³o nad obiektem UI
            }
            else
            {
                savedMouse = false; // Zresetuj flagê, jeœli klikniêcie nast¹pi³o poza obiektem UI
            }

            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0) && !savedMouse)
        {
            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
            float rotationAroundXAxis = direction.y * 180; // camera moves vertically

            float currentXRotation = cam.transform.localEulerAngles.x;
            float clampedXRotation = currentXRotation + rotationAroundXAxis;

            if (clampedXRotation > 180)
            {
                clampedXRotation -= 360;
            }

            clampedXRotation = Mathf.Clamp(clampedXRotation, minYRotation, maxYRotation);

            // Apply rotations
            cam.transform.RotateAround(target.position, Vector3.up, rotationAroundYAxis);
            cam.transform.localEulerAngles = new Vector3(clampedXRotation, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);

            // Move the camera to the correct distance from the target
            cam.transform.position = target.position - cam.transform.forward * distanceToTarget;

            previousPosition = newPosition;
        }
    }
}
