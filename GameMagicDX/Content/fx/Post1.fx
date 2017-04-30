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
//float time;
//float2 res;

// This data comes from the sprite batch vertex shader
struct VertexShaderOutput
{
	float4 Position : POSITION0;
	float4 Color : COLOR0;
	float2 TextureCordinate : TEXCOORD0;
};



// Our pixel shader
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	int samples = 10;
	float scale = 0.0001;

	float4 sum = float4(0.0,0.0,0.0,0.0);
	float2 p = input.TextureCordinate;
    float4 source = tex2D(TextureSampler, p);
	int diff = (samples - 1) / 2;

	for (int x = -diff; x <= diff; x++)
	{
		for (int y = -diff; y <= diff; y++)
		{
			float2 offset = float2(x, y) * scale;
			sum += tex2D(TextureSampler, p + offset);
		}
	}
	
	float4 color = (sum / (samples * samples) + source) * source;
	return color;
}

// Compile our shader
technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_4_0 PixelShaderFunction();
	}
}