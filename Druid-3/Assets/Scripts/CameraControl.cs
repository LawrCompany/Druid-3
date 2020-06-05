using UnityEngine;


public class CameraControl : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] public Vector3 offset;
    public float sensitivity = 3; // чувствительность мышки
    public float limit = 80; // ограничение вращения по Y
    public float zoom = 0.25f; // чувствительность при увеличении, колесиком мышки
    public float zoomMax = 4; // макс. увеличение
    public float zoomMin = 2; // мин. увеличение
    private float X;
    private float Y;
 
    private float zoomFaer;
    private bool leftEndRight;
 
 
    void Start()
    {
        limit = Mathf.Abs(limit);
        if (limit > 90) limit = 90;
        offset = new Vector3(offset.x, offset.y, -Mathf.Abs(zoomMax) / 2);
        transform.position = target.position + offset;
    }
 
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) offset.z += zoom;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0) offset.z -= zoom;
 
        #region
        if (Input.GetKeyDown(KeyCode.Mouse1) == true)
        {
            zoomFaer = offset.z;
            offset.z = 0;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1) == true)
        {
            offset.z = zoomFaer;
        }
 
        if (Input.GetKeyUp(KeyCode.Tab) == true)
        {
            if (leftEndRight == false)
            {
                leftEndRight = true;
                offset.x = -0.5f;
            }
            else if (leftEndRight == true)
            {
                leftEndRight = false;
                offset.x = 0.5f;
            }
        }
        #endregion
 
        offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));
        
        X += Input.GetAxis("Mouse X") * sensitivity;
        Y += Input.GetAxis("Mouse Y") * sensitivity;
        Y = Mathf.Clamp(Y, -limit, limit);
        transform.localEulerAngles = new Vector3(-Y, X, 0);
        transform.position = transform.localRotation * offset + target.position;
 
    }
}