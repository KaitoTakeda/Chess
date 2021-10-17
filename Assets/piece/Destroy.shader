Shader "Unlit/Destroy"
{
        Properties {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _ScaleFactor ("Scale Factor", float) = 0.5
        _Range("Range", Range(0.0, 1.0)) = 0.0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #pragma multi_compile FOG_EXP FOG_EXP2 FOG_LINEAR

            #include "UnityCG.cginc"

            fixed4 _Color;
            float _ScaleFactor;
            float _Range;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct g2f {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };

            appdata vert (appdata v) {
                return v;
            }

            [maxvertexcount(3)]
            void geom (triangle appdata input[3], inout TriangleStream<g2f> stream) {
                float3 vec1 = input[1].vertex - input[0].vertex;
                float3 vec2 = input[2].vertex - input[0].vertex;
                float3 normal = normalize(cross(vec1, vec2));
                [unroll]
                for (int i = 0; i < 3; i++) {
                    appdata v = input[i];
                    g2f o;
                    //v.vertex.xyz += normal * (_SinTime.w * 0.5 + 0.5) * _ScaleFactor;
                    v.vertex.xyz += normal *_Range * _ScaleFactor;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    UNITY_TRANSFER_FOG(o, o.vertex);
                    stream.Append(o);
                }
                stream.RestartStrip();
            }

            fixed4 frag (g2f i) : SV_Target {
                fixed4 col = _Color;
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
    FallBack "Unlit/Color"
}