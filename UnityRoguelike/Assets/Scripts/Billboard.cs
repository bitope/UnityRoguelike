using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Update()
    {
        var lrv = Vector3.Scale((transform.position - Camera.main.transform.position), new Vector3(1, 0, 1));
        if (lrv!=Vector3.zero)
            transform.rotation = Quaternion.LookRotation(lrv);
    }
}