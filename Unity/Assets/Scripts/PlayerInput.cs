using UnityEngine;
using System.Collections;
using Prime31;


public class PlayerInput : MonoBehaviour
{
    public GameObject PrefDash;

    // movement config
    public float gravity = -25f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
    public float DashTimeBoostDuration = 1f;
    public float JumpTimeBoostDuration = 1f;

    [HideInInspector]
	private float normalizedHorizontalSpeed = 0;

	private CharacterController2D _controller;
	private Animator _animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;

    private Player player;

    public bool hasActed;

    TimeController tc;

    void Awake()
	{
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();

		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;

        player = GetComponent<Player>();
        tc = GameObject.Find("GameController").GetComponent<TimeController>(); // sorry 
    }


	#region Event Listeners

	void onControllerCollider( RaycastHit2D hit )
	{
		// bail out on plain old ground hits cause they arent very interesting
		if( hit.normal.y == 1f )
			return;

		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}


	void onTriggerEnterEvent( Collider2D col )
	{
		Debug.Log( "onTriggerEnterEvent: " + col.gameObject.name );
	}


	void onTriggerExitEvent( Collider2D col )
	{
		Debug.Log( "onTriggerExitEvent: " + col.gameObject.name );
	}

    #endregion


    // the Update loop contains a very simple example of moving the character around and controlling the animation
    void Update() {
        bool anyRightKey = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool anyLeftKey = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool anyUpKey = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        bool anyDownKey = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);

