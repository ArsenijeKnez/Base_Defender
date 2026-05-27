Shader "Custom/RevealShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MinX ("Min X", Float) = 0.0
        _MaxX ("Max X", Float) = 1.0
        _Color ("Color", Color) = (1,1,1,1)
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
            float4 _MainTex_ST;
            float _MinX;
            float _MaxX;
            float4 _Color; // Declare color variable

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                float x = i.worldPos.x;
                half4 col = tex2D(_MainTex, i.uv);

                if (col.a == 0.0 || x < _MinX || x > _MaxX)
                    discard;

                return col * _Color; // Multiply color
            }
            ENDCG
        }
    }
}
