using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float yPosition;
    [SerializeField] private Player _player;
    public static float speed;
    private float _NormalSpeed;
    private CharacterController _charController;

    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            _NormalSpeed = speed;
            speed = _NormalSpeed * 1.5f;

        }

        if (Input.GetKeyUp(KeyCode.RightShift) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = _NormalSpeed;
        }

        if (!_player.isHidden)
        {
            float deltaX = Input.GetAxis("Horizontal") * speed;
            float deltaZ = Input.GetAxis("Vertical") * speed;
            Vector3 movement = new Vector3(deltaX, 0, deltaZ);
            movement = Vector3.ClampMagnitude(movement, speed);
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            _charController.Move(movement);
            if(System.Math.Abs(transform.position.y - yPosition) > 0.005f)
            {
                _charController.Move(new Vector3(0, -(transform.position.y-yPosition), 0));
            }
        }
        
    }
}
