#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

// Our texture sampler
texture Texture;
sampler TextureSampler = sampler_state
{
	Texture = <Texture>;
};

// Uniforms
float time;
float2 res;
float4 col;

// This data comes from the sprite batch vertex shader
struct VertexShaderOutput
{
	float4 Position : POSITION0;
	float4 Color : COLOR0;
	float2 TextureCordinate : TEXCOORD0;
};

float2 reduxino(float2 pos, float redux) {

	pos.x = pos.x - pos.x%redux;
	pos.y = pos.y - pos.y%redux;

	return pos;
}


// Our pixel shader
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	return float4(1.0,0.5,0.2,0.05);
}

// Compile our shader
technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_4_0_level_9_1 PixelShaderFunction();
	}
}