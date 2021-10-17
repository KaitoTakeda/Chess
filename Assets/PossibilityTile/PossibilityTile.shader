Shader "Unlit/PossibilityTile"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color",Color) = (1,1,1,1)
        _ColorAlpha("ColorAlpha",Range(0,1.0)) = 1
        _ColorZero("ColorZero",Color) = (0,0,0,0)
        _Value("Value",Range(0,1.0))=0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" 
                "RenderType" = "Transparent"
                }
        Blend SrcAlpha OneMinusSrcAlpha 
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

            fixed4 _Color;
            float _ColorAlpha;
            fixed4 _ColorZero;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed _Cutoff;
            float _Value;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float box(float2 st, float size)
            {
                size = 0.5 + size * 0.5;
                st = step(st, size) * step(1.0 - st, size);
                return st.x * st.y;
            }

            float wave(float2 st, float n)
            {
                st = (floor(st * n) + 0.5) / n;
                float d = distance(0.5, st);
                //return (1 + sin(d * 3 - _Time.y * 3)) * 0.5;
                return 1-d*(1-sin(_Time.y*2)*0.9)*5;
            }

            float box_wave(float2 uv, float n)
            {
                float2 st = frac(uv * n);
                float size = wave(uv, n);
                return box(st, size);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                /*
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
                */

                
                //return box_wave(i.uv, 10);

                if(box_wave(i.uv, 8) >= 1)
                {
                    _Color.a = _ColorAlpha;
                    fixed4 col = tex2D(_MainTex, i.uv) * _Color;
					//clip(col.a - _Cutoff);
                    return col;
                }
                else
                {
                    return _ColorZero;
                }
            }
            ENDCG
        }
    }
}
