
Shader "Custom/UIStarTwinkle"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TwinkleSpeed ("Twinkle Speed", Range(0.1, 5)) = 1
        _TwinkleIntensity ("Twinkle Intensity", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane"}
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

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
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _TwinkleSpeed;
            float _TwinkleIntensity;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                // Simple pseudo-random noise based on UV
                float noise = frac(sin(dot(i.uv , float2(12.9898,78.233))) * 43758.5453);

                // Twinkle using time and noise
                float flicker = sin(_Time.y * _TwinkleSpeed + noise * 6.28);
                flicker = (flicker * 0.5 + 0.5); // Map from -1..1 to 0..1
                flicker = lerp(1.0 - _TwinkleIntensity, 1.0, flicker);

                // Apply flicker to alpha
                col.a *= flicker;

                return col;
            }
            ENDCG
        }
    }
}
