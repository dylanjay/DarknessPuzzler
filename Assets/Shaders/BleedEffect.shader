Shader "Unlit/BleedEffect"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
        _RevealTex("Reveal Texture", 2D) = "white" {}
        _MainColor("Default Color", Color) = (0,0,0,0)
        _RevealColor("Reveal Color", Color) = (1,1,1,1)
        _RevealAmount("Reveal Amount", Range(0, 1)) = 0
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
			sampler2D _RevealTex;
			float4 _RevealTex_ST;
			float4 _MainTex_ST;
			fixed4 _MainColor;
			fixed4 _RevealColor;
            float _RevealAmount;
			
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
                fixed3 rgb = _MainColor;

                fixed4 reveal = tex2D(_RevealTex, i.uv * _RevealTex_ST.xy + _RevealTex_ST.zw);
                if (reveal.r < 1 - _RevealAmount)
                {
                    return fixed4(_MainColor.rgb * alpha, alpha);
                }
                return fixed4(_RevealColor.rgb * alpha, alpha);
			}
			ENDCG
		}
	}
}
