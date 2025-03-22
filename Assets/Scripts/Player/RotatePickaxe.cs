using UnityEngine;

public class RotatePickaxe : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    private Vector3 baseScale, flippedScale;

    private void Start()
    {
        baseScale = new Vector3(1, 1, 1);
        flippedScale = new Vector3(-1, 1, 1);
    }

    void Update()
    {
        Vector3 mousePos = targetCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 perpendicular = transform.position - mousePos;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);

        if (transform.eulerAngles.z > 180)
        {
            transform.localScale = flippedScale;
        }
        else if (transform.eulerAngles.z <= 180)
        {
            transform.localScale = baseScale;
        }
    }
}
