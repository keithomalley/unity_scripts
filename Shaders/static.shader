﻿//ddTVStatic shader: Daniel DeEntremont (dandeentremont@gmail.com)
//Applies a television static noise in screen space
//Options for noise colors, resolution, and scaling to camera

    Shader "ddShaders/ddTVStatic"
    {

        Properties
        {
            _ColorA ("Color A", Color) = (1,1,1,1)
                _ColorB ("Color B", Color) = (0,0,0,0)

                    _ResX ("X Resolution", Float) = 100
                    _ResY ("Y Resolution", Float) = 200

                    _ScaleWithZoom("Scale With Cam Distance", Range(0,1)) = 1.0

        }

        SubShader
        {
            Pass
            {
                CGPROGRAM

                    #pragma vertex vert
                    #pragma fragment frag
                    #pragma target 3.0

                    #include "UnityCG.cginc"

                    //This produces random values between 0 and 1
                    float rand(float2 co)
                {
                    return frac((sin( dot(co.xy , float2(12.345 * _Time.w, 67.890 * _Time.w) )) * 12345.67890+_Time.w));
                }

                fixed4 _ColorA;
                fixed4 _ColorB;

                float _ResX;
                float _ResY;
                float _ScaleWithZoom;

                struct vertexInput
                {
                    float4 vertex : POSITION;
                    float4 texcoord0 : TEXCOORD0;
                    float4 texcoord2 : TEXCOORD2;
                };

                struct fragmentInput
                {
                    float4 position : SV_POSITION;
                    float4 texcoord0 : TEXCOORD0;
                    float4 camDist : TEXCOORD2;
                };

                fragmentInput vert(vertexInput i)
                {
                    fragmentInput o;
                    o.position = mul (UNITY_MATRIX_MVP, i.vertex);
                    o.texcoord0 = i.texcoord0;

                    //get the model's origin, so we can calculate the distance to camera (and scale the noise accordingly)
                    float4 modelOrigin = mul(_Object2World, float4(0.0, 0.0, 0.0, 1.0));

                    o.camDist.x = distance(_WorldSpaceCameraPos.xyz, modelOrigin.xyz);

                    o.camDist.x = lerp(1.0, o.camDist.x, _ScaleWithZoom);

                    return o;

                }


                fixed4 frag(float4 screenPos : SV_POSITION, fragmentInput i) : SV_Target
                {

                    fixed4 sc = fixed4((screenPos.xy), 0.0, 1.0);
                    sc *= 0.001;

                    sc.xy -= 0.5;
                    sc.xy *= i.camDist.xx;
                    sc.xy += 0.5;

                    //round the screen coordinates to give it a blocky appearance
                    sc.x = round(sc.x*_ResX)/_ResX;
                    sc.y = round(sc.y*_ResY)/_ResY;



                    float noise = rand(sc.xy);

                    float4 stat = lerp(_ColorA, _ColorB, noise.x);


                    return fixed4(stat.xyz, 1.0);
                }

                ENDCG
            }
        }
    }