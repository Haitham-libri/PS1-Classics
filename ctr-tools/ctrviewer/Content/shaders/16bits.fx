#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_5_0

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
    Texture = <SpriteTexture>;
};

struct Vertex
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TextureCoordinates : TEXCOORD0;
};

float chopComponent(float input, int bits)
{
    float max = pow(2, bits) - 1;
    return (int)(input * max) / max;
}

float4 chopColor(Vertex input) : COLOR
{
    float4 color = tex2D(SpriteTextureSampler, input.TextureCoordinates);

    color.r = chopComponent(color.r, 5);
    color.g = chopComponent(color.g, 5);
    color.b = chopComponent(color.b, 5);

    return color;
}

technique
{
    pass
    {
        PixelShader = compile PS_SHADERMODEL chopColor();
    }
}