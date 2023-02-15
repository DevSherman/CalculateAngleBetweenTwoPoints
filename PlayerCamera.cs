using UnityEngine;
using TMPro;

public class PlayerCamera : MonoBehaviour
{
    public enum Plane
    {
        XZ,
        XY,
    };
    
    public Transform origin;
    public TextMeshPro textMesh;
    private Camera mainCamera;
    private RaycastHit hit;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit))
        {
            Debug.DrawLine(mainCamera.transform.position, hit.point, Color.red);
            Debug.DrawLine(origin.position, hit.point, Color.green);
            float angleXZ = CalculateAngle(origin, hit.point, Plane.XZ);
            float angleXYZ = CalculateAngle(origin, hit.point, Plane.XY);

            textMesh.text = "XZ: " + angleXZ + ", XZ: " + angleXYZ;
        }
    }

    /// <summary>
    /// Angle(in degrees) between two points in 3D relative to a plane(XY or XZ), clamped to -180 to 180.
    /// </summary>
    /// <param name="_t"></param>
    /// <param name="_target"></param>
    /// <param name="_plane"></param>
    /// <returns></returns>
    float CalculateAngle(Transform _t, Vector3 _target, Plane _plane)
    {
        Vector3 relative = _t.InverseTransformPoint(_target);
        if (_plane == Plane.XZ)
        {
            return Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
        }
        else
        {
            float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg + 90;
            if (_t.position.x < _target.x) return -1 * (angle - 180);
            else return angle;
        }
    }
}
