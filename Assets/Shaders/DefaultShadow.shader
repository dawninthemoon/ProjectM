Shader "Sprites/Custom/DefaultShadow"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
        _ShadowOffset("Shadow Offset", Vector) = (0.02,-0.02,0,0)
        _ShadowColor("Shadow Color", Color) = (0, 0, 0, 0.5)
    }
 
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }
 
        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha
 
        Pass
        {
        CGPROGRAM
            #pragma vertex SpriteVertShadow
            #pragma fragment SpriteFragShadow
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnitySprites.cginc"
 
            fixed4 _ShadowColor;
            fixed4 _ShadowOffset;
 
            v2f SpriteVertShadow(appdata_t IN)
            {
                v2f OUT;
 
                UNITY_SETUP_INSTANCE_ID (IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
 
                #ifdef UNITY_INSTANCING_ENABLED
                    IN.vertex.xy *= _Flip;
                #endif
 
                OUT.vertex = UnityObjectToClipPos(IN.vertex + _ShadowOffset);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color * _RendererColor;
 
                #ifdef PIXELSNAP_ON
                    OUT.vertex = UnityPixelSnap (OUT.vertex);
                #endif
 
                return OUT;
            }
 
            fixed4 SpriteFragShadow(v2f IN) : SV_Target
            {
                fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
                if (c.a < 0.99) discard;
                c.rgb = _ShadowColor;
                c.a = _ShadowColor.a;
                return c;
            }
 
        ENDCG
        }
 
        Pass
        {
            CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment SpriteFrag
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnitySprites.cginc"
            ENDCG
        }
    }
}