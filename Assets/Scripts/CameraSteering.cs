using UnityEngine;
using Vector2 = UnityEngine.Vector2;


public class CameraSteering : MonoBehaviour
{
    public Vector2 levelSize;
    public float cameraSize;
    // Camera is always centered to start, and above values are always positive.
    
    [HideInInspector]
    public Camera riggedCamera;
    [HideInInspector]
    public SpriteRenderer startArea;
    [HideInInspector]
    public SpriteRenderer levelArea;
    [HideInInspector]
    public SpriteRenderer oob;

    void OnValidate()
    {
        riggedCamera.orthographicSize = cameraSize;
        startArea.size = new Vector2(cameraSize * riggedCamera.aspect * 2, cameraSize * 2);
        levelArea.size = levelSize;
        oob.size = levelSize + new Vector2(1, 1);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
