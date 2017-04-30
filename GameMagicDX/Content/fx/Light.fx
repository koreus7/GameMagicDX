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


float Orb(VertexShaderOutput input)
{
    float2 aspect = res/min(res.x,res.y);
   
	float2 p = (input.TextureCordinate - 0.5)*aspect*40.0;

	//p = reduxino(p, 0.5*(sin(time) + 1.0) *0.5);
	
    float c = 1.0/length(0.9*p + 0.1*p*abs(sin(time/5.0)) ) -0.2;

	c*=step(0.01,c);
	
    return c;
}


// Our pixel shader
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float4 color = tex2D(TextureSampler, input.TextureCordinate);

	float value = 0.299*color.r + 0.587*color.g + 0.114*color.b;
	color.r = value;
	color.g = value;
	color.b = value;
	color.a = 1.0f;
    float o = Orb(input);
	return color*float4(o,o,o,o)*col;
}

// Compile our shader
technique Technique1
{
	pass Pass1
	{
		PixelShader = compile PS_SHADERMODEL PixelShaderFunction();
	}
}