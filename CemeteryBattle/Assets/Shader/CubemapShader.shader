Shader "NathaliaShader/PropertyShader" {
	Properties {
		_myColor ("Color", Color) = (1,1,1,1)
        _myRange("Smoothness", Range(0,5)) = 1
		_myTex ("Albedo (RGB)", 2D) = "white" {}		
        _myCube("Cube", CUBE) = "" {}
        _myFloat ("Floating point", Float)= 0.5
        _myVector3 ("Vector 3 ", Vector)=(1,1,1,1)

	}
	SubShader {
	
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input { //the Input, is needed to calculate de surface and uv_myTex is a "variable" in the Struct Input
			float2 uv_myTex; //this is the texture FROM THE MODEL
            float3 worldRefl;  //Use the colors of the CUBEMAP
		};

        fixed4 _myColor;
		half _myRange;
		sampler2D _myTex;
        samplerCUBE _myCube;
        float _myFloat;
        float4 _myVector;
	

        void surf(Input IN, inout SurfaceOutputStandard o) {
            o.Albedo = ((tex2D(_myTex, IN.uv_myTex)* _myRange )* _myColor).rgb; //Multipling the texture multiply a channel to 0 to 5 make the color darker or lighter. 
            o.Emission = texCUBE(_myCube, IN.worldRefl).rgb;
        } 
		ENDCG
	}
	FallBack "Diffuse"
}
