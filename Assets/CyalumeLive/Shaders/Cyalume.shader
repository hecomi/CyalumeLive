Shader "Custom/Cyalume" {
    Properties {
        _BaseColor ("Base Color", Color) = (0.0, 1.0, 0.0)
        _WaveFactorX("Wave Factor X", Range(0.0, 2.0)) = 0.0
        _WaveFactorZ("Wave Factor Z", Range(0.0, 2.0)) = 0.0
        _WaveCorrection("Wave Correction", float) = 0.3
        _Pitch("Wave Pitch", float) = 1.0
        _Delay("Delay by Distance", float) = 0.02
        _Bend("Bend", float) = 0.3
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "Queue"           = "Transparent"
            "RenderType"      = "Transparent"
        }
        Blend SrcAlpha One
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define PI 3.14159

            #include "UnityCG.cginc"

            uniform float _WaveFactorX;
            uniform float _WaveFactorZ;
            uniform float _WaveCorrection;
            uniform float _Pitch;
            uniform float _Delay;
            uniform float _Bend;
            uniform float _WaveFactorY;
            uniform float4 _BaseColor;
            uniform sampler2D _MainTex;

            struct v2f {
                float4 position : SV_POSITION;
                fixed4 color    : COLOR;
                float2 uv       : TEXCOORD0;
            };

            v2f vert(appdata_full v) {
                float wave   = 2 * PI * _Time.x * 1000 / 60 / _Pitch;
                float delay  = _Delay * v.vertex.z;
                float bendX  = _Bend * v.texcoord.x;
                float angleX = wave + delay + bendX;
                float bendY  = _Bend * v.texcoord.y;
                float angleY = wave + delay + bendY;
                float bendZ  = _Bend * v.texcoord.z;
                float angleZ = wave + delay + bendZ;
                float lean   = sin((v.texcoord.y + 0.5) * PI) - 1.0;

                v.vertex.x += _WaveFactorX * sin(angleX) * lean;
                v.vertex.y += (_WaveFactorX + _WaveFactorZ) * _WaveCorrection * (1.0 - pow(cos(angleX), 2.0)) * lean;
                v.vertex.z += _WaveFactorZ * sin(angleZ) * lean;

                // Parameters given to fragment shader
                v2f o;
                o.position = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv       = v.texcoord;
                o.color    = v.color;
                return o;
            }

            fixed4 frag(v2f i) : COLOR {
                fixed4 tex = tex2D(_MainTex, i.uv);
                tex.rgb *= _BaseColor * i.color.rgb;
                tex.a   *= i.color.a;
                return tex;
            }
            ENDCG
        }
    }
    Fallback "VertexLit"
}