        bool anyUpKeyHeld = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
        bool anyDownKeyHeld = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);

        if (_controller.isGrounded)
            _velocity.y = 0;

        if (anyRightKey) {
            normalizedHorizontalSpeed = 1;
            if (transform.localScale.x < 0f)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

            if (_controller.isGrounded)
                _animator.Play(Animator.StringToHash("Run"));
        }
        else if (anyLeftKey) {
            normalizedHorizontalSpeed = -1;
            if (transform.localScale.x > 0f)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

            if (_controller.isGrounded)
                _animator.Play(Animator.StringToHash("Run"));
        }
        else {
            normalizedHorizontalSpeed = 0;

            if (_controller.isGrounded)
                _animator.Play(Animator.StringToHash("Idle"));
        }

        // we can only stomp while flying and not on cooldown
        if (!_controller.isGrounded && anyDownKey && player.canStomp) {
            _velocity.y = -1f * Mathf.Sqrt(2f * player.jumpHeight * -gravity);
            _animator.Play(Animator.StringToHash("Jump"));
            player.canStomp = false;
            player.ResetStomp();
        }

        // we can only jump whilst grounded
        if (_controller.isGrounded && anyUpKey && !anyDownKeyHeld) {
            _velocity.y = Mathf.Sqrt(2f * player.jumpHeight * -gravity);
            _animator.Play(Animator.StringToHash("Jump"));
            tc.TimeBoost(JumpTimeBoostDuration);
        }

        // apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
        var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
        _velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * player.speedMoveGround, Time.deltaTime * smoothedMovementFactor);

        // apply gravity before moving
        _velocity.y += gravity * Time.deltaTime;

        // if holding down bump up our movement amount and turn off one way platform detection for a frame.
        // this lets us jump down through one way platforms
        if (_controller.isGrounded && Input.GetKey(KeyCode.DownArrow)) {
            _velocity.y *= 3f;
            _controller.ignoreOneWayPlatformsThisFrame = true;
        }

        _controller.move(_velocity * Time.deltaTime);

        // grab our current _velocity to use as a base for all calculations
        _velocity = _controller.velocity;

        // We have acted if we're holding down any movement key- this also sets it to false
        hasActed = (anyDownKey && !_controller.isGrounded) || anyLeftKey || anyRightKey;

        // dash overrides everything
        // TODO: check line of sight etc.

        if (Input.GetKey(KeyCode.LeftShift)  && player.canDash) {

            if (anyRightKey || anyUpKey || anyDownKey || anyLeftKey) {
                player.canDash = false;
                player.ResetDash();
            }

            if (anyRightKey && anyUpKey) 
            {
                Vector3 vn = transform.position + new Vector3(1f, 1f, 0f).normalized * player.distanceDash;
                spawnDashTail(transform.position, vn);
                transform.position = vn;
            } else if (anyRightKey && anyDownKey) 
            {
                Vector3 vn = transform.position + new Vector3(1f, -1f, 0f).normalized * player.distanceDash;
                spawnDashTail(transform.position, vn);
                transform.position = vn;
            }
            else if (anyLeftKey && anyUpKey) 
            {
                Vector3 vn = transform.position + new Vector3(-1f, 1f, 0f).normalized * player.distanceDash;
                spawnDashTail(transform.position, vn);
                transform.position = vn;
            }
            else if (anyLeftKey && anyDownKey) 
            {
                Vector3 vn = gameObject.transform.position + new Vector3(-1f, -1f, 0f).normalized * player.distanceDash;
                spawnDashTail(transform.position, vn);
                transform.position = vn;
            }
            else if (anyLeftKey) 
            {
                Vector3 vn = gameObject.transform.position + new Vector3(-1f, 0f, 0f).normalized * player.distanceDash;
                spawnDashTail(transform.position, vn);
                transform.position = vn;
            } 
            else if (anyRightKey) 
            {
                Vector3 vn = gameObject.transform.position + new Vector3(1f, 0f, 0f).normalized * player.distanceDash;
                spawnDashTail(transform.position, vn);
                transform.position = vn;
            } 
            else if (anyUpKey) 
            {
                Vector3 vn = gameObject.transform.position + new Vector3(0f, 10f, 0f).normalized * player.distanceDash;
                spawnDashTail(transform.position, vn);
                transform.position = vn;
            } 
            else if (anyDownKey) 
            {
                Vector3 vn = gameObject.transform.position + new Vector3(0f, -1f, 0f).normalized * player.distanceDash;
                spawnDashTail(transform.position, vn);
                transform.position = vn;
            }
        } else if (Input.GetMouseButton(1) && player.canDash) {
            player.canDash = false;
            player.ResetDash();
            Vector3 charToMouse = player.reticule.transform.position - player.transform.position;
            Vector3 vn = charToMouse.normalized * player.distanceDash;
            spawnDashTail(transform.position, transform.position + vn);
            transform.position += vn;
            //hasActed = true;
            tc.TimeBoost(DashTimeBoostDuration);
        }

        if (ShouldFire()) {
            player.Fire(player.reticule.transform.position - player.transform.position);
            //hasActed = true;
        }
    }

    void spawnDashTail (Vector3 start, Vector3 end) {
        Vector3 startToEnd = end - start;
        float length = Vector3.Magnitude(startToEnd);
        int l = Mathf.FloorToInt(length);

        //if (start.y < end.y) l--; // when the player dashes upwards make the tail a bit shorter so gravity doesnt pull you into it instantly
        l--; // tail should always be shorter

        for (int i = 0; i < l; i++) {
            Vector3 pos = start + startToEnd.normalized * i;
            GameObject pfx = GameObject.Instantiate(PrefDash, pos, Quaternion.identity);
            SpriteRenderer rend = pfx.GetComponentInChildren<SpriteRenderer>();
            if (player.transform.localScale.x < 0) rend.flipX = true;
        }
    }

    bool ShouldFire() {
        var weapon = player.GetWeapon();

        if (weapon == null)
            return false;

        switch (weapon.fireMode) {
            case Weapon.FireMode.Semi:
                return Input.GetMouseButtonDown(0);
            case Weapon.FireMode.Auto:
                return Input.GetMouseButton(0);
            case Weapon.FireMode.Charge:
                return Input.GetMouseButtonUp(0);
            default:
                return false;
        }
    }
}