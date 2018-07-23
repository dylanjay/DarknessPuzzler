Shader "Unlit/ForceColor"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _AlphaThreshhold("Alpha Threshhold", float) = 1
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
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
            #pragma multi_compile _ PIXELSNAP_ON
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
			float4 _MainTex_ST;
			fixed3 _Color;
            fixed _AlphaThreshhold;
            float _RevealAmount;
			
			v2f vert (appdata v)
			{
				v2f o;
                #ifdef PIXELSNAP_ON
				o.vertex = UnityPixelSnap(UnityObjectToClipPos(v.vertex));
				#else
				o.vertex = UnityObjectToClipPos(v.vertex);
                #endif
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				float alpha = tex2D(_MainTex, i.uv * _MainTex_ST.xy + _MainTex_ST.wz).a;

                clip(alpha - _AlphaThreshhold);
                return fixed4(_Color, 1.0);
			}
			ENDCG
		}
	}
}
