Shader "Unlit/CoinSwoosh_S"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
				_Color("Color", Color) = (1,1,1,1)
				_ScrollRate ("Scroll Rate", Range(0,100)) = 1.0
				_UOffset("UOffset", Range(-1,1)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100


        Pass
        {
						Blend One One
						ZWrite Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
          //  #pragma multi_compile_fog

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
						float _ScrollRate;
						float4 _Color;
						float _UOffset;

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
								float deltaX = _ScrollRate * _Time.y + i.uv.x;

             //   fixed4 col = tex2D(_MainTex, float2(deltaX, i.uv.y)) * _Color;
						fixed4 col = tex2D(_MainTex, i.uv.xy + float2(_UOffset, 0)) * _Color;
								col.w = 0;
                // apply fog
            //    UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
