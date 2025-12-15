#define DECLARE_TEXTURE(Name, index) \
    texture Name: register(t##index); \
    sampler Name##Sampler: register(s##index)

#define SAMPLE_TEXTURE(Name, texCoord) tex2D(Name##Sampler, texCoord)

uniform float Time; // level.TimeActive
uniform float2 CamPos; // level.Camera.Position
uniform float2 Dimensions; // new Vector2(320, 180)
uniform float sizeMult = 1.0;
uniform float speedMult = 1.0;
uniform float4x4 TransformMatrix;
uniform float4x4 ViewMatrix;

DECLARE_TEXTURE(text, 0);

float YoYo(float value)
{
    if (value <= 0.5)
    {
        return value * 2;
    }

    return 1 - (value - 0.5) * 2;
}


float4 HsvToColor(float hue, float s, float v)
{
    int num = (int)(hue * 360.0);
    float num2 = s * v;
    float num3 = num2 * (1.0 - abs((float)num / 60.0 % 2.0 - 1.0));
    float num4 = v - num2;
    if (num < 60)
    {
        return float4(num4 + num2, num4 + num3, num4, 1);
    }

    if (num < 120)
    {
        return float4(num4 + num3, num4 + num2, num4, 1);
    }

    if (num < 180)
    {
        return float4(num4, num4 + num2, num4 + num3, 1);
    }

    if (num < 240)
    {
        return float4(num4, num4 + num3, num4 + num2, 1);
    }

    if (num < 300)
    {
        return float4(num4 + num3, num4, num4 + num2,1 );
    }

    return float4(num4 + num2, num4, num4 + num3, 1);
}


float4 SpritePixelShader(float2 uv : TEXCOORD0) : COLOR0
{
    float4 color = SAMPLE_TEXTURE(text, uv);
    float2 worldPos = (uv * Dimensions) + CamPos;
    float num = 280.0 * sizeMult;
    float value = (worldPos.x + worldPos.y + Time * 50.0 * speedMult) % num / num;
    return (float4(HsvToColor(value * 1, 1, 1)) * 1) * color;
    //color.rgb = 1 - color.rgb;

    //return float4(1,0,1,1); //color;
}

void SpriteVertexShader(inout float4 color    : COLOR0,
                        inout float2 texCoord : TEXCOORD0,
                        inout float4 position : SV_Position)
{
    position = mul(position, ViewMatrix);
    position = mul(position, TransformMatrix);
}

technique Shader
{
    pass pass0
    {
        VertexShader = compile vs_3_0 SpriteVertexShader();
        PixelShader = compile ps_3_0 SpritePixelShader();
    }
}
