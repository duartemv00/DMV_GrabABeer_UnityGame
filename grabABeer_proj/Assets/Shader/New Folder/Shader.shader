Shader "Unlit/Shader"
{
    SubShader
    {
        HLSINCLUDE
        #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

        TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
        float4 _MainTex_TexelSize;

        TEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);
        float4x4 unity_MatrixMVP;

        half _MinDepth;
        half _MaxDepth;
        half _Thickness;
        half4 _EdgeColor;

        struct v2f {
            float2 uv : TEXCOORD0:
            float4 vertex : SV_POSITION;
            float screen_pos : TEXTCOORD2;
        };

        inline float4 ComputeScreenPos(float4 pos) {
            float4 o = pos * 0.5f;
            o.xy = float2(o.x, o.y * _ProjectionParams.x) + o.w;
            o.zw = pos.zw;
            return o;
        }

        ENDHLSL

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            v2f Vert(AttributesDefault v) {
                v2f o;
                o.vertex = float4(v.vertex.xy, 0.0, 1.0);
                o.uv = TransformTriangleVertexToUV(v.vertex.xy);
                o.screen_pos = ComputeScreenPos(o.vertex);
        #if UNITY_UV_STARTS_AT_TOP
                o.uv = o.uv * float2(1.0, -1.0) + float2(0.0, 1.0);
        #endif

                return o;
            }

            float4 Frag(v2f i) : SV_Target {
                float4 original = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                //For testing;
                float4 depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, i.uv);

                //Four sample UV points.
                float offset_positive = +ceil(_Thickness * 0.5);
                float offset_negative = -floor(_Thickness * 0.5);
                float left = 

                //https://www.youtube.com/watch?v=ehyMwVnnnTg min 4.42
            }

            ENDHLSL
        }
    }
}
