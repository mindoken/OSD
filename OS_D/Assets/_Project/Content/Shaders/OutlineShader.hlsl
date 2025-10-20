void Outline_float(
    float4 In,
    UnityTexture2D MainTex,
    UnitySamplerState samplerState,
    float2 UV,
    float4 OutlineColor,
    float Threshold,
    float2 TextureSize,
    float OutlineWidth,
    bool IsEnable,
    out float4 Out)
{
    // Основной сэмпл текстуры
    float4 tex = SAMPLE_TEXTURE2D(MainTex, samplerState, UV);
    float alpha = tex.a;
    
    if (IsEnable == false)
    {
        Out = In;
        return;
    }
    
    if (alpha >= Threshold)
    {
        Out = In;
        return;
    }
    
    float2 pixelSize = OutlineWidth / TextureSize;
    
    // Сэмплируем соседей
    float alphaUp = SAMPLE_TEXTURE2D(MainTex, samplerState, UV + float2(0, pixelSize.y)).a;
    float alphaDown = SAMPLE_TEXTURE2D(MainTex, samplerState, UV - float2(0, pixelSize.y)).a;
    float alphaLeft = SAMPLE_TEXTURE2D(MainTex, samplerState, UV - float2(pixelSize.x, 0)).a;
    float alphaRight = SAMPLE_TEXTURE2D(MainTex, samplerState, UV + float2(pixelSize.x, 0)).a;
    
    if (any(float4(alphaUp, alphaDown, alphaLeft, alphaRight) > Threshold))
        Out = OutlineColor;
    else
        Out = In;
}

void Outline_half(
    float4 In,
    UnityTexture2D MainTex,
    UnitySamplerState samplerState,
    float2 UV,
    float4 OutlineColor,
    float Threshold,
    float2 TextureSize,
    float OutlineWidth,
    bool IsEnable,
    out float4 Out)
{
    Outline_float(In, MainTex, samplerState, UV, OutlineColor, Threshold, TextureSize, OutlineWidth, IsEnable, Out);
}