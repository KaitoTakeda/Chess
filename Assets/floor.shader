Shader "Unlit/floor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SquareNum("SquareNum",Int) = 8
        _Range("Range", Range(0.0, 1.0)) = 0.0
        _TimeValue("_TimeValue",Float) = 0.0
        _ColBlack("ColBlack",Color) = (0.1,0.1,0.1,1)
        _ColWhite("ColWhite",Color) = (0.9,0.9,0.9,1)
        _ColPlusPer("_ColPlusPer",Float) = 20
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile FOG_EXP FOG_EXP2 FOG_LINEAR

            int _SquareNum;
            float _Range;
            float _TimeValue;
            fixed4 _ColBlack;
            fixed4 _ColWhite;
            float _ColPlusPer;

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                UNITY_FOG_COORDS(1)
            };

            float2 random2(float2 st)
            {
                st = float2(dot(st, float2(127.1, 311.7)),
                            dot(st, float2(269.5, 183.3)));
                return -1.0 + 2.0 * frac(sin(st) * 43758.5453123);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                float2 st = i.uv;
                st *= _SquareNum; //格子状の１辺のマス目数

                float2 ist = floor(st);//整数
                float2 fst = frac(st);//小数点以下

                float d = 5;
                float2 p_Min;

                fixed4 col;

                
                
                
                //ボロノイ図
                for (int y = -1; y <= 1; y++)
                for (int x = -1; x <= 1; x++)
                {
                    float2 neighbor = float2(x, y);
                    
                    float2 p = 0.5 + 0.5 * sin((_Time.y*_TimeValue)  + 6.2831 * random2(ist + neighbor))*_Range/2;

                    float2 diff = neighbor + p - fst;
                    float dist = length(diff);

                    if(d > dist){
                        d = dist;
                        p_Min = p;
                        float2 Num = ist + neighbor;
                        if((Num.x+Num.y)%2 == 0)
                        {
                            col = _ColBlack + p.x/_ColPlusPer;
                        }
                        else
                        {
                            col = _ColWhite + p.x/_ColPlusPer;
                        }
                    }

                }
                //p_Min = (p_Min -0.5)*2;
                
                //fixed4 Col = col;
                UNITY_APPLY_FOG(i.fogCoord, col);

                if(d >= 0)
                {
                    
                    return col;
                }
                else
                {
                    return 0;
                }
                

                /*
                if((ist.x+ist.y)%2 == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
                */
                
                //return fixed4(p_Min.x,p_Min.x,p_Min.x,1);
                //return fixed4(p_Min.x,p_Min.x,p_Min.x,1);
                
            }
            ENDCG
        }
    }
}
