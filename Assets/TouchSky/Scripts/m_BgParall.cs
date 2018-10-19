using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(m_Bg))]
public class m_BgParall : MonoBehaviour
{
	public Transform target;
	public Vector2 factor;

	m_Bg spriteCicle;

	void Awake()
	{
		spriteCicle = GetComponent<m_Bg>();
	}

	void Start()
	{
		if(!target)
		{
			if(Camera.main)
			{
				target = Camera.main.transform;
			}
		}
	}

	void Update()
	{
		if(target && spriteCicle)
		{
			spriteCicle.position = target.position.y*factor.y;

			Vector3 localPosition = transform.localPosition;
			localPosition.x = target.position.x*factor.x;
			transform.localPosition = localPosition;
		}
	}
}
