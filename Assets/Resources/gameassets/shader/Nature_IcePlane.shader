Shader "Nature/IcePlane" {
	Properties {
		_Color ("MainColor", Vector) = (1,1,1,1)
		_horizonColor ("Horizon color", Vector) = (0.172,0.463,0.435,0)
		_MainTex ("BaseTexture", 2D) = "white" {}
		_ReflectionTex ("Internal Reflection", 2D) = "" {}
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Transparent"
}