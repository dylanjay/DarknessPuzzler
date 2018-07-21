Shader "Unlit/BleedEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _BleedTex("Bleed Texture", 2D) = "white" {}
        _MainColor ("Default Color", Color) = (0,0,0,0)
        _RevealColor ("Reveal Color", Color) = (1,1,1,1)
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

        Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _BleedTex;
			float4 _MainTex_ST;
			fixed4 _MainColor;
			fixed4 _RevealColor;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				float alpha = tex2D(_MainTex, i.uv).a;
                float bleedAlpha = tex2D(_BleedTex, i.uv).a;// .5 + sin(_Time.r * 6.28*60) / 2.0;//
                fixed3 rgb = _MainColor * (1 - bleedAlpha) + _RevealColor * bleedAlpha;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
                fixed4 col = fixed4(rgb, alpha);
                col.rgb *= col.a;
				return col;
			}
			ENDCG
		}
	}
}
