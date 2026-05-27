Shader "Custom/MultiRangeRevealShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _ArrayLength ("Array Length", Float) = 0.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _Color;
            float _ArrayLength;
            float _MinXArray[1000]; 
            float _MaxXArray[1000];

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

          half4 frag (v2f i) : SV_Target
            {
                float x = i.worldPos.x;
                half4 col = tex2D(_MainTex, i.uv);

                for (int j = 0; j < _ArrayLength; j++)
                {
                    if (x > _MinXArray[j] && x < _MaxXArray[j])
                    {
                        discard;
                    }
                }

                return col * _Color;
            } 
            ENDCG
        }
    }
}
