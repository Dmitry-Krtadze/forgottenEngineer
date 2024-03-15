Shader "Custom/GravityBeam"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Width ("Width", Range(0.01, 0.1)) = 0.05
        _Power ("Power", Range(0.1, 10)) = 1.0
    }
    SubShader
    {
        Tags { "Queue" = "Overlay" }
        Blend SrcAlpha One
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            float _Width;
            float _Power;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                float2 uv = i.pos.xy / i.pos.w;
                float d = length(uv);

                float g = 1.0 / (d * _Power);
                float t = saturate(1.0 - g);

                return fixed4(0, 0, 1, t); // Синий цвет
            }
            ENDCG
        }
    }
}
