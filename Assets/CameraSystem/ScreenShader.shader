Shader "Custom/ScreenShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _OverlayColor ("OverlayColor", Color) = (1,1,1,1)
        _BaseTex ("Base", 2D) = "black" {}
        _OverlayTex ("Overlay", 2D) = "black" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _NoiseScale ("NoiseScale", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _BaseTex;
        sampler2D _OverlayTex;

        struct Input
        {
            float2 uv_BaseTex;
            float2 uv_OverlayTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _OverlayColor;
        fixed4 _Color;
        float _NoiseScale;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        float rand(float2 co) {
            return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
        }

        fixed4 makeNoise(float2 uv)
        {
            float col = floor(uv.x * 200.0f) / 200.0f;
            float row = floor(uv.y * 200.0f);
            float n = rand(float2(col + row, _Time.x));
            return fixed4(n, n, n, 1.0f);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 theNoise = makeNoise(IN.uv_BaseTex);
            fixed4 base = tex2D(_BaseTex, IN.uv_BaseTex) * _Color;
            fixed4 baseWithNoise = lerp(base, theNoise, theNoise.a * _NoiseScale);
            fixed4 overlay = tex2D(_OverlayTex, IN.uv_OverlayTex);
            fixed4 c = lerp(baseWithNoise, _OverlayColor, overlay.r);

            o.Emission = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
