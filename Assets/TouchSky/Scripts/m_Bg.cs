using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class m_Bg : MonoBehaviour
{
	public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

	[Range(0,1)]
	public float offset = 0f;

	float totalWidth = 1f;

	float mPosition = 0f;
	public float position
	{
		get{
			return mPosition;
		}
		set {

			float scaleY = transform.localScale.y;

			mPosition = value;

			if(scaleY > 0f)
			{
				mPosition /= scaleY;
			}

			Vector3 l_position = Vector3.zero;

			totalWidth = 0f;

			for(int i = 0; i < spriteRenderers.Count; ++i)
			{
				SpriteRenderer sr = spriteRenderers[i];

				if(sr)
				{
					if(sr.sprite)
					{
						sr.transform.localPosition = l_position;
						l_position.y += sr.sprite.bounds.size.y;
						totalWidth += sr.sprite.bounds.size.y;
					}
				}
			}

			float dx = mPosition % totalWidth;

			for(int i = 0; i < spriteRenderers.Count; ++i)
			{
				SpriteRenderer sr = spriteRenderers[i];

				if(sr)
				{
					if(sr.sprite)
					{
						Vector3 localPos = sr.transform.localPosition + Vector3.up*dx;

						if(localPos.y <= -sr.sprite.bounds.size.y)
						{
							localPos.y += totalWidth;

						}else if(localPos.y > totalWidth)
						{
							localPos.y -= totalWidth;
						}

						localPos.y -= offset*totalWidth;

						sr.transform.localPosition = localPos;
					}
				}
			}
		}
	}

	void Awake()
	{
		position = 0f;
	}

	void OnValidate()
	{
		position = 0f;
	}
}
