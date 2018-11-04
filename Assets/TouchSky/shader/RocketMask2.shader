// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Mesh"
{
	Properties
	{
		_MainTex ("MainTex", 2D) = "white" {}
		_MeshTex ("MeshTex", 2D) = "white" {}

		_ThresholdY ("ThresholdY", Range(0, 10)) = 10//调试出来的范围
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "true" "RenderType"="Transparent" }
 
		Pass
		{
			ZWrite off
			Blend SrcAlpha OneMinusSrcAlpha
 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
 
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};
 
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};
 
			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _MeshTex;
			half _ThresholdY;
 
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
 
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				//最好选择世界空间坐标系下的顶点而非模型空间下的顶点
				//因为后者会因为模型制作的规范不同而不同
				half y = -i.vertex.y;
				if(y < _ThresholdY * 50)//50是调试出来的值
				{
					fixed4 col = tex2D(_MainTex, i.uv);
					return col;
				}
				else
				{
					return fixed4(0, 0, 0, 0);
				}
			}
			ENDCG
		}
	}
}

