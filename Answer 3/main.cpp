#include <iostream>
#include <math.h>
#include <random>

#define PI 3.14159265359

struct Vector2
{
    float X;
    float Y;

    Vector2(float _x, float _y)
    : X(_x), Y(_y)
    {}
};

void PrintPoints(Vector2 center, float radius, int quantity)
{
    std::default_random_engine engine;
    std::uniform_real_distribution<float> dist(0.0f, 1.0f);

    for (size_t i = 0; i < quantity; i++)
    {
        float angle = dist(engine);
        /* 
        Only using values between [0, 1] because a semicircle only covers [0, PI]
        radian degrees and this result is from convert some value between [0, 180]
        in sexagesimal degrees to radians degrees using:
        radAngle = angle * PI/180
        */
        float radians = angle * PI;
        float x = radius * cosf(radians) + center.X;
        float y = radius * sinf(radians) + center.Y;
        std::cout << "(" << x << ", " << y << ") at angle " << angle * 180.0f << "\n";
    }
}

int main()
{
    Vector2 center(0.0f, 0.0f);
    float radius = 1.0f;
    int pointQuantity = 5;
    PrintPoints(center, radius, pointQuantity);
    return 0;
}