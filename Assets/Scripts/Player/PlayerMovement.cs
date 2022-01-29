using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float yPosition;
    [SerializeField] private Player _player;
    public static float speed;
    private float _NormalSpeed;
    private CharacterController _charController;

    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioSource stepAudio;
    [SerializeField] private AudioSource runAudio;

    private bool _wasWalk = false;
    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
        soundSource = stepAudio;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            soundSource.Stop();
            soundSource = runAudio;
            _NormalSpeed = speed;
            if(_wasWalk)
            {
                soundSource.Play();
            }
            speed = _NormalSpeed * 1.5f;

        }

        if (Input.GetKeyUp(KeyCode.RightShift) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            soundSource.Stop();
            soundSource = stepAudio;
            if (_wasWalk)
            {
                soundSource.Play();
            }
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

            if (_wasWalk)
            {
                if (deltaX == 0 && deltaZ == 0)
                {
                    soundSource.Stop();
                    _wasWalk = false;
                }

            }
            else
            {
                if (deltaX != 0 || deltaZ != 0)
                {
                    soundSource.Play();
                    _wasWalk = true;
                }
            }

            _charController.Move(movement);
            if(System.Math.Abs(transform.position.y - yPosition) > 0.005f)
            {
                _charController.Move(new Vector3(0, -(transform.position.y-yPosition), 0));
            }
        }
        else
        {
            if(_wasWalk)
            {
                soundSource.Stop();
                _wasWalk = false;
            }
        }
        
    }
}
