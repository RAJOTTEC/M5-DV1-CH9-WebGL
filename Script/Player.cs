using UnityEngine;

public class Player : MonoBehaviour, IActorTemplate
{
	int travelSpeed;
	int health;
	int hitPower;
	GameObject actor;
	GameObject fire;
	GameObject _Player;

	public int Health
	{
		get { return health; }
		set { health = value; }
	}

	public GameObject Fire
	{
		get { return fire; }
		set { fire = value; }
	}

	float width;
	float height;

	void Start()
	{
		height = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0)).y - .5f);
		width = 1 / (Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0)).x - .5f);

		_Player = GameObject.Find("_Player");
	}

	void Update()
	{
		Movement();
		Attack();
	}

	public void ActorStats(SOActorModel actorModel)
	{
		health = actorModel.health;
		travelSpeed = actorModel.speed;
		hitPower = actorModel.hitPower;
		fire = actorModel.actorsBullets;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			if (health >= 1)
			{
				if (transform.Find("energy +1(Clone)"))
				{
					Destroy(transform.Find("energy +1(Clone)").gameObject);
					health -= other.GetComponent<IActorTemplate>().SendDamage();
				}
				else
				{
					health -= 1;
				}
			}

			if (health <= 0)
			{
				Die();
			}
		}
	}

	public void TakeDamage(int incomingDamage)
	{
		health -= incomingDamage;
	}

	public int SendDamage()
	{
		return hitPower;
	}

	void Movement()
	{
		if (Input.GetAxisRaw("Horizontal") > 0)
		{
			if (transform.localPosition.x < width + width / 0.9f)
			{
				transform.localPosition += new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * travelSpeed, 0, 0);
			}
		}

		if (Input.GetAxisRaw("Horizontal") < 0)
		{
			if (transform.localPosition.x > width + width / 6)
			{
				transform.localPosition += new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * travelSpeed, 0, 0);
			}
		}

		if (Input.GetAxisRaw("Vertical") < 0)
		{
			if (transform.localPosition.y > -height / 3f)
			{
				transform.localPosition += new Vector3(0, Input.GetAxisRaw("Vertical") * Time.deltaTime * travelSpeed, 0);
			}
		}

		if (Input.GetAxisRaw("Vertical") > 0)
		{
			if (transform.localPosition.y < height / 2.5f)
			{
				transform.localPosition += new Vector3(0, Input.GetAxisRaw("Vertical") * Time.deltaTime * travelSpeed, 0);
			}
		}
	}

	public void Die()
	{
		GameManager.Instance.LifeLost();
		Destroy(this.gameObject);
	}

	public void Attack()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			GameObject bullet = GameObject.Instantiate(fire, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
			bullet.transform.SetParent(_Player.transform);
			bullet.transform.localScale = new Vector3(7, 7, 7);
		}
	}
}