using UnityEngine;

class MapGestures : MonoBehaviour
{
    public Camera Camera;
    private void Awake()
    {
        if (Camera == null)
            Camera = Camera.main;
    }


    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.deltaPosition;
                Camera.transform.Translate(-delta.x * 0.005f, -delta.y * 0.005f, 0);

                float clampedX = Mathf.Clamp(Camera.transform.position.x, -5f, 7f);
                float clampedY = Mathf.Clamp(Camera.transform.position.y, -6, 8);

            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 prev0 = touch0.position - touch0.deltaPosition;
            Vector2 prev1 = touch1.position - touch1.deltaPosition;

            float prevMag = (prev0 - prev1).magnitude;
            float currentMag = (touch0.position - touch1.position).magnitude;

            float diff = currentMag - prevMag;

            Camera.orthographicSize -= diff * 0.002f;
            Camera.orthographicSize = Mathf.Clamp(Camera.orthographicSize, 1.5f, 7.5f);

            Debug.Log("zoom level in a weird ass unit " + Camera.orthographicSize);

        }
    }
}