Shader "Unlit/AttackRange"
{
   Properties {
		_Color("Color",COLOR)=(1,1,1,1)
			_MainTex ("Base Layer (RGB)", 2D) = "white" {}
		_ScrollX ("Base layer Scroll Speed", Float) = 1.0
		_Multiplier ("Layer Multiplier", Float) = 1	}
	SubShader {
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
 
        ZWrite on
 
        Blend SrcAlpha OneMinusSrcAlpha		
		Pass { 
			Tags { "LightMode"="ForwardBase" }
			
			CGPROGRAM
			
			#pragma vertex vert
 
            #pragma fragment frag
 
            #include "UnityCG.cginc"
 
            #include "UnityUI.cginc"			

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _ScrollX;
			float _Multiplier;
			fixed4 _Color;
			
			struct a2v {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};
			
			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};
			
			v2f vert (a2v v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				float2 dir=float2(_ScrollX,0.0)+float2(0.0,0.0);
				o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex) + frac(dir * _Time.y);
				
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target {

				//得到纹理贴图的纹理
               fixed4 mainColor = tex2D(_MainTex, i.uv);
				fixed realpha = 1-mainColor.a;
				//得到纹理贴图的颜色
               fixed4 clothcolor = fixed4( mainColor.rgb * _Color, realpha); 
				

				
				return (_Color*clothcolor*_Multiplier);
			}
			
			ENDCG
		}
	}
//	FallBack "VertexLit"
}
